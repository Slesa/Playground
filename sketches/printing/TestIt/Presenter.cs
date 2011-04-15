using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using dotnetpro.WPF.TableReport;
using System.Windows;
using System.Data.SqlClient;
using System.Data;

namespace TestIt
{
    public class Presenter
    {

        #region "Berichtsauswahl des Kunden"
        private Berichte alleBerichte;
        public Berichte AlleBerichte
        {
            get
            {
                if (alleBerichte == null)
                    alleBerichte = Berichte.LoadFrom("berichte.xml");
                return alleBerichte;
            }
        }

        private String aktuellerBerichtsName = "";
        public String AktuellerBerichtsName
        {
            get { return aktuellerBerichtsName; }
            set { aktuellerBerichtsName = value; }
        }

        private Bericht aktuellerbericht = null;
        public Bericht AktuellerBericht
        {
            get
            {
                if (aktuellerbericht != null)
                    if (aktuellerbericht.Berichtname == aktuellerBerichtsName)
                        return aktuellerbericht;

                foreach (Bericht b in AlleBerichte)
                {
                    if (b.Berichtname == AktuellerBerichtsName)
                    { aktuellerbericht = b; break; }
                }
                return aktuellerbericht;
            }
            set { aktuellerbericht = value; }
        }

        #endregion

        public void SQL2Data(String parameters)
        {
            SqlCommand cmd = GetCommand();

            FillParameters(parameters, cmd);

            SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            DataTable oTable = new DataTable();
            foreach (ColumnDefinition coldef in AktuellerBericht.ColumnDefinitions)
            {
                oTable.Columns.Add(new DataColumn(coldef.Name, typeof(string)));

                int i = dataReader.GetOrdinal(coldef.Name);
                coldef.Type =  dataReader.GetFieldType(i);
            }

            while (dataReader.Read())
            {
                string[] arr = new string[AktuellerBericht.ColumnDefinitions.Count];
                int i = 0;
                foreach (ColumnDefinition coldef in AktuellerBericht.ColumnDefinitions)
                {
                    arr[i] = dataReader[coldef.Name].ToString();
                    i++;
                }
                oTable.Rows.Add(arr);
            }

            IReportController oExport = new ReportController();
            oExport.DataSource = oTable;
            oExport.ColumnDef = AktuellerBericht.ColumnDefinitions;

            ReportSettings oSettings = new ReportSettings();
            oSettings.Benutzer = "Bernhard Pichler";
            oSettings.Bericht = AktuellerBericht.Berichtname;
            oSettings.Kontext = "informare Consulting GmbH";
            oSettings.CopyCount = 1;
            oSettings.FixedColumnWidths = true;
            oSettings.HasColumnLines = true;
            oSettings.HasAlternatingRows = true;
            oSettings.Fit2Height = true;
            oSettings.PageOrientation = System.Windows.Controls.Orientation.Vertical;
            oExport.Configuration = oSettings;

            Preview window = new Preview((oExport.Export()));
            window.Show();

        }


        private void FillParameters(String parameters, SqlCommand cmd)
        {
            string[] parameterArr = parameters.Split(';');
            foreach (string newPara in parameterArr)
            {
                string[] newParaParts = newPara.Split('=');
                foreach (SqlParameter sqlpara in cmd.Parameters)
                {
                    if (sqlpara.ParameterName.ToUpper() == newParaParts[0].ToUpper())
                        sqlpara.Value = newParaParts[1];
                }
            }
        }

        private SqlCommand GetCommand()
        {
            SqlCommand cmd = GetConnectionCommand();
            cmd.CommandText = AktuellerBericht.SQL;

            foreach (Parameter par in AktuellerBericht.Parameter)
            {
                SqlParameter sqlpar = new SqlParameter(par.Name, par.Default);
                cmd.Parameters.Add(sqlpar);
            }
            return cmd;
        }

        public List<String> GetParameterValues(Parameter item)
        {
            SqlCommand cmd = GetConnectionCommand();
            cmd.CommandText = item.CommandString;
            SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            List<string> values = new List<string>();
            while (reader.Read())
            {
                values.Add(Convert.ToString(reader[0]));
            }
            return values;
        }

        private SqlCommand GetConnectionCommand()
        {
            SqlConnection conn = new SqlConnection(AktuellerBericht.ConnectionString);
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            return cmd;
        }
    }
}
