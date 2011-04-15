using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Threading;

namespace dotnetpro.WPF.TableReport
{
    /// <summary>
    /// Interaktionslogik für ProgressBar.xaml
    /// </summary>
    public partial class ProgressWindow : Window, IProgressContext
    {
        private bool cancel = false;
        private ReportPresenter pres;
        private string TextPattern;

        public ProgressWindow(ReportPresenter presenter)
        {
            InitializeComponent();
            pres = presenter;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            HwndSource source = PresentationSource.FromVisual(this) as HwndSource;
            HwndTarget hwndTarget = source.CompositionTarget;

            if (hwndTarget != null)
                hwndTarget.RenderMode = RenderMode.SoftwareOnly;
        }

        #region IProgressContext Member

        public void Init(string text, int MaxPages)
        {
            TextPattern = text.Replace("$2", MaxPages.ToString());
            Progress.Maximum = MaxPages;
            cancel = false;
            Cursor = Cursors.Wait;
        }

        public void UpdateProgress(int Page)
        {
            DoEvents();
            if (Page > Progress.Maximum)
                Page = (int)Progress.Maximum;
            StatusText.Text = TextPattern.Replace("$1", Page.ToString());
            Progress.Value = Page;
            this.Show();
            pres.IsBusy = true;
        }

        public bool Canceled
        {
            get { return cancel; }
        }

        public void Finish()
        {
            pres.IsBusy = false;
            Cursor = Cursors.Arrow;
            this.Hide();
        }

        void DoEvents()
        {
            DispatcherFrame f = new DispatcherFrame();
            Dispatcher.CurrentDispatcher.BeginInvoke(DispatcherPriority.Render,
            (SendOrPostCallback)delegate(object arg)
            {
                DispatcherFrame fr = arg as DispatcherFrame;
                fr.Continue = false;
            }, f);
            Dispatcher.PushFrame(f);
        }

        #endregion
    }
}
