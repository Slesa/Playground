/*

        DLX RISC Simulator - Microcode CPU Source

        D. J. Viner
*/

#include    "cpu.h"
#include    "micro.h"

VOID DoAluOp (UWORD Dest, UWORD AluOp, UWORD Src1, UWORD Src2, UWORD Const,
              UBYTE S1Type, UBYTE S2Type, BOOL Unsigned)
{
    ULONG   IResult = 0, ISrc1 = 0, ISrc2 = 0, Tmp, i;
    DOUBLE  DResult = 0, DSrc1 = 0, DSrc2 = 0;
    FLOAT   FResult = 0, FSrc1 = 0, FSrc2 = 0;
    UBYTE   AType = INT;


    switch (Src1)
    {
        case SRC_A :     ISrc1 = dlx.RegA;              break;
        case SRC_AD :    DSrc1 = dlx.RegAD;             break;
        case SRC_Temp :  ISrc1 = dlx.Temp;              break;
        case SRC_PC :    ISrc1 = dlx.PC;                break;
        case SRC_IAR :   ISrc1 = dlx.IAR;               break;
        case SRC_MAR :   ISrc1 = dlx.MAR;               break;
        case SRC_Const : ISrc1 = Const;                 break;
        case SRC_SR :    ISrc1 = dlx.IStatus;           break;
        case SRC_FPSR :  ISrc1 = dlx.FPStatus;          break;

        case SRC_MDR :
            switch (S1Type)
            {
                default :
                    ISrc1 = dlx.MDR;
                    break;

                case FPD :
                    DSrc1 = dlx.MDR8;
            }

            break;

        case SRC_imm16 :
            ISrc1 = dlx.IR & 0xFFFF;

            if (!Unsigned)
                if (ISrc1 & 0x00008000)
                    ISrc1 |= 0xFFFF0000;

            break;

        case SRC_imm26 :
            ISrc1 = dlx.IR & 0x03FFFFFF;

            if (!Unsigned)
                if (ISrc1 & 0x02000000)
                    ISrc1 |= 0xFC000000;
    }

    switch (Src2)
    {
        case SRC_B :        ISrc2 = dlx.RegB;           break;
        case SRC_BD :       DSrc2 = dlx.RegBD;          break;
        case SRC_Temp :     ISrc2 = dlx.Temp;           break;
        case SRC_PC :       ISrc2 = dlx.PC;             break;
        case SRC_IAR :      ISrc2 = dlx.IAR;            break;
        case SRC_MAR :      ISrc2 = dlx.MAR;            break;
        case SRC_Const :    ISrc2 = Const;              break;
        case SRC_SR :       ISrc2 = dlx.IStatus;        break;
        case SRC_FPSR :     ISrc2 = dlx.FPStatus;       break;

        case SRC_MDR :
            switch (S2Type)
            {
                default :
                    ISrc2 = dlx.MDR;
                    break;

                case FPD :
                    DSrc2 = dlx.MDR8;
            }

            break;

        case SRC_imm16 :
            ISrc2 = dlx.IR & 0xFFFF;

            if (!Unsigned)
                if (ISrc2 & 0x00008000)
                    ISrc2 |= 0xFFFF0000;

            break;

        case SRC_imm26 :
            ISrc2 = dlx.IR & 0x03FFFFFF;

            if (!Unsigned)
                if (ISrc2 & 0x02000000)
                    ISrc2 |= 0xFC000000;
    }

    memcpy (&FSrc1, &ISrc1, 4);
    memcpy (&FSrc2, &ISrc2, 4);

    switch (AluOp)
    {
        case ALU_ADD :
            IResult = ISrc1 + ISrc2;
            break;

        case ALU_SUB :
            IResult = ISrc1 - ISrc2;
            break;

        case ALU_RSUB :
            IResult = ISrc2 - ISrc1;
            break;

        case ALU_AND :
            IResult = ISrc1 & ISrc2;
            break;

        case ALU_OR :
            IResult = ISrc1 | ISrc2;
            break;

        case ALU_XOR :
            IResult = ISrc1 ^ ISrc2;
            break;

        case ALU_SLL :
            if (ISrc2 < 33)
                IResult = ISrc1 << ISrc2;

            break;

        case ALU_SRL :
            if (ISrc2 < 33)
                IResult = ISrc1 >> ISrc2;

            break;

        case ALU_SRA :
            if (ISrc2 < 33)
            {
                Tmp = ISrc1 & 0x80000000;
                IResult = (ISrc1 >> ISrc2);

                if (Tmp)
                {
                    for (i = 0; i < ISrc2; i++)
                    {
                        IResult |= Tmp;
                        Tmp = Tmp >> 1;
                    }
                }
            }

            break;

        case ALU_PassS1 :
            IResult = ISrc1;
            DResult = DSrc1;
            break;

        case ALU_PassS2 :
            IResult = ISrc2;
            DResult = DSrc2;
            break;

        case ALU_MULTI :
            IResult = ISrc1 * ISrc2;
            break;

        case ALU_DIVI :
            if (ISrc2)
                IResult = ISrc1 / ISrc2;
            else
                SetInterrupt (INT_DIVIDEBYZERO);

            break;

        case ALU_MULTF :
            FResult = FSrc1 * FSrc2;
            AType = FPS;
            break;

        case ALU_DIVF :
            if (FSrc2)
                FResult = FSrc1 / FSrc2;
            else
                SetInterrupt (INT_DIVIDEBYZERO);

            AType = FPS;
            break;

        case ALU_ADDF :
            FResult = FSrc1 + FSrc2;
            AType = FPS;
            break;

        case ALU_SUBF :
            FResult = FSrc1 - FSrc2;
            AType = FPS;
            break;

        case ALU_RSUBF :
            FResult = FSrc2 - FSrc1;
            AType = FPS;
            break;

        case ALU_MULTD :
            DResult = DSrc1 * DSrc2;
            break;

        case ALU_DIVD :
            if (DSrc2)
                DResult = DSrc1 / DSrc2;
            else
                SetInterrupt (INT_DIVIDEBYZERO);

            break;

        case ALU_ADDD :
            DResult = DSrc1 + DSrc2;
            break;

        case ALU_SUBD :
            DResult = DSrc1 - DSrc2;
            break;

        case ALU_RSUBD :
            DResult = DSrc2 - DSrc1;
            break;

        case ALU_CVTF2D :
            DResult = (DOUBLE) FSrc1;
            break;

        case ALU_CVTF2I :
            IResult = (ULONG) FSrc1;
            break;

        case ALU_CVTD2F :
            FResult = (FLOAT) DSrc1;
            AType = FPS;
            break;

        case ALU_CVTD2I :
            IResult = (ULONG) DSrc1;
            break;

        case ALU_CVTI2F :
            FResult = (FLOAT) ISrc1;
            AType = FPS;
            break;

        case ALU_CVTI2D :
            DResult = (DOUBLE) ISrc1;
    }

    /* NOTE: only Integer stuff sets these flags */

    dlx.Zero = (IResult == 0);
    dlx.Negative = (((LONG) IResult) < 0);

    if (AType == FPS)
        memcpy (&IResult, &FResult, 4);

    switch (Dest)
    {
        case DEST_C :       dlx.RegC     = IResult;     break;
        case DEST_CD :      dlx.RegCD    = DResult;     break;
        case DEST_Temp :    dlx.Temp     = IResult;     break;
        case DEST_PC :      dlx.PC       = IResult;     break;
        case DEST_IAR :     dlx.IAR      = IResult;     break;
        case DEST_MAR :     dlx.MAR      = IResult;     break;
        case DEST_MDR :     dlx.MDR      = IResult;     break;
        case DEST_SR :      dlx.IStatus  = IResult;     break;
        case DEST_FPSR :    dlx.FPStatus = IResult;     break;
    }
}

VOID IncInsType (UBYTE Type)
{
    switch (Type)
    {
        case T_Load :
            dlx.Loads++;
            break;

        case T_Store :
            dlx.Stores++;
            break;

        case T_ALU :
            dlx.ALU++;
            break;

        case T_Set :
            dlx.Set++;
            break;

        case T_Jump :
            dlx.Jumps++;
            break;

        case T_JAL :
            dlx.JALs++;
            break;

        case T_Move :
            dlx.Move++;
            break;

        case T_Convert :
            dlx.Convert++;
            break;

        case T_Branch :
            dlx.BranchTaken++;
            break;

        case T_Other :
            dlx.TrapRfe++;
    }
}

VOID SpecialTraps (LONG imm26)
{
    dlx.PC += 4;    /* Have to cheat here! */

    /* Correct for final TRAP 0 (unless its a breakpoint) */

    if (!BP.Copy)
        dlx.TrapRfe++;

    IncClock (4);   /* Make them use up some time */

    switch (imm26)
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
}

BOOL RunOneMicrocodeInstr (BOOL Debug)
{
    UBYTE   MDest, MSrc1, MSrc2;
    WORD    MFunc, Sz, i, MemDelay = 0;
    WORD    Index = 0;                          /* Into microcode tables */
    UBYTE   Dest, AluOp, Src1, Src2, Const, Misc, Cond;
    BOOL    RunState = dlx.Running, Unsigned = FALSE,
            Continue = TRUE;                    /* FALSE if TRAP 0 */
    LONG    imm26;
    UBYTE   Ptr, S1Type = 0, S2Type = 0, DType = 0, Op, OpF;


    dlx.Running = TRUE;

    CallDebug (0, 0);   /* Break point test */

    dlx.Cycles = 0;
    dlx.Instr++;

    if (Debug)
        CallDebug (1, dlx.PC);

    do {
        IncClock (1);

        if (!MemDelay)
        {
            /* Unpack the microcode word into its component parts */

            Dest  = (dlx.Microcode [Index] >> 28) & 0x0F;
            AluOp = (dlx.Microcode [Index] >> 22) & 0x3F;
            Src1  = (dlx.Microcode [Index] >> 18) & 0x0F;
            Src2  = (dlx.Microcode [Index] >> 14) & 0x0F;
            Const = (dlx.Microcode [Index] >> 9)  & 0x1F;
            Misc  = (dlx.Microcode [Index] >> 5)  & 0x0F;
            Cond  =  dlx.Microcode [Index]        & 0x1F;

            /* First do the ALU operation */

            DoAluOp (Dest, AluOp, Src1, Src2, Const,
                     S1Type, S2Type, Unsigned);
        }

        /* Next do any Misc operation */

        if (!MemDelay)
        switch (Misc)
        {
            case MISC_InstrRd : /* Read and decode macro instruction */
                dlx.IR = FetchL (dlx.PC);
                SetRegisterTypes (dlx.IR, &S1Type, &S2Type, &DType);
                Op = (dlx.IR & 0xFC000000) >> 26;
                OpF = (dlx.IR & 0x3F);
                Sz = 4;
                Unsigned = FALSE;

                if ((Op & 0x3C) == 0) /* J Type */
                {
                    DecodeJType (dlx.IR, &imm26);

                    /* Check for special TRAPs (if enabled) */

                    if ((dlx.Type & TRAPSON) &&
                        (dlx.IR & 0xFC000000) == 0 &&
                         imm26 >= 0 && imm26 < 64)
                    {
                        Index = -1; /* Force next increment to 0 */
                        SpecialTraps (imm26);
                    }
                }
                else
                if (Op == 0x3F) /* R Type */
                {
                    DecodeRType (dlx.IR, &MDest, &MSrc1, &MSrc2, &MFunc);
                    Unsigned = (DecodeT [OpF].FlagsLen > 0x7F);
                }
                else
                {   /* I Type */
                    DecodeIType (dlx.IR, &MDest, &MSrc1, &MFunc);
                    Sz = DecodeT [Op].FlagsLen & 0x7F;
                    Unsigned = (DecodeT [Op].FlagsLen > 0x7F);

                    /* Stores require Src2 to be Dest details */

                    MSrc2 = MDest;
                    S2Type = DType;
                }

                Continue = (dlx.IR != 0); /* Check for TRAP 0 */
                MemDelay = 2;
                break;

            case MISC_DataRd :
                MemDelay = 2;

                switch (Sz)
                {
                    case 1 :
                        dlx.MDR = FetchB (dlx.MAR);
                        break;

                    case 2 :
                        dlx.MDR = FetchW (dlx.MAR);
                        break;

                    case 8 : /* 8-byte mem access takes 4 clock cycles */
                        memcpy (&dlx.MDR8, Fetch8 (dlx.MAR), 8);
                        MemDelay = 4;
                        break;

                    default :
                        dlx.MDR = FetchL (dlx.MAR);
                }

                break;

            case MISC_Write :
                MemDelay = 2;

                switch (Sz)
                {
                    case 1 :
                        StoreB (dlx.MAR, dlx.MDR);
                        break;

                    case 2 :
                        StoreW (dlx.MAR, dlx.MDR);
                        break;

                    case 8 :
                        Store8 (dlx.MAR, (STRPTR) &dlx.MDR8);
                        MemDelay = 4;
                        break;

                    default :
                        StoreL (dlx.MAR, dlx.MDR);
                }

                break;

            case MISC_ABRF :
                switch (S1Type)
                {
                    case INT :
                        dlx.RegA = dlx.R [MSrc1];
                        break;

                    case INTFP :
                        dlx.RegA = dlx.FP.I [MSrc1];
                        break;

                    case FPS :
                        memcpy (&dlx.RegA, &dlx.FP.F [MSrc1], 4);
                        break;

                    case FPD :
                        dlx.RegAD = dlx.FP.D [MSrc1 / 2];
                }

                switch (S2Type)
                {
                    case INT :
                        dlx.RegB = dlx.R [MSrc2];
                        break;

                    case INTFP :
                        dlx.RegB = dlx.FP.I [MSrc2];
                        break;

                    case FPS :
                        memcpy (&dlx.RegB, &dlx.FP.F [MSrc2], 4);
                        break;

                    case FPD :
                        dlx.RegBD = dlx.FP.D [MSrc2 / 2];
                }

                break;

            case MISC_RdC :
                switch (DType)
                {
                    case INT :
                        if (MDest)  /* Don't store in R0 */
                            dlx.R [MDest] = dlx.RegC;

                        break;

                    case INTFP :
                        dlx.FP.I [MDest] = dlx.RegC;
                        break;

                    case FPS :
                        memcpy (&dlx.FP.F [MDest], &dlx.RegC, 4);
                        break;

                    case FPD :
                        dlx.FP.D [MDest / 2] = dlx.RegCD;
                }

                break;

            case MISC_R31C :
                dlx.R [31] = dlx.RegC;
        }

        if (Debug && Index > -1)
            CallDebug (2, Index);

        if (MemDelay)
            MemDelay--;

        /* Do condition and jump */

        if (!MemDelay)
        switch (Cond)
        {
            case COND_NextInstr :
            case COND_Mem : /* Extra mem cycles done in Misc */
                Index++;
                break;

            case COND_Uncond :
                Index = dlx.MicroJump [Index];
                break;

            case COND_Int :
                if (dlx.IStatus && !dlx.IAR)
                    Index = dlx.MicroJump [Index];
                else
                    Index++;

                break;

            case COND_Zero :
                if (dlx.Zero)
                    Index = dlx.MicroJump [Index];
                else
                    Index++;

                break;

            case COND_Neg :
                if (dlx.Negative)
                    Index = dlx.MicroJump [Index];
                else
                    Index++;

                break;

            case COND_Load :
                if ((dlx.IR & 0xE0000000) == 0x40000000)
                    Index = dlx.MicroJump [Index];
                else
                    Index++;

                break;

            case COND_Decode1 :
                if (Op == 0x3F)
                    i = OpF + 64;
                else
                    i = Op;

                Ptr = DecodeT [i].Dec1;

                if (Ptr != ERROR)
                {
                    IncInsType (DecodeT [i].Type);
                    Index = dlx.Decode1 [Ptr];
                }
                else
                    SetInterrupt (INT_UNDEF_OPCODE);

                break;

            case COND_Decode2 :
            case COND_Decode3 :
                if (Op == 0x3F)
                    i = OpF + 64;
                else
                    i = Op;

                Ptr = DecodeT [i].Dec2;

                if (Ptr != ERROR)
                    Index = dlx.Decode2 [Ptr];
                else
                    SetInterrupt (INT_UNDEF_OPCODE);

                break;

            case COND_DestIAR :
                if (MDest == 0)
                    Index = dlx.MicroJump [Index];
                else
                    Index++;

                break;

            case COND_DestSR :
                if (MDest == 1)
                    Index = dlx.MicroJump [Index];
                else
                    Index++;

                break;

            case COND_DestFPSR :
                if (MDest == 2)
                    Index = dlx.MicroJump [Index];
                else
                    Index++;

                break;

            case COND_SrcIAR :
                if (MSrc1 == 0)
                    Index = dlx.MicroJump [Index];
                else
                    Index++;

                break;

            case COND_SrcSR :
                if (MSrc1 == 1)
                    Index = dlx.MicroJump [Index];
                else
                    Index++;

                break;

            case COND_SrcFPSR :
                if (MSrc1 == 2)
                    Index = dlx.MicroJump [Index];
                else
                    Index++;

                break;
        }

        if (Debug && !MemDelay && Index > -1)
            CallDebug (3, 0);

        CheckCtrlC ();

        if (!dlx.Running)
            Index = 0;

    } while (Index || MemDelay);

    /* Drop out when Index indicates that a jump has been made to
    ** location 0 (i.e. a complete instruction). This is also forced
    ** by the 'captured' special TRAPs.
    */

    if (!dlx.Running)
        Continue = FALSE;

    dlx.Running = RunState;

    return Continue;
}
