/*

        DLX RISC Simulator - Monitor Disassembler

        D. J. Viner
*/

#include "cpu.h"
#include "mon.h"

extern  ULONG   LastAddr;
extern  ULONG   ExtractNo (STRPTR Str, WORD *Pos, BOOL *Blank);

struct OpCodeType
{
    UBYTE   OpCode [8];
    UBYTE   Regs [8];
    UBYTE   Type;
} MainCodes [64] =
{
    "TRAP",    "T  o  ", 'J', /* 0x00000000 */
    "RFE",     "      ", 'J', /* 0x04000000 */
    "J",       "O  o  ", 'J', /* 0x08000000 */
    "JAL",     "O  o  ", 'J', /* 0x0C000000 */
    " ",       " ",      '@', /* 0x10000000 */
    " ",       " ",      '@', /* 0x14000000 */
    " ",       " ",      '@', /* 0x18000000 */
    " ",       " ",      '@', /* 0x1C000000 */
    " ",       " ",      '@', /* 0x20000000 */
    " ",       " ",      '@', /* 0x24000000 */
    "JR",      "R  1  ", 'I', /* 0x28000000 */
    "JALR",    "R  1  ", 'I', /* 0x2C000000 */
    "BEQZ",    "RO 1o ", 'B', /* 0x30000000 */
    "BNEZ",    "RO 1o ", 'B', /* 0x34000000 */
    "BFPF",    "O  o  ", 'B', /* 0x38000000 */
    "BFPT",    "O  o  ", 'B', /* 0x3C000000 */
    "LB",      "RoRdo1", 'I', /* 0x40000000 */
    "LBU",     "RoRdo1", 'I', /* 0x44000000 */
    "LH",      "RoRdo1", 'I', /* 0x48000000 */
    "LHU",     "RoRdo1", 'I', /* 0x4C000000 */
    "LW",      "RoRdo1", 'I', /* 0x50000000 */
    " ",       " ",      '@', /* 0x54000000 */
    "LF",      "FoRdo1", 'I', /* 0x58000000 */
    "LD",      "FoRdo1", 'I', /* 0x5C000000 */
    "SB",      "oRRo1d", 'I', /* 0x60000000 */
    " ",       " ",      '@', /* 0x64000000 */
    "SH",      "oRRo1d", 'I', /* 0x68000000 */
    " ",       " ",      '@', /* 0x6C000000 */
    "SW",      "oRRo1d", 'I', /* 0x70000000 */
    " ",       " ",      '@', /* 0x74000000 */
    "SF",      "oRFo1d", 'I', /* 0x78000000 */
    "SD",      "oRFo1d", 'I', /* 0x7C000000 */
    "ADDI",    "RRId1i", 'I', /* 0x80000000 */
    "ADDUI",   "RRId1i", 'I', /* 0x84000000 */
    " ",       " ",      '@', /* 0x88000000 */
    " ",       " ",      '@', /* 0x8C000000 */
    "SUBI",    "RRId1i", 'I', /* 0x90000000 */
    "SUBUI",   "RRId1i", 'I', /* 0x94000000 */
    " ",       " ",      '@', /* 0x98000000 */
    " ",       " ",      '@', /* 0x9C000000 */
    " ",       " ",      '@', /* 0xA0000000 */
    " ",       " ",      '@', /* 0xA4000000 */
    " ",       " ",      '@', /* 0xA8000000 */
    " ",       " ",      '@', /* 0xAC000000 */
    " ",       " ",      '@', /* 0xB0000000 */
    " ",       " ",      '@', /* 0xB4000000 */
    " ",       " ",      '@', /* 0xB8000000 */
    "LHI",     "RI di ", 'I', /* 0xBC000000 */
    "ANDI",    "RRId1i", 'I', /* 0xC0000000 */
    " ",       " ",      '@', /* 0xC4000000 */
    "ORI",     "RRId1i", 'I', /* 0xC8000000 */
    "XORI",    "RRId1i", 'I', /* 0xCC000000 */
    "SLLI",    "RRId1i", 'I', /* 0xD0000000 */
    " ",       " ",      '@', /* 0xD4000000 */
    "SRLI",    "RRId1i", 'I', /* 0xD8000000 */
    "SRAI",    "RRId1i", 'I', /* 0xDC000000 */
    "SLTI",    "RRId1i", 'I', /* 0xE0000000 */
    "SGTI",    "RRId1i", 'I', /* 0xE4000000 */
    "SLEI",    "RRId1i", 'I', /* 0xE8000000 */
    "SGEI",    "RRId1i", 'I', /* 0xEC000000 */
    "SEQI",    "RRId1i", 'I', /* 0xF0000000 */
    "SNEI",    "RRId1i", 'I', /* 0xF4000000 */
    " ",       " ",      '@', /* 0xF8000000 */
    " ",       " ",      'R'  /* 0xFC000000 */
};

struct OpCodeType RCodes [64] =
{
    "MOVS2I",  "RS d1 ", 'R', /* 0xFC000000 */
    "MOVI2S",  "SR d1 ", 'R', /* 0xFC000001 */
    "MOVFP2I", "RF d1 ", 'R', /* 0xFC000002 */
    "MOVI2FP", "FR d1 ", 'R', /* 0xFC000003 */
    "MOVF",    "FF d1 ", 'R', /* 0xFC000004 */
    "MOVD",    "FF d1 ", 'R', /* 0xFC000005 */
    " ",       " ",      '@', /* 0xFC000006 */
    " ",       " ",      '@', /* 0xFC000007 */
    "CVTF2D",  "FF d1 ", 'R', /* 0xFC000008 */
    "CVTF2I",  "FF d1 ", 'R', /* 0xFC000009 */
    "CVTD2F",  "FF d1 ", 'R', /* 0xFC00000A */
    "CVTD2I",  "FF d1 ", 'R', /* 0xFC00000B */
    "CVTI2F",  "FF d1 ", 'R', /* 0xFC00000C */
    "CVTI2D",  "FF d1 ", 'R', /* 0xFC00000D */
    " ",       " ",      '@', /* 0xFC00000E */
    " ",       " ",      '@', /* 0xFC00000F */
    "LTF",     "FF 12 ", 'R', /* 0xFC000010 */
    "GTF",     "FF 12 ", 'R', /* 0xFC000011 */
    "LEF",     "FF 12 ", 'R', /* 0xFC000012 */
    "GEF",     "FF 12 ", 'R', /* 0xFC000013 */
    "EQF",     "FF 12 ", 'R', /* 0xFC000014 */
    "NEF",     "FF 12 ", 'R', /* 0xFC000015 */
    " ",       " ",      '@', /* 0xFC000016 */
    " ",       " ",      '@', /* 0xFC000017 */
    "LTD",     "FF 12 ", 'R', /* 0xFC000018 */
    "GTD",     "FF 12 ", 'R', /* 0xFC000019 */
    "LED",     "FF 12 ", 'R', /* 0xFC00001A */
    "GED",     "FF 12 ", 'R', /* 0xFC00001B */
    "EQD",     "FF 12 ", 'R', /* 0xFC00001C */
    "NED",     "FF 12 ", 'R', /* 0xFC00001D */
    " ",       " ",      '@', /* 0xFC00001E */
    " ",       " ",      '@', /* 0xFC00001F */
    "ADD",     "RRRd12", 'R', /* 0xFC000020 */
    "ADDU",    "RRRd12", 'R', /* 0xFC000021 */
    "ADDF",    "FFFd12", 'R', /* 0xFC000022 */
    "ADDD",    "FFFd12", 'R', /* 0xFC000023 */
    "SUB",     "RRRd12", 'R', /* 0xFC000024 */
    "SUBU",    "RRRd12", 'R', /* 0xFC000025 */
    "SUBF",    "FFFd12", 'R', /* 0xFC000026 */
    "SUBD",    "FFFd12", 'R', /* 0xFC000027 */
    "MULT",    "FFFd12", 'R', /* 0xFC000028 */
    "MULTU",   "FFFd12", 'R', /* 0xFC000029 */
    "MULTF",   "FFFd12", 'R', /* 0xFC00002A */
    "MULTD",   "FFFd12", 'R', /* 0xFC00002B */
    "DIV",     "FFFd12", 'R', /* 0xFC00002C */
    "DIVU",    "FFFd12", 'R', /* 0xFC00002D */
    "DIVF",    "FFFd12", 'R', /* 0xFC00002E */
    "DIVD",    "FFFd12", 'R', /* 0xFC00002F */
    "AND",     "RRRd12", 'R', /* 0xFC000030 */
    " ",       " ",      '@', /* 0xFC000031 */
    "OR",      "RRRd12", 'R', /* 0xFC000032 */
    "XOR",     "RRRd12", 'R', /* 0xFC000033 */
    "SLL",     "RRRd12", 'R', /* 0xFC000034 */
    " ",       " ",      '@', /* 0xFC000035 */
    "SRL",     "RRRd12", 'R', /* 0xFC000036 */
    "SRA",     "RRRd12", 'R', /* 0xFC000037 */
    "SLT",     "RRRd12", 'R', /* 0xFC000038 */
    "SGT",     "RRRd12", 'R', /* 0xFC000039 */
    "SLE",     "RRRd12", 'R', /* 0xFC00003A */
    "SGE",     "RRRd12", 'R', /* 0xFC00003B */
    "SEQ",     "RRRd12", 'R', /* 0xFC00003C */
    "SNE",     "RRRd12", 'R', /* 0xFC00003D */
    " ",       " ",      '@', /* 0xFC00003E */
    " ",       " ",      '@'  /* 0xFC00003F */
};

/*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/

VOID PrReg (UBYTE Type, UBYTE Reg, WORD Dest, WORD Src1, WORD Src2)
{
    fputc (Type, Log);

    if (Reg == 'd')
        fprintf (Log, "%d", Dest);
    else
    if (Reg == '1')
        fprintf (Log, "%d", Src1);
    else
        fprintf (Log, "%d", Src2);
}

/*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/

VOID DoRCodes (WORD Dest, WORD Src1, WORD Src2, WORD Func)
{
    WORD    j;


    fprintf (Log, "%-8.8s  ", RCodes [Func].OpCode);

    for (j = 0; j < 3; j++)
    {
        if (j > 0 && RCodes [Func].Regs [j] > ' ')
            fputc (',', Log);

        if (RCodes [Func].Regs [j] > ' ')
            PrReg (RCodes [Func].Regs [j], RCodes [Func].Regs [j + 3],
                Dest, Src1, Src2);
    }

    fputc ('\n', Log);
}

/*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/

VOID Dump4Bytes (ULONG Instr)
{
    UBYTE   D [4];
    WORD    i, St = 0, En = 4, Dir = 1;


    if (LittleEndian)
    {
        St = 3;
        En = -1;
        Dir = -1;
    }

    memcpy (D, &Instr, 4);
    fprintf (Log, "????      \"");

    for (i = St; i != En; i += Dir)
        if (D [i] >= ' ' && D [i] <= 126)
            fputc (D [i], Log);
        else
            fputc ('.', Log);

    fputc ('"', Log);
    fputc ('\n', Log);
}

/*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/

VOID DisassembleInstr (ULONG Addr, ULONG Instr)
{
    UBYTE   Dest, Src1, Src2;
    WORD    i, j, Offset16, Func;
    LONG    Offset26;
    BOOL    OSet;
    ULONG   Vector;


    i = (WORD) (Instr >> 26);

    switch (MainCodes [i].Type)
    {
        case 'I' :
            DecodeIType (Instr, &Dest, &Src1, &Offset16);
            fprintf (Log, "%-8.8s  ", MainCodes [i].OpCode);
            OSet = FALSE;

            for (j = 0; j < 3; j++)
            {
                switch (MainCodes [i].Regs [j])
                {
                    case 'R' :
                    case 'F' :
                        PrReg (MainCodes [i].Regs [j],
                            MainCodes [i].Regs [j + 3], Dest, Src1, 0);

                        if (OSet)
                        {
                            fputc (')', Log);
                            OSet = FALSE;
                        }

                        break;

                    case 'I' :
                        if (Offset16 > -1 && Offset16 < 10)
                            fprintf (Log, "#%d", (UWORD) Offset16);
                        else
                            fprintf (Log, "#$%X", (UWORD) Offset16);

                        break;

                    case 'o' :
                        if (Offset16 > -1 && Offset16 < 10)
                            fprintf (Log, "%d(", (UWORD) Offset16);
                        else
                            fprintf (Log, "$%X(", (UWORD) Offset16);

                        OSet = TRUE;
                }

                if (j < 2 && MainCodes [i].Regs [j + 1] != ' ' &&
                    MainCodes [i].Regs [j] != 'o')
                        fputc (',', Log);
            }

            fputc ('\n', Log);
            break;

        case 'B' :
            DecodeIType (Instr, &Dest, &Src1, &Offset16);
            fprintf (Log, "%-8.8s  ", MainCodes [i].OpCode);

            for (j = 0; j < 2; j++)
            {
                switch (MainCodes [i].Regs [j])
                {
                    case 'R' :
                        PrReg ('R', MainCodes [i].Regs [j + 3], Dest, Src1, 0);
                        break;

                    case 'O' :
                        fprintf (Log, "$%X", Addr + Offset16 + 4);
                        break;
                }

                if (!j && MainCodes [i].Regs [j + 1] != ' ')
                    fputc (',', Log);
            }

            fputc ('\n', Log);
            break;

        case 'J' :
            DecodeJType (Instr, &Offset26);
            fprintf (Log, "%-8.8s  ", MainCodes [i].OpCode);

            switch (MainCodes [i].Regs [0])
            {
                case 'O' :
                    fprintf (Log, "$%08lX", Addr + Offset26 + 4);
                    break;

                case 'T' :
                    memcpy (&Vector, &Offset26, 4);

                    if (Vector < 10)
                        fprintf (Log, "%d", Vector);
                    else
                        fprintf (Log, "$%lX", Vector);
            }

            fputc ('\n', Log);
            break;

        case 'R' :
            DecodeRType (Instr, &Dest, &Src1, &Src2, &Func);

            if (RCodes [Func].Type != '@')
            {
                DoRCodes (Dest, Src1, Src2, Func);
                break;
            }

            /* Drop through */

        case '@' :
            Dump4Bytes (Instr);
    }
}

/*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/
/* Disassemble from entered address (or LastAddr)                         */
/*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/

VOID Disassemble (STRPTR Cmd)
{
    ULONG   Start, End = 0, Instr, i;
    WORD    Pos = 1;
    BOOL    Blank;


    Start = ExtractNo (Cmd, &Pos, &Blank);

    if (Blank)
        Start = LastAddr;
    else
    {
        if (Start % 4 != 0)
        {
            fprintf (Log, "Misaligned start address\n");
            return;
        }

        End = ExtractNo (Cmd, &Pos, &Blank);
    }

    if (Blank || End < Start)
        End = Start + 63;

    for (i = Start; i <= End; i += 4)
    {
        Instr = FetchL (i);
        fprintf (Log, "%08lX : %08lX  ", i, Instr);
        DisassembleInstr (i, Instr);
    }

    LastAddr = i;
}

/*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/



