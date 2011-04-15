using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestIt
{
    public class Parameter
    {
        public int Column { get; set; }
        public int Row{ get; set; }

        public string CommandString { get; set; }
        public string Values { get; set; }

        public string Name{ get; set; }
        public string Default{ get; set; }
        public string Type{ get; set; }
        public int Length{ get; set; }
        
    }
}
