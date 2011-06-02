/////////////////////////////////////////////////////////////////////////////////////////
//
// DLX Assembler - Symbol table header
//
// D. J. Viner

#define STABNAMESIZE    22

typedef struct MacroType
{
    struct  MacroType   *NextM;
    STRPTR  Line;
} *Macro;

typedef struct SymTabType
{
    UBYTE   Name [STABNAMESIZE];// Symbol name
    UBYTE   FName [20];			// File name it was defined in
    ULONG   Val, 				// The value or address of this symbol
    		LineNo;				// Which line it was defined on
    BOOL    SetUp,				// If true then the values have been assigned
    		Mac, 				// This is a macro definition
    		Bumped;				// True if address bumped because of a DoPad
    Macro   M;					// The macro definition
    struct  SymTabType *Left,
    				   *Right;
} *SymTab;


extern SymTab   AddSymbol (STRPTR Sym, ULONG Val, BOOL SetUp);
extern VOID     DeleteSymbolTable ();
extern VOID     DisplaySymbolTable (BOOL NotFound);
extern SymTab   FindSymbol (STRPTR Sym);



