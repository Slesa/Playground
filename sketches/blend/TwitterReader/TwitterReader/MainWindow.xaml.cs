using System;
using System.Windows;
using System.Windows.Data;

namespace TwitterReader
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow 
    {
        public MainWindow()
        {
            InitializeComponent();

            var rssBinding = FindResource("rssDs") as XmlDataProvider;
            if (rssBinding != null)
                rssBinding.DataChanged += RssDataChanged;
            ToggleRefreshState(false);
        }

        private void RssDataChanged(object sender, EventArgs e)
        {
            ToggleRefreshState(true);
        }

        private void ToggleRefreshState(bool show)
        {
            if (show)
            {
                buttonRefresh.IsEnabled = true;
                statusLine.Text = "";
            }
            else
            {
                buttonRefresh.IsEnabled = false;
                statusLine.Text = "Please be patient...";
            }
        }

        private void OnRefresh(object sender, RoutedEventArgs e)
        {
            var rssBinding = FindResource("rssDs") as XmlDataProvider;
            if (rssBinding != null)
                rssBinding.Refresh();
            ToggleRefreshState(true);
        }
    }
}