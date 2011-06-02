/*

        DLX RISC Simulator - File input/output

        D. J. Viner
*/

#include "cpu.h"

/*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/
/* Extract a file name from Cmd at position Pos                           */
/*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/

BOOL GetFileName (STRPTR Cmd, WORD *Pos, STRPTR FileName)
{
    int     l = strlen (Cmd), j = 0;
    BOOL    Quote = FALSE, Loop = TRUE;


    *Pos = 0;

    /* Move past spaces */

    while (Cmd [*Pos] == ' ')
        (*Pos)++;

    /* Find the initial quote " or ' */

    if (Cmd [*Pos] == 0x22 || Cmd [*Pos] == 0x27)
    {
        (*Pos)++;
        Quote = TRUE;
    }

    if (*Pos < l)     /* Ok so far */
    {
        /* Copy bytes of file name into FileName */

        do {
            switch (Cmd [*Pos])
            {
                case 0 :
                    Loop = FALSE;
                    break;

                case 0x22 : /* " */
                case 0x27 : /* ' */
                    if (!Quote)
                    {
                        printf ("Quote in filename error\n");
                        return FALSE;
                    }

                    Loop = FALSE;
                    (*Pos)++;     /* Move past final quote */
                    break;

                case ' ' :
                    if (!Quote)
                    {
                        Loop = FALSE;
                        break;
                    }

                    /* Drop through */

                default :
                    FileName [j++] = Cmd [(*Pos)++];
            }

        } while (Loop && *Pos <= l);
    }

    FileName [j] = 0;

    if (*Pos > l)
        printf ("Filename error\n");

    return (BOOL) (*Pos <= l);
}

/*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/

VOID DoLoad (STRPTR Cmd, BOOL Display)
{
    ULONG   Start = 0, St, Len;
    BOOL    Blank;
    FILE    *fp;
    UBYTE   FileName [80];
    WORD    Pos = 0, Err = 0;


    if (!GetFileName (Cmd, &Pos, FileName))
        return;

    Start = ExtractNo (Cmd, &Pos, &Blank);

    if (!Blank)
    {
        if (Start % 4 != 0)
        {
            printf ("Misaligned start address\n");
            return;
        }
    }

    fp = fopen (FileName, "rb");

    if (fp)
    {
        if (fread ((char *) &St, 4, 1, fp) != 1)
            Err = 1;

        if (!Err)
        {
            if (Display)
                printf ("Loading %c%s%c at ", 34, FileName, 34);

            if (Blank)
            {
                if (DefLoad)
                    Start = DefLoad;
                else
                    Start = St;
            }

            if (Display)
                printf ("$%lX\n", Start);

            St = Start;
            Len = 1024;

            while (Len == 1024 && !Err)
            {
                if (St >= dlx.SizeOfMem)
                    Err = 2;
                else
                {
                    Len = fread ((char *) &dlx.Memory [St], 1, 1024, fp);
                    St += 1024;
                }
            }
        }

        fclose (fp);

        switch (Err)
        {
            case 1 :
                printf ("Error reading load address\n");
                break;

            case 2 :
                printf ("Error: File loaded into non-existant memory\n");
        }
    }
    else
    if (Display)
        printf ("Could not open file %c%s%c to load\n", 34, FileName, 34);
}

/*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/

VOID DoSave (STRPTR Cmd)
{
    BOOL    Blank, Err = FALSE;
    UBYTE   FileName [80];
    ULONG   Start, End, Len;
    FILE    *fp;
    WORD    Pos = 0;


    if (!GetFileName (Cmd, &Pos, FileName))
        return;

    Start = ExtractNo (Cmd, &Pos, &Blank);

    if (Blank)
    {
        printf ("Start address missing\n");
        return;
    }

    End = ExtractNo (Cmd, &Pos, &Blank);

    if (Blank)
    {
        printf ("End address missing\n");
        return;
    }

    if (End <= Start)
    {
        printf ("End address lower than start\n");
        return;
    }

    if (End >= dlx.SizeOfMem)
    {
        printf ("Saving non-existant memory\n");
        return;
    }

    Len = End - Start + 1;

    fp = fopen (FileName, "wb");

    if (fp)
    {
        printf ("Saving %c%s%c\n", 34, FileName, 34);

        /* Start Address */

        if (fwrite ((char *) &Start, 4, 1, fp) != 1)
            Err = TRUE;

        /* Data */

        if (!Err)
            if (fwrite ((char *) &dlx.Memory [Start], Len, 1, fp) != 1)
                Err = TRUE;

        fclose (fp);

        if (Err)
            printf ("Error writing file\n");
    }
    else
        printf ("Could not open file %c%s%c to save\n", 34, FileName, 34);
}

/*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/

VOID DoMcLoad (STRPTR Cmd)
{
    UBYTE   FileName [80];
    WORD    Pos = 0;


    if (!GetFileName (Cmd, &Pos, FileName))
        return;

    printf ("Loading microcode table '%s'\n", FileName);
    LoadMicrocode (FileName, FALSE);
}

/*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/


