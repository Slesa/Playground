using System.Windows.Forms;
using AppLimit.NetSparkle;

namespace HelloSparkle
{
    public partial class Form1 : Form
    {
        readonly Sparkle _sparkle;

        public Form1()
        {
            InitializeComponent();

            _sparkle = new Sparkle("http://www.slesa.de/download/updates/HelloSparkle/versioninfo.xml");
            _sparkle.ShowDiagnosticWindow = true;
            _sparkle.StartLoop(true);
        }
    }
}
