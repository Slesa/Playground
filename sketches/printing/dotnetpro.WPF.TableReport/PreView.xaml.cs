using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace dotnetpro.WPF.TableReport
{
    /// <summary>
    /// Interaktionslogik für PreView.xaml
    /// </summary>
    public partial class PreView : UserControl, INotifyPropertyChanged
    {
        public ReportPresenter presenter { get; set; }

        public PreView()
        {
            InitializeComponent();
        }

        private void GenerateNumerics()
        {

            presenter.updatePaging = true;

            NumericUpDown rad = new NumericUpDown();
            rad.Minimum = 1;
            rad.Maximum = 99;
            rad.Margin = new Thickness(0, 3, 0, 3);
            rad.HorizontalAlignment = HorizontalAlignment.Left;
            rad.Width = 70;

            Binding NumberOfCopies = new Binding("NumberOfCopies");
            NumberOfCopies.Source = presenter;
            NumberOfCopies.Mode = BindingMode.TwoWay;
            rad.SetBinding(NumericUpDown.TextProperty, NumberOfCopies);
            
            RadNumericAnzahl.Content = rad;

            NumericUpDown radFrom = new NumericUpDown();
            radFrom.Minimum = 1;
            radFrom.HorizontalAlignment = HorizontalAlignment.Left;
            radFrom.Margin = new Thickness(0, 3, 0, 3);
            radFrom.Width = 70;

            Binding Selection = new Binding("PageSelectionUntil");
            Selection.Source = presenter;
            Selection.Mode = BindingMode.TwoWay;
            radFrom.SetBinding(NumericUpDown.MaximumProperty, Selection);

            Binding SelectionFrom = new Binding("PageSelectionFrom");
            SelectionFrom.Source = presenter;
            SelectionFrom.Mode = BindingMode.TwoWay;
            radFrom.SetBinding(NumericUpDown.TextProperty, SelectionFrom);

            RadNumericSeitenVon.Content = radFrom;
           
            NumericUpDown radUntil = new NumericUpDown();
            radUntil.HorizontalAlignment = HorizontalAlignment.Left;
            radUntil.Margin = new Thickness(0, 3, 0, 3);
            radUntil.Width = 70;
            
            Binding Max = new Binding("Maximum");
            Max.Source = presenter;
            Max.Mode = BindingMode.TwoWay;
            radUntil.SetBinding(NumericUpDown.MaximumProperty, Max);

            Binding Min = new Binding("PageSelectionFrom");
            Min.Source = presenter;
            Min.Mode = BindingMode.TwoWay;
            radUntil.SetBinding(NumericUpDown.MinimumProperty, Min);

            Binding SelectionUntil = new Binding("PageSelectionUntil");
            SelectionUntil.Source = presenter;
            SelectionUntil.Mode = BindingMode.TwoWay;
            radUntil.SetBinding(NumericUpDown.TextProperty, SelectionUntil);
            RadNumericSeitenBis.Content = radUntil;

            presenter.updatePaging = false;
        }

        public PreView(ReportPresenter pre)
        {
            Cursor = Cursors.Wait;

            presenter = pre;
            this.DataContext = presenter;

            presenter.updatePaging = true;
            InitializeComponent();
            presenter.updatePaging = false;

            Preview.SizeChanged += new SizeChangedEventHandler(Preview_SizeChanged);
            GenerateNumerics();
            pre.GenerateFixedDocument(false);
            UpdateLayout();

            AccessKeyManager.Register("D", btStart);
            
            Cursor = Cursors.Arrow;
        }

        void Preview_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (presenter.Configuration.Fit2Height)
                Preview.FitToHeight();
            else
                Preview.FitToWidth();
        }
        
        private void PrinterSettings(object sender, RoutedEventArgs e)
        {
            presenter.ShowSettings();
        }

        #region INotifyPropertyChanged Member

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        #endregion

       
        private void btStart_Click(object sender, RoutedEventArgs e)
        {
            Cursor = Cursors.Wait;
            presenter.Print();
            Cursor = Cursors.Arrow;
        }

        private void DocumentHigh(object sender, RoutedEventArgs e)
        {
            Preview.FitToHeight();
        }

        private void DocumentWidth(object sender, RoutedEventArgs e)
        {
            Preview.FitToWidth();
        }
    }
}
