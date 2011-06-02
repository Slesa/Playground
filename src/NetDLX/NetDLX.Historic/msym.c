/*

        DLX MAsm MicroAssembler - Symbol table handling

        D. J. Viner
*/

#include "masm.h"
#include "msym.h"

SymTab SymBase = (SymTab) NULL;

BOOL    DoTitle;

VOID DelSTab (SymTab S)
{
    if (S)
    {
        DelSTab (S->Left);
        DelSTab (S->Right);
        free (S);
    }
}

VOID DeleteSymbolTable ()
{
    if (SymBase)
        DelSTab (SymBase);

      SymBase = (SymTab) NULL;
}

VOID InsertIntoTree (SymTab ST, SymTab S)
{
    WORD  cmp = strcmp (ST->Name, S->Name);


    /* If already exists then copy contents */

    if (cmp == 0)
    {
        /* Copy the data */

        ST->Val = S->Val;
        free (S);
    }
    else
    if (cmp < 0)      /* Go to the right */
    {
        if (!ST->Right)
            ST->Right = S;
        else
            InsertIntoTree (ST->Right, S);
    }
    else              /* Go to the left */
    {
        if (!ST->Left)
            ST->Left = S;
        else
            InsertIntoTree (ST->Left, S);
    }
}

SymTab AddSymbol (STRPTR Sym, ULONG Val, BOOL SetUp)
{
    SymTab  ST, S;


    ST = SymBase;
    S = (SymTab) calloc (1, sizeof (struct SymTabType));

    if (!S)
        Error ("Out of memory (AddSymbol)", FALSE);

    strcpy (S->Name, Sym);

    if (SetUp)
    {
        S->Val = Val;
        S->SetUp = TRUE;
    }

    /* Do the insert */

    if (ST)
        InsertIntoTree (ST, S);
    else
        SymBase = S;

    return S;
}

SymTab FSymbol (SymTab ST, STRPTR Name)
{
    WORD  cmp;


    if (!ST)
        return (SymTab) NULL;

    cmp = strcmp (Name, ST->Name);

    if (cmp == 0)
        return ST;

    if (cmp < 0)
        return FSymbol (ST->Left, Name);
    else
        return FSymbol (ST->Right, Name);
}

SymTab FindSymbol (STRPTR Sym)
{
    return FSymbol (SymBase, Sym);
}

VOID SDisplay (SymTab ST, BOOL NotFound)
{
    UWORD   i;


    if (!ST)
        return;

    SDisplay (ST->Left, NotFound);

    if (NotFound && DoTitle && !ST->SetUp)
    {
        DoTitle = FALSE;
        printf ("\nUnresolved labels\n");
    }

    if (!NotFound || !ST->SetUp)
    {
        printf ("%-10s = %d  Line %d", ST->Name, ST->Val, ST->LineNo);

        if (!NotFound && !ST->SetUp)
            printf ("  <--- Unresolved");

        printf ("\n");

        if (ST->NoRefs)
            for (i = 0; i < ST->NoRefs; i++)
                printf ("  Ref %2d in line %2d\n", i + 1, ST->Refs [i]);
    }

    SDisplay (ST->Right, NotFound);
}

VOID DisplaySymbolTable (BOOL NotFound)
{
    DoTitle = NotFound;
    SDisplay (SymBase, NotFound);
}
