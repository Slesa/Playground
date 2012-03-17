using System.Windows;
using Godot.IcsEditor.Ui.ViewModel;

// http://forums.sdn.sap.com/message.jspa?messageID=8995372

namespace Godot.IcsEditor.Ui.View
{
    /// <summary>
    /// Interaction logic for CrystalReport.xaml
    /// </summary>
    public partial class CrystalReportView 
    {
        public CrystalReportView()
        {
            InitializeComponent();
            //ReportViewer.BackColor = System.Drawing.Color.AliceBlue;
        }

        private void OnWindowLoaded(object sender, RoutedEventArgs e)
        {
            /*
            var reportDocument = new ReportDocument();
            InitializeLanguage(reportDocument);
            reportDocument.Load(_reportFile);

            var objects = reportDocument.ReportDefinition.ReportObjects;
            foreach (var anobject in objects)
            {
                if (anobject is FieldHeadingObject)
                {
                    var fho = anobject as FieldHeadingObject;
                    fho.Text = TranslateText(fho.Text);
                    continue;
                }
                if (anobject is TextObject)
                {
                    var to = anobject as TextObject;
                    to.Text = TranslateText(to.Text);
                    continue;
                }
            }
*/
            /*
            var dataContext = DataContext as CrystalReportViewModel;
            if (dataContext != null) ReportViewer.ReportSource =  
                dataContext.GetReportDocument();
             */
        }

#if NEVER
        static void InitializeLanguage(ReportDocument reportDocument)
        {
            if (reportDocument == null) return;
            
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

        static string TranslateText(string sourceText)
        {
            var translated = ReportStrings.ResourceManager.GetString(sourceText, Thread.CurrentThread.CurrentUICulture);
            if (string.IsNullOrWhiteSpace(translated))
                translated = sourceText;
            return translated;
        }

#endif
    }
}
