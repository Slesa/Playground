using System;
using System.Threading;
using System.Windows.Forms;

namespace EmailViewer
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            if (Thread.CurrentThread.GetApartmentState() != ApartmentState.STA)
                throw new Exception("ApartmentState must be set to STA");
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
