/*

        DLX MicroAssembler - Main module

        D. J. Viner
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
Microcode layout:

    31-28   27-22   21-18   17-14   13-9    8-5     4-0
    4       6       4       4       5       4       5       8 (allow 256)
    Dest    ALU Op  Src1    Src2    Const   Misc    Cond    Jump Addr
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

*/

#include "masm.h"
#include "msym.h"
#include "micro.h"

/*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/
/* In masmlink.c */

extern  int     LinkNo;
extern  char    LinkDate [];

/*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/

FILE    *In;                    /* Input file */

BOOL    LittleEndian = FALSE, Ok = TRUE, ESym = FALSE;

UBYTE   FName [50];
UBYTE   Line [LINELEN + 2];
STRPTR  ptr;
ULONG   Addr, LineNo;

#define INVALID  0xFFFF

/*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/
/* The Microcode tables                                                   */
/*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/

ULONG Microcode [MICROSIZE];     /* Main microcode */
UBYTE MicroJump [MICROSIZE];     /* Jump table */
UBYTE Decode1 [DECODE1SIZE];
UBYTE Decode2 [DECODE2SIZE];

/*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/
/* Destination table (0-14)                                               */
/*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/

STRPTR Dest [] =
{
    "C",
    "Temp",
    "PC",
    "IAR",
    "MAR",
    "MDR",
    "SR",
    "FPSR",
    "CD",
    NULL
};

/*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/
/* ALU Table (0-62)                                                       */
/*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/

STRPTR ALU [] =
{
    "ADD",
    "SUB",
    "RSUB",
    "AND",
    "OR",
    "XOR",
    "SLL",
    "SRL",
    "SRA",
    "PassS1",
    "PassS2",
    "MULTI",
    "DIVI",
    "MULTF",
    "DIVF",
    "ADDF",
    "SUBF",
    "RSUBF",
    "MULTD",
    "DIVD",
    "ADDD",
    "SUBD",
    "RSUBD",
    "CVTF2D",
    "CVTF2I",
    "CVTD2F",
    "CVTD2I",
    "CVTI2F",
    "CVTI2D",
    NULL
};

/*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/
/* Src1 Table (0-14)                                                      */
/*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/

STRPTR Src1 [] =
{
    "A",
    "Temp",
    "PC",
    "IAR",
    "MAR",
    "MDR",
    "imm16",
    "imm26",
    "Const",
    "SR",
    "FPSR",
    "AD",
    NULL
};

/*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/
/* Src2 Table                                                             */
/*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/

STRPTR Src2 [] =
{
    "B",
    "Temp",
    "PC",
    "IAR",
    "MAR",
    "MDR",
    "imm16",
    "imm26",
    "Const",
    "SR",
    "FPSR",
    "BD",
    NULL
};

/*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/
/* Misc Table                                                             */
/*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/

STRPTR Misc [] =
{
    "InstrRd",
    "DataRd",
    "DataWr",
    "AB<-RF",
    "Rd<-C",
    "R31<-C",
    NULL
};

/*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/
/* Conditional Table                                                      */
/*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/

STRPTR Cond [] =
{
    "Uncond",
    "Interrupt",
    "Mem",
    "Zero",
    "Negative",
    "Load",
    "Decode1",
    "Decode2",
    "Decode3",
    "DestIAR",
    "DestSR",
    "DestFPSR",
    "SrcIAR",
    "SrcSR",
    "SrcFPSR",
    NULL
};

/*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/
/* Decode 1 Tables                                                        */
/*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/

STRPTR Decode1Table [] =
{
    "Memory",
    "MOVI2S",
    "MOVS2I",
    "S2=B",
    "S2=Imm",
    "BEQZ",
    "BNEZ",
    "J",
    "JR",
    "JAL",
    "JALR",
    "TRAP",
    "RFE",

    "CVT",
    "SETF",
    "BFP",
    "MOVFP2I",
    "MOVI2FP",
    "MOVFP",
    NULL
};

/*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/
/* Decode 2 Table                                                         */
/*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/

STRPTR Decode2Table [] =
{
    "LB",
    "LBU",
    "LH",
    "LHU",
    "LW",
    "LF",
    "LD",
    "ADD",
    "SUB",
    "AND",
    "OR",
    "XOR",
    "SLL",
    "SRL",
    "SRA",
    "LHI",
    "SEQ",
    "SNE",
    "SLT",
    "SGE",
    "SGT",
    "SLE",


    NULL
};

/*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/

VOID Error (STRPTR Msg)
{
    if (strlen (Line))
    {
        fprintf (stderr, "%s", Line);
        fprintf (stderr, "Error: Line %d ", LineNo);
    }

    fprintf (stderr, "%s\n", Msg);

    if (ESym)
    {
        printf ("\nLabel table\n");
        DisplaySymbolTable (FALSE);
    }

    DeleteSymbolTable ();
    fclose (In);
    exit (20);
}

/*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/

VOID SkipWhitespace ()
{
    while (*ptr == ' ' || *ptr == '\t')
        ptr++;
}

/*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/

VOID SkipToWhitespace ()
{
    BOOL    Loop = TRUE;

    do {
        switch (*ptr)
        {
            case ' ' : case ',' : case '\t' : case ';' :
            case '\n' :
                Loop = FALSE;
                break;

            default :
                ptr++;
        }

    } while (Loop);
}

/*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/

STRPTR CollectSym (STRPTR p, STRPTR Str)
{
    WORD    i = 0;
    BOOL    Loop = TRUE;


    do {
        switch (*p)
        {
            case ' ' : case ',' : case '\t' :
            case ';' : case '\n' :
                Loop = FALSE;
                break;

            default :
                Str [i++] = *p;
                p++;
        }

    } while (Loop);

    Str [i] = 0;
    return p;
}

/*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/

BOOL HandleSymbol (BOOL Jump)
{
    SymTab  ST;
    UBYTE   Txt [80], Sym [STABNAMESIZE];
    STRPTR  p;


    p = ptr;
    p = CollectSym (p, Sym);

    /* Search for symbol */

    ST = FindSymbol (Sym);

    SkipToWhitespace ();
    SkipWhitespace ();

    if (!ST)    /* New symbol */
    {
        if (strlen (Sym) == 0)
            return FALSE;

        ST = AddSymbol (Sym, Addr, !Jump);
    }
    else
    if (ST->SetUp && !Jump)
    {
        sprintf (Txt, "Multiple definition of label <%s>\n", Sym);
        Error (Txt);
    }

    if (!Jump)
    {
        ST->Val = Addr;
        ST->SetUp = TRUE;

        if (!ST->LineNo)
            ST->LineNo = LineNo;
    }
    else
        ST->Refs [ST->NoRefs++] = Addr;

    return TRUE;
}

/*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/

VOID WriteTables (STRPTR FName)
{
    UWORD   i, j;
    FILE    *o;
    UBYTE   Conv1 [4], Conv2 [4], Txt [80];


    Line [0] = 0;   /* Clear Line in case of error */

    o = fopen (FName, "wb");

    if (!o)
    {
        sprintf (Txt, "Cannot open output microcode file '%s'", FName);
        Error (Txt);
    }

    for (i = 0; i < MICROSIZE; i++)
    {
        if (LittleEndian)
        {
            memcpy (Conv1, &Microcode [i], 4);

            for (j = 0; j < 4; j++)
                Conv2 [3 - j] = Conv1 [j];
        }
        else
            memcpy (Conv2, &Microcode [i], 4);

        fwrite (Conv2, 1, 4, o);
    }

    fwrite (MicroJump, MICROSIZE, 1, o);
    fwrite (Decode1, DECODE1SIZE, 1, o);
    fwrite (Decode2, DECODE2SIZE, 1, o);
    fclose (o);
}

/*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/

VOID ShowTables ()
{
    UWORD   i, j;
    UBYTE   Conv1 [4], Conv2 [4];
    ULONG   Mc;


    printf ("TABLES\n\nMicrocode\n");

    for (i = 0; i < Addr; i++)
    {
        if (LittleEndian)
        {
            memcpy (Conv1, &Microcode [i], 4);

            for (j = 0; j < 4; j++)
                Conv2 [3 - j] = Conv1 [j];

            memcpy (&Mc, Conv2, 4);
        }
        else
            Mc = Microcode [i];

        printf ("%3d  %08lX %d\n", i, Mc, MicroJump [i]);
    }

    printf ("\nDecode1\n");

    for (i = 0; i < DECODE1SIZE; i++)
        printf ("%3d  %d\n", i, Decode1 [i]);

    printf ("\nDecode2/3\n");

    for (i = 0; i < DECODE2SIZE; i++)
        printf ("%3d  %d\n", i, Decode2 [i]);
}

/*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/

VOID SetJump (SymTab ST)
{
    UWORD   i;
    UBYTE   Txt [80];


    if (!ST)
        return;

    SetJump (ST->Left);

    if (!ST->SetUp)
    {
        sprintf (Txt, "Label <%s> never defined\n", ST->Name);
        Error (Txt);
    }

    if (ST->NoRefs)
        for (i = 0; i < ST->NoRefs; i++)
            MicroJump [ST->Refs [i]] = ST->Val;

    SetJump (ST->Right);
}

/*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/

VOID SetJumps ()
{
    SetJump (SymBase);
}

/*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/

VOID DoDecode1 ()
{
    ULONG   i, Ind;
    BOOL    Found;
    UBYTE   Keyword [20], Txt [80], Sym [STABNAMESIZE];
    SymTab  ST;


    /* Get opcode */

    ptr = CollectSym (ptr, Keyword);

    for (i = 0, Found = FALSE; !Found && Decode1Table [i]; i++)
    {
        if (stricmp (Keyword, Decode1Table [i]) == 0)
        {
            Ind = i;
            Found = TRUE;
        }
    }

    if (!Found)
    {
        sprintf (Txt, "Unrecognised Decode1 keyword <%s>\n", Keyword);
        Error (Txt);
    }

    ptr++;
    SkipWhitespace ();

    /* Get jump label */

    ptr = CollectSym (ptr, Sym);

    /* Search for symbol */

    ST = FindSymbol (Sym);

    if (!ST)    /* New label */
    {
        sprintf (Txt, "Unrecognised label <%s>\n", Sym);
        Error (Txt);
    }

    if (!ST->SetUp)
    {
        sprintf (Txt, "Label <%s> not declared\n", ST->Name);
        Error (Txt);
    }

    Decode1 [Ind] = ST->Val;
}

/*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/

VOID DoDecode2 ()
{
    ULONG   i, Ind;
    BOOL    Found;
    UBYTE   Keyword [20], Txt [80], Sym [STABNAMESIZE];
    SymTab  ST;


    /* Get opcode */

    ptr = CollectSym (ptr, Keyword);

    for (i = 0, Found = FALSE; !Found && Decode2Table [i]; i++)
    {
        if (stricmp (Keyword, Decode2Table [i]) == 0)
        {
            Ind = i;
            Found = TRUE;
        }
    }

    if (!Found)
    {
        sprintf (Txt, "Unrecognised Decode2 keyword <%s>\n", Keyword);
        Error (Txt);
    }

    ptr++;
    SkipWhitespace ();

    /* Get jump label */

    ptr = CollectSym (ptr, Sym);

    /* Search for symbol */

    ST = FindSymbol (Sym);

    if (!ST)    /* New label */
    {
        sprintf (Txt, "Unrecognised label <%s>\n", Sym);
        Error (Txt);
    }

    if (!ST->SetUp)
    {
        sprintf (Txt, "Label <%s> not declared\n", ST->Name);
        Error (Txt);
    }

    Decode2 [Ind] = ST->Val;
}

/*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/

VOID DoMicroCode ()
{
    ULONG   i, Ind;
    UBYTE   Keyword [20], Txt [80];


    /* Check for initial label */

    if (*ptr != ',')
        HandleSymbol (FALSE);

    ptr++;
    SkipWhitespace ();

    /* Get Dest (4 bits: 31-28) */

    if (*ptr != ',')
    {
        ptr = CollectSym (ptr, Keyword);

        for (i = 0, Ind = INVALID; Ind == INVALID && Dest [i]; i++)
            if (stricmp (Keyword, Dest [i]) == 0)
                Ind = i;

        if (Ind == INVALID)
        {
            sprintf (Txt, "Unrecognised Dest keyword <%s>\n", Keyword);
            Error (Txt);
        }

        Microcode [Addr] |= ((Ind + 1) << 28);    /* 0 = no dest */
    }

    ptr++;
    SkipWhitespace ();

    /* Get ALU (6 bits: 27-22) */

    if (*ptr != ',')
    {
        ptr = CollectSym (ptr, Keyword);

        for (i = 0, Ind = INVALID; Ind == INVALID && ALU [i]; i++)
            if (stricmp (Keyword, ALU [i]) == 0)
                Ind = i;

        if (Ind == INVALID)
        {
            sprintf (Txt, "Unrecognised ALU keyword <%s>\n", Keyword);
            Error (Txt);
        }

        Microcode [Addr] |= (Ind << 22);
    }
    else
        Microcode [Addr] |= (((ULONG) 0x0000003F) << 22);

    ptr++;
    SkipWhitespace ();

    /* Get Src1 (4 bits: 21-18) */

    if (*ptr != ',')
    {
        ptr = CollectSym (ptr, Keyword);

        for (i = 0, Ind = INVALID; Ind == INVALID && Src1 [i]; i++)
            if (stricmp (Keyword, Src1 [i]) == 0)
                Ind = i;

        if (Ind == INVALID)
        {
            sprintf (Txt, "Unrecognised Source 1 keyword <%s>\n", Keyword);
            Error (Txt);
        }

        Microcode [Addr] |= (Ind << 18);
    }
    else
        Microcode [Addr] |= (((ULONG) 0x0000000F) << 18);

    ptr++;
    SkipWhitespace ();

    /* Get Src2 (4 bits: 17-14) */

    if (*ptr != ',')
    {
        ptr = CollectSym (ptr, Keyword);

        for (i = 0, Ind = INVALID; Ind == INVALID && Src2 [i]; i++)
            if (stricmp (Keyword, Src2 [i]) == 0)
                Ind = i;

        if (Ind == INVALID)
        {
            sprintf (Txt, "Unrecognised Source 2 keyword <%s>\n", Keyword);
            Error (Txt);
        }

        Microcode [Addr] |= (Ind << 14);
    }
    else
        Microcode [Addr] |= (((ULONG) 0x0000000F) << 14);

    ptr++;
    SkipWhitespace ();

    /* Get Const (5 bits: 13-9) */

    if (*ptr != ',')
    {
        i = atol (ptr);
        ptr = CollectSym (ptr, Keyword);
        Microcode [Addr] |= (i << 9);
    }

    ptr++;
    SkipWhitespace ();

    /* Get Misc (4 bits: 8-5) */

    if (*ptr != ',')
    {
        ptr = CollectSym (ptr, Keyword);

        for (i = 0, Ind = INVALID; Ind == INVALID && Misc [i]; i++)
            if (stricmp (Keyword, Misc [i]) == 0)
                Ind = i;

        if (Ind == INVALID)
        {
            sprintf (Txt, "Unrecognised Misc keyword <%s>\n", Keyword);
            Error (Txt);
        }

        Microcode [Addr] |= (Ind << 5);
    }
    else
        Microcode [Addr] |= (((ULONG) 0x0000000F) << 5);

    ptr++;
    SkipWhitespace ();

    /* Get Cond (5 bits: 4-0) */

    if (*ptr != ',')
    {
        ptr = CollectSym (ptr, Keyword);

        for (i = 0, Ind = INVALID; Ind == INVALID && Cond [i]; i++)
            if (stricmp (Keyword, Cond [i]) == 0)
                Ind = i;

        if (Ind == INVALID)
        {
            sprintf (Txt, "Unrecognised Condition keyword <%s>\n", Keyword);
            Error (Txt);
        }

        Microcode [Addr] |= (Ind + 1); /* 0 = no cond */
    }

    ptr++;
    SkipWhitespace ();

    if (*ptr == '\n' || *ptr == ';' || *ptr == 0)
        return;

    /* Get jump label */

    HandleSymbol (TRUE);
}

/*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/

int main (int argc, char **argv)
{
    ULONG   i;
    BOOL    MainLoop = TRUE, Symbols = FALSE, ShowTable = FALSE;
    UBYTE   LI [4], Txt [50], OutName [40] = "dlxmcode.tbl", c;
    WORD    AsmMode = 0;


    i = 0x01020304;
    memcpy (LI, &i, 4);

    if (LI [0] == 4)
        LittleEndian = TRUE;

    fprintf (stderr, "\nDLX MicroAssembler, Version %s.%d (%s)\n",
        Version, LinkNo, LinkDate);

    if (LittleEndian)
        fprintf (stderr, ">>> Little Endian System <<<\n");

    fprintf (stderr, "CMP-3P4Y 1.1995/6 - D J Viner - u9323864\n\n");

    if (argc == 1)
    {
        printf ("Usage: %s [params] Filename\n", argv [0]);
        printf ("  params:  -e      Display label table upon error\n");
        printf ("           -oxxx   Specify output file name as xxx\n");
        printf ("           -s      Display label table\n");
        printf ("           -t      Display final tables on screen\n");
        exit (0);
    }

    for (i = 1; i < argc; i++)
    {
        if (argv [i] [0] == '-')
        {
            c = argv [i] [1];

            if (islower (c))
                c = toupper (c);

            switch (c)
            {
                case 'E' :
                    ESym = TRUE;
                    break;

                case 'O' :
                    strcpy (OutName, &argv [i] [2]);
                    break;

                case 'S' :
                    Symbols = TRUE;
                    break;

                case 'T' :
                    ShowTable = TRUE;
            }
        }
        else
            strcpy (FName, argv [i]);
    }

    /* Initialise code tables to zero */

    for (i = 0; i < MICROSIZE; i++)
    {
        Microcode [i] = 0;
        MicroJump [i] = 0;
    }

    for (i = 0; i < DECODE1SIZE; i++)
        Decode1 [i] = 0;

    for (i = 0; i < DECODE2SIZE; i++)
        Decode2 [i] = 0;

    /* Do the assembly */

    In = fopen (FName, "r");

    if (In)
    {
        printf ("Processing '%s'\n", FName);
        printf ("  Microcode table\n");

        /* Loop until no more input */

        LineNo = 0;
        Addr = 0;

        do {
            fgets (Line, LINELEN, In);
            LineNo++;

            if (feof (In))
                MainLoop = FALSE;
            else
            {
                ptr = &Line [0];
                SkipWhitespace ();

                switch (*ptr)
                {
                    case 0    :
                    case ';'  :
                    case '\n' :
                        break;

                    case '#' :
                        AsmMode++;

                        switch (AsmMode)
                        {
                            case 1 :
                                SetJumps ();
                                printf ("  Decode 1 table\n");
                                break;

                            case 2 :
                                printf ("  Decode 2/3 table\n");
                        }

                        break;

                    default :
                        switch (AsmMode)
                        {
                            case 0 :
                                DoMicroCode ();
                                Addr++;

                                if (Addr >= MICROSIZE)
                                {
                                    sprintf (Txt, "Too many microcode lines - maximum %d", MICROSIZE);
                                    Error (Txt);
                                }

                                break;

                            case 1 :
                                DoDecode1 ();
                                break;

                            case 2 :
                                DoDecode2 ();
                                break;

                            case 3 :
                                MainLoop = FALSE;
                        }
                }
            }
        } while (MainLoop);

        if (AsmMode < 2)
            Ok = FALSE;

        fclose (In);

        if (Ok)
        {
            printf ("Finished assemble\n");

            if (ShowTable)
                ShowTables ();

            printf ("  Writing output file '%s'\n", OutName);
            WriteTables (OutName);
        }

        if (Symbols)
        {
            printf ("\nLabel table\n");
            DisplaySymbolTable (FALSE);
        }
    }
    else
        fprintf (stderr, "Could not open '%s'\n", FName);

    DeleteSymbolTable ();

    return 0;
}

/*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/


