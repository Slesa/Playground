using System.IO;
using System.Windows.Forms;
using Lesnikowski.Mail;
using Lesnikowski.Windows;

namespace EmailViewer
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void _btnLoad_Click(object sender, System.EventArgs e)
        {
            IMail email = new MailBuilder().CreateFromEml(File.ReadAllText("Order.eml"));
            _mailBrowser.Navigate(new MailHtmlDataProvider(email));
        }
    }
}
