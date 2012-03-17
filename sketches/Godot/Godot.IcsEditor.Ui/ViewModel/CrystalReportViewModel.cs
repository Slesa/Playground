using System;
using System.Configuration;
using System.Threading;
/*
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.ReportAppServer.DataDefModel;
using Godot.IcsEditor.Ui.Localization;
using ConnectionInfo = CrystalDecisions.Shared.ConnectionInfo;
using Table = CrystalDecisions.CrystalReports.Engine.Table;
*/
namespace Godot.IcsEditor.Ui.ViewModel
{
    /*
    public class CrystalReportViewModel : ResponsibleWorkspaceViewModel
    {
        readonly string _reportFile;
        readonly string _description;
        ConnectionInfo _connectionInfo;

        public CrystalReportViewModel(string reportFile, string description)
            : base(null, null)
        {
            ContainedObject = _reportFile;
            _reportFile = reportFile;
            _description = description;

            SetDatabaseConfiguration();
            base.DisplayName = _description;
        }

        void SetDatabaseConfiguration()
        {
            var serverName = ConfigurationManager.AppSettings["DbServer"];
            if (serverName == null) return;

            bool integrated;
            Boolean.TryParse(ConfigurationManager.AppSettings["DbIntegrated"], out integrated);

            _connectionInfo = new ConnectionInfo
                {
                    ServerName = serverName,
                    IntegratedSecurity = integrated,
                    UserID = ConfigurationManager.AppSettings["DbUser"],
                    Password = ConfigurationManager.AppSettings["DbPassword"],
                };
        }

        public ReportDocument GetReportDocument()
        {
            var reportDocument = new ReportDocument();
            InitializeLanguage(reportDocument);
            reportDocument.Load(_reportFile);
            InitializeTables(reportDocument);

            var objects = reportDocument.ReportDefinition.ReportObjects;
            foreach (var anobject in objects)
            {
                if (anobject is FieldHeadingObject)
                {
                    var fho = anobject as FieldHeadingObject;
                    fho.Text = TranslateText(fho.Name, fho.Text);
                    continue;
                }
                if (anobject is TextObject)
                {
                    var to = anobject as TextObject;
                    to.Text = TranslateText(to.Name, to.Text);
                    continue;
                }
            }
            return reportDocument;
            //ReportViewer.ReportSource = reportDocument;
        }

        static void InitializeLanguage(ReportDocument reportDocument)
        {
            if (reportDocument == null) return;

            var language = GetCurrentLanguage();
            reportDocument.ReportClientDocument.PreferredViewingLocaleID = language;
            reportDocument.ReportClientDocument.LocaleID = language;
            reportDocument.ReportClientDocument.ProductLocaleID = language;
        }

        void InitializeTables(ReportDocument reportDocument)
        {
            if (_connectionInfo == null) return; 

            var tables = reportDocument.Database.Tables;
            foreach (Table table in tables)
            {
                var logonInfo = table.LogOnInfo;
                logonInfo.ConnectionInfo = _connectionInfo;
                table.ApplyLogOnInfo(logonInfo);
            }
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

        static string TranslateText(string itemName, string sourceText)
        {
            var translated = ReportStrings.ResourceManager.GetString(itemName, Thread.CurrentThread.CurrentUICulture);
            if (string.IsNullOrWhiteSpace(translated))
                translated = sourceText;
            return translated;
        }
    }*/
}