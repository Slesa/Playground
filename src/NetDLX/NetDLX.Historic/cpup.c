/*

        DLX RISC Simulator - Pipelined CPU Source

        D. J. Viner
*/

#include "cpu.h"

/* Dummy defines to make the 'parallel' code clearer. If ALL the platforms
   had similar methods of multitasking the pipeline units could be made
   into separate processes (multitasking on MS-DOS - ha! ha!).
*/

#define PARBEGIN    {
#define PAREND      }

VOID PPerformLoad (UBYTE p)
{
    FLOAT   Fp;
    ULONG   L;


    if (p == EX)
        dlx.Loads++;

    switch (dlx.Pipe [p].Op & 7)
    {
        case 0 : /* 000 Byte signed */
            if (p == MEM)
            {
                dlx.Pipe [p].IntC = (ULONG) FetchB (dlx.Pipe [p].IntA +
                    (UWORD) dlx.Pipe [p].Offset16);

                /* Copy sign bit */

                if (dlx.Pipe [p].IntC & 0x80)
                    dlx.Pipe [p].IntC |= 0xFFFFFF00;
            }

            break;

        case 1 : /* 001 Byte unsigned */
            if (p == MEM)
                dlx.Pipe [p].IntC = (ULONG) FetchB (dlx.Pipe [p].IntA +
                    (UWORD) dlx.Pipe [p].Offset16);

            break;

        case 2 : /* 010 Halfword signed */
            if (p == MEM)
            {
                dlx.Pipe [p].IntC = (ULONG) FetchW (dlx.Pipe [p].IntA +
                    (UWORD) dlx.Pipe [p].Offset16);

                /* Copy sign bit */

                if (dlx.Pipe [p].IntC & 0x80)
                    dlx.Pipe [p].IntC |= 0xFFFFFF00;
            }

            break;

        case 3 : /* 011 Halfword unsigned */
            if (p == MEM)
                dlx.Pipe [p].IntC = (ULONG) FetchW (dlx.Pipe [p].IntA +
                    (UWORD) dlx.Pipe [p].Offset16);

            break;

        case 4 : /* 100 Word */
            if (p == MEM)
                dlx.Pipe [p].IntC = (ULONG) FetchL (dlx.Pipe [p].IntA +
                    (UWORD) dlx.Pipe [p].Offset16);

            break;

        case 6 : /* 110 SP Floating point */
            if (p == MEM)
            {
                L = FetchL (dlx.Pipe [p].IntA +
                    (UWORD) dlx.Pipe [p].Offset16);

                memcpy (&Fp, &L, 4);
                dlx.Pipe [p].FpC = Fp;
            }

            break;

        case 7 : /* 111 DP Floating point */
            if (p == MEM)
                memcpy (&dlx.Pipe [p].FpC, Fetch8 (dlx.Pipe [p].IntA +
                    (UWORD) dlx.Pipe [p].Offset16), 8);

            break;

        default :
            if (p == EX)
            {
                SetInterrupt (INT_UNDEF_OPCODE);
                dlx.Loads--;
            }
    }
}

VOID PPerformStore (UBYTE p)
{
    FLOAT   Fp;
    ULONG   Ul;


    if (p == EX)
        dlx.Stores++;

    switch (dlx.Pipe [p].Op & 7)
    {
        case 0 : /* 000 Byte */
            if (p == MEM)
                StoreB (dlx.Pipe [p].IntA + (UWORD) dlx.Pipe [p].Offset16,
                    (UBYTE) dlx.Pipe [p].IntB);

            break;

        case 2 : /* 010 Halfword */
            if (p == MEM)
                StoreW (dlx.Pipe [p].IntA + (UWORD) dlx.Pipe [p].Offset16,
                    (UWORD) dlx.Pipe [p].IntB);

            break;

        case 4 : /* 100 Word */
            if (p == MEM)
                StoreL (dlx.Pipe [p].IntA + (UWORD) dlx.Pipe [p].Offset16,
                    dlx.Pipe [p].IntB);

            break;

        case 6 : /* 110 SP Floating point */
            if (p == MEM)
            {
                Fp = dlx.Pipe [p].FpB;
                memcpy (&Ul, &Fp, 4);

                StoreL (dlx.Pipe [p].IntA +
                    (UWORD) dlx.Pipe [p].Offset16, Ul);
            }

            break;

        case 7 : /* 111 DP Floating point */
            if (p == MEM)
                Store8 (dlx.Pipe [p].IntA + (UWORD) dlx.Pipe [p].Offset16,
                    (STRPTR) &dlx.Pipe [p].FpB);

            break;

        default :
            if (p == EX)
            {
                SetInterrupt (INT_UNDEF_OPCODE);
                dlx.Stores--;
            }
    }
}

VOID PPerformIMath (UBYTE p)
{
    if (p == EX)
    {
        dlx.ALU++;

        switch (dlx.Pipe [p].Op & 15)
        {
            case 0 : /* 000 Add immediate signed */
                dlx.Pipe [p].IntC = (ULONG) ((LONG)
                    dlx.Pipe [p].IntA + dlx.Pipe [p].Offset16);

                break;

            case 1 : /* 001 Add immediate unsigned */
                dlx.Pipe [p].IntC = (ULONG) ((LONG)
                    dlx.Pipe [p].IntA + (UWORD) dlx.Pipe [p].Offset16);

                break;

            case 4 : /* 100 Subtract immediate signed */
                dlx.Pipe [p].IntC = (ULONG) ((LONG)
                    dlx.Pipe [p].IntA - dlx.Pipe [p].Offset16);

                break;

            case 5 : /* 101 Subtract immediate unsigned */
                dlx.Pipe [p].IntC = (ULONG) ((LONG)
                    dlx.Pipe [p].IntA - (UWORD) dlx.Pipe [p].Offset16);

                break;

            case 15 : /* 110 Load upper halfword immediate */
                dlx.Pipe [p].IntC = (((ULONG) dlx.Pipe [p].Offset16) << 16);
                break;

            default :
                SetInterrupt (INT_UNDEF_OPCODE);
                dlx.ALU--;
        }
    }
}

VOID PPerformRMath (UBYTE p)
{
    DOUBLE  d1, d2;


    dlx.ALU++;

    switch (dlx.Pipe [p].Func & 0xF)
    {
        case 0x00 : /* ADD */
        case 0x04 : /* SUB */
        case 0x08 : /* MULT */
            d1 = (DOUBLE) ((LONG) dlx.Pipe [p].IntA);
            d2 = (DOUBLE) ((LONG) dlx.Pipe [p].IntB);
            break;

        case 0x01 : /* ADDU */
        case 0x09 : /* MULTU */
            d1 = (DOUBLE) dlx.Pipe [p].IntA;
            d2 = (DOUBLE) dlx.Pipe [p].IntB;
            break;
    }

    switch (dlx.Pipe [p].Func & 0xF)
    {
        case 0x00 : /* ADD */
            dlx.Pipe [p].IntC = (ULONG) ((LONG) dlx.Pipe [p].IntA +
                (LONG) dlx.Pipe [p].IntB);

            if (abs (d1 + d2) > (DOUBLE) 0x7FFFFFFF)
                SetInterrupt (INT_OVERFLOW);

            break;

        case 0x01 : /* ADDU */
            dlx.Pipe [p].IntC = dlx.Pipe [p].IntA + dlx.Pipe [p].IntB;

            if ((d1 + d2) > (DOUBLE) 0xFFFFFFFF)
                SetInterrupt (INT_OVERFLOW);

            break;

        case 0x02 : /* ADDF */
        case 0x03 : /* ADDD */
            dlx.Pipe [p].FpC = dlx.Pipe [p].FpA + dlx.Pipe [p].FpB;
            break;

        case 0x04 : /* SUB */
            dlx.Pipe [p].IntC = (ULONG) ((LONG) dlx.Pipe [p].IntA -
                (LONG) dlx.Pipe [p].IntB);

            if (abs (d1 - d2) > (DOUBLE) 0x7FFFFFFF)
                SetInterrupt (INT_OVERFLOW);

            break;

        case 0x05 : /* SUBU */
            if (dlx.Pipe [p].IntA < dlx.Pipe [p].IntB)
                SetInterrupt (INT_OVERFLOW);

            dlx.Pipe [p].IntC = dlx.Pipe [p].IntA - dlx.Pipe [p].IntB;
            break;

        case 0x06 : /* SUBF */
        case 0x07 : /* SUBD */
            dlx.Pipe [p].FpC = dlx.Pipe [p].FpA - dlx.Pipe [p].FpB;
            break;

        case 0x08 : /* MULT */
            dlx.Pipe [p].IntC = (ULONG) ((LONG) dlx.Pipe [p].IntA *
                (LONG) dlx.Pipe [p].IntB);

            if (abs (d1 * d2) > (DOUBLE) 0x7FFFFFFF)
                SetInterrupt (INT_OVERFLOW);

            break;

        case 0x09 : /* MULTU */
            dlx.Pipe [p].IntC = dlx.Pipe [p].IntA * dlx.Pipe [p].IntB;

            if ((d1 * d2) > (DOUBLE) 0xFFFFFFFF)
                SetInterrupt (INT_OVERFLOW);

            break;

        case 0x0A : /* MULTF */
        case 0x0B : /* MULTD */
            dlx.Pipe [p].FpC = dlx.Pipe [p].FpA * dlx.Pipe [p].FpB;
            break;

        case 0x0C : /* DIV */
            if (dlx.Pipe [p].IntB)
                dlx.Pipe [p].IntC = (ULONG) (((LONG) dlx.Pipe [p].IntA) /
                    (LONG) dlx.Pipe [p].IntB);
            else
                SetInterrupt (INT_DIVIDEBYZERO);

            break;

        case 0x0D : /* DIVU */
            if (dlx.Pipe [p].IntB)
                dlx.Pipe [p].IntC =
                    dlx.Pipe [p].IntA / dlx.Pipe [p].IntB;
            else
                SetInterrupt (INT_DIVIDEBYZERO);

            break;

        case 0x0E : /* DIVF */
        case 0x0F : /* DIVD */
            if (dlx.Pipe [p].FpB)
                dlx.Pipe [p].FpC =
                    dlx.Pipe [p].FpA / dlx.Pipe [p].FpB;
            else
                SetInterrupt (INT_DIVIDEBYZERO);

            break;

        default :
            SetInterrupt (INT_UNDEF_OPCODE);
            dlx.ALU--;
    }
}

VOID PPerformBitShift (UBYTE p, WORD Op, ULONG Val)
{
    ULONG   Tmp, i, v;


    dlx.ALU++;

    switch (Op)
    {
        case 0 : /* AND */
            dlx.Pipe [p].IntC = (dlx.Pipe [p].IntA & Val);
            break;

        case 2 : /* OR */
            dlx.Pipe [p].IntC = (dlx.Pipe [p].IntA | Val);
            break;

        case 3 : /* XOR */
            dlx.Pipe [p].IntC = (dlx.Pipe [p].IntA ^ Val);
            break;

        case 4 : /* Shift Left Logical */
            if (Val < 33)
                dlx.Pipe [p].IntC = (dlx.Pipe [p].IntA << Val);
            else
                dlx.Pipe [p].IntC = 0;

            break;

        case 6 : /* Shift Right Logical */
            if (Val < 33)
                dlx.Pipe [p].IntC = (dlx.Pipe [p].IntA >> Val);
            else
                dlx.Pipe [p].IntC = 0;

            break;

        case 7 : /* Shift Right Arithmetic */
            if (Val < 33)
            {
                Tmp = dlx.Pipe [p].IntA & 0x80000000;
                v = (dlx.Pipe [p].IntA >> Val);

                if (Tmp)
                {
                    for (i = 0; i < Val; i++)
                    {
                        v |= Tmp;
                        Tmp = Tmp >> 1;
                    }
                }

                dlx.Pipe [p].IntC = v;
            }

            break;

        default :
            SetInterrupt (INT_UNDEF_OPCODE);
            dlx.ALU--;
    }
}

VOID PPerformSet (UBYTE p, WORD Op, LONG Val)
{
    LONG    Cmp = (LONG) dlx.Pipe [p].IntA;


    dlx.Set++;

    switch (Op)
    {
        case 0 : /* Less than */
            dlx.Pipe [p].IntC = (Cmp < Val);
            break;

        case 1 : /* Greater than */
            dlx.Pipe [p].IntC = (Cmp > Val);
            break;

        case 2 : /* Less than or equal */
            dlx.Pipe [p].IntC = (Cmp <= Val);
            break;

        case 3 : /* Greater than or equal */
            dlx.Pipe [p].IntC = (Cmp >= Val);
            break;

        case 4 : /* Equal */
            dlx.Pipe [p].IntC = (Cmp == Val);
            break;

        case 5 : /* Not Equal */
            dlx.Pipe [p].IntC = (Cmp != Val);
            break;

        default :
            SetInterrupt (INT_UNDEF_OPCODE);
            dlx.Set--;
    }
}

VOID PPerformMove (UBYTE p)
{
    dlx.Move++;

    switch (dlx.Pipe [p].Func & 7)
    {
        case 0 : /* MOVS2I */
            switch (dlx.Pipe [p].RegA)
            {
                case 0 :
                    dlx.Pipe [p].IntC = dlx.IAR;
                    break;

                case 1 :
                    dlx.Pipe [p].IntC = dlx.IStatus;
                    break;

                case 2 :
                    dlx.Pipe [p].IntC = dlx.FPStatus;
            }

            break;

        case 1 : /* MOVI2S */
            switch (dlx.Pipe [p].RegC)
            {
                case 0 :
                    dlx.IAR = dlx.Pipe [p].IntA;
                    break;

                case 1 :
                    dlx.IStatus = dlx.Pipe [p].IntA;
                    break;

                case 2 :
                    dlx.FPStatus = dlx.Pipe [p].IntA;
            }

            break;

        case 2 : /* MOVFP2I */
        case 3 : /* MOVI2FP */
            dlx.Pipe [p].IntC = dlx.Pipe [p].IntA;
            break;

        case 4 : /* MOVF */
        case 5 : /* MOVD */
            dlx.Pipe [p].FpC = dlx.Pipe [p].FpA;
            break;

        default :
            SetInterrupt (INT_UNDEF_OPCODE);
            dlx.Move--;
    }
}

VOID PPerformBranchJump (UBYTE p)
{
    UBYTE   DoEX = EX, DoWB = WB;


    if (BRANCHTYPE >= BR_EARLY)
    {
        DoEX = ID;
        DoWB = ID;
    }

    switch (dlx.Pipe [p].Op & 7)
    {
        case 2 : /* JR */
            if (p == DoEX)
            {
                dlx.Pipe [p].NewPC = dlx.Pipe [p].IntA;
                dlx.Pipe [p].DoNewPC = TRUE;
                dlx.Jumps++;
            }

            break;

        case 3 : /* JALR */
            if (p == DoEX)
            {
                dlx.Pipe [p].NewPC = dlx.Pipe [p].IntA;
                dlx.Pipe [p].DoNewPC = TRUE;
                dlx.JALs++;
            }

            if (p == DoWB)
                dlx.R [31] = dlx.Pipe [p].PC + 4;

            break;

        case 4 : /* BEQZ */
            if (p == DoEX)
            {
                if (!dlx.Pipe [p].IntA)
                {
                    dlx.BranchTaken++;
                    dlx.Pipe [p].DoNewPC = TRUE;
                    dlx.Pipe [p].NewPC = dlx.Pipe [p].PC +
                        dlx.Pipe [p].Offset16 + 4;
                }
                else
                    dlx.BranchNotTaken++;
            }

            break;

        case 5 : /* BNEZ */
            if (p == DoEX)
            {
                if (dlx.Pipe [p].IntA)
                {
                    dlx.BranchTaken++;
                    dlx.Pipe [p].DoNewPC = TRUE;
                    dlx.Pipe [p].NewPC = dlx.Pipe [p].PC +
                        dlx.Pipe [p].Offset16 + 4;
                }
                else
                    dlx.BranchNotTaken++;
            }

            break;

        case 6 : /* BFPF */
            if (p == DoEX)
            {
                if (!(dlx.FPStatus & FST_COMPARE))
                {
                    dlx.BranchTaken++;
                    dlx.Pipe [p].DoNewPC = TRUE;
                    dlx.Pipe [p].NewPC = dlx.Pipe [p].PC +
                        dlx.Pipe [p].Offset16 + 4;
                }
                else
                    dlx.BranchNotTaken++;
            }

            break;

        case 7 : /* BFPT */
            if (p == DoEX)
            {
                if (dlx.FPStatus & FST_COMPARE)
                {
                    dlx.BranchTaken++;
                    dlx.Pipe [p].DoNewPC = TRUE;
                    dlx.Pipe [p].NewPC = dlx.Pipe [p].PC +
                        dlx.Pipe [p].Offset16 + 4;
                }
                else
                    dlx.BranchNotTaken++;
            }

            break;

        default :
            if (p == DoEX)
                SetInterrupt (INT_UNDEF_OPCODE);
    }
}

VOID PPerformConvert (UBYTE p)
{
    dlx.Convert++;

    switch (dlx.Pipe [p].Func & 7)
    {
        case 0 : /* CVTF2D */
        case 2 : /* CVTD2F */
            dlx.Pipe [p].FpC = dlx.Pipe [p].FpA;
            break;

        case 1 : /* CVTF2I */
        case 3 : /* CVTD2I */
            dlx.Pipe [p].IntC = (ULONG) (dlx.Pipe [p].FpA);
            break;

        case 4 : /* CVTI2F */
        case 5 : /* CVTI2D */
            dlx.Pipe [p].FpC = (DOUBLE) (dlx.Pipe [p].IntA);
            break;

        default :
            SetInterrupt (INT_UNDEF_OPCODE);
            dlx.Convert--;
    }
}

VOID PPerformSetFP (UBYTE p)
{
    DOUBLE  F1, F2;


    dlx.Set++;

    F1 = dlx.Pipe [p].FpA;
    F2 = dlx.Pipe [p].FpB;

    dlx.FPStatus &= (~FST_COMPARE); /* Reset compare bit */

    switch (dlx.Pipe [p].Func & 7)
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
    }
}

VOID PExecuteIType (UBYTE p)
{
    switch (dlx.Pipe [p].Op >> 3)
    {
        case 1 : /* Branch/Jump */
            if (BRANCHTYPE < BR_EARLY)   /* Done directly from ID stage */
                PPerformBranchJump (p);  /* for BR_EARLY/DELAY */

            break;

        case 2 : /* Loads */
            PPerformLoad (p);
            break;

        case 3 : /* Stores */
            PPerformStore (p);
            break;

        case 4 : case 5 : /* Math */
            PPerformIMath (p);
            break;

        case 6 : /* Bit and Shift */
            if (p == EX)
                PPerformBitShift (p, dlx.Pipe [p].Op & 7,
                    dlx.Pipe [p].Offset16);

            break;

        case 7 : /* Set */
            if (p == EX)
                PPerformSet (p, dlx.Pipe [p].Op & 7,
                    dlx.Pipe [p].Offset16);

            break;

        default :
            if (p == EX)
                SetInterrupt (INT_UNDEF_OPCODE);
    }
}

VOID PExecuteRType (UBYTE p)
{
    switch (dlx.Pipe [p].Func >> 3)
    {
        case 0 : /* Move */
            if (p == EX)
                PPerformMove (p);

            break;

        case 1 : /* Convert */
            if (p == EX)
                PPerformConvert (p);

            break;

        case 2 : case 3 : /* Set FP */
            if (p == EX)
                PPerformSetFP (p);

            break;

        case 4 : case 5 : /* Math */
            if (p == EX)
                PPerformRMath (p);

            break;

        case 6 : /* Bit/Shift */
            if (p == EX)
                PPerformBitShift (p, dlx.Pipe [p].Func & 7,
                    dlx.Pipe [p].IntB);

            break;

        case 7 : /* Set Int */
            if (p == EX)
                PPerformSet (p, dlx.Pipe [p].Func & 7,
                    dlx.Pipe [p].IntB);
    }
}

VOID PExecuteJType (UBYTE p)
{
    UBYTE   DoEX = EX, DoWB = WB;


    if (BRANCHTYPE >= BR_EARLY)
    {
        DoEX = ID;
        DoWB = ID;
    }

    switch (dlx.Pipe [p].Op)
    {
        case 0 : /* TRAP */
            if (p == EX)
                dlx.TrapRfe++;

            if ((dlx.Type & TRAPSON) && dlx.Pipe [p].Offset26 < 64)
            switch (dlx.Pipe [p].Offset26)
            {
                case 0 : /* TRAP 0 = Halt/Break */
                    break;

                case 4 :
                    if (p == WB)
                        OutputChr (dlx.R [1] & 0xFF);

                    break;

                case 8 :
                    if (p == WB)
                        OutputDecVal (dlx.R [1]);

                    break;

                case 12 :
                    if (p == WB)
                        OutputHexVal (dlx.R [1]);

                    break;

                case 16 :
                    if (p == WB)
                        dlx.R [1] = InputChr ();
            }
            else
            {
                if (p == EX)
                {
                    dlx.IAR = dlx.Pipe [p].PC + 4;

                    if (dlx.Type & VECTORTRAPS)
                        dlx.Pipe [p].NewPC = FetchL (dlx.Pipe [p].Offset26);
                    else
                        dlx.Pipe [p].NewPC = dlx.Pipe [p].Offset26;

                    dlx.Pipe [p].DoNewPC = TRUE;
                }
            }

            break;

        case 1 : /* RFE */
            if (p == DoEX)
            {
                dlx.TrapRfe++;

                /* Restore previous machine state */

                dlx.Pipe [p].NewPC = dlx.IAR;
                dlx.Pipe [p].DoNewPC = TRUE;
                dlx.IAR = 0;
            }

            break;

        case 2 : /* J */
            if (p == DoEX)
            {
                dlx.Jumps++;
                dlx.Pipe [p].DoNewPC = TRUE;
                dlx.Pipe [p].NewPC = dlx.Pipe [p].PC +
                    dlx.Pipe [p].Offset26 + 4;
            }

            break;

        case 3 : /* JAL */
            if (p == DoEX)
            {
                dlx.JALs++;
                dlx.Pipe [p].DoNewPC = TRUE;
                dlx.Pipe [p].NewPC = dlx.Pipe [p].PC +
                    dlx.Pipe [p].Offset26 + 4;
            }

            if (p == DoWB)
                dlx.R [31] = dlx.Pipe [p].PC + 4;
    }
}

/*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/
/* Returns TRUE is op is a branch or jump instruction                     */
/*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/

BOOL IsBranchJump (UBYTE Op)
{
    switch (Op)
    {
        case 0x02 : case 0x03 : case 0x0A : case 0x0B :
        case 0x0C : case 0x0D : case 0x0E : case 0x0F :
            return TRUE;
    }

    return FALSE;
}

VOID DoIFStage ()
{
    ULONG   Int = dlx.IStatus & 0xFFFF;


    /* Interrupts - a thorny problem! Currently only one interrupt
       can be handled at a time. This is done by only handling
       interrupts if the IAR register is zero. An interrupt should
       only be generated by the EX stage so the ID stage must be
       flushed to stop instructions after the one that generated
       the interrupt from executing (other than the interrupt
       handling routine, of course). The IAR is set to the address
       of the instruction following the one that caused the int.
       Setting PC to 0 automatically loads in the jump address that
       is (should be) stored at location zero. There appears to be
       a problem here as I've had programs manage to clear the
       interrupt flag and still leave the IAR with a value. This
       prevents subsequent ints from being handled. */

    if (Int && !dlx.IAR)
    {
        dlx.IAR = dlx.Pipe [EX].PC + 4;
        dlx.PC = 0;
        dlx.Trapped = FALSE;
        dlx.Pipe [ID].Status = FLUSH;
    }

    if (!dlx.Trapped)
    {
        if (dlx.Pipe [IF].Status != STALL)
        {
            dlx.Pipe [IF].Status = RUN;
            dlx.Pipe [IF].DoNewPC = FALSE;
            dlx.Pipe [IF].IR = FetchL (dlx.PC);

            dlx.IStart++;
            dlx.Pipe [IF].PC = dlx.PC;
            dlx.PC += 4;
            dlx.Pipe [IF].RegA = NA;
            dlx.Pipe [IF].RegB = NA;
            dlx.Pipe [IF].RegC = NA;
        }
    }
    else
        dlx.Pipe [IF].Status = FLUSH;
}

BOOL DoIDStage ()
{
    BOOL    Stall = FALSE, Fwd = FALSE;
    UBYTE   i;


    /* Do Decode only if status is RUN */

    if (dlx.Pipe [ID].Status == RUN)
    {
        dlx.Pipe [ID].Op = ((dlx.Pipe [ID].IR >> 26) & 0x3F);

        /* Decode the regs */

        if (dlx.Pipe [ID].Op < 4)           /* J Type */
            DecodeJType (dlx.Pipe [ID].IR,
                &dlx.Pipe [ID].Offset26);
        else
        if (dlx.Pipe [ID].Op == 0x3F)       /* R Type */
            DecodeRType (dlx.Pipe [ID].IR,
                &dlx.Pipe [ID].RegC,
                &dlx.Pipe [ID].RegA,
                &dlx.Pipe [ID].RegB,
                &dlx.Pipe [ID].Func);
        else                                /* I Type */
            DecodeIType (dlx.Pipe [ID].IR,
                &dlx.Pipe [ID].RegC,
                &dlx.Pipe [ID].RegA,
                &dlx.Pipe [ID].Offset16);

        /* Find out what types of registers we are dealing with */

        SetRegisterTypes (dlx.Pipe [ID].IR, &dlx.Pipe [ID].TRegA,
            &dlx.Pipe [ID].TRegB, &dlx.Pipe [ID].TRegC);

        /* If store instruction then move Dest to Src2 */

        if (dlx.Pipe [ID].Op >> 3 == 3)
        {
            dlx.Pipe [ID].RegB = dlx.Pipe [ID].RegC;
            dlx.Pipe [ID].TRegB = dlx.Pipe [ID].TRegC;
            dlx.Pipe [ID].RegC = NA;
            dlx.Pipe [ID].TRegC = NA;
        }
    }

    /* Allocate values if RUNning or STALLED */

    if (dlx.Pipe [ID].Status != FLUSH)
    {
        /* Allocate values */

        switch (dlx.Pipe [ID].TRegA)
        {
            case INT :
                dlx.Pipe [ID].IntA = dlx.R [dlx.Pipe [ID].RegA];
                break;

            case INTFP :
                dlx.Pipe [ID].IntA = dlx.FP.I [dlx.Pipe [ID].RegA];
                break;

            case FPS :
                dlx.Pipe [ID].FpA = dlx.FP.F [dlx.Pipe [ID].RegA];
                break;

            case FPD :
                dlx.Pipe [ID].FpA = dlx.FP.D [dlx.Pipe [ID].RegA / 2];
        }

        switch (dlx.Pipe [ID].TRegB)
        {
            case INT :
                dlx.Pipe [ID].IntB = dlx.R [dlx.Pipe [ID].RegB];
                break;

            case INTFP :
                dlx.Pipe [ID].IntB = dlx.FP.I [dlx.Pipe [ID].RegB];
                break;

            case FPS :
                dlx.Pipe [ID].FpB = dlx.FP.F [dlx.Pipe [ID].RegB];
                break;

            case FPD :
                dlx.Pipe [ID].FpB = dlx.FP.D [dlx.Pipe [ID].RegB / 2];
        }

        /* If FW_STALL or FW_FEED modes then check for
           data hazard and stall or feed forward if found */

        if (FEEDTYPE == FW_STALL || FEEDTYPE == FW_FEED)
        {
            if (FEEDTYPE == FW_FEED)
                Fwd = TRUE;

            /* Loop to check MEM and EX stage (EX is most recent) */

            for (i = MEM; i > ID; i--)
            {
                /* See if RegA needs checking */

                if (dlx.Pipe [ID].RegA != NA)
                {
                    /* Check that source stage is RUNning and that the
                       reg types and numbers correspond */

                    if (dlx.Pipe [i].Status == RUN &&
                        dlx.Pipe [ID].TRegA == dlx.Pipe [i].TRegC &&
                        dlx.Pipe [ID].RegA  == dlx.Pipe [i].RegC)
                    {
                        if (Fwd)
                        {
                            switch (dlx.Pipe [ID].TRegA)
                            {
                                case INT :
                                case INTFP :
                                    dlx.Pipe [ID].IntA = dlx.Pipe [i].IntC;
                                    break;

                                case FPS :
                                case FPD :
                                    dlx.Pipe [ID].FpA = dlx.Pipe [i].FpC;
                            }
                        }
                        else
                            Stall = TRUE;
                    }

                    /* Check for JAL(R) which should hazard on R31 */

                    if (dlx.Pipe [i].Status == RUN &&
                        dlx.Pipe [ID].TRegA == INT &&
                        dlx.Pipe [ID].RegA  == 31 &&
                        (dlx.Pipe [i].Op == 3 || dlx.Pipe [i].Op == 0xB))
                            Stall = TRUE;
                }

                /* See if RegB needs checking */

                if (dlx.Pipe [ID].RegB != NA)
                {
                    if (dlx.Pipe [i].Status == RUN &&
                        dlx.Pipe [ID].TRegB == dlx.Pipe [i].TRegC &&
                        dlx.Pipe [ID].RegB  == dlx.Pipe [i].RegC)
                    {
                        if (Fwd)
                        {
                            switch (dlx.Pipe [ID].TRegB)
                            {
                                case INT :
                                case INTFP :
                                    dlx.Pipe [ID].IntB = dlx.Pipe [i].IntC;
                                    break;

                                case FPS :
                                case FPD :
                                    dlx.Pipe [ID].FpB = dlx.Pipe [i].FpC;
                            }
                        }
                        else
                            Stall = TRUE;
                    }

                    /* Check for JAL(R) which should hazard on R31 */

                    if (dlx.Pipe [i].Status == RUN &&
                        dlx.Pipe [ID].TRegB == INT &&
                        dlx.Pipe [ID].RegB  == 31 &&
                        (dlx.Pipe [i].Op == 3 || dlx.Pipe [i].Op == 0xB))
                            Stall = TRUE;
                }
            }
        }

        /* If LD_HAZARD mode then check for load hazard
           and stall if found */

        if (LOADTYPE == LD_HAZARD && (dlx.Pipe [EX].Op >> 3) == 2)
        {
            /* See if RegA needs checking */

            if (dlx.Pipe [ID].RegA != NA)
            {
                /* Check that source stage is RUNning and that the reg
                   types and numbers correspond */

                if (dlx.Pipe [EX].Status == RUN &&
                    dlx.Pipe [ID].TRegA == dlx.Pipe [EX].TRegC &&
                    dlx.Pipe [ID].RegA  == dlx.Pipe [EX].RegC)
                        Stall = TRUE;
            }

            /* See if RegB needs checking */

            if (dlx.Pipe [ID].RegB != NA)
            {
                if (dlx.Pipe [EX].Status == RUN &&
                    dlx.Pipe [ID].TRegB == dlx.Pipe [EX].TRegC &&
                    dlx.Pipe [ID].RegB  == dlx.Pipe [EX].RegC)
                        Stall = TRUE;
            }
        }

        /* Now do early branch handling if turned on */

        if (!Stall && IsBranchJump (dlx.Pipe [ID].Op) &&
            BRANCHTYPE >= BR_EARLY)
        {
            if (dlx.Pipe [ID].Op < 4) /* For J and JAL */
                PExecuteJType (ID);
            else
                PPerformBranchJump (ID);
        }

        /* Check for RFE and early branch is on */

        if (!Stall && dlx.Pipe [ID].Op == 1)
            PExecuteJType (ID);
    }

    return Stall;
}

VOID DoEXStage ()
{
    if (dlx.Pipe [EX].Status == RUN)
    {
        if (dlx.Pipe [EX].IR == 0) /* Check for TRAP 0 */
            dlx.Trapped = TRUE;

        if (dlx.Pipe [EX].Op < 4) /* J Type */
            PExecuteJType (EX);
        else
        if (dlx.Pipe [EX].Op == 0x3F) /* R Type */
            PExecuteRType (EX);
        else /* I Type */
            PExecuteIType (EX);

        dlx.Instr++;
    }
}

VOID DoMEMStage ()
{
    if (dlx.Pipe [MEM].Status == RUN)
    {
        if (dlx.Pipe [MEM].Op < 4) /* J Type */
            PExecuteJType (MEM);
        else
        if (dlx.Pipe [MEM].Op == 0x3F) /* R Type */
            PExecuteRType (MEM);
        else /* I Type */
            PExecuteIType (MEM);
    }
}

VOID DoWBStage ()
{
    if (dlx.Pipe [WB].Status == RUN)
    {
        dlx.IDone++;

        if (dlx.Pipe [WB].Op < 4) /* J Type */
            PExecuteJType (WB);
        else
        if (dlx.Pipe [MEM].Op == 0x3F) /* R Type */
            PExecuteRType (WB);
        else /* I Type */
            PExecuteIType (WB);

        switch (dlx.Pipe [WB].TRegC)
        {
            case INT :
                if (dlx.Pipe [WB].RegC) /* Check for R0 */
                    dlx.R [dlx.Pipe [WB].RegC] = dlx.Pipe [WB].IntC;

                break;

            case INTFP :
                dlx.FP.I [dlx.Pipe [WB].RegC] = dlx.Pipe [WB].IntC;
                break;

            case FPS :
                dlx.FP.F [dlx.Pipe [WB].RegC] = dlx.Pipe [WB].FpC;
                break;

            case FPD :
                dlx.FP.D [dlx.Pipe [WB].RegC / 2] = dlx.Pipe [WB].FpC;
        }
    }
}

BOOL RunOnePipelinedInstr (BOOL Debug)
{
    BOOL    Continue = TRUE, Stall = FALSE;
    WORD    i, j;
    UBYTE   Stage = MEM;


    /* Turn off Trapped if all stages flushed */

    for (i = IF, j = 0; i <= WB; i++)
        if (dlx.Pipe [i].Status == FLUSH)
            if (++j == 5)
                dlx.Trapped = FALSE;

    /* Indicate finished run if TRAP 0 found at WB stage */

    if (dlx.Pipe [WB].Status == RUN && dlx.Pipe [WB].IR == 0 && dlx.Trapped)
    {
        Continue = FALSE;
        dlx.IDone++;
    }
    else
    {
        PARBEGIN
            DoWBStage ();     /* Placing WB before ID gives the same */
            DoMEMStage ();    /* effect as having a multi-phase clock */
            DoEXStage ();     /* (see JRWG DLX05 slide 10) */
            Stall = DoIDStage ();
            DoIFStage ();
        PAREND

        /* Check if Jump or Branch taken - normally done during MEM stage
           (unless BR_EARLY is enabled in which case it is the ID stage)
           but ONLY after all other stages have completed their stuff
           which is why it is here instead of inside DoMEM/IDStage) */

        if (BRANCHTYPE >= BR_EARLY)
        {
            Stage = ID;

            if (dlx.Pipe [Stage].Status == STALL &&
                dlx.Pipe [Stage].DoNewPC && !Stall)
                    dlx.Pipe [Stage].Status = RUN;
        }

        if (dlx.Pipe [Stage].Status == RUN && dlx.Pipe [Stage].DoNewPC)
        {
            dlx.PC = dlx.Pipe [Stage].NewPC;

            /* Flush current IF, ID and EX stages if turned on */

            if (BRANCHTYPE == BR_STALL)
            {
                dlx.Pipe [EX].Status = FLUSH;
                dlx.Pipe [ID].Status = FLUSH;
                dlx.Pipe [IF].Status = FLUSH;
                Stall = FALSE;  /* In case we stalled */
            }
            else
            if (BRANCHTYPE == BR_EARLY) /* Not if BR_DELAY slot */
            {
                dlx.Pipe [IF].Status = FLUSH;
                Stall = FALSE;  /* In case we stalled */
            }
        }
    }

    IncClock (1);

    if (Debug)
        CallDebug (4, 0);

    PARBEGIN
        /* Copy the pipeline structs down a stage unless stalled */

        memcpy (&dlx.Pipe [WB],  &dlx.Pipe [MEM], sizeof (DLXPipe));
        memcpy (&dlx.Pipe [MEM], &dlx.Pipe [EX],  sizeof (DLXPipe));

        if (Stall)
        {
            dlx.Pipe [IF].Status = STALL;
            dlx.Pipe [ID].Status = STALL;
            dlx.Pipe [EX].Status = FLUSH;
        }
        else
        {
            if (dlx.Pipe [IF].Status == STALL)
                dlx.Pipe [IF].Status = RUN;

            if (dlx.Pipe [ID].Status == STALL)
                dlx.Pipe [ID].Status = RUN;

            memcpy (&dlx.Pipe [EX], &dlx.Pipe [ID], sizeof (DLXPipe));
            memcpy (&dlx.Pipe [ID], &dlx.Pipe [IF], sizeof (DLXPipe));
        }

    PAREND

    return Continue;
}

