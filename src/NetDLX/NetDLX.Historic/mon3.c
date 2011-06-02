/*
        DLX RISC Simulator - Monitor Source Module 3
        External routines required by cpu modules

        D. J. Viner
*/

#include "cpu.h"
#include "dis.h"
#include "mon.h"

ULONG   DebugLevel = DB_DISASSEM | DB_PIPESTAGES;
FILE    *Log;   /* Log file handle */

VOID OutputChr (UBYTE Ch)
{
    putchar (Ch);
    fflush (stdout);
}

UBYTE InputChr ()
{
    LONG    Ch;

    do {
#ifdef  AMIGA
        Ch = getch ();
#else
/*  THIS DOESN'T WORK AS INTENDED ON UNIX - MAY NEED TO USE Curses */
        Ch = getchar ();
#endif
    } while (Ch == EOF);

    return (UBYTE) Ch;
}

VOID OutputDecVal (ULONG Val)
{
    printf ("%ld", Val);
}

VOID OutputHexVal (ULONG Val)
{
    printf ("%lX", Val);
}

VOID BreakpointTest ()
{
    WORD    i;


    if (BP.Num)
    {
        for (i = 0; i < BP.Num; i++)
            if (BP.BPs [i] == dlx.PC && dlx.PC != BP.PC)
            {
                /* Copy instruction and then replace it with a HALT */

                BP.Copy = FetchL (dlx.PC);
                StoreL (dlx.PC, 0L);
                dlx.Instr--;
                break;
            }
    }

    BP.PC = 0;
}

VOID ShowMicrocode (ULONG Index, BOOL ShowCycles)
{
    UWORD   Cond;


    fprintf (Log, "%2ld  ", Index);

    switch ((dlx.Microcode [Index] >> 28) & 15)
    {
        case DEST_C :       fprintf (Log, "C    ");          break;
        case DEST_Temp :    fprintf (Log, "Temp ");          break;
        case DEST_PC :      fprintf (Log, "PC   ");          break;
        case DEST_IAR :     fprintf (Log, "IAR  ");          break;
        case DEST_MAR :     fprintf (Log, "MAR  ");          break;
        case DEST_MDR :     fprintf (Log, "MDR  ");          break;
        case DEST_SR :      fprintf (Log, "SR   ");          break;
        case DEST_FPSR :    fprintf (Log, "FPSR ");          break;
        case DEST_CD :      fprintf (Log, "CD   ");          break;
        default :           fprintf (Log, "     ");
    }

    switch ((dlx.Microcode [Index] >> 22) & 63)
    {
        case ALU_ADD :      fprintf (Log, "ADD    ");   break;
        case ALU_SUB :      fprintf (Log, "SUB    ");   break;
        case ALU_RSUB :     fprintf (Log, "RSUB   ");   break;
        case ALU_AND :      fprintf (Log, "AND    ");   break;
        case ALU_OR :       fprintf (Log, "OR     ");   break;
        case ALU_XOR :      fprintf (Log, "XOR    ");   break;
        case ALU_SLL :      fprintf (Log, "SLL    ");   break;
        case ALU_SRL :      fprintf (Log, "SRL    ");   break;
        case ALU_SRA :      fprintf (Log, "SRA    ");   break;
        case ALU_PassS1 :   fprintf (Log, "PassS1 ");   break;
        case ALU_PassS2 :   fprintf (Log, "PassS2 ");   break;
        case ALU_MULTI  :   fprintf (Log, "MULTI  ");   break;
        case ALU_DIVI   :   fprintf (Log, "DIVI   ");   break;
        case ALU_MULTF  :   fprintf (Log, "MULTF  ");   break;
        case ALU_DIVF   :   fprintf (Log, "DIVF   ");   break;
        case ALU_ADDF   :   fprintf (Log, "ADDF   ");   break;
        case ALU_SUBF   :   fprintf (Log, "SUBF   ");   break;
        case ALU_RSUBF  :   fprintf (Log, "RSUBF  ");   break;
        case ALU_MULTD  :   fprintf (Log, "MULTD  ");   break;
        case ALU_DIVD   :   fprintf (Log, "DIVD   ");   break;
        case ALU_ADDD   :   fprintf (Log, "ADDD   ");   break;
        case ALU_SUBD   :   fprintf (Log, "SUBD   ");   break;
        case ALU_RSUBD  :   fprintf (Log, "RSUBD  ");   break;
        case ALU_CVTF2D :   fprintf (Log, "CVTF2D ");   break;
        case ALU_CVTF2I :   fprintf (Log, "CVTF2I ");   break;
        case ALU_CVTD2F :   fprintf (Log, "CVTD2F ");   break;
        case ALU_CVTD2I :   fprintf (Log, "CVTD2I ");   break;
        case ALU_CVTI2F :   fprintf (Log, "CVTI2F ");   break;
        case ALU_CVTI2D :   fprintf (Log, "CVTI2D ");   break;
        default :           fprintf (Log, "       ");
    }

    switch ((dlx.Microcode [Index] >> 18) & 15)
    {
        case SRC_A :        fprintf (Log, "A     ");   break;
        case SRC_Temp :     fprintf (Log, "Temp  ");   break;
        case SRC_PC :       fprintf (Log, "PC    ");   break;
        case SRC_IAR :      fprintf (Log, "IAR   ");   break;
        case SRC_MAR :      fprintf (Log, "MAR   ");   break;
        case SRC_MDR :      fprintf (Log, "MDR   ");   break;
        case SRC_imm16 :    fprintf (Log, "imm16 ");   break;
        case SRC_imm26 :    fprintf (Log, "imm26 ");   break;
        case SRC_Const :    fprintf (Log, "Const ");   break;
        case SRC_SR :       fprintf (Log, "SR    ");   break;
        case SRC_FPSR :     fprintf (Log, "FPSR  ");   break;
        case SRC_AD :       fprintf (Log, "AD    ");   break;
        default :           fprintf (Log, "      ");
    }

    switch ((dlx.Microcode [Index] >> 14) & 15)
    {
        case SRC_B :        fprintf (Log, "B     ");   break;
        case SRC_Temp :     fprintf (Log, "Temp  ");   break;
        case SRC_PC :       fprintf (Log, "PC    ");   break;
        case SRC_IAR :      fprintf (Log, "IAR   ");   break;
        case SRC_MAR :      fprintf (Log, "MAR   ");   break;
        case SRC_MDR :      fprintf (Log, "MDR   ");   break;
        case SRC_imm16 :    fprintf (Log, "imm16 ");   break;
        case SRC_imm26 :    fprintf (Log, "imm26 ");   break;
        case SRC_Const :    fprintf (Log, "Const ");   break;
        case SRC_SR :       fprintf (Log, "SR    ");   break;
        case SRC_FPSR :     fprintf (Log, "FPSR  ");   break;
        case SRC_BD :       fprintf (Log, "BD    ");   break;
        default :           fprintf (Log, "      ");
    }

    if (((dlx.Microcode [Index] >> 18) & 15) == SRC_Const ||
        ((dlx.Microcode [Index] >> 14) & 15) == SRC_Const)
        fprintf (Log, "%2d ", (UBYTE) ((dlx.Microcode [Index] >> 9) & 31));
    else
        fprintf (Log, "   ");

    switch ((dlx.Microcode [Index] >> 5) & 15)
    {
        case MISC_InstrRd : fprintf (Log, "InstrRd ");   break;
        case MISC_DataRd :  fprintf (Log, "DataRd  ");   break;
        case MISC_Write :   fprintf (Log, "Write   ");   break;
        case MISC_ABRF :    fprintf (Log, "AB<-RF  ");   break;
        case MISC_RdC :     fprintf (Log, "Rd<-C   ");   break;
        case MISC_R31C :    fprintf (Log, "R31<-C  ");   break;
        default :           fprintf (Log, "        ");
    }

    Cond = (dlx.Microcode [Index] & 31);

    switch (Cond)
    {
        case COND_Uncond :    fprintf (Log, "Uncond    ");   break;
        case COND_Mem :       fprintf (Log, "Mem?      ");   break;
        case COND_Int :       fprintf (Log, "Int?      ");   break;
        case COND_Zero :      fprintf (Log, "Zero?     ");   break;
        case COND_Neg :       fprintf (Log, "Neg?      ");   break;
        case COND_Load :      fprintf (Log, "Load?     ");   break;
        case COND_Decode1 :   fprintf (Log, "Decode1   ");   break;
        case COND_Decode2 :   fprintf (Log, "Decode2   ");   break;
        case COND_Decode3 :   fprintf (Log, "Decode3   ");   break;
        case COND_DestIAR :   fprintf (Log, "DestIAR?  ");   break;
        case COND_DestSR :    fprintf (Log, "DestSR?   ");   break;
        case COND_DestFPSR :  fprintf (Log, "DestFPSR? ");   break;
        case COND_SrcIAR :    fprintf (Log, "SrcIAR?   ");   break;
        case COND_SrcSR :     fprintf (Log, "SrcSR?    ");   break;
        case COND_SrcFPSR :   fprintf (Log, "SrcFPSR?  ");   break;
        default :             fprintf (Log, "          ");
    }

    if (Cond && (Cond < COND_Decode1 || Cond > COND_Decode3))
        fprintf (Log, " Jump %-3d", dlx.MicroJump [Index]);
    else
        fprintf (Log, "         ");

    if (ShowCycles)
        fprintf (Log, " (%ld)", dlx.Cycles);

    fprintf (Log, "\n");
}

VOID DisMicrocode ()
{
    ULONG   i;


    for (i = 0; i < MICROSIZE && dlx.Microcode [i]; i++)
    {
        ShowMicrocode (i, FALSE);

        if ((i + 1) % 20 == 0)
            if (PauseForKey (1) == 'q')
                break;
    }
}

VOID DoReg (UBYTE Reg, UBYTE RegT)
{
    if (Reg == NA || RegT == NA)
        fprintf (Log, "N/A  ");
    else
    {
        switch (RegT)
        {
            case INT   : fprintf (Log, "Int"); break;
            case INTFP : fprintf (Log, "IFp"); break;
            case FPS   : fprintf (Log, "SFp"); break;
            case FPD   : fprintf (Log, "DFp"); break;
            case SPEC  : fprintf (Log, "Spe"); break;
        }

        fprintf (Log, "%-2d", Reg);
    }
}

VOID ShowPipeStages ()
{
    WORD    i, St = IF, End = WB;


    if (DebugLevel & DB_EXONLY)
    {
        St = EX;
        End = EX;
        fprintf (Log, "%6ld %6ld ", dlx.Instr, dlx.Cycles);
    }
    else
    {
        fprintf (Log, "(Instructions Started=%ld, Executed=%ld",
            dlx.IStart, dlx.Instr);
        fprintf (Log, ", Complete=%ld. Cycles=%ld)\n", dlx.IDone, dlx.Cycles);
        fprintf (Log, "----------------------------------");
        fprintf (Log, "--------------------------------\n");
    }

    for (i = St; i <= End; i++)
    {
        switch (i)
        {
            case IF  : fprintf (Log, " IF "); break;
            case ID  : fprintf (Log, " ID "); break;
            case EX  : fprintf (Log, " EX "); break;
            case MEM : fprintf (Log, " MEM"); break;
            case WB  : fprintf (Log, " WB "); break;
        }

        if (dlx.Pipe [i].Status == FLUSH)
            fprintf (Log, " Flushed\n");
        else
        {
            fprintf (Log, " %08lX : %08lX  ",
                dlx.Pipe [i].PC, dlx.Pipe [i].IR);
            DisassembleInstr (dlx.Pipe [i].PC, dlx.Pipe [i].IR);

            if (dlx.Pipe [i].Status == STALL)
                fprintf (Log, "      Stalled\n");
        }

        if (DebugLevel & DB_PIPEREGS)
        {
            if (i > IF && dlx.Pipe [i].Status != FLUSH)
            {
                fprintf (Log, "      Op=%02X  Func=%04X  Off16=%04X  Off26=%08lX\n",
                    dlx.Pipe [i].Op,
                    dlx.Pipe [i].Func,
                    dlx.Pipe [i].Offset16,
                    dlx.Pipe [i].Offset26);

                fprintf (Log, "      Regs        A ");
                DoReg (dlx.Pipe [i].RegA, dlx.Pipe [i].TRegA);
                fprintf (Log, "     B ");
                DoReg (dlx.Pipe [i].RegB, dlx.Pipe [i].TRegB);
                fprintf (Log, "     C ");
                DoReg (dlx.Pipe [i].RegC, dlx.Pipe [i].TRegC);

                fprintf (Log, "\n      Int Values  A %08lX  B %08lX  C %08lX\n",
                    dlx.Pipe [i].IntA,
                    dlx.Pipe [i].IntB,
                    dlx.Pipe [i].IntC);

                if (dlx.Pipe [i].DoNewPC)
                    fprintf (Log, "      New PC = %08lX\n",
                        dlx.Pipe [i].NewPC);
            }
        }

        if (!((DebugLevel & DB_EXONLY) && !(DebugLevel & DB_PIPEREGS)))
        {
            fprintf (Log, "----------------------------------");
            fprintf (Log, "--------------------------------\n");
        }
    }
}

VOID CallDebug (ULONG Val1, ULONG Val2)
{
    ULONG   IR;


    switch (Val1)
    {
        case 0 :
            BreakpointTest ();
            break;

        case 1 : /* Disassemble current instruction - Val2 is PC */
            if (DebugLevel & DB_DISASSEM)
            {
                IR = FetchL (Val2);
                fprintf (Log, "%6ld  %08lX : %08lX  ", dlx.Instr, Val2, IR);
                DisassembleInstr (Val2, IR);
            }

            break;

        case 2 : /* Show Microcode at index Val2 */
            if (DebugLevel & DB_MICROCODE)
                ShowMicrocode (Val2, TRUE);

            break;

        case 4 : /* Show Pipeline stages */
            if (DebugLevel & DB_PIPESTAGES)
                ShowPipeStages ();

            /* Drop through */

        case 3 : /* Show other DLX regs */
            if (DebugLevel & DB_REGS)
            {
                fprintf (Log, "PC   $%08lX    IST  $%08lX    ",
                    dlx.PC, dlx.IStatus);
                fprintf (Log, "FPST $%08lX    IAR  $%08lX\n",
                    dlx.FPStatus, dlx.IAR);
                fprintf (Log, "IR   $%08lX    MAR  $%08lX    ",
                    dlx.IR, dlx.MAR);
                fprintf (Log, "MDR  $%08lX    Temp $%08lX\n",
                    dlx.MDR, dlx.Temp);
                fprintf (Log, "A    $%08lX    B    $%08lX    C    $%08lX\n",
                    dlx.RegA, dlx.RegB, dlx.RegC);
                fprintf (Log, "AD   %9.3f    BD   %9.3f    CD   %9.3f\n",
                    dlx.RegAD, dlx.RegBD, dlx.RegCD);
            }
    }
}
