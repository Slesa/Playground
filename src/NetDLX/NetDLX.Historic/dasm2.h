/////////////////////////////////////////////////////////////////////////////////////////
//
// DLX Assembler - Assembler routines header
//
// D. J. Viner

struct OpCodeType
{
    UBYTE   Op [10];
    UBYTE   Mask [8];
    UWORD   Type;
    ULONG   OpCode;
};

/////////////////////////////////////////////////////////////////////////////////////////
//  Mask types                      Examples
//
//  R   Integer reg                 R0 r1 r31
//  I   Immediate 16-bit value      #label #12 #$23 <#label >#label
//  o   Offset (on next reg)        label(r1)
//  O   Offset alone                label
//  F   SP reg                      R0 r3     (also allows S0 s23 etc)
//  D   DP reg                      R0 r4     (also allows D0 d24 etc)
//                                  reg number for D types always even
/////////////////////////////////////////////////////////////////////////////////////////

#define xxx   1
#define DDx   2
#define DDD   3
#define DFx   4
#define DIx   5
#define DoR   6
#define FDx   7
#define FFx   8
#define FFF   9
#define FIx  10
#define FoR  11
#define FRx  12
#define IFx  13
#define Oxx  14
#define oRD  15
#define oRF  16
#define oRR  17
#define Rxx  18
#define RDx  19
#define RFx  20
#define RIx  21
#define ROx  22
#define RoR  23
#define RRI  24
#define RRR  25

extern UBYTE Offsets [26] [2];
extern struct OpCodeType OpCodes [];

#define PSEUDO_DSB      0
#define PSEUDO_DSH      1
#define PSEUDO_DSW      2
#define PSEUDO_DCB      3
#define PSEUDO_DCH      4
#define PSEUDO_DCW      5
#define PSEUDO_INCLUDE  6
#define PSEUDO_PAGE     7
#define PSEUDO_SKIP     8
#define PSEUDO_END      9
#define PSEUDO_ENDIF   10
#define PSEUDO_IFDEF   11
#define PSEUDO_IFNDEF  12
#define PSEUDO_ENDM    13
#define PSEUDO_MACRO   14
#define PSEUDO_LIST    15
#define PSEUDO_NOLIST  16
#define PSEUDO_SYM     17
#define PSEUDO_NOSYM   18
#define PSEUDO_DEBUG   19
#define PSEUDO_NODEBUG 20
#define PSEUDO_PAD     21
#define PSEUDO_GENINC  22

/////////////////////////////////////////////////////////////////////////////////////////

extern void     Error (STRPTR Msg, BOOL PrintLine);
extern void     Warn (STRPTR Msg, BOOL PrintLine);
extern void     SkipWhitespace ();
extern void     SkipToWhitespace ();
extern void     AddFile ();
extern void     EndFile ();
extern void     OutputByte (UBYTE b);
extern void     OutputWord (UWORD w);
extern void     OutputLong (ULONG l);
extern UBYTE    GetLine ();
extern STRPTR   CollectSym (STRPTR p, STRPTR Str);
extern void     SetOffsets ();

/////////////////////////////////////////////////////////////////////////////////////////
// GetLine defines
/////////////////////////////////////////////////////////////////////////////////////////

#define COMMENT     0       // Comment or blank line
#define VALID       1       // Valid line for further parsing
#define SETADDR     2       // Address setting line
#define ENDCOND     3       // End of conditional statement
