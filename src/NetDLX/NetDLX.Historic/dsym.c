/////////////////////////////////////////////////////////////////////////////////////////
//
// DLX Assembler - Symbol table handling
//
// D. J. Viner
/////////////////////////////////////////////////////////////////////////////////////////

#include "dasm.h"
#include "dsym.h"

SymTab SymBase = (SymTab) NULL;

BOOL    DoTitle;

/////////////////////////////////////////////////////////////////////////////////////////
// Delete a macro
/////////////////////////////////////////////////////////////////////////////////////////

VOID DelMacro (Macro M)
{
    if (M->NextM)
        DelMacro (M->NextM);

    free (M->Line);
    free (M);
}

/////////////////////////////////////////////////////////////////////////////////////////
// Delete a node (and all of its children) in the symbol table
/////////////////////////////////////////////////////////////////////////////////////////

VOID DelSTab (SymTab S)
{
    if (S)
    {
        DelSTab (S->Left);
        DelSTab (S->Right);

        if (S->M)
            DelMacro (S->M);

        free (S);
    }
}

/////////////////////////////////////////////////////////////////////////////////////////
// Delete entire table
/////////////////////////////////////////////////////////////////////////////////////////

VOID DeleteSymbolTable ()
{
    if (SymBase)
        DelSTab (SymBase);

      SymBase = (SymTab) NULL;
}

/////////////////////////////////////////////////////////////////////////////////////////
// Insert a node into the tree (if found then replace with new contents)
/////////////////////////////////////////////////////////////////////////////////////////

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

/////////////////////////////////////////////////////////////////////////////////////////
// Add a symbol to the tree
/////////////////////////////////////////////////////////////////////////////////////////

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

    if (Debug)
        printf ("\t\t\tAddSymbol '%s' %d/%X\n", S->Name, S->Val, S->Val);

    /* Do the insert */

    if (ST)
        InsertIntoTree (ST, S);
    else
        SymBase = S;

    return S;
}

/////////////////////////////////////////////////////////////////////////////////////////
// Find a symbol in the tree and return it
/////////////////////////////////////////////////////////////////////////////////////////

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

    return FSymbol (ST->Right, Name);
}

/////////////////////////////////////////////////////////////////////////////////////////
// Find a symbol (stub)
/////////////////////////////////////////////////////////////////////////////////////////

SymTab FindSymbol (STRPTR Sym)
{
    if (Debug)
        printf ("\t\t\tFindSymbol '%s'\n", Sym);

    return FSymbol (SymBase, Sym);
}

/////////////////////////////////////////////////////////////////////////////////////////
// Display a node of the tree
/////////////////////////////////////////////////////////////////////////////////////////

VOID SDisplay (SymTab ST, BOOL NotFound)
{
    Macro   M;

    if (!ST)
        return;

    SDisplay (ST->Left, NotFound);

    if (NotFound && DoTitle && !ST->SetUp)
    {
        DoTitle = FALSE;
        printf ("\nUnresolved symbols\n");
    }

    if (!NotFound || !ST->SetUp)
    {
        if (ST->Mac)
        {
            printf ("%-13s = MACRO     (%s/%d)\n",
                ST->Name, ST->FName, ST->LineNo);

            M = ST->M;

            while (M)
            {
                printf ("\t\t  %s", M->Line);
                M = M->NextM;
            }

            printf ("\t\tENDM\n\n");
        }
        else
        {
            printf ("%-13s = %08lX", ST->Name, ST->Val);

            if (!NotFound && !ST->SetUp)
                printf ("  <--- Unresolved");

            printf ("  (%s/%d)\n\n", ST->FName, ST->LineNo);
        }
    }

    SDisplay (ST->Right, NotFound);
}

/////////////////////////////////////////////////////////////////////////////////////////
// Display the entire symbol table
/////////////////////////////////////////////////////////////////////////////////////////

VOID DisplaySymbolTable (BOOL NotFound)
{
    DoTitle = NotFound;
    SDisplay (SymBase, NotFound);
}

/////////////////////////////////////////////////////////////////////////////////////////


