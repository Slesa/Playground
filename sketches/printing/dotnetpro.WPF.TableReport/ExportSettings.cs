using System.Windows.Controls;

namespace dotnetpro.WPF.TableReport
{
    public class ReportSettings : IPageInfo
    {
        #region IPageSupplemental Member

        public string Kontext { get; set; }

        public string Bericht { get; set; }

        public string Benutzer { get; set; }

        #endregion

        public string PrinterName { get; set; }

        public short CopyCount { get; set; }

        public bool HasColumnLines { get; set; }

        public bool HasRowLines { get; set; }

        public bool HasAlternatingRows { get; set; }

        public bool FixedColumnWidths { get; set; }

        public bool Fit2Height { get; set; }

        public double FontSize { get; set; }
        
        public Orientation PageOrientation { get; set; }

    }
}
