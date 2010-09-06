using System.Collections.Generic;
using System.IO;
using System.Windows;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using ReportViewer.Reports;

namespace ReportViewer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            cbReport.DataContext = ReportInfos;
        }

        private void OnWindowLoaded(object sender, RoutedEventArgs e)
        {
            var connectionInfo = new ConnectionInfo
                                     {
                                         ServerName = @".\SQLEXPRESS",
                                         DatabaseName = "NHibernateSketches",
                                         IntegratedSecurity = true
                                     };
            InitializeExternalReport(connectionInfo);
            InitializeEmbeddedReport(connectionInfo);
        }

        void InitializeExternalReport(ConnectionInfo connectionInfo)
        {
            var reportDocument = new ReportDocument();
            reportDocument.Load(@"Reports\CrystalReport2.rpt");
            InitializeTables(connectionInfo, reportDocument);
            extReportViewer.ReportSource = reportDocument;
        }

        void InitializeEmbeddedReport(ConnectionInfo connectionInfo)
        {
            var reportDocument = new CrystalReport1();
            InitializeTables(connectionInfo, reportDocument);
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
        }
    }

    public class ReportInfo
    {
        public string Filename { get; set; }
        public string Name { get; set; }
        public string Author{ get; set; }
    }
}
