using System;
using System.Collections.Generic;
using System.Windows;

namespace dotnetpro.WPF.TableReport
{
    public interface IPageInfo
    {
        string Kontext { get; set; }
        string Bericht { get; set; }
        string Benutzer { get; set; }
    }

    public class PageInfo : DependencyObject, IPageInfo
    {
        public string Datum { get { return DateTime.Now.ToString("dd.MM.yyyy HH:mm"); } }

        public string Seite { get; set; }
        public string Info { get; set; }
        public string Kontext { get; set; }
        public string Bericht { get; set; }
        public string Benutzer { get; set; }

    }

    public class PageSupplemental
    {
        #region "Constructors"
        
        PageInfo Info;

        public PageSupplemental(IPageInfo dates)
        {
            Info = new PageInfo();
            Info.Kontext = dates.Kontext;
            Info.Bericht = dates.Bericht;
            Info.Benutzer = dates.Benutzer;
            initDataTemplates();
        }
        #endregion


        public Dictionary<String, DataTemplate> dataTemplates = new Dictionary<string, DataTemplate>();

        public void initDataTemplates()
        {
            ResourceDictionary oRes = new ResourceDictionary();
            oRes.Source = new Uri("/dotnetpro.WPF.TableReport;component/Themes.xaml", UriKind.Relative);
            foreach (System.Collections.DictionaryEntry item in oRes)
            {
                dataTemplates.Add(item.Key.ToString(), (DataTemplate)item.Value);
            }
        }

        public FrameworkElement PageHeader()
        {
            return VisualFromData("Header");
        }

        public FrameworkElement PageFooter(string pageNumber, int total)
        {
            Info.Seite = Properties.Resources.sSeite.Replace("$1", pageNumber).Replace("$2", total.ToString());
            string sInfo = Properties.Resources.sInfo;
            sInfo = sInfo.Replace("$1", Info.Datum);
            sInfo = sInfo.Replace("$2", Info.Benutzer);
            Info.Info = sInfo;
            return VisualFromData("Footer");
        }

        private FrameworkElement VisualFromData(string Template)
        {
            DataTemplate oTemplate = dataTemplates[Template];
            FrameworkElement oVisual = (FrameworkElement)oTemplate.LoadContent();
            oVisual.DataContext = Info;
            return oVisual;
        }
    }
}
