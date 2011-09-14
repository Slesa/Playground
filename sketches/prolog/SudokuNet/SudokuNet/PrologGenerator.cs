using System;
using System.Text;

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
            var result = DefinitionPart + Environment.NewLine +
                         PuzzlePart + Environment.NewLine +
                         RowsPart + Environment.NewLine;
                         //ColumnsPart + Environment.NewLine +
                         //SquaresPart + Environment.NewLine +
                         //ValidationPart;
            return result;
        }

        public string ValidationPart
        {
            get
            {
                var sb = new StringBuilder();
                sb.Append("is_valid([");
                for (var row = 0; row < Base * Base; row++)
                    sb.AppendFormat("Row{0:X}, ", row);
                for (var column = 0; column < Base * Base; column++)
                    sb.AppendFormat("Col{0:X}, ", column);
                for (var square = 0; square < Base * Base; square++)
                {
                    sb.AppendFormat("Square{0:X}", square);
                    if (square != Base * Base - 1)
                        sb.Append(", ");
                }
                sb.Append("]).");
                return sb.ToString();
            }
        }

        public string SquaresPart
        {
            get
            {
                var sb = new StringBuilder();
                var squareCount = 0;
                for (var n = 0; n < Base; n++)
                {
                    for (var i = 0; i < Base; i++)
                    {
                        sb.AppendFormat("Square{0:X} = [", squareCount++);
                        for (var column = 0; column < Base; column++)
                        {
                            for (var row = 0; row < Base; row++)
                            {
                                sb.AppendFormat("S{0:X}{1:X}", (n*Base) + column, (i*Base) + row);
                                if (column != Base - 1 || row != Base - 1)
                                    sb.Append(", ");
                            }
                        }
                        sb.AppendLine("],");
                    }
                }
                return sb.ToString();
            }
        }

        public string ColumnsPart
        {
            get
            {
                var sb = new StringBuilder();
                for (var row = 0; row < Base * Base; row++)
                {
                    sb.AppendFormat("Col{0:X} = [", row);
                    for (var column = 0; column < Base*Base; column++)
                    {
                        sb.AppendFormat("S{0:X}{1:X}", column, row);
                        if (column != Base * Base - 1)
                            sb.Append(", ");
                    }
                    sb.AppendLine("],");
                }
                return sb.ToString();
            }
        }

        public string RowsPart
        {
            get
            {
                var sb = new StringBuilder();
                for (var row = 0; row < Base * Base; row++)
                {
                    sb.AppendFormat("Row{0:X} = [", row);
                    for (var column = 0; column < Base*Base; column++)
                    {
                        sb.AppendFormat("S{0:X}{1:X}", row, column);
                        if (column != Base * Base - 1)
                            sb.Append(", ");
                    }
                    sb.AppendLine("],");
                }
                return sb.ToString();
            }
        }

        public string PuzzlePart
        {
            get
            {
                var sb = new StringBuilder();

                sb.AppendLine("Solution = Puzzle,")
                    .Append("Puzzle = [");

                for (var row = 0; row < Base*Base; row++)
                {
                    for (var column = 0; column < Base*Base; column++)
                    {
                        sb.AppendFormat("S{0:X}{1:X}", row, column);
                        if (row != Base * Base - 1 || column != Base * Base - 1)
                            sb.Append(", ");
                    }
                }
                sb.AppendLine("],")
                    .AppendFormat("fd_domain(Solution, 1, {0}),", Base*Base).AppendLine();
                return sb.ToString();
            }
        }

        public string DefinitionPart
        {
            get
            {
                return "is_valid([])." + Environment.NewLine
                       + "is_valid([Head|Tail]) :-" + Environment.NewLine
                       + "  fd_all_different(Head)," + Environment.NewLine
                       + "  is_valid(Tail)." + Environment.NewLine;
            }
        }
    }
}