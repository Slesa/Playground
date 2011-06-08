using System;
using System.Collections.Generic;

namespace NetDLX.Core
{
    public class Assembler
    {
        IAssemble[] _assembles;

        public IEnumerable<string> Source { get; set; }
        public IEnumerable<UInt32> Binary { get; set; }

        public Assembler(IAssemble[] assembles)
        {
            _assembles = assembles;
        }

        public void run()
        {
            
        }
    }
}