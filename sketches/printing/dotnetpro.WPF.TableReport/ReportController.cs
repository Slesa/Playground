using System;
using System.Data;
using System.IO;

namespace dotnetpro.WPF.TableReport
{
    public class ReportController : IReportController
    {
        ReportPresenter presenter;

        public ReportController()
        {
            presenter = new ReportPresenter();
        }

        #region "Schnittstelle"

        public System.Windows.Controls.UserControl Export()
        {
            return this.OpenDialog();
        }

        private DataTable _dataSource;
        public DataTable DataSource { set { presenter.DataSource = value; _dataSource = value; } get { return _dataSource; } }

        public ReportSettings Configuration { set { presenter.Configuration = value; } }

        #endregion

        #region "Functions"

        public System.Windows.Controls.UserControl OpenDialog()
        {
            PreView control = new PreView(presenter);
            return (System.Windows.Controls.UserControl)control;
        }


        ProgressWindow window;
        public IProgressContext ProgressContext
        {
            get
            {
                if (window == null) window = new ProgressWindow(presenter);
                return window;
            }
        }

        #endregion

        public ColumnDefinitions ColumnDef
        {
            set { presenter.ColumDef = value; }
        }
    }
}
