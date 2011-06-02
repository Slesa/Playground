/*

        DLX RISC Simulator - Hardwired CPU Source

        D. J. Viner
*/

#include "cpu.h"

/*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/
/* Store a value in an integer register unless Dest is 0                  */
/*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/

VOID SetDest (UBYTE Dest, ULONG Val)
{
    dlx.RegC = Val;

    if (Dest)
        dlx.R [Dest] = Val;
}

VOID PerformLoad (UBYTE Dest, UBYTE Src, WORD Imm)
{
    dlx.Loads++;

    switch ((dlx.IR & 0x1C000000) >> 26)
    {
        case 0 : /* 000 Byte signed */
            SetDest (Dest, (ULONG) FetchB (dlx.R [Src] + Imm));

            /* Copy sign bit - no check needed for R0 as it
               cannot contain 0x80 */

            if (dlx.R [Dest] & 0x80)
                dlx.R [Dest] |= 0xFFFFFF00;

            break;

        case 1 : /* 001 Byte unsigned */
            SetDest (Dest, (ULONG) FetchB (dlx.R [Src] + Imm));
            break;

        case 2 : /* 010 Halfword signed */
            SetDest (Dest, (ULONG) FetchW (dlx.R [Src] + Imm));

            /* Copy sign bit - no check needed for R0 as it
               cannot contain 0x8000 */

            if (dlx.R [Dest] & 0x8000)
                dlx.R [Dest] |= 0xFFFF0000;

            break;

        case 3 : /* 011 Halfword unsigned */
            SetDest (Dest, (ULONG) FetchW (dlx.R [Src] + Imm));
            break;

        case 4 : /* 100 Word */
            SetDest (Dest, FetchL (dlx.R [Src] + Imm));
            break;

        case 6 : /* 110 SP Floating point (use Int union for transfer) */
            dlx.FP.I [Dest] = FetchL (dlx.R [Src] + Imm);
            dlx.RegCD = dlx.FP.F [Dest];
            break;

        case 7 : /* 111 DP Floating point */
            memcpy (&dlx.FP.D [Dest / 2], Fetch8 (dlx.R [Src] + Imm), 8);
            dlx.RegCD = dlx.FP.D [Dest / 2];
            break;

        default :
            SetInterrupt (INT_UNDEF_OPCODE);
            dlx.Loads--;
            return;
    }

    IncClock (4);
}

VOID PerformStore (UBYTE Dest, UBYTE Src, WORD Imm)
{
    dlx.Stores++;

    switch ((dlx.IR & 0x1C000000) >> 26)
    {
        case 0 : /* 000 Byte */
            StoreB (dlx.R [Dest] + Imm, (UBYTE) dlx.R [Src]);
            break;

        case 2 : /* 010 Halfword */
            StoreW (dlx.R [Dest] + Imm, (UWORD) dlx.R [Src]);
            break;

        case 4 : /* 100 Word */
            StoreL (dlx.R [Dest] + Imm, dlx.R [Src]);
            break;

        case 6 : /* 110 SP Floating point (use int union for transfer) */
            StoreL (dlx.R [Dest] + Imm, dlx.FP.I [Src]);
            break;

        case 7 : /* 111 DP Floating point */
            Store8 (dlx.R [Dest] + Imm, (STRPTR) &dlx.FP.D [Src / 2]);
            break;

        default :
            SetInterrupt (INT_UNDEF_OPCODE);
            dlx.Stores--;
            return;
    }

    IncClock (4);
}

VOID PerformIMath (UBYTE Dest, UBYTE Src, WORD Imm)
{
    ULONG   Val;


    dlx.ALU++;

    switch ((dlx.IR & 0x3C000000) >> 26)
    {
        case 0 : /* 000 Add immediate signed */
            Val = (ULONG) ((LONG) dlx.R [Src] + (LONG) Imm);
            break;

        case 1 : /* 001 Add immediate unsigned */
            Val = dlx.R [Src] + (UWORD) Imm;
            break;

        case 4 : /* 100 Subtract immediate signed */
            Val = (ULONG) ((LONG) dlx.R [Src] - (LONG) Imm);
            break;

        case 5 : /* 101 Subtract immediate unsigned */
            Val = dlx.R [Src] - (UWORD) Imm;
            break;

        case 15 : /* 110 Load upper halfword immediate */
            Val = (((ULONG) Imm) << 16);
            break;

        default :
            SetInterrupt (INT_UNDEF_OPCODE);
            dlx.ALU--;
            return;
    }

    SetDest (Dest, Val);
    IncClock (3);
}

VOID PerformRMath (WORD Op, UBYTE Dest, UBYTE Src1, UBYTE Src2)
{
    DOUBLE  d1, d2;


    dlx.ALU++;

    switch (Op)
    {
        case 0x00 : /* ADD */
        case 0x04 : /* SUB */
        case 0x08 : /* MULT */
            d1 = (DOUBLE) ((LONG) dlx.R [Src1]);
            d2 = (DOUBLE) ((LONG) dlx.R [Src2]);
            break;

        case 0x01 : /* ADDU */
        case 0x09 : /* MULTU */
            d1 = (DOUBLE) dlx.R [Src1];
            d2 = (DOUBLE) dlx.R [Src2];
            break;
    }

    switch (Op)
    {
        case 0x00 : /* ADD */
            SetDest (Dest, (ULONG) ((LONG) dlx.R [Src1] + (LONG) dlx.R [Src2]));

            if (abs (d1 + d2) > (DOUBLE) 0x7FFFFFFF)
                SetInterrupt (INT_OVERFLOW);

            IncClock (3);
            break;

        case 0x01 : /* ADDU */
            SetDest (Dest, dlx.R [Src1] + dlx.R [Src2]);

            if ((d1 + d2) > (DOUBLE) 0xFFFFFFFF)
                SetInterrupt (INT_OVERFLOW);

            IncClock (3);
            break;

        case 0x02 : /* ADDF */
            dlx.RegCD = dlx.FP.F [Dest] = dlx.FP.F [Src1] + dlx.FP.F [Src2];
            IncClock (3);
            break;

        case 0x03 : /* ADDD */
            dlx.RegCD = dlx.FP.D [Dest / 2] = dlx.FP.D [Src1 / 2] + dlx.FP.D [Src2 / 2];
            IncClock (6);
            break;

        case 0x04 : /* SUB */
            SetDest (Dest, (ULONG) ((LONG) dlx.R [Src1] - (LONG) dlx.R [Src2]));

            if (abs (d1 - d2) > (DOUBLE) 0x7FFFFFFF)
                SetInterrupt (INT_OVERFLOW);

            IncClock (3);
            break;

        case 0x05 : /* SUBU */
            if (dlx.R [Src1] < dlx.R [Src2])
                SetInterrupt (INT_OVERFLOW);

            SetDest (Dest, dlx.R [Src1] - dlx.R [Src2]);

            IncClock (3);
            break;

        case 0x06 : /* SUBF */
            dlx.RegCD = dlx.FP.F [Dest] = dlx.FP.F [Src1] - dlx.FP.F [Src2];
            IncClock (3);
            break;

        case 0x07 : /* SUBD */
            dlx.RegCD = dlx.FP.D [Dest / 2] = dlx.FP.D [Src1 / 2] - dlx.FP.D [Src2 / 2];
            IncClock (6);
            break;

        case 0x08 : /* MULT */
            dlx.RegC = dlx.FP.I [Dest] =
                (ULONG) ((LONG) dlx.FP.I [Src1] + (LONG) dlx.FP.I [Src2]);

            d1 = (DOUBLE) dlx.FP.I [Src1];
            d2 = (DOUBLE) dlx.FP.I [Src2];

            if (abs (d1 * d2) > (DOUBLE) 0x7FFFFFFF)
                SetInterrupt (INT_OVERFLOW);

            IncClock (3);
            break;

        case 0x09 : /* MULTU */
            dlx.RegC = dlx.FP.I [Dest] = dlx.FP.I [Src1] * dlx.FP.I [Src2];
            d1 = (DOUBLE) dlx.FP.I [Src1];
            d2 = (DOUBLE) dlx.FP.I [Src2];

            if ((d1 * d2) > (DOUBLE) 0xFFFFFFFF)
                SetInterrupt (INT_OVERFLOW);

            IncClock (3);
            break;

        case 0x0A : /* MULTF */
            dlx.RegCD = dlx.FP.F [Dest] = dlx.FP.F [Src1] * dlx.FP.F [Src2];
            IncClock (6);
            break;

        case 0x0B : /* MULTD */
            dlx.RegCD = dlx.FP.D [Dest / 2] = dlx.FP.D [Src1 / 2] * dlx.FP.D [Src2 / 2];
            IncClock (6);
            break;

        case 0x0C : /* DIV */
            if (dlx.FP.I [Src2])
            {
                dlx.FP.I [Dest] = (ULONG) (((LONG) dlx.FP.I [Src1]) /
                    ((LONG) dlx.FP.I [Src2]));
                dlx.RegC = dlx.FP.I [Dest];
            }
            else
                SetInterrupt (INT_DIVIDEBYZERO);

            IncClock (3);
            break;

        case 0x0D : /* DIVU */
            if (dlx.FP.I [Src2])
            {
                dlx.FP.I [Dest] = dlx.FP.I [Src1] / dlx.FP.I [Src2];
                dlx.RegC = dlx.FP.I [Dest];
            }
            else
                SetInterrupt (INT_DIVIDEBYZERO);

            IncClock (3);
            break;

        case 0x0E : /* DIVF */
            if (dlx.FP.F [Src2])
            {
                dlx.FP.F [Dest] = dlx.FP.F [Src1] / dlx.FP.F [Src2];
                dlx.RegCD = dlx.FP.F [Dest];
            }
            else
                SetInterrupt (INT_DIVIDEBYZERO);

            IncClock (6);
            break;

        case 0x0F : /* DIVD */
            if (dlx.FP.D [Src2 / 2])
            {
                dlx.FP.D [Dest / 2] = dlx.FP.D [Src1 / 2] / dlx.FP.D [Src2 / 2];
                dlx.RegCD = dlx.FP.D [Dest / 2];
            }
            else
                SetInterrupt (INT_DIVIDEBYZERO);

            IncClock (6);
            break;

        default :
            SetInterrupt (INT_UNDEF_OPCODE);
            dlx.ALU--;
    }
}

/*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/

VOID PerformBitShift (WORD Op, UBYTE Dest, UBYTE Src, ULONG Val)
{
    ULONG   Tmp, i, v;


    dlx.ALU++;

    switch (Op)
    {
        case 0 : /* AND */
            SetDest (Dest, dlx.R [Src] & Val);
            break;

        case 2 : /* OR */
            SetDest (Dest, dlx.R [Src] | Val);
            break;

        case 3 : /* XOR */
            SetDest (Dest, dlx.R [Src] ^ Val);
            break;

        case 4 : /* Shift Left Logical */
            if (Val < 33)
                SetDest (Dest, dlx.R [Src] << Val);
            else
                SetDest (Dest, 0);

            break;

        case 6 : /* Shift Right Logical */
            if (Val < 33)
                SetDest (Dest, dlx.R [Src] >> Val);
            else
                SetDest (Dest, 0);

            break;

        case 7 : /* Shift Right Arithmetic */
            if (Val < 33)
            {
                Tmp = dlx.R [Src] & 0x80000000;
                v = (dlx.R [Src] >> Val);

                if (Tmp)
                {
                    for (i = 0; i < Val; i++)
                    {
                        v |= Tmp;
                        Tmp = Tmp >> 1;
                    }
                }

                SetDest (Dest, v);
            }

            break;

        default :
            SetInterrupt (INT_UNDEF_OPCODE);
            dlx.ALU--;
            return;
    }

    IncClock (3);
}

/*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/

VOID PerformSet (WORD Op, UBYTE Dest, UBYTE Src, LONG Val)
{
    LONG    Cmp = (LONG) dlx.R [Src];


    dlx.Set++;

    switch (Op)
    {
        case 0 : /* Less than */
            SetDest (Dest, (Cmp < Val));
            break;

        case 1 : /* Greater than */
            SetDest (Dest, (Cmp > Val));
            break;

        case 2 : /* Less than or equal */
            SetDest (Dest, (Cmp <= Val));
            break;

        case 3 : /* Greater than or equal */
            SetDest (Dest, (Cmp >= Val));
            break;

        case 4 : /* Equal */
            SetDest (Dest, (Cmp == Val));
            break;

        case 5 : /* Not Equal */
            SetDest (Dest, (Cmp != Val));
            break;

        default :
            SetInterrupt (INT_UNDEF_OPCODE);
            dlx.Set--;
            return;
    }

    IncClock (4);
}

/*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/

VOID PerformMove (WORD Op, UBYTE Dest, UBYTE Src)
{
    dlx.Move++;

    switch (Op)
    {
        case 0 : /* MOVS2I */
            switch (Src)
            {
                case 0 :
                    SetDest (Dest, dlx.IAR);
                    break;

                case 1 :
                    SetDest (Dest, dlx.IStatus);
                    break;

                case 2 :
                    SetDest (Dest, dlx.FPStatus);
            }

            IncClock (2);
            break;

        case 1 : /* MOVI2S */
            switch (Dest)
            {
                case 0 :
                    dlx.IAR = dlx.R [Src];
                    break;

                case 1 :
                    dlx.IStatus = dlx.R [Src];
                    break;

                case 2 :
                    dlx.FPStatus = dlx.R [Src];
            }

            IncClock (1);
            break;

        case 2 : /* MOVFP2I */
            SetDest (Dest, dlx.FP.I [Src]);
            IncClock (2);
            break;

        case 3 : /* MOVI2FP */
            dlx.RegC = dlx.FP.I [Dest] = dlx.R [Src];
            IncClock (2);
            break;

        case 4 : /* MOVF */
            dlx.RegCD = dlx.FP.F [Dest] = dlx.FP.F [Src];
            IncClock (2);
            break;

        case 5 : /* MOVD */
            dlx.RegCD = dlx.FP.D [Dest / 2] = dlx.FP.D [Src / 2];
            IncClock (4);
            break;

        default :
            SetInterrupt (INT_UNDEF_OPCODE);
            dlx.Move--;
    }
}

/*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/

VOID PerformBranchJump (WORD Src, WORD Offset)
{
    switch ((dlx.IR & 0x1C000000) >> 26)
    {
        case 2 : /* JR */
            dlx.PC = dlx.R [Src];
            dlx.Jumps++;
            IncClock (1);
            break;

        case 3 : /* JALR */
            dlx.RegC = dlx.R [31] = dlx.PC;
            dlx.PC = dlx.R [Src];
            dlx.JALs++;
            IncClock (3);
            break;

        case 4 : /* BEQZ */
            if (!dlx.R [Src])
            {
                dlx.PC += Offset;
                dlx.BranchTaken++;
                IncClock (2);
            }
            else
            {
                dlx.BranchNotTaken++;
                IncClock (1);
            }

            break;

        case 5 : /* BNEZ */
            if (dlx.R [Src])
            {
                dlx.PC += Offset;
                dlx.BranchTaken++;
                IncClock (2);
            }
            else
            {
                dlx.BranchNotTaken++;
                IncClock (1);
            }

            break;

        case 6 : /* BFPF */
            if (!(dlx.FPStatus & FST_COMPARE))
            {
                dlx.PC += Offset;
                dlx.BranchTaken++;
                IncClock (2);
            }
            else
            {
                dlx.BranchNotTaken++;
                IncClock (1);
            }

            break;

        case 7 : /* BFPT */
            if (dlx.FPStatus & FST_COMPARE)
            {
                dlx.PC += Offset;
                dlx.BranchTaken++;
                IncClock (2);
            }
            else
            {
                dlx.BranchNotTaken++;
                IncClock (1);
            }

            break;

        default :
            SetInterrupt (INT_UNDEF_OPCODE);
    }
}

/*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/

VOID PerformConvert (WORD Op, UBYTE Dest, UBYTE Src)
{
    dlx.Convert++;

    switch (Op)
    {
        case 0 : /* CVTF2D */
            dlx.FP.D [Dest / 2] = (DOUBLE) (dlx.FP.F [Src]);
            IncClock (4);
            break;

        case 1 : /* CVTF2I */
            dlx.FP.I [Dest] = (ULONG) (dlx.FP.F [Src]);
            IncClock (2);
            break;

        case 2 : /* CVTD2F */
            dlx.FP.F [Dest] = (FLOAT) (dlx.FP.D [Src / 2]);
            IncClock (4);
            break;

        case 3 : /* CVTD2I */
            dlx.FP.I [Dest] = (ULONG) (dlx.FP.D [Src / 2]);
            IncClock (4);
            break;

        case 4 : /* CVTI2F */
            dlx.FP.F [Dest] = (FLOAT) (dlx.FP.I [Src]);
            IncClock (2);
            break;

        case 5 : /* CVTI2D */
            dlx.FP.D [Dest / 2] = (DOUBLE) (dlx.FP.I [Src]);
            IncClock (4);
            break;

        default :
            SetInterrupt (INT_UNDEF_OPCODE);
            dlx.Convert--;
    }
}

/*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/

VOID PerformSetFP (WORD Op, UBYTE Src1, UBYTE Src2)
{
    DOUBLE  F1, F2;


    dlx.Set++;

    if (Op > 7)
    {
        F1 = dlx.FP.D [Src1 / 2];
        F2 = dlx.FP.D [Src2 / 2];
    }
    else
    {
        F1 = (DOUBLE) dlx.FP.F [Src1];
        F2 = (DOUBLE) dlx.FP.F [Src2];
    }

    dlx.FPStatus &= (~FST_COMPARE); /* Reset compare bit */

    switch (Op & 7)
    {
        case 0 : /* Less than */
            if (F1 < F2)
                dlx.FPStatus |= FST_COMPARE;

            break;

        case 1 : /* Greater than */
            if (F1 > F2)
                dlx.FPStatus |= FST_COMPARE;

            break;

        case 2 : /* Less than or equal */
            if (F1 <= F2)
                dlx.FPStatus |= FST_COMPARE;

            break;

        case 3 : /* Greater than or equal */
            if (F1 >= F2)
                dlx.FPStatus |= FST_COMPARE;

            break;

        case 4 : /* Equal */
            if (F1 == F2)
                dlx.FPStatus |= FST_COMPARE;

            break;

        case 5 : /* Not Equal */
            if (F1 != F2)
                dlx.FPStatus |= FST_COMPARE;

            break;

        default :
            SetInterrupt (INT_UNDEF_OPCODE);
            dlx.Set--;
            return;
    }

    IncClock (4);
}

/*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/

VOID ExecuteJType ()
{
    LONG    Offset26;


    DecodeJType (dlx.IR, &Offset26);

    switch ((dlx.IR & 0x0C000000) >> 26)
    {
        case 0 : /* TRAP */
            dlx.TrapRfe++;
            IncClock (2);

            if ((dlx.Type & TRAPSON) && Offset26 < 64)
            switch (Offset26)
            {
                case 0 : /* TRAP 0 = Halt/Break */
                    break;

                case 4 :
                    OutputChr (dlx.R [1] & 0xFF);
                    break;

                case 8 :
                    OutputDecVal (dlx.R [1]);
                    break;

                case 12 :
                    OutputHexVal (dlx.R [1]);
                        break;

                case 16 :
                    dlx.R [1] = InputChr ();
            }
            else
            {
                dlx.IAR = dlx.PC;

                if (dlx.Type & VECTORTRAPS)
                    dlx.PC = FetchL (Offset26);
                else
                    dlx.PC = Offset26;
            }

            break;

        case 1 : /* RFE */
            dlx.TrapRfe++;

            /* Restore previous machine state */

            dlx.PC = dlx.IAR;
            dlx.IAR = 0;

            IncClock (1);
            break;

        case 2 : /* J */
            dlx.PC += Offset26;
            dlx.Jumps++;
            IncClock (1);
            break;

        case 3 : /* JAL */
            dlx.R [31] = dlx.PC;
            dlx.PC += Offset26;
            dlx.JALs++;
            IncClock (3);
    }
}

/*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/

VOID ExecuteInstruction ()
{
    UBYTE   Dest, Src1, Src2;
    WORD    Offset16, Func;


    if ((dlx.IR >> 28) == 0) /* J Type */
        ExecuteJType ();
    else
    if ((dlx.IR >> 26) == 0x3F) /* R Type */
    {
        DecodeRType (dlx.IR, &Dest, &Src1, &Src2, &Func);
        dlx.RegA = dlx.R [Src1];
        dlx.RegB = dlx.R [Src2];

        switch (Func >> 3)
        {
            case 0 : /* Move */
                PerformMove (Func & 7, Dest, Src1);
                break;

            case 1 : /* Convert */
                PerformConvert (Func & 7, Dest, Src1);
                break;

            case 2 : case 3 : /* Set FP */
                PerformSetFP (Func & 0xF, Src1, Src2);
                break;

            case 4 : case 5 : /* Math */
                PerformRMath (Func & 0xF, Dest, Src1, Src2);
                break;

            case 6 : /* Bit/Shift */
                PerformBitShift (Func & 7, Dest, Src1, dlx.R [Src2]);
                break;

            case 7 : /* Set Int */
                PerformSet (Func & 7, Dest, Src1, (LONG) dlx.R [Src2]);
        }
    }
    else /* I Type */
    {
        DecodeIType (dlx.IR, &Dest, &Src1, &Offset16);
        dlx.RegA = dlx.R [Src1];

        switch (dlx.IR >> 29)
        {
            case 1 : /* Branch/Jump */
                PerformBranchJump (Src1, Offset16);
                break;

            case 2 : /* Loads */
                PerformLoad (Dest, Src1, Offset16);
                break;

            case 3 : /* Stores (note swapped params) */
                PerformStore (Src1, Dest, Offset16);
                break;

            case 4 : case 5 : /* Math */
                PerformIMath (Dest, Src1, Offset16);
                break;

            case 6 : /* Bit and Shift */
                PerformBitShift ((dlx.IR & 0x1C000000) >> 26,
                    Dest, Src1, (ULONG) Offset16);

                break;

            case 7 : /* Set */
                PerformSet ((dlx.IR & 0x1C000000) >> 26,
                    Dest, Src1, (LONG) Offset16);
                break;

            default :
                SetInterrupt (INT_UNDEF_OPCODE);
        }
    }
}

/*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/
/* Fetch instruction, increment PC, increment count of instructions       */
/* processed so far. Also increase clock/cycle counts.                    */
/*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/

VOID FetchInstruction ()
{
    dlx.IR = FetchL (dlx.PC);
    dlx.PC += 4;
    dlx.Instr++;
    dlx.Cycles = 0;
    IncClock (3);   /* Memory access = 2 + 1 for PC inc */
}

/*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/

BOOL RunOneHardwiredInstr (BOOL Debug)
{
    ULONG   PC;

    CallDebug (0, 0);

    /* Check for int flag set and not already processing an interrupt */

    if (dlx.IStatus && !dlx.IAR)
    {
        dlx.IAR = dlx.PC;
        dlx.PC = 0;
    }

    if (Debug)
        PC = dlx.PC;    /* Copy PC for debugging purposes */

    FetchInstruction ();

    if (Debug)
        CallDebug (1, PC);

    if (dlx.IR)   /* If not TRAP 0 (Halt) */
    {
        ExecuteInstruction ();
        return TRUE;        /* Ok, no TRAP 0 */
    }

    /* Correct for final TRAP 0 (unless its a breakpoint) */

    if (!BP.Copy)
        dlx.TrapRfe++;

    return FALSE;           /* Indicate TRAP 0 */
}

/*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/



