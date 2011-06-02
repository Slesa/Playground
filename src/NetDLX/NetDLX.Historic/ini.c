/*

        DLX RISC Simulator - INI file handling

        D. J. Viner
*/

#include    "cpu.h"

/*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/
/* INI Command table                                                      */
/*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/

STRPTR IniTable [] =
{
    "Memory",
    "Timer",
    "Traps",
    "DefLoad",
    "TrapType",
    "CPUType",
    "PipeFeedType",
    "PipeBranchType",
    "PipeLoadType",
    NULL
};

#define INI_MEMORY      0
#define INI_TIMER       1
#define INI_TRAPS       2
#define INI_DEFLOAD     3
#define INI_TRAPTYPE    4
#define INI_CPUTYPE     5
#define INI_PFEEDTYPE   6
#define INI_PBRANCHTYPE 7
#define INI_PLOADTYPE   8

ULONG   DefLoad = 0;

/*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/

STRPTR CollectKeyword (STRPTR p, STRPTR Str)
{
    WORD    i = 0;
    BOOL    Loop = TRUE;


    do {
        switch (*p)
        {
            case ' ' : case ',' : case '\t' :
            case ';' : case '\n' :
                Loop = FALSE;
                break;

            default :
                Str [i++] = *p;
                p++;
        }

    } while (Loop);

    Str [i] = 0;
    return p;
}

/*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/

VOID IniSetting (ULONG Which, STRPTR ptr)
{
    ULONG   Val;


    Val = atol (ptr);

    switch (Which)
    {
        case INI_MEMORY :
            dlx.SizeOfMem = Val * 1024;
            break;

        case INI_TIMER :
            if (Val == 0)
                dlx.Type &= ~TIMERON;
            else
                dlx.Type |= TIMERON;

            break;

        case INI_TRAPS :
            if (Val == 0)
                dlx.Type &= ~TRAPSON;
            else
                dlx.Type |= TRAPSON;

            break;

        case INI_TRAPTYPE :
            if (Val == 0)
                dlx.Type &= ~VECTORTRAPS;
            else
                dlx.Type |= VECTORTRAPS;

            break;

        case INI_DEFLOAD :
            DefLoad = Val;
            break;

        case INI_CPUTYPE :
            dlx.Type &= 0xFFFFFFF8;

            switch (Val)
            {
                default :
                    dlx.Type |= HARDWIRED;
                    break;

                case 2 :
                    dlx.Type |= MICROCODE;
                    break;

                case 3 :
                    dlx.Type |= PIPELINED;
            }

            break;

        case INI_PBRANCHTYPE :
            dlx.Type &= 0xFFF0FFFF;
            dlx.Type |= ((Val & 3) << 16);
            break;

        case INI_PFEEDTYPE :
            dlx.Type &= 0xFF0FFFFF;
            dlx.Type |= ((Val & 3) << 20);
            break;

        case INI_PLOADTYPE :
            dlx.Type &= 0xF0FFFFFF;
            dlx.Type |= ((Val & 1) << 24);
            break;
    }
}

/*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/

VOID ReadIni ()
{
    FILE    *fIni;
    UBYTE   Line [122], Keyword [30];
    BOOL    Loop = TRUE, Found;
    STRPTR  ptr;
    ULONG   i;


    /* Open the ini file (if it exists - no error if not) */

    fIni = fopen ("dlx.ini", "rb");

    if (fIni)
    {
        do {
            fgets (Line, 120, fIni);

            if (feof (fIni))
                Loop = FALSE;
            else
            {
                ptr = &Line [0];

                while (*ptr == ' ' || *ptr == '\t')
                    ptr++;

                if (!(*ptr == 0    || *ptr == ';' ||
                      *ptr == '\n' || *ptr == '['))
                {
                    ptr = CollectKeyword (ptr, Keyword);

                    for (i = 0, Found = FALSE; !Found && IniTable [i]; i++)
                    {
                        if (stricmp (Keyword, IniTable [i]) == 0)
                        {
                            Found = TRUE;
                            ptr++;

                            while (*ptr == ' ' || *ptr == '\t')
                                ptr++;

                            /* *ptr should be '=' at this point */

                            if (*ptr == '=')
                                IniSetting (i, ++ptr);
                        }
                    }
                }
            }

        } while (Loop);

        fclose (fIni);
    }
}

/*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/


