using System;
using Machine.Fakes;
using Machine.Specifications;

namespace SudokuNet
{
    [Subject(typeof(PrologGenerator))]
    public class When_constructing_validation_part : WithSubject<PrologGenerator>
    {
        Establish context = () =>
            {
                _expected =
                    "is_valid([])." + Environment.NewLine +
                    "is_valid([Head|Tail]) :-" + Environment.NewLine +
                    "  fd_all_different(Head)," + Environment.NewLine +
                    "  is_valid(Tail)." + Environment.NewLine;

            };
        Because of = () => _validation = Subject.DefinitionPart;
        It should_return_the_right_sequence = () => _validation.ShouldEqual(_expected);

        static string _validation;
        static string _expected;
    }

    [Subject(typeof(PrologGenerator))]
    public class When_construction_puzzle_part_for_base_2 : WithSubject<PrologGenerator>
    {
        Establish context = () =>
            {
                _expected =
                    "Solution = Puzzle," + Environment.NewLine +
                    "Puzzle = [S00, S01, S02, S03, S10, S11, S12, S13, S20, S21, S22, S23, S30, S31, S32, S33]," + Environment.NewLine +
                    "fd_domain(Solution, 1, 4)," + Environment.NewLine;
            };

        Because of = () => _puzzle = Subject.PuzzlePart;
        It should_return_the_right_sequence = () => _puzzle.ShouldEqual(_expected);

        static string _expected;
        static string _puzzle;
    }

    [Subject(typeof(PrologGenerator))]
    public class When_construction_puzzle_part_for_base_3 : WithSubject<PrologGenerator>
    {
        Establish context = () =>
            {
                Subject.Base = 3;
                _expected =
                    "Solution = Puzzle," + Environment.NewLine +
                    "Puzzle = [S00, S01, S02, S03, S04, S05, S06, S07, S08, "+
                      "S10, S11, S12, S13, S14, S15, S16, S17, S18, "+
                      "S20, S21, S22, S23, S24, S25, S26, S27, S28, "+
                      "S30, S31, S32, S33, S34, S35, S36, S37, S38, "+
                      "S40, S41, S42, S43, S44, S45, S46, S47, S48, "+
                      "S50, S51, S52, S53, S54, S55, S56, S57, S58, "+
                      "S60, S61, S62, S63, S64, S65, S66, S67, S68, "+
                      "S70, S71, S72, S73, S74, S75, S76, S77, S78, "+
                      "S80, S81, S82, S83, S84, S85, S86, S87, S88],"+Environment.NewLine +
                    "fd_domain(Solution, 1, 9)," + Environment.NewLine;
            };

        Because of = () => _puzzle = Subject.PuzzlePart;
        It should_return_the_right_sequence = () => _puzzle.ShouldEqual(_expected);

        static string _expected;
        static string _puzzle;
    }

    [Subject(typeof(PrologGenerator))]
    public class When_constructing_row_part_for_base_2 : WithSubject<PrologGenerator>
    {
        Establish context = () =>
            {
                _expected =
                    "Row0 = [S00, S01, S02, S03]," + Environment.NewLine +
                    "Row1 = [S10, S11, S12, S13]," + Environment.NewLine +
                    "Row2 = [S20, S21, S22, S23]," + Environment.NewLine +
                    "Row3 = [S30, S31, S32, S33]," + Environment.NewLine;
            };

        Because of = () => _rows = Subject.RowsPart;
        It should_return_the_right_sequence = () => _rows.ShouldEqual(_expected);

        static string _expected;
        static string _rows;
    }
    
    [Subject(typeof(PrologGenerator))]
    public class When_constructing_row_part_for_base_3 : WithSubject<PrologGenerator>
    {
        Establish context = () =>
            {
                Subject.Base = 3;
                _expected =
                    "Row0 = [S00, S01, S02, S03, S04, S05, S06, S07, S08]," + Environment.NewLine +
                    "Row1 = [S10, S11, S12, S13, S14, S15, S16, S17, S18]," + Environment.NewLine +
                    "Row2 = [S20, S21, S22, S23, S24, S25, S26, S27, S28]," + Environment.NewLine +
                    "Row3 = [S30, S31, S32, S33, S34, S35, S36, S37, S38]," + Environment.NewLine +
                    "Row4 = [S40, S41, S42, S43, S44, S45, S46, S47, S48]," + Environment.NewLine +
                    "Row5 = [S50, S51, S52, S53, S54, S55, S56, S57, S58]," + Environment.NewLine +
                    "Row6 = [S60, S61, S62, S63, S64, S65, S66, S67, S68]," + Environment.NewLine +
                    "Row7 = [S70, S71, S72, S73, S74, S75, S76, S77, S78]," + Environment.NewLine +
                    "Row8 = [S80, S81, S82, S83, S84, S85, S86, S87, S88]," + Environment.NewLine;
            };

        Because of = () => _rows = Subject.RowsPart;
        It should_return_the_right_sequence = () => _rows.ShouldEqual(_expected);

        static string _expected;
        static string _rows;
    }

    [Subject(typeof(PrologGenerator))]
    public class When_constructing_columns_part_for_base_2 : WithSubject<PrologGenerator>
    {
        Establish context = () =>
        {
            _expected =
                "Col0 = [S00, S10, S20, S30]," + Environment.NewLine +
                "Col1 = [S01, S11, S21, S31]," + Environment.NewLine +
                "Col2 = [S02, S12, S22, S32]," + Environment.NewLine +
                "Col3 = [S03, S13, S23, S33]," + Environment.NewLine;
        };

        Because of = () => _columns = Subject.ColumnsPart;
        It should_return_the_right_sequence = () => _columns.ShouldEqual(_expected);

        static string _expected;
        static string _columns;
    }

    [Subject(typeof(PrologGenerator))]
    public class When_constructing_columns_part_for_base_3 : WithSubject<PrologGenerator>
    {
        Establish context = () =>
        {
            Subject.Base = 3;
            _expected =
                "Col0 = [S00, S10, S20, S30, S40, S50, S60, S70, S80]," + Environment.NewLine +
                "Col1 = [S01, S11, S21, S31, S41, S51, S61, S71, S81]," + Environment.NewLine +
                "Col2 = [S02, S12, S22, S32, S42, S52, S62, S72, S82]," + Environment.NewLine +
                "Col3 = [S03, S13, S23, S33, S43, S53, S63, S73, S83]," + Environment.NewLine +
                "Col4 = [S04, S14, S24, S34, S44, S54, S64, S74, S84]," + Environment.NewLine +
                "Col5 = [S05, S15, S25, S35, S45, S55, S65, S75, S85]," + Environment.NewLine +
                "Col6 = [S06, S16, S26, S36, S46, S56, S66, S76, S86]," + Environment.NewLine +
                "Col7 = [S07, S17, S27, S37, S47, S57, S67, S77, S87]," + Environment.NewLine +
                "Col8 = [S08, S18, S28, S38, S48, S58, S68, S78, S88]," + Environment.NewLine;
        };

        Because of = () => _columns = Subject.ColumnsPart;
        It should_return_the_right_sequence = () => _columns.ShouldEqual(_expected);

        static string _expected;
        static string _columns;
    }


    [Subject(typeof(PrologGenerator))]
    public class When_constructing_squares_part_for_base_2 : WithSubject<PrologGenerator>
    {
        Establish context = () =>
        {
            _expected =
                "Square0 = [S00, S01, S10, S11]," + Environment.NewLine +
                "Square1 = [S02, S03, S12, S13]," + Environment.NewLine +
                "Square2 = [S20, S21, S30, S31]," + Environment.NewLine +
                "Square3 = [S22, S23, S32, S33]," + Environment.NewLine;
        };

        Because of = () => _squares = Subject.SquaresPart;
        It should_return_the_right_sequence = () => _squares.ShouldEqual(_expected);

        static string _expected;
        static string _squares;
    }

    [Subject(typeof(PrologGenerator))]
    public class When_constructing_squares_part_for_base_3 : WithSubject<PrologGenerator>
    {
        Establish context = () =>
        {
            Subject.Base = 3;
            _expected =
                "Square0 = [S00, S01, S02, S10, S11, S12, S20, S21, S22]," + Environment.NewLine +
                "Square1 = [S03, S04, S05, S13, S14, S15, S23, S24, S25]," + Environment.NewLine +
                "Square2 = [S06, S07, S08, S16, S17, S18, S26, S27, S28]," + Environment.NewLine +
                "Square3 = [S30, S31, S32, S40, S41, S42, S50, S51, S52]," + Environment.NewLine +
                "Square4 = [S33, S34, S35, S43, S44, S45, S53, S54, S55]," + Environment.NewLine +
                "Square5 = [S36, S37, S38, S46, S47, S48, S56, S57, S58]," + Environment.NewLine +
                "Square6 = [S60, S61, S62, S70, S71, S72, S80, S81, S82]," + Environment.NewLine +
                "Square7 = [S63, S64, S65, S73, S74, S75, S83, S84, S85]," + Environment.NewLine +
                "Square8 = [S66, S67, S68, S76, S77, S78, S86, S87, S88]," + Environment.NewLine;
        };

        Because of = () => _squares = Subject.SquaresPart;
        It should_return_the_right_sequence = () => _squares.ShouldEqual(_expected);

        static string _expected;
        static string _squares;
    }

    [Subject(typeof(PrologGenerator))]
    public class When_constructing_validation_part_for_base_2 : WithSubject<PrologGenerator>
    {
        Establish context = () =>
            {
                _expected =
                    "is_valid([Row0, Row1, Row2, Row3, Col0, Col1, Col2, Col3, Square0, Square1, Square2, Square3]).";
            };

        Because of = () => _squares = Subject.ValidationPart;
        It should_return_the_right_sequence = () => _squares.ShouldEqual(_expected);

        static string _expected;
        static string _squares;
    }

    [Subject(typeof(PrologGenerator))]
    public class When_construction_program_for_base_4 : WithSubject<PrologGenerator>
    {
        Establish context = () =>
            {
                Subject.Base = 4;
                _expected =
                    "is_valid([])." + Environment.NewLine +
                    "is_valid([Head|Tail]) :-" + Environment.NewLine +
                    "  fd_all_different(Head)," + Environment.NewLine +
                    "  is_valid(Tail)." + Environment.NewLine +
                    Environment.NewLine +
                    "Solution = Puzzle," + Environment.NewLine +
                    "Puzzle = [S00, S01, S02, S03, S04, S05, S06, S07, S08, S09, S0A, S0B, S0C, S0D, S0E, S0F, " +
                    "S10, S11, S12, S13, S14, S15, S16, S17, S18, S19, S1A, S1B, S1C, S1D, S1E, S1F, " +
                    "S20, S21, S22, S23, S24, S25, S26, S27, S28, S29, S2A, S2B, S2C, S2D, S2E, S2F, " +
                    "S30, S31, S32, S33, S34, S35, S36, S37, S38, S39, S3A, S3B, S3C, S3D, S3E, S3F, " +
                    "S40, S41, S42, S43, S44, S45, S46, S47, S48, S49, S4A, S4B, S4C, S4D, S4E, S4F, " +
                    "S50, S51, S52, S53, S54, S55, S56, S57, S58, S59, S5A, S5B, S5C, S5D, S5E, S5F, " +
                    "S60, S61, S62, S63, S64, S65, S66, S67, S68, S69, S6A, S6B, S6C, S6D, S6E, S6F, " +
                    "S70, S71, S72, S73, S74, S75, S76, S77, S78, S79, S7A, S7B, S7C, S7D, S7E, S7F, " +
                    "S80, S81, S82, S83, S84, S85, S86, S87, S88, S89, S8A, S8B, S8C, S8D, S8E, S8F, " +
                    "S90, S91, S92, S93, S94, S95, S96, S97, S98, S99, S9A, S9B, S9C, S9D, S9E, S9F, " +
                    "SA0, SA1, SA2, SA3, SA4, SA5, SA6, SA7, SA8, SA9, SAA, SAB, SAC, SAD, SAE, SAF, " +
                    "SB0, SB1, SB2, SB3, SB4, SB5, SB6, SB7, SB8, SB9, SBA, SBB, SBC, SBD, SBE, SBF, " +
                    "SC0, SC1, SC2, SC3, SC4, SC5, SC6, SC7, SC8, SC9, SCA, SCB, SCC, SCD, SCE, SCF, " +
                    "SD0, SD1, SD2, SD3, SD4, SD5, SD6, SD7, SD8, SD9, SDA, SDB, SDC, SDD, SDE, SDF, " +
                    "SE0, SE1, SE2, SE3, SE4, SE5, SE6, SE7, SE8, SE9, SEA, SEB, SEC, SED, SEE, SEF, " +
                    "SF0, SF1, SF2, SF3, SF4, SF5, SF6, SF7, SF8, SF9, SFA, SFB, SFC, SFD, SFE, SFF]," +
                    Environment.NewLine +
                    "fd_domain(Solution, 1, 16)," + Environment.NewLine +
                    Environment.NewLine +
                    "Row0 = [S00, S01, S02, S03, S04, S05, S06, S07, S08, S09, S0A, S0B, S0C, S0D, S0E, S0F]," + Environment.NewLine +
                    "Row1 = [S10, S11, S12, S13, S14, S15, S16, S17, S18, S19, S1A, S1B, S1C, S1D, S1E, S1F]," + Environment.NewLine +
                    "Row2 = [S20, S21, S22, S23, S24, S25, S26, S27, S28, S29, S2A, S2B, S2C, S2D, S2E, S2F]," +
                    Environment.NewLine +
                    "Row3 = [S30, S31, S32, S33, S34, S35, S36, S37, S38, S39, S3A, S3B, S3C, S3D, S3E, S3F]," +
                    Environment.NewLine +
                    "Row4 = [S40, S41, S42, S43, S44, S45, S46, S47, S48, S49, S4A, S4B, S4C, S4D, S4E, S4F]," +
                    Environment.NewLine +
                    "Row5 = [S50, S51, S52, S53, S54, S55, S56, S57, S58, S59, S5A, S5B, S5C, S5D, S5E, S5F]," +
                    Environment.NewLine +
                    "Row6 = [S60, S61, S62, S63, S64, S65, S66, S67, S68, S69, S6A, S6B, S6C, S6D, S6E, S6F]," +
                    Environment.NewLine +
                    "Row7 = [S70, S71, S72, S73, S74, S75, S76, S77, S78, S79, S7A, S7B, S7C, S7D, S7E, S7F]," +
                    Environment.NewLine +
                    "Row8 = [S80, S81, S82, S83, S84, S85, S86, S87, S88, S89, S8A, S8B, S8C, S8D, S8E, S8F]," +
                    Environment.NewLine +
                    "Row9 = [S90, S91, S92, S93, S94, S95, S96, S97, S98, S99, S9A, S9B, S9C, S9D, S9E, S9F]," +
                    Environment.NewLine +
                    "RowA = [SA0, SA1, SA2, SA3, SA4, SA5, SA6, SA7, SA8, SA9, SAA, SAB, SAC, SAD, SAE, SAF]," +
                    Environment.NewLine +
                    "RowB = [SB0, SB1, SB2, SB3, SB4, SB5, SB6, SB7, SB8, SB9, SBA, SBB, SBC, SBD, SBE, SBF]," +
                    Environment.NewLine +
                    "RowC = [SC0, SC1, SC2, SC3, SC4, SC5, SC6, SC7, SC8, SC9, SCA, SCB, SCC, SCD, SCE, SCF]," +
                    Environment.NewLine +
                    "RowD = [SD0, SD1, SD2, SD3, SD4, SD5, SD6, SD7, SD8, SD9, SDA, SDB, SDC, SDD, SDE, SDF]," +
                    Environment.NewLine +
                    "RowE = [SE0, SE1, SE2, SE3, SE4, SE5, SE6, SE7, SE8, SE9, SEA, SEB, SEC, SED, SEE, SEF]," +
                    Environment.NewLine +
                    "RowF = [SF0, SF1, SF2, SF3, SF4, SF5, SF6, SF7, SF8, SF9, SFA, SFB, SFC, SFD, SFE, SFF]," +
                    Environment.NewLine +
                    Environment.NewLine;
            };

        Because of = () => _program = Subject.Generate();
        It should_generate_the_program = () => _program.ShouldEqual(_expected);

        static string _expected;
        static string _program;
    }
}