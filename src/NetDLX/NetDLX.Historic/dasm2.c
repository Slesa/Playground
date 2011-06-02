/////////////////////////////////////////////////////////////////////////////////////////
//
// DLX Assembler - Assembler routines
//
// D. J. Viner
/////////////////////////////////////////////////////////////////////////////////////////

#include "dasm.h"
#include "dasm2.h"
#include "dsym.h"

/////////////////////////////////////////////////////////////////////////////////////////
// Main structure which assembles DLX machine code from assembler instructions
/////////////////////////////////////////////////////////////////////////////////////////

struct OpCodeType OpCodes [] =
{
    "ADD ",     "RRRd12", 'R', 0xFC000020,   // 1111 1100  10 0000
    "ADDD ",    "FFFd12", 'R', 0xFC000023,   // 1111 1100  10 0011
    "ADDF ",    "fffd12", 'R', 0xFC000022,   // 1111 1100  10 0010
    "ADDI ",    "RRId1i", 'I', 0x80000000,   // 1000 0000
    "ADDU ",    "RRRd12", 'R', 0xFC000021,   // 1111 1100  10 0001
    "ADDUI ",   "RRId1i", 'I', 0x84000000,   // 1000 0100
    "AND ",     "RRRd12", 'R', 0xFC000030,   // 1111 1100  11 0000
    "ANDI ",    "RRId1i", 'I', 0xC0000000,   // 1100 0000
    "BEQZ ",    "RQ 1o ", 'B', 0x30000000,   // 0011 0000
    "BFPF ",    "Q  o  ", 'B', 0x38000000,   // 0011 1000
    "BFPT ",    "Q  o  ", 'B', 0x3C000000,   // 0011 1100
    "BNEZ ",    "RQ 1o ", 'B', 0x34000000,   // 0011 0100
    "CVTD2F ",  "fF d1 ", 'R', 0xFC00000A,   // 1111 1100  00 1010
    "CVTD2I ",  "fF d1 ", 'R', 0xFC00000B,   // 1111 1100  00 1011
    "CVTF2D ",  "Ff d1 ", 'R', 0xFC000008,   // 1111 1100  00 1000
    "CVTF2I ",  "ff d1 ", 'R', 0xFC000009,   // 1111 1100  00 1001
    "CVTI2D ",  "Ff d1 ", 'R', 0xFC00000D,   // 1111 1100  00 1101
    "CVTI2F ",  "ff d1 ", 'R', 0xFC00000C,   // 1111 1100  00 1100
    "DC.B ",    " ",      'P', PSEUDO_DCB,
    "DC.H ",    " ",      'P', PSEUDO_DCH,
    "DC.W ",    " ",      'P', PSEUDO_DCW,
    "DEBUG ",   " ",      'P', PSEUDO_DEBUG,
    "DIV ",     "fffd12", 'R', 0xFC00002C,   // 1111 1100  10 1100
    "DIVD ",    "FFFd12", 'R', 0xFC00002F,   // 1111 1100  10 1111
    "DIVF ",    "fffd12", 'R', 0xFC00002E,   // 1111 1100  10 1110
    "DIVU ",    "fffd12", 'R', 0xFC00002D,   // 1111 1100  10 1101
    "DS.B ",    " ",      'P', PSEUDO_DSB,
    "DS.H ",    " ",      'P', PSEUDO_DSH,
    "DS.W ",    " ",      'P', PSEUDO_DSW,
    "END ",     " ",      'P', PSEUDO_END,
    "ENDIF ",   " ",      'P', PSEUDO_ENDIF,
    "ENDM ",    " ",      'P', PSEUDO_ENDM,
    "EQD ",     "FF 12 ", 'R', 0xFC00001C,   // 1111 1100  01 1100
    "EQF ",     "ff 12 ", 'R', 0xFC000014,   // 1111 1100  01 0100
    "GED ",     "FF 12 ", 'R', 0xFC00001B,   // 1111 1100  01 1011
    "GEF ",     "ff 12 ", 'R', 0xFC000013,   // 1111 1100  01 0011
    "GENINC ",  " ",      'P', PSEUDO_GENINC,
    "GTD ",     "FF 12 ", 'R', 0xFC000019,   // 1111 1100  01 1001
    "GTF ",     "ff 12 ", 'R', 0xFC000011,   // 1111 1100  01 0001
    "IFDEF ",   " ",      'P', PSEUDO_IFDEF,
    "IFNDEF ",  " ",      'P', PSEUDO_IFNDEF,
    "INCLUDE ", " ",      'P', PSEUDO_INCLUDE,
    "J ",       "Q  o  ", 'J', 0x08000000,   // 0000 1000
    "JAL ",     "Q  o  ", 'J', 0x0C000000,   // 0000 1100
    "JALR ",    "R  1  ", 'I', 0x2C000000,   // 0010 1100
    "JR ",      "R  1  ", 'I', 0x28000000,   // 0010 1000
    "LB ",      "RORdo1", 'I', 0x40000000,   // 0100 0000
    "LBU ",     "RORdo1", 'I', 0x44000000,   // 0100 0100
    "LD ",      "FORdo1", 'I', 0x5C000000,   // 0101 1100
    "LED ",     "FF 12 ", 'R', 0xFC00001A,   // 1111 1100  01 1010
    "LEF ",     "ff 12 ", 'R', 0xFC000012,   // 1111 1100  01 0010
    "LF ",      "fORdo1", 'I', 0x58000000,   // 0101 1000
    "LH ",      "RORdo1", 'I', 0x48000000,   // 0100 1000
    "LHI ",     "RI di ", 'I', 0xBC000000,   // 1011 1100
    "LHU ",     "RORdo1", 'I', 0x4C000000,   // 0100 1100
    "LIST ",    " ",      'P', PSEUDO_LIST,
    "LTD ",     "FF 12 ", 'R', 0xFC000018,   // 1111 1100  01 1000
    "LTF ",     "ff 12 ", 'R', 0xFC000010,   // 1111 1100  01 0000
    "LW ",      "RORdo1", 'I', 0x50000000,   // 1101 0000
    "MACRO ",   " ",      'P', PSEUDO_MACRO,
    "MOVD ",    "FF d1 ", 'R', 0xFC000005,   // 1111 1100  00 0101
    "MOVF ",    "ff d1 ", 'R', 0xFC000004,   // 1111 1100  00 0100
    "MOVFP2I ", "Rf d1 ", 'R', 0xFC000002,   // 1111 1100  00 0010
    "MOVI2FP ", "fR d1 ", 'R', 0xFC000003,   // 1111 1100  00 0011
    "MOVI2S ",  "SR d1 ", 'R', 0xFC000001,   // 1111 1100  00 0001
    "MOVS2I ",  "RS d1 ", 'R', 0xFC000000,   // 1111 1100  00 0000
    "MULT ",    "fffd12", 'R', 0xFC000028,   // 1111 1100  10 1000
    "MULTD ",   "FFFd12", 'R', 0xFC00002B,   // 1111 1100  10 1011
    "MULTF ",   "fffd12", 'R', 0xFC00002A,   // 1111 1100  10 1010
    "MULTU ",   "fffd12", 'R', 0xFC000029,   // 1111 1100  10 1001
    "NED ",     "FF 12 ", 'R', 0xFC00001D,   // 1111 1100  01 1101
    "NEF ",     "ff 12 ", 'R', 0xFC000015,   // 1111 1100  01 0101
    "NODEBUG ", " ",      'P', PSEUDO_NODEBUG,
    "NOLIST ",  " ",      'P', PSEUDO_NOLIST,
    "NOSYM ",   " ",      'P', PSEUDO_NOSYM,
    "OR ",      "RRRd12", 'R', 0xFC000032,   // 1111 1100  11 0010
    "ORI ",     "RRId1i", 'I', 0xC8000000,   // 1100 1000
    "PAD ",     " ",      'P', PSEUDO_PAD,
    "PAGE ",    " ",      'P', PSEUDO_PAGE,
    "RFE ",     "      ", 'J', 0x04000000,   // 0000 0100
    "SB ",      "ORRo1d", 'I', 0x60000000,   // 0110 0000
    "SD ",      "ORFo1d", 'I', 0x7C000000,   // 0111 1100
    "SEQ ",     "RRRd12", 'R', 0xFC00003C,   // 1111 1100  11 1100
    "SEQI ",    "RRId1i", 'I', 0xF0000000,   // 1111 0000
    "SF ",      "ORfo1d", 'I', 0x78000000,   // 0111 1000
    "SGE ",     "RRRd12", 'R', 0xFC00003B,   // 1111 1100  11 1011
    "SGEI ",    "RRId1i", 'I', 0xEC000000,   // 1110 1100
    "SGT ",     "RRRd12", 'R', 0xFC000039,   // 1111 1100  11 1001
    "SGTI ",    "RRId1i", 'I', 0xE4000000,   // 1110 0100
    "SH ",      "ORRo1d", 'I', 0x68000000,   // 0110 1000
    "SKIP ",    " ",      'P', PSEUDO_SKIP,
    "SLE ",     "RRRd12", 'R', 0xFC00003A,   // 1111 1100  11 1010
    "SLEI ",    "RRId1i", 'I', 0xE8000000,   // 1110 1000
    "SLL ",     "RRRd12", 'R', 0xFC000034,   // 1111 1100  11 0100
    "SLLI ",    "RRId1i", 'I', 0xD0000000,   // 1101 0000
    "SLT ",     "RRRd12", 'R', 0xFC000038,   // 1111 1100  11 1000
    "SLTI ",    "RRId1i", 'I', 0xE0000000,   // 1110 0000
    "SNE ",     "RRRd12", 'R', 0xFC00003D,   // 1111 1100  11 1101
    "SNEI ",    "RRId1i", 'I', 0xF4000000,   // 1111 0100
    "SRA ",     "RRRd12", 'R', 0xFC000037,   // 1111 1100  11 0111
    "SRAI ",    "RRId1i", 'I', 0xDC000000,   // 1101 1100
    "SRL ",     "RRRd12", 'R', 0xFC000036,   // 1111 1100  11 0110
    "SRLI ",    "RRId1i", 'I', 0xD8000000,   // 1101 1000
    "SUB ",     "RRRd12", 'R', 0xFC000024,   // 1111 1100  10 0100
    "SUBD ",    "FFFd12", 'R', 0xFC000027,   // 1111 1100  10 0111
    "SUBF ",    "fffd12", 'R', 0xFC000026,   // 1111 1100  10 0110
    "SUBI ",    "RRId1i", 'I', 0x90000000,   // 1001 0000
    "SUBU ",    "RRRd12", 'R', 0xFC000025,   // 1111 1100  10 0101
    "SUBUI ",   "RRId1i", 'I', 0x94000000,   // 1001 0100
    "SW ",      "ORRo1d", 'I', 0x70000000,   // 0111 0000
    "SYM ",     " ",      'P', PSEUDO_SYM,
    "TRAP ",    "Q  o  ", 'T', 0x00000000,   // 0000 0000
    "XOR ",     "RRRd12", 'R', 0xFC000033,   // 1111 1100  11 0011
    "XORI ",    "RRId1i", 'I', 0xCC000000,   // 1100 1100
    NULL,       NULL,       0,          0
};

UBYTE   Offsets [26] [2];
BOOL    DoAddr = FALSE;

/////////////////////////////////////////////////////////////////////////////////////////
// Initialise the array of name pointers into the OpCodes structure array
/////////////////////////////////////////////////////////////////////////////////////////

VOID SetOffsets ()
{
    WORD    i = 0, j = 0, Count = 0;
    UBYTE   Last = 'A';

    memset (Offsets, 0, sizeof (Offsets));

    while (OpCodes [i].Op [0] > ' ')
    {
        if (OpCodes [i].Op [0] == Last)
            Count++;
        else
        {
            Offsets [j] [1] = Count;
            Count = 1;
            Last = OpCodes [i].Op [0];
            j = Last - 'A';
            Offsets [j] [0] = i;
        }

        i++;
    }

    Offsets [j] [1] = Count;
}

/////////////////////////////////////////////////////////////////////////////////////////
// Output a warning
/////////////////////////////////////////////////////////////////////////////////////////

VOID Warn (STRPTR Msg, BOOL PrintLine)
{
    if (PrintLine)
    {
        fprintf (stderr, "%s", Line);
        fprintf (stderr, "Warning: Line %d ", LineNo);
    }

    fprintf (stderr, "%s\n", Msg);
}

/////////////////////////////////////////////////////////////////////////////////////////
// Output an error message and terminate execution
/////////////////////////////////////////////////////////////////////////////////////////

VOID Error (STRPTR Msg, BOOL PrintLine)
{
    struct  FileDetails *f;

    if (PrintLine)
    {
        fprintf (stderr, "%s", Line);
        fprintf (stderr, "Error: Line %d ", LineNo);
    }

    fprintf (stderr, "%s\n", Msg);

    if (ErrorSym)
    {
        printf ("\nSymbol table\n");
        DisplaySymbolTable (FALSE);
    }

    DeleteSymbolTable ();
    fclose (In);

    if (Pass == 2)
        fclose (Op);

    while (Fd)
    {
        f = Fd->Prev;
        free (Fd);
        Fd = f;
    }

    if (DoList)
        fclose (Lst);

    exit (20);
}

/////////////////////////////////////////////////////////////////////////////////////////
// Skip any whitespace
/////////////////////////////////////////////////////////////////////////////////////////

VOID SkipWhitespace ()
{
    while (*ptr == ' ' || *ptr == '\t')
        ptr++;
}

/////////////////////////////////////////////////////////////////////////////////////////
// Skip until we reach some 'whitespace' (includes a multitude of other chars)
/////////////////////////////////////////////////////////////////////////////////////////

VOID SkipToWhitespace ()
{
    BOOL    Loop = TRUE;

    do {
        switch (*ptr)
        {
            case ' ' : case ',' : case '\t' : case ';' :
            case '*' : case '+' : case '\n' : case '-' :
            case '/' : case '&' : case '!' :
            case '=' : case ')' : case '('  : case ':' :
            case 0x27 : case 0x22 :
                Loop = FALSE;
                break;

            default :
                ptr++;
        }

    } while (Loop);
}

/////////////////////////////////////////////////////////////////////////////////////////
// Move onto a new file via an INCLUDE statement
/////////////////////////////////////////////////////////////////////////////////////////

VOID AddFile ()
{
    struct FileDetails *f;

    f = (struct FileDetails *) calloc (1, sizeof (struct FileDetails));

    if (!f)
        Error ("Out of memory (AddFile)", FALSE);

    strcpy (f->FName, FName);
    f->In = In;
    f->LineNo = LineNo;
    f->Prev = Fd;

    Fd = f;
}

/////////////////////////////////////////////////////////////////////////////////////////
// Return to the parent file
/////////////////////////////////////////////////////////////////////////////////////////

VOID EndFile ()
{
    struct FileDetails *f;

    if (Fd)
    {
        fclose (In);

        strcpy (FName, Fd->FName);
        In = Fd->In;
        LineNo = Fd->LineNo;

        f = Fd;
        Fd = Fd->Prev;
        free (f);
    }
    else
        MainLoop = FALSE;   // End of all files
}

/////////////////////////////////////////////////////////////////////////////////////////
// Output a byte (if pass = 2). If this is the first time called in pass 2 then output
// the load address as well. In pass 1 we just increment the address.
/////////////////////////////////////////////////////////////////////////////////////////

VOID OutputByte (UBYTE b)
{
    WORD    i;

    if (Pass == 2)
    {
        if (!CodeGen)
        {
            CodeGen = TRUE;
            DoAddr = TRUE;
            OutputLong (Addr);
            DoAddr = FALSE;
        }
        else // Filler bytes
        if (!DoAddr && Addr2 < Addr)
        {
            for (i = Addr2; i < Addr; i++)
                fputc (0, Op);
        }

        fputc (b, Op);
    }

    if (!DoAddr)
        Addr++;

    Addr2 = Addr;
}

/////////////////////////////////////////////////////////////////////////////////////////
// Output a word
/////////////////////////////////////////////////////////////////////////////////////////

VOID OutputWord (UWORD w)
{
    UBYTE   W [2];


    memcpy (W, &w, 2);

    if (LittleEndian)
    {
        OutputByte (W [1]);
        OutputByte (W [0]);
    }
    else
    {
        OutputByte (W [0]);
        OutputByte (W [1]);
    }
}

/////////////////////////////////////////////////////////////////////////////////////////
// Output a long word
/////////////////////////////////////////////////////////////////////////////////////////

VOID OutputLong (ULONG l)
{
    UBYTE   L [4];

    memcpy (L, &l, 4);

    if (LittleEndian)
    {
        OutputByte (L [3]);
        OutputByte (L [2]);
        OutputByte (L [1]);
        OutputByte (L [0]);
    }
    else
    {
        OutputByte (L [0]);
        OutputByte (L [1]);
        OutputByte (L [2]);
        OutputByte (L [3]);
    }
}

/////////////////////////////////////////////////////////////////////////////////////////
// Put the current symbol pointed to by p into Str and increment p past it.
/////////////////////////////////////////////////////////////////////////////////////////

STRPTR CollectSym (STRPTR p, STRPTR Str)
{
    WORD    i = 0;
    BOOL    Loop = TRUE;

    do {
        switch (*p)
        {
            case ' ' : case ',' : case '\t' : case ';' :
            case '*' : case '+' : case '\n' : case '-' :
            case '/' : case '&' : case '!' :
            case '=' : case ')' : case '('  : case ':' :
            case 0x27 : case 0x22 :
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

/////////////////////////////////////////////////////////////////////////////////////////
// Returns 0 if blank/comment line
/////////////////////////////////////////////////////////////////////////////////////////

UBYTE GetLine ()
{
    BOOL NotGot;

    do {
        NotGot = TRUE;
        LineNo++;
        fgets (Line, LINELEN, In);

        if (feof (In))
        {
            EndFile ();

            if (!MainLoop)
                return COMMENT;
        }
        else
            NotGot = FALSE;

    } while (NotGot);

    if (Debug)
        printf ("%08lX:  %s", Addr, Line);

    ptr = &Line [0];

    SkipWhitespace ();

    switch (*ptr)
    {
        case 0    :
        case ';'  :
        case '/'  :
        case '\n' :
            return COMMENT;

        case '*'  :
            return SETADDR;
    }

    if (memcmp (ptr, "ENDIF", 5) == 0)
        return ENDCOND;

    return VALID;
}

/////////////////////////////////////////////////////////////////////////////////////////
// Copy a macro from the source file into a macro definition until an ENDM command is
// found.
/////////////////////////////////////////////////////////////////////////////////////////

VOID CopyMacro (SymTab S)
{
    Macro   M, M2 = (Macro) NULL;
    WORD    l;
    BOOL    First = TRUE;

    S->Mac = TRUE;
    S->Val = 0;

    while (1)
    {
        GetLine ();

        if (strnicmp (ptr, "ENDM", 4) == 0)
            return;

        S->Val++;   // Count of lines

        M = (Macro) calloc (1, sizeof (struct MacroType));

        if (!M)
            Error ("Out of memory (CopyMacro1)", TRUE);

        l = strlen (ptr);

        M->Line = (STRPTR) calloc (1, l + 1);

        if (!M->Line)
            Error ("Out of memory (CopyMacro2)", TRUE);

        strcpy (M->Line, ptr);

        if (First)
        {
            S->M = M;
            First = FALSE;
        }
        else
            M2->NextM = M;

        M2 = M;
    }
}

/////////////////////////////////////////////////////////////////////////////////////////


