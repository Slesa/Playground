/*

        DLX RISC Simulator - Monitor Header

        D. J. Viner
*/

extern  ULONG   LastAddr;

/*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/
/* mon2.c                                                                 */
/*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/

extern  VOID    DisplayDPRegisters ();
extern  VOID    DisplayIRegisters ();
extern  VOID    DisplayRegisters ();
extern  VOID    DisplaySettings ();
extern  VOID    DisplaySPRegisters ();
extern  VOID    DisplayStatRegisters ();
extern  VOID    DisplayTitle ();
extern  VOID    DoHelp ();
extern  ULONG   ExtractDecNo (STRPTR Str, WORD *Pos, BOOL *Blank);
extern  ULONG   ExtractNo (STRPTR Str, WORD *Pos, BOOL *Blank);
extern  UBYTE   PauseForKey (UBYTE Msg);

/*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/
/* mon3.c                                                                 */
/*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/

extern  ULONG   DebugLevel;
extern  FILE    *Log;

#define DB_DISASSEM     0x0001
#define DB_MICROCODE    0x0002
#define DB_REGS         0x0004
#define DB_PIPESTAGES   0x0008
#define DB_PIPEREGS     0x0010
#define DB_EXONLY       0x0020
