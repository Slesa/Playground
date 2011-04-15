using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing.Printing;
using System.IO;
using System.IO.Packaging;
using System.Linq;
using System.Printing;
using System.Printing.Interop;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using System.Windows.Xps;
using System.Windows.Xps.Packaging;
using dotnetpro.WPF.TableReport.Properties;

namespace dotnetpro.WPF.TableReport
{
    public class ReportPresenter : INotifyPropertyChanged
    {

        private delegate void InitAllPrinters();
        private delegate void PrintPDF(PrintQueue queue, String Reportname, String FileName, bool OpenFile);

        public ReportPresenter()
        {
            Printers = new ObservableCollection<PrinterComboBoxElement>();
            NotifyPropertyChanged("Printers");
            InitPrinters();
        }

        #region "Properties"

        private Resources stringResources = new Resources();
        public Resources StringResources { get { return stringResources; } }

        public string Caption
        {
            get
            {
                string CaptionStart = "";
                CaptionStart = Resources.WTitelDruck;
                return CaptionStart + " - " + Configuration.Bericht;
            }
        }
        public string StartButtonText
        {
            get
            {
                return Resources.btStartPrint;
            }
        }

        #region "Progress"

        ProgressWindow window;

        public IProgressContext ProgressContext
        {
            get
            {
                if (window == null) window = new ProgressWindow(this);
                return window;
            }
        }


        private static DispatcherOperationCallback exitFrameCallback = new DispatcherOperationCallback(ExitFrame);
        private static object ExitFrame(Object state)
        {
            DispatcherFrame frame = (DispatcherFrame)state;
            frame.Continue = false;
            return null;
        }
        public static void DoEvents() { DoEvents(DispatcherPriority.Background); }
        public static void DoEvents(DispatcherPriority nPrio)
        {
            DispatcherFrame nestedFrame = new DispatcherFrame();
            DispatcherOperation exitOperation = Dispatcher.CurrentDispatcher.BeginInvoke(nPrio, exitFrameCallback, nestedFrame);
            Dispatcher.PushFrame(nestedFrame);
            if (exitOperation.Status != DispatcherOperationStatus.Completed)
                exitOperation.Abort();
        }
        private bool isBusy = false;
        public bool IsBusy
        {
            set
            {
                isBusy = value;
                NotifyPropertyChanged("OpacityValue");
                NotifyPropertyChanged("IsEnabled");
                DoEvents();
            }
            get { return isBusy; }
        }
        public bool IsEnabled { get { return !isBusy; } }
        public Double OpacityValue { get { return IsBusy ? 0.5 : 1; } }

        #endregion

        //Seitenaufbau
        public FixedDocument PrintDocument { get; set; }
        private Ranges ColRanges = new Ranges();
        private Range CurrentRange { get { return ColRanges[offset]; } }
        private int offset = 0;
        private int DisplayColumns = 0;
        Grid oTable;

        //Datenquelle
        private DataTable oData;
        public DataTable DataSource { set { oData = value; } get { return oData; } }
        private ColumnDefinitions Columns;
        public ColumnDefinitions ColumDef { set { Columns = value; } }

        #region "Einstellungen"

        private ReportSettings configuration;
        public ReportSettings Configuration
        {
            get { return configuration; }
            set { configuration = value; UpdatePrinter(); NotifyPropertyChanged("WerteListe"); }
        }

        public int PageOrientation
        {
            get
            {
                if (selectedPrinterIndex != -1)
                    return ticket.PageOrientation == System.Printing.PageOrientation.Landscape ? 1 : 0;
                else
                    return configuration.PageOrientation == System.Windows.Controls.Orientation.Horizontal ? 1 : 0;
            }
            set
            {
                if (value == 1)
                    ticket.PageOrientation = System.Printing.PageOrientation.Landscape;
                else
                    ticket.PageOrientation = System.Printing.PageOrientation.Portrait;

                Configuration.PageOrientation = (value == 1) ? System.Windows.Controls.Orientation.Horizontal : System.Windows.Controls.Orientation.Vertical;
                NotifyPropertyChanged("PageOrientation");
                GenerateFixedDocument(false);

            }
        }

        //Anzahl Exemplare
        public Int16 NumberOfCopies
        {
            get { return (Int16)ticket.CopyCount; }
            set
            {
                ticket.CopyCount = value;
                NotifyPropertyChanged("NumberOfCopies");
                Configuration.CopyCount = value;
            }
        }
        
        public Double FontSize
        {
            get
            {
                if (configuration.FontSize == 0)
                    configuration.FontSize = 8;
                return Math.Max(0, Configuration.FontSize - 4);
            }
            set
            {
                if (IsBusy) return;
                Configuration.FontSize = value + 4;
                NotifyPropertyChanged("FontSize");
                GenerateFixedDocument(false);
            }
        }

        public Boolean HasColumnLines
        {
            get { return Configuration.HasColumnLines; }
            set
            {
                if (IsBusy) return;
                Configuration.HasColumnLines = value;
                NotifyPropertyChanged("HasColumnLines");
                GenerateFixedDocument(false);
            }
        }

        public Boolean HasFixedColumnWidths
        {
            get { return Configuration.FixedColumnWidths; }
            set
            {
                if (IsBusy) return;
                Configuration.FixedColumnWidths = value;
                NotifyPropertyChanged("HasFixedColumnWidths");
                GenerateFixedDocument(false);
            }
        }


        public Boolean HasAlternatingRows
        {
            get { return Configuration.HasAlternatingRows; }
            set
            {
                if (IsBusy) return;
                NotifyPropertyChanged("HasAlternatingRows");
                Configuration.HasAlternatingRows = value;
                GenerateFixedDocument(false);
            }
        }

        public Boolean HasRowLines
        {
            get { return Configuration.HasRowLines; }
            set
            {
                if (IsBusy) return;
                Configuration.HasRowLines = value;
                NotifyPropertyChanged("HasRowLines");
                GenerateFixedDocument(false);
            }
        }
        
        #endregion

        #endregion

        #region "Von Bis Seiten"

        private int pageSelectionFrom = 1;
        public int PageSelectionFrom
        {
            get
            {
                return pageSelectionFrom;
            }
            set
            {
                bool Valid = true;
                if (IsBusy) Valid = false;
                if (value < 1) Valid = false;
                if (value > PageSelectionUntil) Valid = false;
                if (value > maxPages) Valid = false;

                if (Valid)
                    if (pageSelectionFrom != value)
                        pageSelectionFrom = value;

                if ((pageSelectionFrom) > pageSelectionUntil)
                    pageSelectionUntil = pageSelectionFrom;
                if (pageSelectionUntil > maxPages)
                    pageSelectionUntil = maxPages;

                NotifyPropertyChanged("PageSelectionFrom");
                NotifyPropertyChanged("PageSelectionUntil");
            }
        }

        private int pageSelectionUntil = 0;
        public int PageSelectionUntil
        {
            get
            {
                return pageSelectionUntil;
            }
            set
            {
                bool Valid = true;
                if (IsBusy) Valid = false;
                if (value > maxPages) Valid = false;
                if (value < pageSelectionFrom) Valid = false;

                if (Valid)
                    if (pageSelectionUntil != value + pageSelectionFrom - 1)
                        pageSelectionUntil = value + pageSelectionFrom - 1;

                NotifyPropertyChanged("PageSelectionFrom");
                NotifyPropertyChanged("PageSelectionUntil");
            }
        }

        private int maxPages = 100;
        public int Maximum { get { return maxPages; } set { maxPages = value; NotifyPropertyChanged("Maximum"); } }
        public bool updatePaging = false;
        private void UpdatePages()
        {
            updatePaging = true;

            if (pageSelectionUntil == 0 | AllPages)
            {
                PageSelectionUntil = maxPages;
            }
            if (pageSelectionFrom > pageSelectionUntil | pageSelectionFrom > Maximum)
                PageSelectionFrom = 1;

            if (pageSelectionUntil > maxPages)
                PageSelectionUntil = maxPages;
            updatePaging = false;

            NotifyPropertyChanged("Maximum");
            NotifyPropertyChanged("PageSelectionFrom");
            NotifyPropertyChanged("PageSelectionUntil");
        }

        #endregion
        //Drucker
        #region "Printer"

        public ObservableCollection<PrinterComboBoxElement> Printers { get; set; }
        PrintTicket mticket;
        PrintTicket ticket
        {
            get
            {
                if (!printqueue.IsNotAvailable)
                {
                    if (mticket == null) mticket = printqueue.UserPrintTicket;
                    if (configuration != null)
                        mticket.CopyCount = configuration.CopyCount;
                }
                else
                    mticket = new PrintTicket();

                return mticket;
            }
            set { mticket = value; }
        }

        Margins margins = new Margins(40, 40, 40, 35);

        PrintQueue printqueue
        {
            get
            {

                PrintServer ps;
                String printername = PrinterName;

                if (printername.Contains("\\"))
                {
                    string[] split = PrinterName.Split("\\".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                    string server = "\\\\" + split[0].ToString();
                    printername = split[1].ToString();
                    try
                    { ps = new PrintServer(server); }
                    catch (Exception ex)
                    {
                        ps = new LocalPrintServer();
                    }
                }
                else
                {
                    ps = new PrintServer(PrintSystemDesiredAccess.AdministrateServer);
                }
                try
                { return ps.GetPrintQueue(printername); }
                catch (Exception ex)
                {
                    return LocalPrintServer.GetDefaultPrintQueue();
                }
            }
        }

        public IntPtr Handle { get; set; }

        public string PrinterName { get { return Printers[SelectedPrinterIndex].PrinterName; } }

        private int selectedPrinterIndex = 0;
        public int SelectedPrinterIndex
        {
            get { return selectedPrinterIndex; }
            set
            {
                selectedPrinterIndex = value;
                UpdatePrinter();
            }
        }

        private void InitPrinters()
        {
            PrinterSettings ps = new PrinterSettings();
            PrinterComboBoxElement printer = new PrinterComboBoxElement();

            var SortedPrinters = from String item in PrinterSettings.InstalledPrinters
                                 orderby item
                                 select item;

            foreach (string item in SortedPrinters)
            {
                printer = new PrinterComboBoxElement();
                printer.PrinterName = item;
                printer.IsDefault = (item == ps.PrinterName);

                if (!item.ToUpper().Contains("PDF-XChange".ToUpper()))
                {
                    Printers.Add(printer);
                    if (printer.IsDefault)
                        SelectedPrinterIndex = Printers.Count - 1;
                }
            }

            UpdatePrinter();

            ticket.PageOrientation = System.Printing.PageOrientation.Portrait;

            if (configuration != null)
                if (configuration.PrinterName != null)
                {
                    foreach (PrinterComboBoxElement item in Printers)
                    {
                        if (item.PrinterName == configuration.PrinterName)
                            selectedPrinterIndex = Printers.IndexOf(item);
                    }
                }
        }

        public void UpdatePrinter()
        {
            if (configuration != null)
            {
                if (configuration.CopyCount >= 1)
                    ticket.CopyCount = configuration.CopyCount;
                ticket.PageOrientation = (configuration.PageOrientation == System.Windows.Controls.Orientation.Horizontal) ? System.Printing.PageOrientation.Landscape : System.Printing.PageOrientation.Portrait;
            }
            NotifyPropertyChanged("SelectedPrinterIndex");
            NotifyPropertyChanged("PageOrientation");
            NotifyPropertyChanged("NumberOfCopies");
            NotifyPropertyChanged("PrinterName");
        }

        public void ShowSettings()
        {
            PrintTicketConverter ptc = new PrintTicketConverter(PrinterName, printqueue.ClientPrintSchemaVersion);
            IntPtr mainWindowPtr = this.Handle;

            byte[] myDevMode = ptc.ConvertPrintTicketToDevMode(ticket, BaseDevModeType.PrinterDefault);
            GCHandle pinnedDevMode = GCHandle.Alloc(myDevMode, GCHandleType.Pinned);
            IntPtr pDevMode = pinnedDevMode.AddrOfPinnedObject();
            int res = Helpers.DocumentProperties(mainWindowPtr, IntPtr.Zero, printqueue.FullName, pDevMode, pDevMode, 14);
            if (res == 1)
            {
                ticket = ptc.ConvertDevModeToPrintTicket(myDevMode);
                pinnedDevMode.Free();
                NotifyPropertyChanged("PageOrientation");
                NotifyPropertyChanged("NumberOfCopies");
                GenerateFixedDocument(false);
            }
        }

        #endregion

        private FixedPage CreatePage()
        {
            //Create new page
            FixedPage page = new FixedPage();
            //Set background
            page.Background = Brushes.White;
            //Set page size (Letter size)
            page.Width = definition.PageSize.Width;
            page.Height = definition.PageSize.Height;
            page.Background = Brushes.White;
            page.PrintTicket = ticket;
            return page;
        }

        private PageDefinition definition
        {
            get
            {
                PageDefinition def = new PageDefinition();
                if (PageOrientation == 1)
                {
                    def.PageSize = new Size((Double)ticket.PageMediaSize.Height, (Double)ticket.PageMediaSize.Width);
                    def.Margins = new Thickness(margins.Left, margins.Top, margins.Right, margins.Bottom);
                }
                else
                {
                    def.PageSize = new Size((Double)ticket.PageMediaSize.Width, (Double)ticket.PageMediaSize.Height);
                    def.Margins = new Thickness(margins.Left, margins.Top, margins.Right, margins.Bottom);
                }

                def.FooterHeight = 23;
                def.HeaderHeight = 23;
                return def;
            }
        }

        public void GenerateFixedDocument(bool InPrintMode)
        {
            PrintDocument = new FixedDocument();
            
            int RowsOnPage = 100;
            PageSupplemental oSupplemental = new PageSupplemental(Configuration);
            PageDefinition def = definition;

            //Verteilung auf mehreren Seiten?
            GenerateRanges();

            int RowsOnFirstPage = CalculateRowsOnPage(ref RowsOnPage, def);

            UpdatePages();

            int from = PageSelectionFrom;
            int until = PageSelectionUntil;

            if (AllPages | !InPrintMode)
            { from = 1; until = maxPages; }

            ProgressContext.Init(Properties.Resources.prgSeiten, (until - from + 1));

            PrintDocument.DocumentPaginator.PageSize = def.PageSize;

            for (int i = from; i <= until; i++)
            {
                for (int x = 1; x <= ColRanges.Count; x++)
                {
                    oTable = new Grid();
                    oTable.BeginInit();

                    ProgressContext.UpdateProgress(i - from + x);
                    if (ProgressContext.Canceled)
                    {
                        ProgressContext.Finish();
                        return;
                    }
                    offset = x - 1;
                    PageContent oPage = new PageContent();
                    FixedPage oFixed = CreatePage();

                    int RowsUntil = Math.Min((i - 1) * RowsOnPage + RowsOnFirstPage, oData.Rows.Count);
                    int RowsFrom = (i - 2) * RowsOnPage + RowsOnFirstPage;
                    if (RowsFrom < 0)
                        RowsFrom = 0;
                    GenerateRows(RowsFrom, RowsUntil);

                    oTable.Width = def.ContentSize.Width;
                    oTable.Margin = new Thickness(def.ContentOrigin.X, def.ContentOrigin.Y, 0, 0);
                    oTable.EndInit();
                    oFixed.Children.Add(oTable);

                    FrameworkElement oHeader = oSupplemental.PageHeader();
                    oHeader.Width = def.ContentSize.Width;
                    oHeader.Margin = new Thickness(def.HeaderRect.Left, def.HeaderRect.Top, 0, 0);
                    oFixed.Children.Add(oHeader);

                    string currentpage = Convert.ToString(((int)i));
                    if (ColRanges.Count > 1)
                        currentpage = currentpage.AddLetter(x);

                    FrameworkElement oFooter = oSupplemental.PageFooter(currentpage, Maximum);
                    oFooter.Margin = new Thickness(def.FooterRect.Left, def.FooterRect.Top, 0, 0);
                    oFooter.Width = def.ContentSize.Width;
                    oFixed.Children.Add(oFooter);

                    oFixed.Measure(def.PageSize);
                    oFixed.Arrange(new Rect(new Point(), def.PageSize));
                    oFixed.UpdateLayout();

                    ((IAddChild)oPage).AddChild(oFixed);
                    PrintDocument.Pages.Add(oPage);
                }
            }
            ticket.PageBorderless = PageBorderless.Borderless;
            oTable = null;
            ProgressContext.Finish();

            NotifyPropertyChanged("PrintDocument");
            NotifyPropertyChanged("Maximum");
            NotifyPropertyChanged("PageSelectionFrom");
            NotifyPropertyChanged("PageSelectionUntil");

        }

        private int CalculateRowsOnPage(ref int RowsOnPage, PageDefinition def)
        {
            int RowsOnFirstPage = (int)Math.Ceiling((def.ContentSize.Height - def.HeaderHeight - def.FooterHeight) / 9.6 * (8 / Configuration.FontSize));
            if (HasRowLines)
                RowsOnFirstPage = (int)(RowsOnFirstPage * .9);

            RowsOnPage = (int)Math.Ceiling((def.ContentSize.Height - def.HeaderHeight - def.FooterHeight) / 9.6 * (8 / Configuration.FontSize));
            if (HasRowLines)
                RowsOnPage = (int)(RowsOnPage * .9);

            int totalRows = oData.Rows.Count;
            totalRows -= RowsOnFirstPage;
            if (totalRows > 0)
            {
                Double PagesNeeded = (Double)totalRows / (Double)RowsOnPage;
                if ((int)PagesNeeded == PagesNeeded)
                    maxPages = (int)PagesNeeded + 1;
                else
                    maxPages = (int)Math.Round(PagesNeeded + .5) + 1;
            }
            else
                maxPages = 1;
            return RowsOnFirstPage;
        }

        public void Print()
        {
            if (!AllPages)
                if (PageSelectionFrom > 1 | pageSelectionUntil < Maximum)
                    GenerateFixedDocument(true);

            printqueue.Refresh();
            if (printqueue.IsNotAvailable)
                return;
            System.Printing.ValidationResult result = printqueue.MergeAndValidatePrintTicket(printqueue.DefaultPrintTicket, ticket);
            PrintDialog dlg = new PrintDialog();
            dlg.PrintTicket = result.ValidatedPrintTicket;
            dlg.PrintQueue = printqueue;
            dlg.PrintDocument(PrintDocument.DocumentPaginator, Configuration.Bericht);

        }


        private string GenerateXPSFile()
        {
            string tempFileName = System.IO.Path.GetTempFileName().Replace(".tmp", ".xps");

            if (File.Exists(tempFileName))
            {
                try
                { File.Delete(tempFileName); }
                catch (Exception ex)
                { }
            }

            using (Package container = Package.Open(tempFileName, FileMode.Create))
            {
                using (XpsDocument xpsDoc = new XpsDocument(container, CompressionOption.Fast))
                {
                    XpsDocumentWriter xpsWriter = XpsDocument.CreateXpsDocumentWriter(xpsDoc);
                    xpsWriter.Write(PrintDocument, ticket);
                }
            }
            return tempFileName;
        }

        #region "Generators"

        private void GenerateRows(int StartZeile, int EndZeile)
        {
            try
            {
                if (StartZeile == EndZeile) return;
                if (oTable == null)
                {
                    oTable = new Grid();
                    oTable.BeginInit();
                }
                oTable.SetValue(TextBlock.FontFamilyProperty, new FontFamily("Verdana"));
                if (Configuration.FontSize < 4) configuration.FontSize = 8;
                oTable.SetValue(TextBlock.FontSizeProperty, (double)Configuration.FontSize);

                GenerateColumns();
                for (int i = StartZeile; i <= EndZeile + 1; i++)
                {
                    oTable.RowDefinitions.Add(new RowDefinition());
                }

                int ColBorders = 1;

                int ColBorderLeft = 1;
                int ColBorderRight = 0;
                int ColBorderButtom = 1;
                if (!HasRowLines)
                    ColBorderButtom = 0;

                if (!HasColumnLines) ColBorders = 0;

                int row = 1;
                int col = -1;
                
                GenerateHeaderRow(ColBorders);
                for (int currow = StartZeile; currow < EndZeile; currow++)
                {
                    DataRow item = oData.Rows[currow];
                    row++;

                    if (currow + 1 == EndZeile)
                        ColBorderButtom = 1;

                    int FirstCol = 1;
                    int colcount = 0; //nur ECHTE Spalten
                    col = -1;
                    for (int i = CurrentRange.From; i <= CurrentRange.Until; i++)
                    {
                        ColumnDefinition colDef = Columns[i];

                        col++;
                        ColBorderLeft = 1;
                        ColBorderRight = 0;
                        colcount++;
                        if (ColBorders == 0 && FirstCol != 1)
                            ColBorderLeft = 0;

                        if (colcount == (CurrentRange.Until - CurrentRange.From + 1))
                            ColBorderRight = 1;

                        string sValue = "";

                        sValue = item.Field<string>(colDef.Name);

                        if (sValue.Contains("\n"))
                            sValue = sValue.Split('\n')[0];
                        if (sValue.Contains("\r"))
                            sValue = sValue.Split('\r')[0];

                        GenerateCell(sValue, colDef, new Thickness(ColBorderLeft, 0, ColBorderRight, ColBorderButtom), row, col, null);

                        FirstCol = 0;
                    }
                }
                return;
            }
            catch (Exception ex)
            { }

        }

        private void GenerateCell(string sValue, ColumnDefinition colDef, Thickness borders, int row, int col, SolidColorBrush brush)
        {
            Border oCell = new Border();

            oCell.BorderBrush = Brushes.Black;
            oCell.BorderThickness = borders;

            if (brush == null)
            {
                oCell.Background = Brushes.White;
                oCell.SetBackground(HasAlternatingRows, row);
            }
            else
                oCell.Background = brush;

            oTable.Children.Add(oCell);
            oCell.Add2Grid(col, row);

            TextBlock text = new TextBlock(new Run(sValue));
            text.TextWrapping = TextWrapping.NoWrap;
            text.TextTrimming = TextTrimming.CharacterEllipsis;
            text.Margin = new Thickness(2, 0, 2, 0);
            
            //Zeigen
            text.TextAlignment = colDef.Alignment;
                        
            oCell.Child = text;
        }


        private void GenerateColumns()
        {

            bool fixedWidthes = configuration.FixedColumnWidths;
            if (ColRanges.Count == 1 & configuration.FixedColumnWidths)
                fixedWidthes = false;

            double totalWidth = 0;
            double pagewidth = ticket.PageMediaSize.Width.Value - margins.Left - margins.Right;
            if (PageOrientation == 1)
                pagewidth = ticket.PageMediaSize.Height.Value - margins.Top - margins.Bottom;

            for (int i = CurrentRange.From; i <= CurrentRange.Until; i++)
            {

                System.Windows.Controls.ColumnDefinition oColTraffic = new System.Windows.Controls.ColumnDefinition(); ;
                System.Windows.Controls.ColumnDefinition oCol = new System.Windows.Controls.ColumnDefinition();

                double width = Columns[i].Width;

                totalWidth += width;

                if (fixedWidthes)
                {
                    if (i == CurrentRange.Until)
                        if (PageOrientation == 0)
                            width = ticket.PageMediaSize.Width.Value - margins.Left - margins.Right - totalWidth + width;
                        else
                            width = ticket.PageMediaSize.Height.Value - margins.Left - margins.Right - totalWidth + width;

                    oCol.Width = new GridLength(width, GridUnitType.Pixel);
                }
                else
                    oCol.Width = new GridLength(width / 10, GridUnitType.Star);
                oTable.ColumnDefinitions.Add(oCol);
            }
        }

        private void GenerateHeaderRow(int ColBorders)
        {
            try
            {

                int ColCount = 0;
                int colCountReal = 0;
                for (int i = CurrentRange.From; i <= CurrentRange.Until; i++)
                {
                    ColumnDefinition colDef = Columns[i];
                    Border oCell = new Border();
                    colCountReal++;
                    oCell.BorderBrush = Brushes.Black;
                    int ColBorderLeft = 1;
                    int ColBorderRight = 0;
                    if (colCountReal == (CurrentRange.Until - CurrentRange.From + 1))
                        ColBorderRight = 1;
                    oCell.BorderThickness = new Thickness(ColBorderLeft, 1, ColBorderRight, 1);

                    DockPanel oDockPanel = new DockPanel();
                    oCell.Child = oDockPanel;
                    oDockPanel.Margin = new Thickness(1, 0, 1, 0);
                    Image oImage = null;


                    if (colDef.Title != null)
                    {
                        TextBlock oText = new TextBlock(new Run(colDef.Title));
                        oText.TextTrimming = TextTrimming.CharacterEllipsis;
                        oText.TextWrapping = TextWrapping.NoWrap;
                        oText.TextAlignment = colDef.Alignment;
                        oDockPanel.Children.Add(oText);
                    }
                    oCell.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D8D8D8"));
                    oCell.Add2Grid(ColCount, 0);
                    ColCount++;
                    oTable.Children.Add(oCell);
                }
            }
            catch (Exception ex)
            {
            }

        }

        private void GenerateRanges()
        {
            double totalWidth = 0;
            ColumnDefinition colDef;
            ColRanges = new Ranges();
            Range ColRange = new Range();
            ColRange.From = 0;
            double pagewidth = ticket.PageMediaSize.Width.Value - margins.Left - margins.Right;
            if (PageOrientation == 1)
                pagewidth = ticket.PageMediaSize.Height.Value - margins.Top - margins.Bottom;

            DisplayColumns = 0;
            for (int i = 0; i < Columns.Count; i++)
            {
                colDef = Columns[i];
                double width = colDef.Width;
                totalWidth += width;
                DisplayColumns++;
                if (Configuration.FixedColumnWidths)
                {
                    if (i < Columns.Count - 1)
                        if (totalWidth + Columns[i + 1].Width > pagewidth)
                        {
                            width = ticket.PageMediaSize.Width.Value - margins.Left - margins.Right - totalWidth + width;
                            ColRange.Until = i;
                            ColRanges.Add(ColRange);
                            ColRange = new Range();
                            ColRange.From = i + 1;
                            totalWidth = 0;
                        }
                }
            }
            ColRange.Until = Columns.Count - 1;
            ColRanges.Add(ColRange);
        }
        #endregion

        #region INotifyPropertyChanged Member

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        #endregion

        private bool allpages = true;
        public bool AllPages
        {
            get { return allpages; }
            set
            {
                allpages = value;
                NotifyPropertyChanged("AllPages");
            }
        }
    }
}
