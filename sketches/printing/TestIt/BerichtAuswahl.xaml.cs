using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace TestIt
{
    /// <summary>
    /// Interaktionslogik für BerichtAuswahl.xaml
    /// </summary>
    public partial class BerichtAuswahl : Window
    {
        Presenter presenter = null;
        public BerichtAuswahl()
        {
            InitializeComponent();
            presenter = new Presenter();
            this.DataContext = presenter;
        }

        private void Erzeugen(object sender, RoutedEventArgs e)
        {
            presenter.SQL2Data(CollectParameterValues());
        }

        private string CollectParameterValues()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in Filter.Children)
            {
                if (item.GetType().Name == "ComboBox")
                {
                    ComboBox cb = (ComboBox)item;
                    if (cb.SelectedIndex != -1)
                        sb.AppendFormat("{0}={1};", cb.Tag, cb.SelectedValue);
                }
                else if (item.GetType().Name == "TextBox")
                {
                    TextBox tb = (TextBox)item;
                    if (!string.IsNullOrEmpty(tb.Text))
                        sb.AppendFormat("{0}={1};", tb.Tag, tb.Text);
                }
            }
            return sb.ToString();
        }

        private void SelectedReportChanged(object sender, SelectionChangedEventArgs e)
        {
            Filter.RowDefinitions.Clear();
            Filter.ColumnDefinitions.Clear();
            Filter.Children.Clear();

            presenter.AktuellerBericht = (Bericht)e.AddedItems[0];

            for (int i = 0; i < 4; i++)
            {
                System.Windows.Controls.ColumnDefinition oCol = new System.Windows.Controls.ColumnDefinition();
                oCol.Width = new GridLength((i == 0 | i == 2) ? 1 : 2, GridUnitType.Star);
                Filter.ColumnDefinitions.Add(oCol);
            }
            int MaxRows = 0;
            foreach (var item in presenter.AktuellerBericht.Parameter)
            {

                MaxRows = Math.Max(MaxRows, item.Row);

                Label lb = new Label();
                lb.Content = item.Name;
                Filter.Children.Add(lb);
                lb.SetValue(Grid.RowProperty, item.Row);
                lb.SetValue(Grid.ColumnProperty, item.Column * 2);
                FrameworkElement tb;

                if (String.IsNullOrEmpty(item.Values) & String.IsNullOrEmpty(item.CommandString))
                {
                    tb = new TextBox();
                    Binding binding = new Binding("Default");
                    binding.Source = item;
                    binding.Mode = BindingMode.TwoWay;
                    tb.SetBinding(TextBox.TextProperty, binding);
                }
                else
                {
                    tb = new ComboBox();
                    if (!String.IsNullOrEmpty(item.Values))
                    {
                        for (int i = 0; i < item.Values.Split(';').Count(); i++)
                        {
                            ((ComboBox)tb).Items.Add(item.Values.Split(';')[i].ToString());
                        }
                    }
                    else if (!String.IsNullOrEmpty(item.CommandString))
                    {
                        foreach (String value in presenter.GetParameterValues(item))
                        {
                            ((ComboBox)tb).Items.Add(value);
                        }
                    }
                }
                Filter.Children.Add(tb);
                tb.VerticalAlignment = VerticalAlignment.Top;
                tb.SetValue(Grid.RowProperty, item.Row);
                tb.SetValue(Grid.ColumnProperty, (item.Column * 2) + 1);
                tb.Width = 120;
                tb.Height = 25;
                tb.Tag = item.Name;
            }
            for (int i = 0; i <= MaxRows; i++)
            {
                Filter.RowDefinitions.Add(new RowDefinition());
            }

        }
    }
}
