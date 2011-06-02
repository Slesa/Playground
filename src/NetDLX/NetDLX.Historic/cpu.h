/////////////////////////////////////////////////////////////////////////////////////////
//	DLX RISC Simulator - Global CPU Header - D. J. Viner
/////////////////////////////////////////////////////////////////////////////////////////

#include "system.h"
#include "micro.h"

/////////////////////////////////////////////////////////////////////////////////////////
// Pipeline stages struct
/////////////////////////////////////////////////////////////////////////////////////////

typedef struct DLXPipeline
{
	BOOL	DoNewPC;
	UWORD	Status;
	ULONG	PC, IR, NewPC;
	LONG	Offset26;
	WORD	Offset16, Func;
	UBYTE	Op;

		//	Src 1	Src 2	Dest
	ULONG	IntA,	IntB,	IntC;
	DOUBLE	FpA,	FpB,	FpC;
	UBYTE	RegA,	RegB,	RegC;		// Index to Reg
	UBYTE	TRegA,	TRegB,	TRegC;		// Src/Dest Reg types
} DLXPipe;

// Defines for Status

#define RUN		0
#define STALL	1
#define FLUSH	2

// Define size of branch prediction table

#define MAXBRPREDICT	8

/////////////////////////////////////////////////////////////////////////////////////////
// Define the DLX CPU 'machine'. Registers defined here for all CPU implementations:
// hardwired, microcode and pipelined. See H&P page 200 for an explanation of how each
// register is used.
/////////////////////////////////////////////////////////////////////////////////////////

typedef struct DLXStruct
{
	STRPTR	Memory;	 	// Pointer to 'system' memory
	ULONG	SizeOfMem;	// Highest memory location - 1

	// The register file (32 integer registers plus 32 floating point registers that can
	// also be used as 16 doubles or accessed as 32 integers)

	ULONG	R [32];	 	// Main registers - R0 is always 0

	union
	{
		FLOAT	F [32];	// Single precision
		DOUBLE	D [16];	// Double precision
		ULONG	I [32];	// Integer (for mult/divide)
	} FP;

	ULONG	PC,			// Program Counter
			IStatus,
			FPStatus,
			IAR,		// Interrupt Address Register
			IR,			// Instruction Register
			MAR,		// Memory Address Register
			MDR,		// Memory Data Register
			RegA,
			RegB,
			RegC,
			Temp;

	DOUBLE	RegAD,
			RegBD,
			RegCD,
			MDR8;

	BOOL	Zero,
			Negative,
			Running;


	//************** Used for Microcode version only ***************

	ULONG	Microcode [MICROSIZE];		// Main microcode
	UBYTE	MicroJump [MICROSIZE];		// Jump table
	UBYTE	Decode1 [DECODE1SIZE];		// Decode1
	UBYTE	Decode2 [DECODE2SIZE];		// Decode2/3

	//************** Used for Pipelined version only ***************

	DLXPipe Pipe [5];	// One each for IF, ID, EX, MEM and WB
	BOOL	Trapped;	// True if TRAP 0 encountered

	// For speed/instruction throughput measuring

	ULONG	Clock;		// Count of total cycles
	ULONG	Cycles;		// Cycles this instruction
	ULONG	Instr;		// Instructions executed since Run
	ULONG	IStart;		// Instructions started (pipeline only)
	ULONG	IDone;		// Instructions completed (pipeline only)

	ULONG	Loads;
	ULONG	Stores;
	ULONG	ALU;
	ULONG	Set;
	ULONG	Jumps;
	ULONG	JALs;
	ULONG	Move;
	ULONG	Convert;
	ULONG	BranchTaken;
	ULONG	BranchNotTaken;
	ULONG	TrapRfe;

	ULONG	Type;		// DLX CPU type - see below for defines
} DLX;


/////////////////////////////////////////////////////////////////////////////////////////
// CPU Types (ORed together - some are mutually exclusive)
/////////////////////////////////////////////////////////////////////////////////////////

// Datapath type

#define HARDWIRED		0x00000001
#define MICROCODE		0x00000002
#define PIPELINED		0x00000003

// Switches for various things

#define TIMERON			0x00000010
#define TRAPSON			0x00000080
#define VECTORTRAPS		0x00000100
#define MOVESON			0x00000200

// Pipeline control

// Branch

#define BR_NONE			0x00000000
#define BR_STALL		0x00010000
#define BR_EARLY		0x00020000
#define BR_DELAY		0x00030000

// Forwarding

#define FW_NONE			0x00000000
#define FW_STALL		0x00100000
#define FW_FEED			0x00200000

// Load Hazard

#define LD_NONE			0x00000000
#define LD_HAZARD		0x01000000

// Defines for easier coding

#define SIMTYPE		(dlx.Type & 0x00000003)
#define BRANCHTYPE	(dlx.Type & 0x000F0000)
#define FEEDTYPE	(dlx.Type & 0x00F00000)
#define LOADTYPE	(dlx.Type & 0x0F000000)


/////////////////////////////////////////////////////////////////////////////////////////
// Defines for the interrupt status register
/////////////////////////////////////////////////////////////////////////////////////////

//	Bits	0-30	Interrupts
//			  31	Interrupt flag


// Interrupts

#define INT_UNDEF_OPCODE	0x00000001
#define INT_DIVIDEBYZERO	0x00000002
#define INT_OVERFLOW		0x00000004
#define INT_TIMER			0x00000008

// Interrupt flag

#define INT_FLAG			0x80000000

/////////////////////////////////////////////////////////////////////////////////////////
// Defines for the fp status register
/////////////////////////////////////////////////////////////////////////////////////////

#define FST_COMPARE		 1

/////////////////////////////////////////////////////////////////////////////////////////
// The pipeline stage defines
/////////////////////////////////////////////////////////////////////////////////////////

#define IF		0
#define ID		1
#define EX		2
#define MEM		3
#define WB		4

/////////////////////////////////////////////////////////////////////////////////////////
// The 'hardware' of the simulated timer
/////////////////////////////////////////////////////////////////////////////////////////

typedef struct TimerType
{
	ULONG	Timer1Latch;
	ULONG	Timer1Count;
	ULONG	Timer1Status;
	ULONG	Spare1;
	ULONG	Timer2Latch;
	ULONG	Timer2Count;
	ULONG	Timer2Status;
	ULONG	Spare2;
	ULONG	Timer3Latch;
	ULONG	Timer3Count;
	ULONG	Timer3Status;
	ULONG	Spare3;
} TIMER;

// Timer register addresses

#define T1_LATCH	0xFF000000
#define T1_COUNT	0xFF000004
#define T1_STATUS	0xFF000008

#define T2_LATCH	0xFF000010
#define T2_COUNT	0xFF000014
#define T2_STATUS	0xFF000018

#define T3_LATCH	0xFF000020
#define T3_COUNT	0xFF000024
#define T3_STATUS	0xFF000028

// Timer status bits

#define T_ENABLED	0x00000001		// Bit 0
#define T_INTERRUPT 0x00000080		// Bit 7

/////////////////////////////////////////////////////////////////////////////////////////
// The 'hardware' of the 'memory management' chip - all this currently does is indicate
// the size of the available memory.
/////////////////////////////////////////////////////////////////////////////////////////

typedef struct MemMgmtType
{
	ULONG	MemSize;
} MEMMGMT;

// Register addresses

#define MEM_BASE	0xFF000100

/////////////////////////////////////////////////////////////////////////////////////////
// Breakpoint handling
/////////////////////////////////////////////////////////////////////////////////////////

#define MAXBREAKPOINTS	30

typedef struct BreakPointType
{
	ULONG	BPs [MAXBREAKPOINTS];
	ULONG	Num, Copy, PC;
	BOOL	Flag;
} BPTYPE;

extern BPTYPE BP;

/////////////////////////////////////////////////////////////////////////////////////////
// Prototyping
/////////////////////////////////////////////////////////////////////////////////////////

// cpu.c

extern	VOID	CheckCtrlC (void);
extern	VOID	ClearDLXRegs (BOOL All);
extern	VOID	DecodeIType (ULONG Instr, UBYTE *d, UBYTE *s, WORD *i);
extern	VOID	DecodeJType (ULONG Instr, LONG *n);
extern	VOID	DecodeRType (ULONG Instr, UBYTE *d, UBYTE *s1, UBYTE *s2, WORD *f);
extern	VOID	IncClock (ULONG Num);
extern	BOOL	InitCpu (void);
extern	VOID	LoadMicrocode (STRPTR FName, BOOL MainTable);
extern	VOID	Run (BOOL Debug);
extern	BOOL	RunOneInstr (BOOL Debug);
extern	VOID	SetInterrupt (ULONG Int);
extern	VOID	SetRegisterTypes (ULONG IR, UBYTE *Src1, UBYTE *Src2, UBYTE *Dest);

extern	BOOL	LittleEndian;
extern	DLX		dlx;
extern	TIMER	timer;

// mem.c

extern	UBYTE	FetchB (ULONG Addr);
extern	UWORD	FetchW (ULONG Addr);
extern	ULONG	FetchL (ULONG Addr);
extern	STRPTR	Fetch8 (ULONG Addr);
extern	VOID	StoreB (ULONG Addr, UBYTE Val);
extern	VOID	StoreW (ULONG Addr, UWORD Val);
extern	VOID	StoreL (ULONG Addr, ULONG Val);
extern	VOID	Store8 (ULONG Addr, STRPTR Val);

// help.c

extern	VOID	DoFullHelp (STRPTR Cmd);

// ini.c

extern	VOID	ReadIni (void);
extern	ULONG	DefLoad;

// in cpuX.c

extern	BOOL	RunOneHardwiredInstr (BOOL Debug);
extern	BOOL	RunOneMicrocodeInstr (BOOL Debug);
extern	BOOL	RunOnePipelinedInstr (BOOL Debug);

// in link file (currently not used for Mac)

extern	int		LinkNo;
extern	char	LinkDate [];

/////////////////////////////////////////////////////////////////////////////////////////
// Output and Debug calls to be provided by the surrounding module
/////////////////////////////////////////////////////////////////////////////////////////

extern	VOID	OutputChr (UBYTE Ch);
extern	VOID	OutputDecVal (ULONG Val);
extern	VOID	OutputHexVal (ULONG Val);
extern	UBYTE	InputChr (void);

extern	VOID	CallDebug (ULONG Val1, ULONG Val2);


/////////////////////////////////////////////////////////////////////////////////////////
// MICROCODE DEFINES
/////////////////////////////////////////////////////////////////////////////////////////

/////////////////////////////////////////////////////////////////////////////////////////
// Decode1 settings
/////////////////////////////////////////////////////////////////////////////////////////

#define	MEMORY			0
#define	MOVI2S			1
#define	MOVS2I			2
#define	S2isB			3
#define	S2isIMM			4
#define	BEQZ			5
#define	BNEZ			6
#define	JUMP			7
#define	JUMPR			8
#define	JAL				9
#define	JALR			10
#define	TRAP			11
#define	RFE				12
#define	CVT				13
#define	SETF			14
#define	BFP				15
#define	MOVFP2I			16
#define	MOVI2FP			17
#define	MOVFP			18


/////////////////////////////////////////////////////////////////////////////////////////
// Decode2 settings
/////////////////////////////////////////////////////////////////////////////////////////

#define	LB				0
#define	LBU				1
#define	LH				2
#define	LHU				3
#define	LW				4
#define	LF				5
#define	LD				6
#define	ADD				7
#define	SUB				8
#define	AND				9
#define	OR				10
#define	XOR				11
#define	SLL				12
#define	SRL				13
#define	SRA				14
#define	LHI				15
#define	SEQ				16
#define	SNE				17
#define	SLT				18
#define	SGE				19
#define	SGT				20
#define	SLE				21

/////////////////////////////////////////////////////////////////////////////////////////
// Source defines
/////////////////////////////////////////////////////////////////////////////////////////

#define SRC_A			0
#define SRC_B			0
#define SRC_Temp		1
#define SRC_PC			2
#define SRC_IAR			3
#define SRC_MAR			4
#define SRC_MDR			5
#define SRC_imm16		6
#define SRC_imm26		7
#define SRC_Const		8
#define SRC_SR			9
#define SRC_FPSR		10
#define SRC_AD			11
#define SRC_BD			11

/////////////////////////////////////////////////////////////////////////////////////////
// ALU Operation defines
/////////////////////////////////////////////////////////////////////////////////////////

#define ALU_ADD			0
#define ALU_SUB			1
#define ALU_RSUB		2
#define ALU_AND			3
#define ALU_OR			4
#define ALU_XOR			5
#define ALU_SLL			6
#define ALU_SRL			7
#define ALU_SRA			8
#define ALU_PassS1		9
#define ALU_PassS2		10
#define ALU_MULTI		11
#define ALU_DIVI		12
#define ALU_MULTF		13
#define ALU_DIVF		14
#define ALU_ADDF		15
#define ALU_SUBF		16
#define ALU_RSUBF		17
#define ALU_MULTD		18
#define ALU_DIVD		19
#define ALU_ADDD		20
#define ALU_SUBD		21
#define ALU_RSUBD		22
#define ALU_CVTF2D		23
#define ALU_CVTF2I		24
#define ALU_CVTD2F		25
#define ALU_CVTD2I		26
#define ALU_CVTI2F		27
#define ALU_CVTI2D		28

/////////////////////////////////////////////////////////////////////////////////////////
// Dest defines
/////////////////////////////////////////////////////////////////////////////////////////

#define DEST_C			1
#define DEST_Temp		2
#define DEST_PC			3
#define DEST_IAR		4
#define DEST_MAR		5
#define DEST_MDR		6
#define DEST_SR			7
#define DEST_FPSR		8
#define DEST_CD			9

/////////////////////////////////////////////////////////////////////////////////////////
// Misc operation defines
/////////////////////////////////////////////////////////////////////////////////////////

#define MISC_InstrRd	0
#define MISC_DataRd		1
#define MISC_Write		2
#define MISC_ABRF		3
#define MISC_RdC		4
#define MISC_R31C		5

/////////////////////////////////////////////////////////////////////////////////////////
// Condition defines
/////////////////////////////////////////////////////////////////////////////////////////

#define COND_NextInstr	0
#define COND_Uncond		1
#define COND_Int		2
#define COND_Mem		3
#define COND_Zero		4
#define COND_Neg		5
#define COND_Load		6
#define COND_Decode1	7
#define COND_Decode2	8
#define COND_Decode3	9
#define COND_DestIAR	10
#define COND_DestSR		11
#define COND_DestFPSR	12
#define COND_SrcIAR		13
#define COND_SrcSR		14
#define COND_SrcFPSR	15

/////////////////////////////////////////////////////////////////////////////////////////

#define ERROR		0xFF
#define RTYPE		0xFE

#define T_None		0
#define T_Load		1
#define T_Store		2
#define T_ALU		3
#define T_Set		4
#define T_Jump		5
#define T_JAL		6
#define T_Move		7
#define T_Convert	8
#define T_Branch	9
#define T_Other		10

// UnSigned flag

#define US			128

// Register types

#define INT	 		0
#define INTFP		1
#define FPS			2
#define FPD			3
#define SPEC		4

#define NA			255	 // Not Applicable

/////////////////////////////////////////////////////////////////////////////////////////

struct DecodeTables
{
	UBYTE	Dec1, Dec2, Type, FlagsLen;
	UBYTE	Regs [4];
};

extern struct DecodeTables DecodeT [128];

/////////////////////////////////////////////////////////////////////////////////////////


