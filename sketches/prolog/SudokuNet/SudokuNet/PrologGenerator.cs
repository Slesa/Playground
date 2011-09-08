using System;

namespace SudokuNet
{
    public class PrologGenerator
    {
        int _base;
        public int Base
        {
            get { return _base; }
            set
            {
                if( value<2 || value>4 )
                    throw new IndexOutOfRangeException();
                _base = value;
            }
        }

        public PrologGenerator()
        {
            Base = 2;
        }

        public string Generate()
        {
            return null;
        }


        public string Rows
        {
            get
            {
                var rows = "";
                var concat = "";
                for (var i = 0; i < Base; i++)
                {
                    rows += concat + string.Format("Row{0} = [", i);
                    rows += "],";
                }
                return rows;
            }
        }

        public string Validation
        {
            get
            {
                return "is_valid([])." + Environment.NewLine
                       + "is_valid([Head|Tail]) :-" + Environment.NewLine
                       + "  fd_all_different(Head)," + Environment.NewLine
                       + "  is_valid(Tail)." + Environment.NewLine
                       + Environment.NewLine;
            }
        }
    }
}