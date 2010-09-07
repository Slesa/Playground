using System.Collections.Generic;
using System.IO;
using System.Resources;
using System.Threading;
using System.Windows;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.ReportAppServer.DataDefModel;
using ReportViewer.Reports;
using ConnectionInfo = CrystalDecisions.Shared.ConnectionInfo;
using Table = CrystalDecisions.CrystalReports.Engine.Table;

// Localization: http://msdn.microsoft.com/en-us/library/ms227631.aspx
// Kompilieren der CR Textdateien: http://msdn.microsoft.com/en-us/library/ms227633%28v=VS.90%29.aspx
namespace ReportViewer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        readonly ConnectionInfo _connectionInfo;

        public MainWindow()
        {
            InitializeComponent();
            cbExternal.DataContext = ReportInfos;
            cbReport.DataContext = ReportInfos;

            _connectionInfo = new ConnectionInfo
            {
                ServerName = @".\SQLEXPRESS",
                DatabaseName = "NHibernateSketches",
                IntegratedSecurity = true
            };
        }

        private void OnWindowLoaded(object sender, RoutedEventArgs e)
        {
            InitializeEmbeddedReport();
        }

        static void InitializeLanguage(ReportDocument reportDocument)
        {
            var language = GetCurrentLanguage();
            reportDocument.ReportClientDocument.PreferredViewingLocaleID = language;
            reportDocument.ReportClientDocument.LocaleID = language;
            reportDocument.ReportClientDocument.ProductLocaleID = language;
        }

        static CeLocale GetCurrentLanguage()
        {
            var language = Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName;
            switch (language)
            {
                case "de":
                    return CeLocale.ceLocaleGerman;
                case "en":
                    return CeLocale.ceLocaleEnglishUK;
                case "fr":
                    return CeLocale.ceLocaleFrench;
            }
            return CeLocale.ceLocaleEnglishUS;
        }

        void InitializeEmbeddedReport()
        {
            var reportDocument = new CrystalReport1();
            InitializeLanguage(reportDocument);
            InitializeTables(_connectionInfo, reportDocument);
            embReportViewer.ReportSource = reportDocument;
        }

        static void InitializeTables(ConnectionInfo connectionInfo, ReportDocument reportDocument)
        {
            var tables = reportDocument.Database.Tables;
            foreach (Table table in tables)
            {
                var logonInfo = table.LogOnInfo;
                logonInfo.ConnectionInfo = connectionInfo;
                table.ApplyLogOnInfo(logonInfo);
            }
        }

        public List<ReportInfo> ReportInfos
        {
            get
            {
                var result = new List<ReportInfo>();
                var directory = new DirectoryInfo("Reports");
                var reportDocument = new ReportDocument();
                foreach (var fileInfo in directory.GetFiles("*.rpt"))
                {
                    reportDocument.Load(fileInfo.FullName);
                    var reportInfo = new ReportInfo
                                         {
                                             Name =
                                                 string.IsNullOrEmpty(reportDocument.Name)
                                                     ? fileInfo.Name
                                                     : reportDocument.Name,
                                             Filename = reportDocument.FileName,
                                             Author = reportDocument.SummaryInfo.ReportAuthor
                                         };
                    result.Add(reportInfo);
                }
                return result;
            }
        }

        private void OnExternalChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            var reportInfo = cbExternal.SelectedItem as ReportInfo;
            if (reportInfo == null)
                return;
            try
            {
                var reportDocument = new ReportDocument();
                InitializeLanguage(reportDocument);

                reportDocument.Load(reportInfo.Filename);
                InitializeTables(_connectionInfo, reportDocument);

                var objects = reportDocument.ReportDefinition.ReportObjects;
                foreach (var anobject in objects)
                {
                    if (anobject is FieldHeadingObject)
                    {
                        var fho = anobject as FieldHeadingObject;
                        fho.Text = translateText(fho.Text);
                        continue;
                    }
                    if (anobject is TextObject)
                    {
                        var to = anobject as TextObject;
                        to.Text = translateText(to.Text);
                        continue;
                    }
                }

                extReportViewer.ReportSource = reportDocument;
            }
            catch(System.Exception)
            {
                MessageBox.Show("Unable to load report {0}", reportInfo.Name);
            }
        }

        string translateText(string sourceText)
        {
            var translated = ReportViewer.Strings.ResourceManager.GetString(sourceText, Thread.CurrentThread.CurrentUICulture);
            if (string.IsNullOrWhiteSpace(translated))
                translated = sourceText;
            return translated;
        }

        private void OnReportChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            var reportInfo = cbReport.SelectedItem as ReportInfo;
            if (reportInfo == null)
                return;
            var reportDocument = new ReportDocument();
            reportDocument.Load(reportInfo.Filename);

            tbReportName.Text = reportDocument.Name;
            tbFileName.Text = reportDocument.FileName;
            tbAuthor.Text = reportDocument.SummaryInfo.ReportAuthor;
            tbSubject.Text = reportDocument.SummaryInfo.ReportSubject;
            tbTitle.Text = reportDocument.SummaryInfo.ReportTitle;
            tbKeywords.Text = reportDocument.SummaryInfo.KeywordsInReport;
            tbComments.Text = reportDocument.SummaryInfo.ReportComments;

            var msg = "";
            var fields = reportDocument.ParameterFields;
            for (var i = 0; i < fields.Count; i++)
            {
                var param = fields[i] as CrystalDecisions.Shared.ParameterField;
                if (param != null)
                    msg += "Parameter field - " + param.Name /*+ param.Nam .LongName*/ + "\n";
            }

            //            msg = "";
            var formulas = reportDocument.DataDefinition.FormulaFields;
            for (var i = 0; i < formulas.Count; i++)
            {
                var formula = formulas[i];
                if (formula != null)
                    msg += "Formula field - " + formula.Name + " = " + formula.Text.Replace('\n', '_') + "\n";

            }

            var objects = reportDocument.ReportDefinition.ReportObjects;
            foreach (var anobject in objects)
            {
                if( anobject is FieldHeadingObject)
                {
                    var fho = anobject as FieldHeadingObject;
                    msg += "Field heading - " + fho.Name + " = " + fho.Text + "\n";
                    continue;
                }
                if( anobject is FieldObject)
                {
                    var fo = anobject as FieldObject;
                    msg += "Field object - " + fo.Name + "\n";
                    continue;
                }
                if( anobject is TextObject)
                {
                    var to = anobject as TextObject;
                    msg += "Text object - " + to.Name + " = " + to.Text;
                    continue;
                }
//                msg += "Object field - " + anobject + "\n";
            }
            tbFields.Text = msg;
        }
    }

    public class ReportInfo
    {
        public string Filename { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
    }
}
