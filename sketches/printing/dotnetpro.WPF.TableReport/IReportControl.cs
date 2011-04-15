using System;
using System.Data;
using System.Windows.Controls;
using System.Collections.Generic;

namespace dotnetpro.WPF.TableReport
{
    public interface IReportController
    {
        /// <summary>
        /// Die Exportdaten.
        /// </summary>
        DataTable DataSource { set; }

        /// <summary>
        /// Zusatzinformationen wie die Daten dargestellt werden sollen.
        /// </summary>
        ColumnDefinitions ColumnDef { set; }

        /// <summary>
        /// Druck- und Exporteinstellungen.
        /// </summary>
        ReportSettings Configuration { set; }

        /// <summary>
        /// Exportiert die Daten und zeigt vorher eine Druckvorschau an.
        /// </summary>
        UserControl Export();

    }
}
