/*

        DLX MAsm MicroAssembler - Symbol table header

        D. J. Viner

        Updates
1.0.0   27.07.1995    	Created from sym.c

*/

#define STABNAMESIZE    22
#define MAXREFS         128

typedef struct SymTabType
{
    UBYTE   Name [STABNAMESIZE];
    UWORD   LineNo;
    UBYTE   Val, NoRefs;
    UBYTE   Refs [MAXREFS];
    BOOL    SetUp;
    struct  SymTabType *Left, *Right;
} *SymTab;


extern SymTab   AddSymbol (STRPTR Sym, ULONG Val, BOOL SetUp);
extern VOID     DeleteSymbolTable ();
extern VOID     DisplaySymbolTable (BOOL NotFound);
extern SymTab   FindSymbol (STRPTR Sym);
extern SymTab   SymBase;


