/*

	Unpack files moved to Unix
*/
#if NEVER

#include <stdlib.h>

#define BUFSZ	4096

int LittleEndian = 0;

/*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/

long GetSize (FILE *f)
{
    char Conv1 [4], Conv2 [4], i;
    long Sz;


    if (LittleEndian)
    {
	fread (Conv1, 4, 1, f);

	for (i = 0; i < 4; i++)
	    Conv2 [i] = Conv1 [3 - i];

	memcpy ((char *) &Sz, Conv2, 4);
    }
    else
	fread (&Sz, 4, 1, f);

    return Sz;
}

/*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/

void main (int argc, char **argv)
{
    FILE    *fIn, *fOut;
    char    Buff [BUFSZ], FName [40], Type [4];
    long    Sz;
    int     Loop = 1;


    Sz = 0x12345678L;
    memcpy (Type, (char *) &Sz, 4);

    if (Type [0] == 0x78)
	LittleEndian = 1;

    if (argc < 2)
    {
	printf ("Usage: Unpack File\n");
	exit (0);
    }

    /* Open input file */

    fIn = fopen (argv [1], "rb");

    if (!fIn)
    {
	printf ("Could not open input file\n");
	exit (20);
    }

    do {
	fread (Type, 4, 1, fIn);

	switch (Type [0])
	{
	    case 'F' : /* FILE */
		fread (FName, 40, 1, fIn);

		fOut = fopen (FName, "w");

		if (fOut)
		{
		    printf ("Unpacking '%s'\n", FName);

		    for (;;)
		    {
			fread (Type, 4, 1, fIn); /* may be EFIL */

			if (memcmp (Type, "BLOC", 4) == 0)
			{
			    Sz = GetSize (fIn);
			    fread (Buff, Sz, 1, fIn);
			    fwrite (Buff, Sz, 1, fOut);
			}
			else
			if (memcmp (Type, "EFIL", 4) == 0)
			    break;
			else
			{
			    printf ("Error found %4.4s at %08lX\n", Type, ftell (fIn) - 4);
			    break;
			}
		    }

		    fclose (fOut);
		}
		else
		{
		    printf ("Could not open output file for %s\n", FName);
		    Loop = 0;
		}

		break;

	    case 'E' : /* END. */
		if (memcmp (Type, "END.", 4) == 0)
		{
		    printf ("Unpack successful\n");
		    Loop = 0;
		    break;
		}

	    default : /* Error */
		printf ("Error found %4.4s at %08lX\n", Type, ftell (fIn) - 4);
		Loop = 0;
	}
    } while (Loop);

    fclose (fIn);
}

#endif
