using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using dotnetpro.WPF.TableReport;

namespace TestIt
{
    public class Bericht
    {

        public string ConnectionString { get; set; }
        
        public string Berichtname { get; set; }
    
        public string SQL { get; set; }

        public List<Parameter> Parameter { get; set; }

        public ColumnDefinitions ColumnDefinitions { get; set; }

    }
}
