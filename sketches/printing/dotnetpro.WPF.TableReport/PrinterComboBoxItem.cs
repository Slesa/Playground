using System;
using System.Windows.Media.Imaging;

namespace dotnetpro.WPF.TableReport
{
    public class PrinterComboBoxElement
    {
        public PrinterComboBoxElement() { }
        public BitmapImage PrinterImage
        {
            get
            {
                string uri = "pack://application:,,,/dotnetpro.WPF.TableReport;component/Images/";
                if (IsDefault)
                    return new BitmapImage(new Uri(uri + "printer-default.png"));

                return new BitmapImage(new Uri(uri + "printer-normal.png"));
            }
        }
        public string PrinterName { get; set; }
        public bool IsDefault { get; set; }
    }
}
