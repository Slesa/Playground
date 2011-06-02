/*
        DLX RISC Simulator - Monitor Source Module 2
        Display routines

        D. J. Viner
*/

#include "cpu.h"
#include "mon.h"

STRPTR HelpList [] =
{
    "A  Memory set/display",
    "B  Breakpoint set/display",
    "C  Continue run",
    "CD Continue run with Debug",
    "D  Disassemble",
    "DM Disassem microcode",
    "E  Enter data",
    "F  Fill memory",
    "G  Go (Run)",
    "H  Go with Debug",
    "I  Set/display Load Addr",
    "J  Set/display debug",
    "K  Pipeline Control",
    "L  Load file",
    "M  Memory dump",
    "N  -",
    "O  Display registers",
    "P  Set PC",
    "Q  Quit Monitor",
    "R  Set registers",
    "S  Save file",
    "T  Trace",
    "U  Trace with Debug",
    "V  Set/display version",
    "W  Write debug info to log file",
    "X  Exit Monitor",
    "Y  Display performance",
    "Z  Zero registers",
    "[  Start/end recording",
    "]  Play back recording",
    "^  Load Microcode table",
    "@  System command",
    NULL
};

VOID DisplaySettings ()
{
    printf ("Type:\t");

    switch (SIMTYPE)
    {
        case HARDWIRED :
            printf ("Hardwired");
            break;

        case MICROCODE :
            printf ("Microcode");
            break;

        case PIPELINED :
            printf ("Pipelined");
    }

    printf (" CPU\n\t");

    if (dlx.Type & TIMERON)
        printf ("Timer enabled");
    else
        printf ("Timer disabled");

    if (SIMTYPE != MICROCODE)
    {
        printf (", ");

        if (dlx.Type & VECTORTRAPS)
            printf ("Vector ");

        if (dlx.Type & TRAPSON)
            printf ("TRAPs enabled");
        else
            printf ("TRAPs disabled");
    }

    if (LittleEndian)
        printf (", Little Endian System");

    printf ("\n\tMemory allocated = %ldK\n", dlx.SizeOfMem / 1024);
}

VOID DisplayTitle ()
{
    printf ("DLX RISC CPU Simulator\nVersion %s.%d (%s)\n",
        Version, LinkNo, LinkDate);
    printf ("(CMP-3P4Y 1.1995/6 - D J Viner - u9323864)\n\n");

    DisplaySettings ();
}

UBYTE PauseForKey (UBYTE Msg)
{
    UBYTE   Pause [4];


    switch (Msg)
    {
        case 0 : break;
        case 1 : printf ("(press RETURN for more)"); break;
        case 2 : printf ("Press RETURN to continue"); break;
    }

    fgets (Pause, 3, stdin);

    return Pause [0];
}

/*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/
/* Minimal help information - more is available in the separate help file */
/*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/

VOID DoHelp ()
{
    UWORD   i = 0, mid;


    while (HelpList [i])
        i++;

    mid = (i + 1) / 2;

    for (i = 0; i < mid; i++)
    {
        printf ("%-31.31s", HelpList [i]);

        if (HelpList [i + mid])
            printf ("%s", HelpList [i + mid]);

        printf ("\n");
    }

    printf ("\nUse ?x where x is any of the above commands for more details\n");
}

/*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/
/* Extract a hexadecimal number from Str starting at position Pos         */
/*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/

ULONG ExtractNo (STRPTR Str, WORD *Pos, BOOL *Blank)
{
    ULONG   Num = 0;
    WORD    p;


    /* Ignore non-hex digits at the beginning */

    while (Str [*Pos] && !isxdigit (Str [*Pos]))
        (*Pos)++;

    p = *Pos;

    /* Read the hex digits */

    while (isxdigit (Str [*Pos]))
    {
        /* Convert a-f to A-F */

        if (islower (Str [*Pos]))
            Str [*Pos] = toupper (Str [*Pos]);

        Num *= 16;

        if (isdigit (Str [*Pos]))
            Num += (Str [*Pos] - '0');
        else
            Num += (Str [*Pos] - 'A' + 10);

        (*Pos)++;
    }

    *Blank = (p == *Pos);   /* Indicate no value found */

    return Num;
}

/*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/
/* Extract a decimal number from Str starting at position Pos             */
/*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/

ULONG ExtractDecNo (STRPTR Str, WORD *Pos, BOOL *Blank)
{
    ULONG   Num = 0;
    WORD    p;


    /* Ignore non-digits at the beginning */

    while (Str [*Pos] && !isdigit (Str [*Pos]))
        (*Pos)++;

    p = *Pos;

    /* Read the digits */

    while (isdigit (Str [*Pos]))
    {
        Num *= 10;
        Num += (Str [*Pos] - '0');
        (*Pos)++;
    }

    *Blank = (p == *Pos);   /* Indicate no value found */

    return Num;
}

/*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/
/* Display all main DLX integer registers                                 */
/*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/

VOID DisplayStatRegisters ()
{
    printf ("PC   $%08lX    IST  $%08lX    FPST $%08lX    IAR  $%08lX\n",
        dlx.PC, dlx.IStatus, dlx.FPStatus, dlx.IAR);
}

/*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/
/* Display all main DLX integer registers                                 */
/*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/

VOID DisplayRegisters ()
{
    WORD    i, j;


    for (i = 0; i < 8; i++)
    {
        for (j = 0; j < 4; j++)
            printf ("R%-2d  $%08lX    ", i + j * 8, dlx.R [i + j * 8]);

        printf ("\n");
    }
}

/*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/
/* Display all SP registers                                               */
/*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/

VOID DisplaySPRegisters ()
{
    WORD    i, j;


    for (i = 0; i < 8; i++)
    {
        for (j = 0; j < 4; j++)
            printf ("F%-2d %12.4f   ", i + j * 8, dlx.FP.F [i + j * 8]);

        printf ("\n");
    }
}

/*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/
/* Display all DP registers                                               */
/*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/

VOID DisplayDPRegisters ()
{
    WORD    i, j;


    for (i = 0; i < 4; i++)
    {
        for (j = 0; j < 4; j++)
            printf ("D%-2d %12.4f   ", (i + j * 4) * 2,
                dlx.FP.D [i + j * 4]);

        printf ("\n");
    }
}

/*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/
/* Display all I registers (that co-exist with the FP regs)               */
/*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/

VOID DisplayIRegisters ()
{
    WORD    i, j;


    for (i = 0; i < 8; i++)
    {
        for (j = 0; j < 4; j++)
            printf ("I%-2d  $%08lX    ", i + j * 8, dlx.FP.I [i + j * 8]);

        printf ("\n");
    }
}
