/*

        DLX RISC Simulator - On-line Help

        D. J. Viner
*/

#include "cpu.h"

#define TAB     8
#define SPACE   32
#define TEXTLEN 92

struct HelpIndexType
{
    UBYTE   Ref [6];
    ULONG   Addr;
};

/*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/
/* Return a line from the Help file ignoring comments.                    */
/*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/

VOID GetHelpLine (FILE *H, STRPTR Text)
{
    UBYTE   Txt [TEXTLEN];
    WORD    i, j;


    /* Get text, ignore comments (;) */

    do {
        fgets (Txt, TEXTLEN - 1, H);
    } while (!feof (H) && Txt [0] == ';');

    /* Copy text and expand TABs */

    for (i = 0, j = 0; i < strlen (Txt); i++)
    {
        if (Txt [i] != TAB)
        {
            if (Txt [i] >= SPACE)
                Text [j++] = Txt [i];
            else
                Text [j++] = SPACE;
        }
        else
        {
            Text [j++] = SPACE;

            while (j % 8)
                Text [j++] = SPACE;
        }
    }

    Text [j] = 0;
}

/*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/

BOOL CreateHelpIndex (FILE *H)
{
    FILE    *I;
    UBYTE   Text [TEXTLEN], c, i;
    BOOL    First = TRUE;
    LONG    Addr;
    struct  HelpIndexType HI;


    printf ("Creating New Help Index - please wait...\n\n");

    I = fopen ("dlxhlp.idx", "wb");

    if (!I)
    {
        fprintf (stderr, "Could not open new help index file\n");
        return FALSE;
    }

    do {
        Addr = ftell (H);
        GetHelpLine (H, Text);

        if (First)
        {
            memcpy (HI.Ref, Text, 6);
            HI.Addr = 0;
            fwrite (&HI, sizeof (HI), 1, I);
            First = FALSE;
        }

        if (Text [0] == ':' || Text [0] == '%')
        {
            /* Copy the ref and address */

            memset (HI.Ref, 0, 6);

            for (i = 1, c = 0; i < 6 && c != ':'; i++)
            {
                c = Text [i];

                if (c != ':')
                {
                    if (islower (c))
                        c = toupper (c);

                    HI.Ref [i - 1] = c;
                }
            }

            HI.Addr = Addr;
            fwrite (&HI, sizeof (HI), 1, I);
        }

    } while (!feof (H));

    rewind (H);
    fclose (I);
    return TRUE;
}

/*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/

BOOL SetHelpAddr (FILE *H, STRPTR Cmd)
{
    FILE    *I;
    LONG    i = 0;
    struct  HelpIndexType HI;
    UBYTE   Text [TEXTLEN];
    BOOL    Recreate, Loop = TRUE, Found = FALSE;


    do {
        i++;
        I = fopen ("dlxhlp.idx", "rb");

        if (!I && i == 1)
        {
            if (!CreateHelpIndex (H))
                return FALSE;
        }
        else
        if (!I)
        {
            fprintf (stderr, "Problems opening help index\n");
            return FALSE;
        }
        else
        {
            /* Check to see if Index is current */

            Recreate = FALSE;

            if (fread (&HI, sizeof (HI), 1, I) != 1)
                Recreate = TRUE;
            else
            {
                GetHelpLine (H, Text);
                rewind (H);

                if (memcmp (HI.Ref, Text, 6))
                    Recreate = TRUE;
            }

            if (Recreate)
            {
                fclose (I);
                CreateHelpIndex (H);
            }
            else
                Loop = FALSE;
        }

    } while (Loop);

    /* Search for entry in table */

    Loop = TRUE;

    while (!feof (I) && Loop)
    {
        if (fread (&HI, sizeof (HI), 1, I) != 1)
            break;

        if (strcmp (HI.Ref, Cmd) == 0)
        {
            fseek (H, HI.Addr, SEEK_SET);
            Loop = FALSE;
            Found = TRUE;
        }
    }

    fclose (I);
    return Found;
}

/*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/

VOID DoFullHelp (STRPTR Cmd)
{
    UBYTE   Text [TEXTLEN], i;
    FILE    *H = (FILE *) NULL;


    for (i = 0; i < strlen (Cmd); i++)
    {
        if (islower (Cmd [i]))
            Cmd [i] = toupper (Cmd [i]);

        if (Cmd [i] < SPACE)    /* Remove any CR and LF */
            Cmd [i] = 0;
    }

    H = fopen ("dlxhlp.dat", "r");

    if (H)
    {
        if (SetHelpAddr (H, Cmd))
        {
            GetHelpLine (H, Text);  /* Read and ignore location line */

            if (memcmp (Text, ":END", 4))   /* Check for not last line */
            do {
                GetHelpLine (H, Text);

                if (Text [0] != ':' && Text [0] != '%')
                    printf ("%s\n", Text);

            } while (Text [0] != ':');
        }
        else
            printf ("No details found on %s\n", Cmd);

        fclose (H);
    }
    else
        printf ("Help file not found\n");
}

/*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/


