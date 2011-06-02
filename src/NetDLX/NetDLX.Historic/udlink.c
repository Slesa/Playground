/*
        Update Link Counter
*/
#if NEVER

#include <stdio.h>
#include <time.h>
#include "system.h"

STRPTR Ver = "$VER: UdLink 1.3 (DJV 21.09.1995)";

UBYTE Months [12] [4] = { "Jan", "Feb", "Mar", "Apr", "May", "Jun",
      "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };

/*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/

int main (int argc, char **argv)
{
    FILE    *f;
    WORD    num, l, x, i;
    UBYTE   S [100];
    struct  tm *T;
    time_t  tt;


    if (argc != 2)
    {
        printf ("Update Link Counter %s\n", &Ver [6]);
        printf ("USAGE : %s <FileName>\n", argv [0]);
        exit (20);
    }

    f = fopen (argv [1], "r");

    if (!f)
    {
        printf ("Creating new file link number = 1\n");
        num = 1;
    }
    else
    {
        fgets (S, 100, f);

        i = 0;
        l = strlen (S);

        for (x = 0; x < l; x++)
        {
            if (S [x] == '=')
            {
                i = x + 1;
                break;
            }
        }

        num = atoi (&S [i]);

        printf ("Link Update from %d to %d\n", num, num + 1);
        num++;

        fclose (f);
    }

    f = fopen (argv [1], "w");

    if (f)
    {
        fprintf (f, "int  LinkNo = %d;\n", num);

        time (&tt);
        T = localtime (&tt);

        fprintf (f, "char LinkDate [] = %c%d %s %4d%c;\n", 34,
            T->tm_mday, Months [T->tm_mon], T->tm_year + 1900, 34);

        fclose (f);
    }
    else
    {
        printf ("Could not write link file\n");
        exit (20);
    }

    strcpy (S, argv [1]);
    strcat (S, "-log");

    f = fopen (S, "a+");

    if (f)
    {
        fprintf (f, "%5d    %2d %s %4d %02d:%02d\n", num, T->tm_mday,
            Months [T->tm_mon], T->tm_year + 1900, T->tm_hour,
            T->tm_min);

        fclose (f);
    }
    else
    {
        printf ("Could not write log file\n");
        exit (20);
    }

    return 0;
}

#endif
