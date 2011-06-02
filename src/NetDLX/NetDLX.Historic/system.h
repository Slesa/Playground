/*

        Global System header - defines standard types and calls
        main includes

        D. J. Viner
*/

#include <stdio.h>
#include <stdlib.h>
#include <ctype.h>
#include <string.h>

#include "machine.h"

#define Version "1.5"

#ifdef  AMIGA
#include <exec/types.h>
#include <dos.h>
#else

/*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/
/* Define some standard types for non-Amiga systems                       */
/*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/

#define VOID            void

typedef long            LONG;       /* signed 32-bit quantity */
typedef unsigned long   ULONG;      /* unsigned 32-bit quantity */
typedef short           WORD;       /* signed 16-bit quantity */
typedef unsigned short  UWORD;      /* unsigned 16-bit quantity */
typedef char            BYTE;       /* signed 8-bit quantity */
typedef unsigned char   UBYTE;      /* unsigned 8-bit quantity */
typedef unsigned char  *STRPTR;     /* string pointer (NULL terminated) */
typedef short           BOOL;
typedef float           FLOAT;
typedef double          DOUBLE;

#define TRUE            1
#define FALSE           0


#ifdef  NULL
#undef  NULL
#endif

#define NULL            0L

#endif


/*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/
/* For Unix gcc (and other) compilers which don't have SASC/Turbo C style */
/* case insensitive string compare routines                               */
/*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/

#ifdef UNIX
#define stricmp         strcasecmp
#define strnicmp        strncasecmp
#endif

#ifdef Pc
#include <bios.h>
#endif

#ifdef MAC
/* The Mac code does not require case insensitive compares (yet!) */
#define stricmp strcmp
#endif


