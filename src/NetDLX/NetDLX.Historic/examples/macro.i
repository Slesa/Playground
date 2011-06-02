//////////////////////////////////////////////////////////////////////////////
//	DLX Macros - D. J. Viner
//////////////////////////////////////////////////////////////////////////////
//
//	1.0.0	06.07.1995	Created initial macros: LI, MOV, NOP, PUSH, PULL
//						SUBR, RTS, BRA and LPC
//	1.0.1	08.07.1995	Added CLR and PRINT
//	1.0.2	12.07.1995	Added INC and DEC, converted PRINT into subroutine
//						(now in print.i)
//	1.0.3	15.07.1995	Added start-up coding here for convenience
//	1.0.4	18.07.1995	Added LUI and all TRAP macros
//	1.0.5	23.10.1995	Added L32 and L32O
//	1.0.6	15.12.1995	Moved start-up code into start.i. Added JSRA and JA.
//	1.0.7	03.04.2001	Added INITSTACK macro. Updated layout and added extra
//						comments.
//
//////////////////////////////////////////////////////////////////////////////
//
// These macros simulate those mentioned in the Hennessy/Patterson book
// (H&P) as well as others that have proved useful. To use them, just
// put INCLUDE "macro.i" near the head of your source code. Note that
// the contents of R28 to R31 are affected by some of these macros and
// these registers should therefore be avoided in your own code.
//
//////////////////////////////////////////////////////////////////////////////

        NODEBUG     // Don't debug or list this include
        NOLIST

//////////////////////////////////////////////////////////////////////////////
// LI    Load immediate a 16-bit value - H&P p163
//
//       Format:     LI  R1,#Val
//////////////////////////////////////////////////////////////////////////////

LI      MACRO
        ADDI    \1,R0,\2
        ENDM


//////////////////////////////////////////////////////////////////////////////
// LUI   Load unsigned immediate a 16-bit value
//
//       Format:     LUI R1,#Val
//////////////////////////////////////////////////////////////////////////////

LUI     MACRO
        ADDUI   \1,R0,\2
        ENDM


//////////////////////////////////////////////////////////////////////////////
// L32   Load a 32-bit address value
//
//       Format:     L32 R1,Val
//////////////////////////////////////////////////////////////////////////////

L32     MACRO
        LHI     \1,#>\2
        ORI     \1,\1,#<\2
        ENDM


//////////////////////////////////////////////////////////////////////////////
// L32O  Load a 32-bit address value and then add in the local offset in
//       R29
//
//       Format:     L32O R1,Val
//////////////////////////////////////////////////////////////////////////////

L32O    MACRO
        LHI     \1,#>\2
        ORI     \1,\1,#<\2
        ADDU    \1,\1,R29       // Add relocation offset
        ENDM


//////////////////////////////////////////////////////////////////////////////
// MOV   Move one register to another - H&P p163 (except ADDU used
//       instead of ADD as this can prevent integer overflow problems)
//
//       Format:     MOV R1,R2   move R2 to R1
//////////////////////////////////////////////////////////////////////////////

MOV     MACRO
        ADDU    \1,\2,R0
        ENDM


//////////////////////////////////////////////////////////////////////////////
// NOP   No operation - H&P E-6
//
//       Format:     NOP
//////////////////////////////////////////////////////////////////////////////

NOP     MACRO
        ADD     R0,R0,R0
        ENDM


//////////////////////////////////////////////////////////////////////////////
// The next two simulate stack operations for a downward growing stack. For
// this purpose, the register R30 is used as the stack pointer and therefore
// should not be touched by any other coding except to initialise it to the
// top of some freely available area. R30 will always point to the next free
// long-word position. Note that no boundary checks are made so the stack
// could grow down into other code or data.
//////////////////////////////////////////////////////////////////////////////
// PUSH  Push a register onto the stack
//
//       Format:     PUSH    R1
//////////////////////////////////////////////////////////////////////////////

PUSH    MACRO
        SW      0(R30),\1
        SUBUI   R30,R30,#4
        ENDM

//////////////////////////////////////////////////////////////////////////////
// PULL  Pull a register from the stack
//
//       Format:     PULL    R1
//////////////////////////////////////////////////////////////////////////////

PULL    MACRO
        ADDUI   R30,R30,#4
        LW      \1,0(R30)
        ENDM


//////////////////////////////////////////////////////////////////////////////
// The DLX method of handling subroutine calls can be a bit laborious and so
// these macros have been included to simplify them. The subroutine should be
// called as normal with JAL which puts the current PC in R31. The first
// instruction of the subroutine should be SUBR which will save R31 on the
// stack. The last instruction of the subroutine should be RTS (ReTurn from
// Subroutine) which retrieves R31 and JRs to it.
//
//       Format:     SUBR
//                   ...  (the actual subroutine coding)
//                   RTS
//////////////////////////////////////////////////////////////////////////////

SUBR    MACRO
        PUSH    R31
        ENDM

RTS     MACRO
        PULL    R31
        JR      R31
        ENDM


//////////////////////////////////////////////////////////////////////////////
// These two routines allow jumping to absolute locations in memory that are
// not affected by relocation. JSRA (Jump to SubRoutine at Address) is a
// subroutine jump (return address stored in R31) and JA (Jump to Address) is
// the straight jump. Note that R28 is corrupted by these macros.
//
//       Format:     JSRA    Absolute location
//////////////////////////////////////////////////////////////////////////////

JSRA    MACRO
        L32     R28,\1
        JALR    R28
        ENDM

JA      MACRO
        L32     R28,\1
        JR      R28
        ENDM

//////////////////////////////////////////////////////////////////////////////
// An unconditional branch (16-bit offset) can be made by using a branch
// if equal to zero check on R0.
//
//       Format:     BRA     Label
//////////////////////////////////////////////////////////////////////////////

BRA     MACRO
        BEQZ    R0,\1
        ENDM


//////////////////////////////////////////////////////////////////////////////
// Load the current PC into a register. Using this at the start of a module
// will allow all offset access to be relative to that start point. This
// allows relocatable code provided that all local data accesses go through
// whichever register is used to hold the start PC. It relies on the fact that
// DASM always starts its address at zero. In MON use the load command L
// followed by the address at which to load, e.g.:
//       L"filename" 3000
// will load the file at hex loation 3000. Try running the same code at
// different locations. Note that start.i uses an LPC call during its startup
// code in order to set R29 to the start of the code block.
//
//       Format:     LPC     R29
//////////////////////////////////////////////////////////////////////////////

LPC     MACRO
        JAL     4+*         // Jump to next instruction in order to get
                            // the PC into R31
        MOV     \1,R31      // Save R31 in specified register
        SUBUI   \1,\1,#4    // ...and subtract 4 to get original address

        ENDM


//////////////////////////////////////////////////////////////////////////////
// Clear a register
//
//       Format:     CLR     R13
//////////////////////////////////////////////////////////////////////////////

CLR     MACRO
        ADDU    \1,R0,R0
        ENDM


//////////////////////////////////////////////////////////////////////////////
// Increment a register
//
//       Format:     INC     R3
//////////////////////////////////////////////////////////////////////////////

INC     MACRO
        ADDUI       \1,\1,#1
        ENDM


//////////////////////////////////////////////////////////////////////////////
// Decrement a register
//
//       Format:     DEC     R3
//////////////////////////////////////////////////////////////////////////////

DEC     MACRO
        SUBUI       \1,\1,#1
        ENDM


//////////////////////////////////////////////////////////////////////////////
// The TRAPs. These provide easily remembered alternatives.
//////////////////////////////////////////////////////////////////////////////

HALT    MACRO
        TRAP        0
        ENDM

OUTPUTBYTE MACRO
        TRAP        4
        ENDM

OUTPUTDEC MACRO
        TRAP        8
        ENDM

OUTPUTHEX MACRO
        TRAP        12
        ENDM

INPUT   MACRO
        TRAP        16
        ENDM


//////////////////////////////////////////////////////////////////////////////
// Initialise the stack pointer
//////////////////////////////////////////////////////////////////////////////

        MEM_BASE            EQU     $FF000100

INITSTACK MACRO
		L32		R30,MEM_BASE	// Set memory management base address
        LW		R30,0(R30)		// Get the memory size into R30
        SUBUI   R30,R30,#4		// Move to first available longword
		ENDM

//////////////////////////////////////////////////////////////////////////////

        INT_UNDEF_OPCODE    EQU     $00000001
        INT_DIVIDEBYZERO    EQU     $00000002
        INT_OVERFLOW        EQU     $00000004
        INT_TIMER           EQU     $00000008
        INT_FLAG            EQU     $80000000

//////////////////////////////////////////////////////////////////////////////

        DEBUG       ; Turn on any debugging/listing as set by
        LIST        ; the DASM command line

//////////////////////////////////////////////////////////////////////////////




