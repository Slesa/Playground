/////////////////////////////////////////////////////////////////////////////////////////
//
// DLX Assembler - Main Header file
//
// D. J. Viner
#include "system.h"

#define LINELEN     130

struct FileDetails
{
    UBYTE   FName [50];
    FILE    *In;
    ULONG   LineNo;
    struct  FileDetails *Prev;
};

extern FILE     *In;                    // Current input file
extern FILE     *Op;                    // Output file
extern FILE     *Lst;                   // Listing file

extern struct   FileDetails *Fd;

extern UBYTE    Pass, Line [LINELEN + 2];
extern UBYTE    FName [50], OName [50];
extern ULONG    Addr, LineNo, Addr2;
extern BOOL     Debug, PutAddr, Listing, MainLoop, ErrorSym, CodeGen,
                DoList, LittleEndian;
extern STRPTR   ptr;
