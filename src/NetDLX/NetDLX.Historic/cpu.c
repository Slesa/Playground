/////////////////////////////////////////////////////////////////////////////////////////
//	DLX RISC Simulator - Common CPU Module - D. J. Viner
/////////////////////////////////////////////////////////////////////////////////////////

#include "cpu.h"
#include "io.h"

BOOL    LittleEndian = FALSE;
DLX     dlx;
TIMER   timer;
BPTYPE  BP;

/////////////////////////////////////////////////////////////////////////////////////////

struct DecodeTables DecodeT [128] =
{

                        //~~~~~~~~~~~
                        // I/J Types
                        //~~~~~~~~~~~

//                          Dec1     Dec2   Type     F/Len    D12
/* TRAP    000000 00 00 */  TRAP,    ERROR, T_Other, US | 4, "   ",
/* RFE     000001 04 01 */  RFE,     ERROR, T_Other,     4,  "   ",
/* J       000010 08 02 */  JUMP,    ERROR, T_Jump,      4,  "   ",
/* JAL     000011 0C 03 */  JAL,     ERROR, T_JAL,       4,  "   ",

/* -       000100 10 04 */  ERROR,   ERROR, T_None,      0,  "   ",
/* -       000101 14 05 */  ERROR,   ERROR, T_None,      0,  "   ",
/* -       000110 18 06 */  ERROR,   ERROR, T_None,      0,  "   ",
/* -       000111 1C 07 */  ERROR,   ERROR, T_None,      0,  "   ",

/* -       001000 20 08 */  ERROR,   ERROR, T_None,      0,  "   ",
/* -       001001 24 09 */  ERROR,   ERROR, T_None,      0,  "   ",

/* JR      001010 28 0A */  JUMPR,   ERROR, T_Jump,      4,  " R ",
/* JALR    001011 2C 0B */  JALR,    ERROR, T_JAL,       4,  " R ",

/* BEQZ    001100 30 0C */  BEQZ,    ERROR, T_Branch,    4,  " R ",
/* BNEZ    001101 34 0D */  BNEZ,    ERROR, T_Branch,    4,  " R ",
/* BFPF    001110 38 0E */  BFP,     ERROR, T_Branch,    4,  " R ",
/* BFPT    001111 3C 0F */  BFP,     ERROR, T_Branch,    4,  " R ",

/* LB      010000 40 10 */  MEMORY,  LB,    T_Load,      1,  "RR ",
/* LBU     010001 44 11 */  MEMORY,  LBU,   T_Load, US | 1,  "RR ",
/* LH      010010 48 12 */  MEMORY,  LH,    T_Load,      2,  "RR ",
/* LHU     010011 4C 13 */  MEMORY,  LHU,   T_Load, US | 2,  "RR ",
/* LW      010100 50 14 */  MEMORY,  LW,    T_Load,      4,  "RR ",
/* -       010101 54 15 */  ERROR,   ERROR, T_None,      0,  "   ",
/* LF      010110 58 16 */  MEMORY,  LF,    T_Load,      4,  "FR ",
/* LD      010111 5C 17 */  MEMORY,  LD,    T_Load,      8,  "DR ",

/* SB      011000 60 18 */  MEMORY,  ERROR, T_Store,     1,  "RR ",
/* -       011001 64 19 */  ERROR,   ERROR, T_None,      0,  "   ",
/* SH      011010 68 1A */  MEMORY,  ERROR, T_Store,     2,  "RR ",
/* -       011011 6C 1B */  ERROR,   ERROR, T_None,      0,  "   ",
/* SW      011100 70 1C */  MEMORY,  ERROR, T_Store,     4,  "RR ",
/* -       011101 74 1D */  ERROR,   ERROR, T_None,      0,  "   ",
/* SF      011110 78 1E */  MEMORY,  ERROR, T_Store,     4,  "RF ",
/* SD      011111 7C 1F */  MEMORY,  ERROR, T_Store,     8,  "RD ",

/* ADDI    100000 80 20 */  S2isIMM, ADD,   T_ALU,       4,  "RR ",
/* ADDUI   100001 84 21 */  S2isIMM, ADD,   T_ALU,  US | 4,  "RR ",
/* -       100010 88 22 */  ERROR,   ERROR, T_None,      0,  "   ",
/* -       100011 8C 23 */  ERROR,   ERROR, T_None,      0,  "   ",
/* SUBI    100100 90 24 */  S2isIMM, SUB,   T_ALU,       4,  "RR ",
/* SUBUI   100101 94 25 */  S2isIMM, SUB,   T_ALU,  US | 4,  "RR ",
/* -       100110 98 26 */  ERROR,   ERROR, T_None,      0,  "   ",
/* -       100111 9C 27 */  ERROR,   ERROR, T_None,      0,  "   ",
/* -       101000 A0 28 */  ERROR,   ERROR, T_None,      0,  "   ",
/* -       101001 A4 29 */  ERROR,   ERROR, T_None,      0,  "   ",
/* -       101010 A8 2A */  ERROR,   ERROR, T_None,      0,  "   ",
/* -       101011 AC 2B */  ERROR,   ERROR, T_None,      0,  "   ",
/* -       101100 B0 2C */  ERROR,   ERROR, T_None,      0,  "   ",
/* -       101101 B4 2D */  ERROR,   ERROR, T_None,      0,  "   ",
/* -       101110 B8 2E */  ERROR,   ERROR, T_None,      0,  "   ",
/* LHI     101111 BC 2F */  S2isIMM, LHI,   T_ALU,       4,  "RR ",

/* ANDI    110000 C0 30 */  S2isIMM, AND,   T_ALU,       4,  "RR ",
/* -       110001 C4 31 */  ERROR,   ERROR, T_None,      0,  "   ",
/* ORI     110010 C8 32 */  S2isIMM, OR,    T_ALU,       4,  "RR ",
/* XORI    110011 CC 33 */  S2isIMM, XOR,   T_ALU,       4,  "RR ",

/* SLLI    110100 D0 34 */  S2isIMM, SLL,   T_ALU,       4,  "RR ",
/* -       110101 D4 35 */  ERROR,   ERROR, T_None,      0,  "   ",
/* SRLI    110110 D8 36 */  S2isIMM, SRL,   T_ALU,       4,  "RR ",
/* SRAI    110111 DC 37 */  S2isIMM, SRA,   T_ALU,       4,  "RR ",

/* SLTI    111000 E0 38 */  S2isIMM, SLT,   T_Set,       4,  "RR ",
/* SGTI    111001 E4 39 */  S2isIMM, SGT,   T_Set,       4,  "RR ",
/* SLEI    111010 E8 3A */  S2isIMM, SLE,   T_Set,       4,  "RR ",
/* SGEI    111011 EC 3B */  S2isIMM, SGE,   T_Set,       4,  "RR ",
/* SEQI    111100 F0 3C */  S2isIMM, SEQ,   T_Set,       4,  "RR ",
/* SNEI    111101 F4 3D */  S2isIMM, SNE,   T_Set,       4,  "RR ",
/* -       111110 F8 3E */  ERROR,   ERROR, T_None,      0,  "   ",
/* RRRR    111111 FC 3F */  RTYPE,   ERROR, T_None,      0,  "   ",

                        //~~~~~~~~~
                        // R Types
                        //~~~~~~~~~

/* MOVS2I  000000 00 00 */  MOVS2I,  ERROR, T_Move,      4,  "RS ",
/* MOVI2S  000001 04 01 */  MOVI2S,  ERROR, T_Move,      4,  "SR ",
/* MOVFP2I 000010 08 02 */  MOVFP2I, ERROR, T_Move,      4,  "IR ",
/* MOVI2FP 000011 0C 03 */  MOVI2FP, ERROR, T_Move,      4,  "RI ",
/* MOVF    000100 10 04 */  MOVFP,   ERROR, T_Move,      4,  "FF ",
/* MOVD    000101 14 05 */  MOVFP,   ERROR, T_Move,      4,  "DD ",
/* -       000110 18 06 */  ERROR,   ERROR, T_None,      0,  "   ",
/* -       000111 1C 07 */  ERROR,   ERROR, T_None,      0,  "   ",

/* CVTF2D  001010 20 08 */  CVT,     ERROR, T_Convert,   4,  "DF ",
/* CVTF2I  001011 24 09 */  CVT,     ERROR, T_Convert,   4,  "IF ",
/* CVTD2F  001100 28 0A */  CVT,     ERROR, T_Convert,   4,  "FD ",
/* CVTD2I  001101 2C 0B */  CVT,     ERROR, T_Convert,   4,  "ID ",
/* CVTI2F  001110 30 0C */  CVT,     ERROR, T_Convert,   4,  "FI ",
/* CVTI2D  001111 34 0D */  CVT,     ERROR, T_Convert,   4,  "DI ",
/* -       001000 38 0E */  ERROR,   ERROR, T_None,      0,  "   ",
/* -       001001 3C 0F */  ERROR,   ERROR, T_None,      0,  "   ",

/* LTF     010000 40 10 */  SETF,    ERROR, T_Set,       4,  "FF ",
/* GTF     010001 44 11 */  SETF,    ERROR, T_Set,       4,  "FF ",
/* LEF     010010 48 12 */  SETF,    ERROR, T_Set,       4,  "FF ",
/* GEF     010011 4C 13 */  SETF,    ERROR, T_Set,       4,  "FF ",
/* EQF     010100 50 14 */  SETF,    ERROR, T_Set,       4,  "FF ",
/* NEF     010101 54 15 */  SETF,    ERROR, T_Set,       4,  "FF ",
/* -       010110 58 16 */  ERROR,   ERROR, T_None,      0,  "   ",
/* -       010111 5C 17 */  ERROR,   ERROR, T_None,      0,  "   ",

/* LTD     011000 60 18 */  SETF,    ERROR, T_Set,       4,  "DD ",
/* GTD     011001 64 19 */  SETF,    ERROR, T_Set,       4,  "DD ",
/* LED     011010 68 1A */  SETF,    ERROR, T_Set,       4,  "DD ",
/* GED     011011 6C 1B */  SETF,    ERROR, T_Set,       4,  "DD ",
/* EQD     011100 70 1C */  SETF,    ERROR, T_Set,       4,  "DD ",
/* NED     011101 74 1D */  SETF,    ERROR, T_Set,       4,  "DD ",
/* -       011110 78 1E */  ERROR,   ERROR, T_None,      0,  "   ",
/* -       011111 7C 1F */  ERROR,   ERROR, T_None,      0,  "   ",

/* ADD     100000 80 20 */  S2isB,   ADD,   T_ALU,       4,  "RRR",
/* ADDU    100001 84 21 */  S2isB,   ADD,   T_ALU,  US | 4,  "RRR",
/* ADDF    100010 88 22 */  S2isB,   ERROR, T_ALU,       4,  "FFF",
/* ADDD    100011 8C 23 */  S2isB,   ERROR, T_ALU,       4,  "DDD",
/* SUB     100100 90 24 */  S2isB,   SUB,   T_ALU,       4,  "RRR",
/* SUBU    100101 94 25 */  S2isB,   SUB,   T_ALU,  US | 4,  "RRR",
/* SUBF    100110 98 26 */  S2isB,   ERROR, T_ALU,       4,  "FFF",
/* SUBD    100111 9C 27 */  S2isB,   ERROR, T_ALU,       4,  "DDD",
/* MULT    101000 A0 28 */  S2isB,   ERROR, T_ALU,       4,  "III",
/* MULTU   101001 A4 29 */  S2isB,   ERROR, T_ALU,  US | 4,  "III",
/* MULTF   101010 A8 2A */  S2isB,   ERROR, T_ALU,       4,  "FFF",
/* MULTD   101011 AC 2B */  S2isB,   ERROR, T_ALU,       4,  "DDD",
/* DIV     101100 B0 2C */  S2isB,   ERROR, T_ALU,       4,  "III",
/* DIVU    101101 B4 2D */  S2isB,   ERROR, T_ALU,  US | 4,  "III",
/* DIVF    101110 B8 2E */  S2isB,   ERROR, T_ALU,       4,  "FFF",
/* DIVD    101111 BC 2F */  S2isB,   ERROR, T_ALU,       4,  "DDD",

/* AND     110000 C0 30 */  S2isB,   AND,   T_ALU,       4,  "RRR",
/* -       110001 C4 31 */  ERROR,   ERROR, T_None,      0,  "   ",
/* OR      110010 C8 32 */  S2isB,   OR,    T_ALU,       4,  "RRR",
/* XOR     110011 CC 33 */  S2isB,   XOR,   T_ALU,       4,  "RRR",

/* SLL     110100 D0 34 */  S2isB,   SLL,   T_ALU,       4,  "RRR",
/* -       110101 D4 35 */  ERROR,   ERROR, T_None,      0,  "   ",
/* SRL     110110 D8 36 */  S2isB,   SRL,   T_ALU,       4,  "RRR",
/* SRA     110111 DC 37 */  S2isB,   SRA,   T_ALU,       4,  "RRR",

/* SLT     111000 E0 38 */  S2isB,   SLT,   T_Set,       4,  "RRR",
/* SGT     111001 E4 39 */  S2isB,   SGT,   T_Set,       4,  "RRR",
/* SLE     111010 E8 3A */  S2isB,   SLE,   T_Set,       4,  "RRR",
/* SGE     111011 EC 3B */  S2isB,   SGE,   T_Set,       4,  "RRR",
/* SEQ     111100 F0 3C */  S2isB,   SEQ,   T_Set,       4,  "RRR",
/* SNE     111101 F4 3D */  S2isB,   SNE,   T_Set,       4,  "RRR",
/* -       111110 F8 3E */  ERROR,   ERROR, T_None,      0,  "   ",
/* -       111111 FC 3F */  ERROR,   ERROR, T_None,      0,  "   "

};

/////////////////////////////////////////////////////////////////////////////////////////
// Determine the register types for the supplied instruction.
/////////////////////////////////////////////////////////////////////////////////////////

VOID SetRegisterTypes (ULONG IR, UBYTE *Src1, UBYTE *Src2, UBYTE *Dest)
{
    UBYTE   Op = ((IR >> 26) & 0x3F), i, *p;


    if (Op == 0x3F) // If R Type then access the 2nd half of the table
        Op = (IR & 0x3F) + 64;

    for (i = 0; i < 3; i++)
    {
        switch (i)
        {
            case 0 : p = Dest; break;
            case 1 : p = Src1; break;
            case 2 : p = Src2;
        }

        *p = NA;    // Default to 'NotApplicable'

        switch (DecodeT [Op].Regs [i])
        {
            case 'R' : // case ' ' :
                *p = INT;
                break;

            case 'I' :
                *p = INTFP;
                break;

            case 'F' :
                *p = FPS;
                break;

            case 'D' :
                *p = FPD;
                break;

            case 'S' :
                *p = SPEC;
        }
    }
}

/////////////////////////////////////////////////////////////////////////////////////////
//  I   Opcode  Source  Dest    Imm
//      6       5       5       16
//      31-26   25-21   20-16   15-0
/////////////////////////////////////////////////////////////////////////////////////////

VOID DecodeIType (ULONG Instr, UBYTE *d, UBYTE *s, WORD *i)
{
    *s = (Instr & 0x03E00000) >> 21;
    *d = (Instr & 0x001F0000) >> 16;
    *i = Instr & 0x0000FFFF;
}

/////////////////////////////////////////////////////////////////////////////////////////
//  R   Opcode  Source1 Source2 Dest    Function
//      6       5       5       5       11
//      31-26   25-21   20-16   15-11   10-0
/////////////////////////////////////////////////////////////////////////////////////////

VOID DecodeRType (ULONG Instr, UBYTE *d, UBYTE *s1, UBYTE *s2, WORD *f)
{
    *s1 = (Instr & 0x03E00000) >> 21;
    *s2 = (Instr & 0x001F0000) >> 16;
    *d  = (Instr & 0x0000F800) >> 11;
    *f  = Instr & 0x000007FF;
}

/////////////////////////////////////////////////////////////////////////////////////////
//  J   Opcode  Name
//      6       26
//      31-26   25-0
/////////////////////////////////////////////////////////////////////////////////////////

VOID DecodeJType (ULONG Instr, LONG *n)
{
    *n  = Instr & 0x03FFFFFF;

    // Check for -ve offset and force top 6 bits to 1 if so

    if (*n & 0x02000000)
        *n |= 0xFC000000;
}

/////////////////////////////////////////////////////////////////////////////////////////
// Increment clock with checks for timer interrupts activated
/////////////////////////////////////////////////////////////////////////////////////////

VOID IncClock (ULONG Num)
{
    // Ignore if breakpoint set (i.e. doing a TRAP 0)

    if (!BP.Copy)
    {
        dlx.Cycles += Num;
        dlx.Clock += Num;

        if (dlx.Type & TIMERON)
        {
            if (timer.Timer1Status & T_ENABLED)
            {
                timer.Timer1Count += Num;

                if (timer.Timer1Count >= timer.Timer1Latch)
                {
                    SetInterrupt (INT_TIMER);
                    timer.Timer1Status |= T_INTERRUPT;
                    timer.Timer1Count = 0;
                }
            }

            if (timer.Timer2Status & T_ENABLED)
            {
                timer.Timer2Count += Num;

                if (timer.Timer2Count >= timer.Timer2Latch)
                {
                    SetInterrupt (INT_TIMER);
                    timer.Timer2Status |= T_INTERRUPT;
                    timer.Timer2Count = 0;
                }
            }

            if (timer.Timer2Status & T_ENABLED)
            {
                timer.Timer2Count += Num;

                if (timer.Timer3Count >= timer.Timer3Latch)
                {
                    SetInterrupt (INT_TIMER);
                    timer.Timer3Status |= T_INTERRUPT;
                    timer.Timer3Count = 0;
                }
            }
        }
    }
}

/////////////////////////////////////////////////////////////////////////////////////////

VOID SetInterrupt (ULONG Int)
{
    dlx.IStatus |= Int;
}

/////////////////////////////////////////////////////////////////////////////////////////
// Ctrl-C/A routines to break into program (Amiga and PC only currently)
/////////////////////////////////////////////////////////////////////////////////////////

#ifdef AMIGA
void __regargs _CXBRK (void)
{
    dlx.Running = FALSE;
    printf ("**Break\n");
}

/////////////////////////////////////////////////////////////////////////////////////////

VOID CheckCtrlC ()
{
    chkabort ();
}
#endif

/////////////////////////////////////////////////////////////////////////////////////////

#ifdef UNIX
VOID CheckCtrlC ()
{
}
#endif

/////////////////////////////////////////////////////////////////////////////////////////

#ifdef MAC
VOID CheckCtrlC ()
{
}
#endif

/////////////////////////////////////////////////////////////////////////////////////////
// This routine modified from the Turbo C example keyboard code
/////////////////////////////////////////////////////////////////////////////////////////

#ifdef Pc
VOID CheckCtrlC ()
{
    int key, modifiers;

    if (bioskey (1))
    {
        key = bioskey (0);
        modifiers = bioskey (2);

        if (modifiers & 4 && (key == 0x1E01 || key == 0x2E03))
        {
            dlx.Running = FALSE;
            printf ("**Break\n");
        }
    }
}
#endif

/////////////////////////////////////////////////////////////////////////////////////////
// Call the relevant module to execute each machine instruction
/////////////////////////////////////////////////////////////////////////////////////////

BOOL RunOneInstr (BOOL Debug)
{
    switch (SIMTYPE)
    {
        case HARDWIRED :
            return RunOneHardwiredInstr (Debug);

        case MICROCODE :
            return RunOneMicrocodeInstr (Debug);

        case PIPELINED :
            return RunOnePipelinedInstr (Debug);
    }

    return FALSE;
}

/////////////////////////////////////////////////////////////////////////////////////////
// Main execution loop
/////////////////////////////////////////////////////////////////////////////////////////

VOID Run (BOOL Debug)
{
    dlx.Running = TRUE;

    do {
        dlx.Running = RunOneInstr (Debug);
        CheckCtrlC ();
    } while (dlx.Running);
}

/////////////////////////////////////////////////////////////////////////////////////////

VOID ErrorExit ()
{
    free (dlx.Memory);
    exit (20);
}

/////////////////////////////////////////////////////////////////////////////////////////
// Clear various internal dlx regs - if All is set then it clears the user accessible
// regs as well.
/////////////////////////////////////////////////////////////////////////////////////////

VOID ClearDLXRegs (BOOL All)
{
    WORD    i;


    dlx.Instr = dlx.IDone = dlx.IStart = 0;
    dlx.Clock = dlx.Cycles = dlx.IStatus = 0;

    dlx.Loads = dlx.Stores = dlx.ALU = dlx.Set = 0;
    dlx.Jumps = dlx.JALs = dlx.Move = dlx.Convert = 0;
    dlx.BranchTaken = dlx.BranchNotTaken = dlx.TrapRfe = 0;
    BP.Copy = BP.PC = 0;

    dlx.Trapped = FALSE;

    for (i = IF; i <= WB; i++)
        dlx.Pipe [i].Status = FLUSH;

    if (All)
    {
        for (i = 0; i < 32; i++)
        {
            dlx.R [i] = 0;
            dlx.FP.I [i] = 0;
        }

        dlx.PC = dlx.IAR = dlx.FPStatus = 0;
        dlx.MAR = dlx.MDR = dlx.Temp = 0;
        dlx.RegA = dlx.RegB = dlx.RegC = 0;
    }
}

/////////////////////////////////////////////////////////////////////////////////////////
// Load a microcode table
/////////////////////////////////////////////////////////////////////////////////////////

VOID LoadMicrocode (STRPTR FName, BOOL MainTable)
{
    UWORD   i;
    FILE    *f;
    ULONG   Sz;
    UBYTE   Conv1 [4], Conv2 [4], j;
    BOOL    McErr = FALSE, Open = FALSE;


    f = fopen (FName, "rb");

    if (!f)
    {
        printf ("Cannot open microcode table file '%s'\n", FName);
        McErr = TRUE;
    }
    else
        Open = TRUE;

    if (!McErr)
		for (i = 0; i < MICROSIZE && !McErr; i++)
		{
			Sz = fread (Conv2, 1, 4, f);

			if (Sz != 4)
			{
				printf ("Read Microcode error at %X (%ld)\n", i * 4, Sz);
				McErr = TRUE;
			}
			else
			if (LittleEndian)
			{
				for (j = 0; j < 4; j++)
					Conv1 [3 - j] = Conv2 [j];

				memcpy (&dlx.Microcode [i], Conv1, 4);
			}
			else
				memcpy (&dlx.Microcode [i], Conv2, 4);
		}

    if (!McErr)
    {
        Sz = fread (dlx.MicroJump, 1, MICROSIZE, f);

        if (Sz != MICROSIZE)
        {
            printf ("Read MicroJump error (%ld/%lX)\n", Sz, ftell (f));
            McErr = TRUE;
        }
    }

    if (!McErr)
    {
        Sz = fread (dlx.Decode1, 1, DECODE1SIZE, f);

        if (Sz != DECODE1SIZE)
        {
            printf ("Read Decode1 error (%ld/%lX)\n", Sz, ftell (f));
            McErr = TRUE;
        }
    }

    if (!McErr)
    {
        Sz = fread (dlx.Decode2, 1, DECODE2SIZE, f);

        if (Sz != DECODE2SIZE)
        {
            printf ("Read Decode2 error (%ld/%lX)\n", Sz, ftell (f));
            McErr = TRUE;
        }
    }

    if (Open)
        fclose (f);

    if (McErr && MainTable)
        ErrorExit ();
}

/////////////////////////////////////////////////////////////////////////////////////////
// Initialise the cpu - default to hardwired (most settings can be changed through
// entries in the dlx.ini file)
/////////////////////////////////////////////////////////////////////////////////////////

BOOL InitCpu ()
{
    FILE    *f;
    UBYTE   Txt [82];

    // Set defaults

    memset (&dlx, 0, sizeof (dlx));

    dlx.Type = HARDWIRED | TIMERON | TRAPSON | MOVESON | VECTORTRAPS;

    dlx.SizeOfMem = 0x40000;  // Start with 256K

    // Read in the microcode tables

    LoadMicrocode ("dlxmcode.tbl", TRUE);

    // Allow user settings to alter defaults

    ReadIni ();

    // Ignore calls for greater than 63K on Turbo-C on an MS-DOS-based PC
    // until calloc probs fixed

//  if (LittleEndian && dlx.SizeOfMem > 0xFC00)
//      dlx.SizeOfMem = 0xFC00;

    // Allocate some system memory

    do {
        dlx.Memory = (STRPTR) calloc (1, dlx.SizeOfMem);

        if (!dlx.Memory)    // If failed then try with half size
            dlx.SizeOfMem /= 2;

    } while (!dlx.Memory && dlx.SizeOfMem > 16384);

    if (!dlx.Memory)
        return FALSE;

    // Autoload any required files

    f = fopen ("dlxaload.lst", "rb");

    if (f)
    {
        while (!feof (f))
        {
            fgets (Txt, 80, f);

            if (Txt [0] > ' ')
                DoLoad (Txt, FALSE);
        }

        fclose (f);
    }

    return TRUE;
}

/////////////////////////////////////////////////////////////////////////////////////////


