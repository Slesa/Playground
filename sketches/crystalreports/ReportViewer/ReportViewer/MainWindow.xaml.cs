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
                    var reportInfo = new ReportInfo();
                    reportInfo.Name = reportDocument.Name;
                    result.Add(reportInfo);
                }
                return result;
            }
        }
    }

    public class ReportInfo
    {
        public string Name { get; set; }
    }
}
