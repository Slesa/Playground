using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Windows;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.ReportAppServer.DataDefModel;
using ReportViewer.Reports;
using ConnectionInfo = CrystalDecisions.Shared.ConnectionInfo;
using Table = CrystalDecisions.CrystalReports.Engine.Table;

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
            InitializeLanguage(reportDocument);

            reportDocument.Load(@"Reports\CrystalReport2.rpt");

            InitializeTables(connectionInfo, reportDocument);
            extReportViewer.ReportSource = reportDocument;
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

        void InitializeEmbeddedReport(ConnectionInfo connectionInfo)
        {
            var reportDocument = new CrystalReport1();
            InitializeLanguage(reportDocument);
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
