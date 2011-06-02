/////////////////////////////////////////////////////////////////////////////////////////
//
// DLX Assembler - Main module
//
// D. J. Viner
#include "dasm.h"
#include "dasm2.h"
#include "dsym.h"
#include <time.h>

/////////////////////////////////////////////////////////////////////////////////////////
// In dasmlink.c
/////////////////////////////////////////////////////////////////////////////////////////

extern  int	 	LinkNo;
extern  char	LinkDate [];

/////////////////////////////////////////////////////////////////////////////////////////

FILE	*In;				// Current input file
FILE	*Op;				// Output file
FILE	*Lst;				// Listing file
FILE	*GI;				// Generated Include file

UBYTE   Pass;	   			// 1 or 2, any higher = error

BOOL	MainLoop, ErrorSym = FALSE, CodeGen, Debug = FALSE,
		Listing = FALSE, Symbols = FALSE, DoList = FALSE,
		DebugSave = FALSE, ListSave = FALSE, LittleEndian = FALSE,
		GenInc, OpenedGenInc, GenIncNext, WasSym;

struct  FileDetails *Fd = NULL;

UBYTE   FName [50],			// Current input file name
		OName [50],			// Output file name
		LName [50],			// Listing name
		GName [50],			// Generated include file name
		Line [LINELEN + 2],
		LTitle [52] = " ",
		Months [12][4] = {"Jan", "Feb", "Mar", "Apr", "May", "Jun",
			"Jul", "Aug", "Sep", "Oct", "Nov", "Dec"};


STRPTR  ptr;

ULONG   Addr, Addr2, LineNo;

UWORD   LLine = 0, LPage = 0, LCol;

SymTab	LastST = NULL;

#define LPAGELEN	58
#define MAXMACROPARAMS  15

VOID Assemble ();

/////////////////////////////////////////////////////////////////////////////////////////

/////////////////////////////////////////////////////////////////////////////////////////
// Output a form feed and a listing header
/////////////////////////////////////////////////////////////////////////////////////////

VOID ListHeader ()
{
	if (LPage)
		fputc (12, Lst);

	fprintf (Lst, "<<<< (%s) %s%*d >>>>\n\n", FName, LTitle,
		65 - (strlen (LTitle) + strlen (FName)), ++LPage);

	LLine = 0;
}

/////////////////////////////////////////////////////////////////////////////////////////
// Increment the listing line count and, if we've reached the end of the page then output
// a new page.
/////////////////////////////////////////////////////////////////////////////////////////

VOID IncLine ()
{
	if (++LLine > LPAGELEN)
		ListHeader ();

	LCol = 0;
}

/////////////////////////////////////////////////////////////////////////////////////////
// Output a list line
/////////////////////////////////////////////////////////////////////////////////////////

VOID ListLine ()
{
	if (Listing)
	{
		if (LPage == 0)
			ListHeader ();

		fprintf (Lst, "%08lX :		   %s", Addr, Line);
		IncLine ();
	}
}

/////////////////////////////////////////////////////////////////////////////////////////
//
/////////////////////////////////////////////////////////////////////////////////////////

LONG GetValue (STRPTR *p)
{
	UBYTE   c, Sym [30];
	LONG	Val = 0;
	BOOL	Neg = FALSE;
	SymTab  ST;

	if (**p == '-')	 // Negative
	{
		Neg = TRUE;
		(*p)++;
	}

	if (**p == 0x27)	// Single quote
	{
		(*p)++;

		if (**p == '\n' || **p == 0)
			Val = ' ';
		else
			Val = **p;

		(*p)++;

		if (**p == 0x27)
			(*p)++;
	}
	else
	if (**p == '*')
	{
		(*p)++;
		Val = Addr;
	}
	else
	if (**p == '%')
	{
		(*p)++;

		while (**p == '0' || **p == '1')
		{
			Val = Val * 2 + (**p - '0');
			(*p)++;
		}
	}
	else
	if (**p == '$')
	{
		(*p)++;

		while (isxdigit (**p))
		{
			c = **p;

			if (c >= 'a')
				c -= 32;

			c -= '0';

			if (c > 9)
				c -= 7;

			Val = Val * 16 + c;
			(*p)++;
		}
	}
	else
	if (isdigit (**p))
	{
		while (isdigit (**p))
		{
			Val = Val * 10 + (**p - '0');
			(*p)++;
		}
	}
	else	// Must be a symbol
	{
		*p = CollectSym (*p, Sym);
		ST = FindSymbol (Sym);

		if (!ST)
		{
			ST = AddSymbol (Sym, 0, FALSE);
			Val = 0;
		}
		else
			Val = ST->Val;

		if (!ST->SetUp)
		{
			strcpy (ST->FName, FName);
			ST->LineNo = LineNo;
		}
	}

	if (Neg)
		Val = -Val;

	return Val;
}

/////////////////////////////////////////////////////////////////////////////////////////
//
/////////////////////////////////////////////////////////////////////////////////////////

LONG GetExprValue (STRPTR *p)
{
	LONG	Val = 0, V;
	BOOL	Loop = TRUE, Lower, Upper;
	UBYTE   LastOp = '+';

	SkipWhitespace ();

	do {
		Lower = FALSE;
		Upper = FALSE;

		switch (**p)
		{
			case '<' :
				Lower = TRUE;
				(*p)++;
				break;

			case '>' :
				Upper = TRUE;
				(*p)++;
		}

		V = GetValue (p);

		if (Lower)
			V &= 0xFFFF;

		if (Upper)
			V = ((V >> 16) & 0xFFFF);

		switch (LastOp)
		{
			case '*' :
				Val *= V;
				break;

			case '+' :
				Val += V;
				break;

			case '-' :
				Val -= V;
				break;

			case '/' :
				Val /= V;
				break;

			case '&' :
				Val &= V;
				break;

			case '|' :
				Val |= V;
				break;

			case '%' :
				Val %= V;
				break;

			case '^' :
				Val ^= V;
		}

		switch (**p)
		{
			case '\t' : case ',' : case ' ' : case ';' :
			case '\n' : case ')' : case '(' :
				Loop = FALSE;
				break;

			default :
				LastOp = **p;
				(*p)++;
		}
	} while (Loop);

	return Val;
}

/////////////////////////////////////////////////////////////////////////////////////////
//
/////////////////////////////////////////////////////////////////////////////////////////

VOID GetAddr ()
{
	ULONG   Adr;
	UBYTE   Txt [80];

	ListLine ();

	// Skip the '*'

	ptr++;

	// Move past the assignment bit

	SkipWhitespace ();

	if (*ptr != '=')
	{
		sprintf (Txt, "Unexpected character '%c'", *ptr);
		Error (Txt, TRUE);
	}

	ptr++;

	Adr = GetExprValue (&ptr);

	if (CodeGen && Addr && Adr < Addr)
	{
		sprintf (Txt, "Error: New addr < Old (%08lX/%08lX)", Adr, Addr);
		Error (Txt, TRUE);
	}

	Addr = Adr;
}

/////////////////////////////////////////////////////////////////////////////////////////
// Pad out with bytes to the next long word
/////////////////////////////////////////////////////////////////////////////////////////

VOID DoPad (ULONG Val)
{
	ULONG   Count = Val;

	do {
		if (Count > 1)
			OutputByte (0);

		if (Addr % 4 != 0)
		{
			if (Listing)
			{
				fprintf (Lst, "%08lX : ", Addr);
				LCol = 11;
			}

			while (Addr % 4 != 0)
			{
				OutputByte (0);

				if (Listing)
				{
					fprintf (Lst, "00 ");
					LCol += 3;
				}
			}

			if (Listing)
			{
				fprintf (Lst, "\n");
				IncLine ();
			}
		}

		Count--;

	} while (Count);
}

/////////////////////////////////////////////////////////////////////////////////////////
// Generate an auto-include file header
/////////////////////////////////////////////////////////////////////////////////////////

VOID GenIncHeader ()
{
	struct  tm *T;
	time_t  tt;

	fprintf (stderr, "Creating auto-include file %s\n", GName);

	fprintf (GI, "; Auto-generated include file for %s\n", FName);

	time (&tt);
	T = localtime (&tt);

	fprintf (GI, "; Created: %d %s %4d\n\n", T->tm_mday,
		Months [T->tm_mon], T->tm_year + 1900);

	fprintf (GI, "\tNODEBUG\n");
}

/////////////////////////////////////////////////////////////////////////////////////////
// Handle pseudo codes
/////////////////////////////////////////////////////////////////////////////////////////

VOID Control (ULONG Pseudo)
{
	int	 j;
	UBYTE   Txt [80];
	ULONG   Val;
	BOOL	InQuote;

	if (Debug)
		printf ("\t\t\tPseudo %ld >%s<\n", Pseudo, ptr);

	switch (Pseudo)
	{
		case PSEUDO_DCB : // DC.B
		case PSEUDO_DCH : // DC.H
		case PSEUDO_DCW : // DC.W
			InQuote = FALSE;

			if (Listing)
			{
				fprintf (Lst, "%08lX : ", Addr);
				LCol = 11;
			}

			do {
				if (!InQuote && *ptr == ',')
					ptr++;

				if (*ptr == 0x27 || *ptr == 0x22)
				{
					InQuote = !InQuote;
					ptr++;
				}
				else
				if (*ptr == '\n')
					InQuote = FALSE;
				else
				{
					if (InQuote)
						Val = *ptr++;
					else
						Val = GetExprValue (&ptr);

					if (Listing && LCol > 72)
					{
						fprintf (Lst, "\n");
						IncLine ();
						fprintf (Lst, "%11.11s", " ");
						LCol = 11;
					}

					switch (Pseudo)
					{
						case PSEUDO_DCB :
							OutputByte ((UBYTE) Val);

							if (Listing)
							{
								fprintf (Lst, "%02lX ", Val);
								LCol += 3;
							}

							break;

						case PSEUDO_DCH :
							OutputWord ((UWORD) Val);

							if (Listing)
							{
								fprintf (Lst, "%04lX ", Val);
								LCol += 5;
							}

							break;

						case PSEUDO_DCW :
							OutputLong (Val);

							if (Listing)
							{
								fprintf (Lst, "%08lX ", Val);
								LCol += 9;
							}

							break;
					}
				}

			} while (*ptr == ',' || InQuote);

			if (Listing)
			{
				if (78 - LCol > strlen (Line))
				{
					fprintf (Lst, "\n");
					IncLine ();
					fprintf (Lst, "%11.11s", " ");
					LCol = 11;
				}

				fprintf (Lst, "%s", Line);
				IncLine ();
			}

			if (Pseudo != PSEUDO_DCB)
				DoPad (1);

			break;

		case PSEUDO_DSB : // DS.B
		case PSEUDO_DSH : // DS.H
		case PSEUDO_DSW : // DS.W
			Val = GetExprValue (&ptr);

			for (j = 0; j < Val; j++)
			{
				switch (Pseudo)
				{
					case PSEUDO_DSB :
						OutputByte (0);
						break;

					case PSEUDO_DSH :
						OutputWord (0);
						break;

					case PSEUDO_DSW :
						OutputLong (0L);
				}
			}

			if (Listing)
			{
				fprintf (Lst, "%08lX :		   %s", Addr, Line);
				IncLine ();
			}

			if (Pseudo != PSEUDO_DSB)
				DoPad (1);

			break;

		case PSEUDO_INCLUDE :
			AddFile ();

			if (*ptr == 0x22 || *ptr == 0x27)
				ptr++;

			ptr = CollectSym (ptr, FName);
			LineNo = 0;

			In = fopen (FName, "r");

			if (In)
				break;

			sprintf (Txt, "Could not open <%s>", FName);
			Error (Txt, TRUE);	// Terminate!
			break;

		case PSEUDO_PAGE :
			if (Listing)
			{
				if (*ptr != '\n')
				{
					if (*ptr == 0x27 || *ptr == 0x22)   // Found new title
					{
						ptr++;
						strcpy (LTitle, ptr);
						j = strlen (LTitle);

						while (LTitle [j - 1] != 0x27 &&
							   LTitle [j - 1] != 0x22)
							LTitle [--j] = 0;
					}
				}

				ListHeader ();
			}

			break;

		case PSEUDO_SKIP :
			if (Listing)
			{
				Val = GetExprValue (&ptr);

				for (j = 0; j < Val && LLine; j++)
				{
					fputc (10, Lst);
					IncLine ();
				}
			}

			break;

		case PSEUDO_IFDEF : // EXPERIMENTAL - THESE DON'T WORK YET!!!
		case PSEUDO_IFNDEF :
			Val = GetExprValue (&ptr);

			if ((!Val && Pseudo == PSEUDO_IFDEF) ||
				 (Val && Pseudo == PSEUDO_IFNDEF))
			do {
				if (Debug)
					printf ("\t\t\t#IF(N)DEF");

			} while (GetLine () != ENDCOND);

			break;

		case PSEUDO_END :
			ListLine ();
			MainLoop = FALSE;
			break;

		case PSEUDO_LIST :
			if (Pass == 2)
				Listing = ListSave;

			break;

		case PSEUDO_NOLIST :
			if (Pass == 2)
				Listing = FALSE;

			break;

		case PSEUDO_SYM :
			Symbols = TRUE;
			break;

		case PSEUDO_NOSYM :
			Symbols = FALSE;
			break;

		case PSEUDO_DEBUG :
			Debug = DebugSave;
			break;

		case PSEUDO_NODEBUG :
			Debug = FALSE;
			break;

		case PSEUDO_PAD :
			if (*ptr > ' ')
			{
				Val = GetExprValue (&ptr);

				if (!Val)
					Val = 1;
			}
			else
				Val = 1;

			DoPad (Val);
			break;

		case PSEUDO_GENINC :
			if (Pass == 2)
			{
				if (!OpenedGenInc)
				{
					GI = fopen (GName, "w");

					if (GI)
					{
						OpenedGenInc = TRUE;
						GenIncHeader ();
					}
				}

				GenIncNext = TRUE;
			}
	}
}

/////////////////////////////////////////////////////////////////////////////////////////
// Look for standard OpCode
/////////////////////////////////////////////////////////////////////////////////////////

int LookForOp ()
{
	UBYTE   c, Op [12];
	UWORD   i = 0, j, k, l;
	STRPTR  p = ptr;

	c = *ptr;

	if (islower (c))
		c = toupper (c);

	if (c < 'A' || c > 'Z')
	{
		if (Debug)
			printf ("\t\t\tLookForOp - First chr not A-Z\n");

		return 0;
	}

	j = c - 'A';
	k = Offsets [j][0];
	l = Offsets [j][1];

	if (!l)
	{
		if (Debug)
			printf ("\t\t\tLookForOp - Not valid first chr in A-Z\n");

		return 0;
	}

	// Allow '2' and '.' as well as all alpha chrs

	while ((isalpha (*p) || *p == '2' || *p == '.') && i < 10)
	{
		Op [i++] = *p;
		p++;
	}

	if (i > 8)
	{
		if (Debug)
			printf ("\t\t\tLookForOp - Len > 8\n");

		return 0;
	}

	Op [i] = ' ';
	Op [i + 1] = 0;

	for (i = k; i < k + l; i++)
		if (stricmp (Op, OpCodes [i].Op) == 0)
		{
			if (Debug)
				printf ("\t\t\tLookForOp - Found %s\n", Op);

			return i + 1;
		}

	if (Debug)
		printf ("\t\t\tLookForOp - Not Found\n");

	return 0;
}

/////////////////////////////////////////////////////////////////////////////////////////
// Check for a register type
/////////////////////////////////////////////////////////////////////////////////////////

BOOL CheckReg (ULONG *Val)
{
	BOOL	Reg = FALSE;
	ULONG   Ind;

	switch (*ptr)
	{
		case 'r' : case 'R' :
		case 'f' : case 'F' :
		case 's' : case 'S' :
			if (isdigit (*(ptr + 1)))
			{
				Ind = atol (ptr + 1);

				if (Ind < 32)
				{
					Reg = TRUE;
					*Val = Ind;
				}
			}
	}

	return Reg;
}

/////////////////////////////////////////////////////////////////////////////////////////
// Get an operand
/////////////////////////////////////////////////////////////////////////////////////////

UBYTE GetOperand (ULONG *Val)
{
	UBYTE   Type = ' ';

	if (Debug)
		printf ("\t\t\tGetOperand at >%s<\n", ptr);

	*Val = 0;

	if (*ptr == 0 || *ptr == '\n')
		return ' ';

	if (*ptr == ',')
		ptr++;

	if (CheckReg (Val))
	{
		Type = *ptr;

		if (islower (Type))
			Type = toupper (Type);
	}
	else
	if (isalnum (*ptr) || *ptr == '-')
	{
		*Val = GetExprValue (&ptr);
		Type = 'Q';
	}
	else
	switch (*ptr)
	{
		case '(' : // Register + offset
			ptr++;
			Type = GetOperand (Val);
			ptr++;  // Skip final ')' - NEEDS CHECKING
			break;

		case '#' : // Immediate value/label
			Type = 'I';
			ptr++;
			*Val = GetExprValue (&ptr);
			break;

		case '$' : // Hex value
			*Val = GetExprValue (&ptr);
			Type = 'Q';
	}

	SkipToWhitespace ();

	if (Type == 'Q' && *ptr == '(')
		Type = 'O';

	if (Debug)
		printf ("\t\t\tGetOperand out Type=%c Val = %ld\n", Type, *Val);

	return Type;
}

/////////////////////////////////////////////////////////////////////////////////////////
// Expand a macro call into the full set of instructions
/////////////////////////////////////////////////////////////////////////////////////////

VOID ExpandMacro (SymTab ST)
{
	UBYTE   Params [MAXMACROPARAMS][20], Txt [50];
	WORD	PCount = 0, i = 0, j, k;
	Macro   M;

	// First get the parameters

	while (*ptr != '\n' && *ptr != ' ' && *ptr != '\t' &&
		   PCount < MAXMACROPARAMS)
	{
		if (*ptr == ',')
		{
			Params [PCount][i] = 0;

			if (Debug)
				printf ("1Found macro parameter (%d) >%s<\n",
					PCount, Params [PCount]);

			i = 0;
			PCount++;
			ptr++;
		}
		else
		{
			if (i < 19)
				Params [PCount][i] = *ptr;

			i++;
			ptr++;
		}
	}

	Params [PCount][i] = 0;

	if (Debug)
		printf ("2Found macro parameter (%d) >%s<\n",
			PCount, Params [PCount]);

	M = ST->M;

	while (M)
	{
		// Substitute parameters into line

		j = k = 0;
		memset (Line, 0, sizeof (Line));

		if (Debug)
			printf ("Processing macro line: %s", M->Line);

		while (M->Line [j])
		{
			if (M->Line [j] == '\\')
			{
				j++;
				i = atoi (&(M->Line [j])) - 1;

				if (i > PCount || i < 0)
				{
					sprintf (Txt, "Invalid parameter reference no. %d in macro", i + 1);
					Error (Txt, TRUE);
				}

				strcat (Line, Params [i]);
				k = strlen (Line);

				while (isdigit (M->Line [j]))
					j++;
			}
			else
				Line [k++] = M->Line [j++];
		}

		if (Debug)
			printf ("Subst line: %s", Line);

		ptr = &Line [0];

		SkipWhitespace ();

		switch (*ptr)
		{
			case 0	:
			case ';'  :
			case '/'  :
			case '\n' :
			case '>' :
				ListLine ();
				break;

			case '*'  :
				GetAddr ();
				break;

			default :
				Assemble ();
		}

		M = M->NextM;
	}
}

/////////////////////////////////////////////////////////////////////////////////////////
// Check for a local symbol (start with $)
/////////////////////////////////////////////////////////////////////////////////////////

#ifdef DOLOCAL
BOOL CheckLocalSymbol (STRPTR S)
{
	WORD	l = strlen (S) - 1;

	if (S [l] == '$')
		return TRUE;

	return FALSE;
}
#endif

/////////////////////////////////////////////////////////////////////////////////////////
// On pass 1 allocate new symbols, check that are not defined more than once. On pass 2
// check symbols have retained their original values (unless bumped by DoPad calls), see
// if they were never defined, and output their values into GenInc files.
/////////////////////////////////////////////////////////////////////////////////////////

BOOL HandleSymbol ()
{
	SymTab  ST;
	UBYTE   Txt [80], Sym [STABNAMESIZE];
	STRPTR  p;

	p = ptr;
	p = CollectSym (p, Sym);

#ifdef DOLOCAL
	// LOCAL SYMBOLS NOT IMPLEMENTED YET

	// Check for local symbol

	if (CheckLocalSymbol (Sym))
		;
	else
	{
		// Clear local symbols

		// ...
#endif
		// Search for symbol

		ST = FindSymbol (Sym);
#ifdef DOLOCAL
	}
#endif

	SkipToWhitespace ();

	// Skip any following ':'

	if (*ptr == ':')
		ptr++;

	SkipWhitespace ();
	WasSym = TRUE;

	if (!ST)	// New symbol
	{
		if (Pass == 1)
		{
			if (strlen (Sym) > 0)
			{
				ST = AddSymbol (Sym, Addr, TRUE);
				LastST = ST;
				ST->LineNo = LineNo;
				strcpy (ST->FName, FName);

				// See what type of symbol this represents

				if (strnicmp (ptr, "MACRO", 5) == 0)	// Set up a macro
					CopyMacro (ST);
				else
				if (strnicmp (ptr, "EQU", 3) == 0)	  // EQUate
				{
					SkipToWhitespace ();
					SkipWhitespace ();
					ST->Val = GetExprValue (&ptr);
				}
				else
				if (*ptr == '*')						// Address
					GetAddr ();
				else
					return TRUE;	// Carry on loop in Assemble
			}

			return FALSE;
		}
		else
		{
			sprintf (Txt, "Symbol %s not found on second pass", Sym);
			Error (Txt, TRUE);
		}
	}
	else
	{
		LastST = ST;

		if (ST->Mac)	// Found a macro symbol
		{
			if (Debug)
				printf ("\t\t\tFound Macro %s\n", ST->Name);

			// Expand a macro if not the definition

			if (strnicmp (ptr, "MACRO", 5) == 0)
			{
				if (Debug)
					printf ("\t\t\tIgnoring MACRO on 2nd pass: %s\n",
						ST->Name);

				while (1)
				{
					GetLine ();

					if (strnicmp (ptr, "ENDM", 4) == 0)
						break;

					ListLine ();
				}
			}
			else
			{
				ExpandMacro (ST);
				return FALSE;
			}
		}
		else
		if (Pass == 1)
		{
			// Warn of multiple symbol definition

			if (ST->SetUp)
			{
				sprintf (Txt, "Warning: Multiple definition of Symbol <%s>\n", Sym);
				Warn (Txt, TRUE);
			}

			ST->Val = Addr;
			ST->SetUp = TRUE;

			if (!ST->LineNo)
				ST->LineNo = LineNo;

			strcpy (ST->FName, FName);

			if (Debug)
				printf ("\t\t\tSetting Symbol '%s' to %04X\n",
					ST->Name, ST->Val);
		}
		else	// Pass 2 - check for changed values on non-macros/EQUates
		{
			// Ignore EQUs on Pass 2

			if (memcmp (ptr, "EQU", 3) == 0)
				return FALSE;

			// Changed values

			if (ST->Val != Addr && !ST->Bumped)
			{
				sprintf (Txt, "Symbol %s changed value between passes (%04X/%04X)",
					Sym, ST->Val, Addr);

				Error (Txt, TRUE);
			}

			if (GenIncNext)
			{
				fprintf (GI, "\t%s\t", ST->Name);

				if (strlen (ST->Name) < 16)
					fprintf (GI, "\t");

				if (strlen (ST->Name) < 8)
					fprintf (GI, "\t");

				fprintf (GI, "EQU\t$%08lX\n", ST->Val);
				GenIncNext = FALSE;
			}
		}
	}

	return TRUE;
}

/////////////////////////////////////////////////////////////////////////////////////////
// Output register types for error reporting
/////////////////////////////////////////////////////////////////////////////////////////

VOID SetOps (STRPTR Txt, STRPTR Ops)
{
	WORD	i;

	for (i = 0; i < 3; i++)
	{
		switch (Ops [i])
		{
			case 'R' :
				strcat (Txt, "Reg");
				break;

			case 'F' :
				strcat (Txt, "FReg");
				break;

			case 'f' :
				strcat (Txt, "DReg");
				break;

			case 'I' :
				strcat (Txt, "Imm");
				break;

			case 'Q' :
				strcat (Txt, "Offset");
				break;

			case 'O' :
				strcat (Txt, "Offset()");
				break;

			case 'S' :
				strcat (Txt, "Special");
		}

		if (i < 2 && Ops [i + 1] > ' ')
			strcat (Txt, ",");
	}
}

/////////////////////////////////////////////////////////////////////////////////////////
// Process a single opcode
/////////////////////////////////////////////////////////////////////////////////////////

VOID ProcessOpcode (WORD Opcode)
{
	WORD	i;
	ULONG   v [3], Instr;
	UBYTE   Type [4], Txt [80];
	LONG	Offset;
	ULONG	adr = Addr;

	Opcode--;

	SkipToWhitespace ();
	SkipWhitespace ();

	if (OpCodes [Opcode].Type == 'P')   // Pseudocodes
	{
		Control (OpCodes [Opcode].OpCode);
		return;
	}

	DoPad (1);

	// Check for the above DoPad changing the address where we have just
	// processed a symbol and update the address held by the symbol

	if (adr != Addr && WasSym)
	{
		LastST->Val = Addr;
		LastST->Bumped = TRUE;
	}

	Type [0] = GetOperand (&v [0]);
	Type [1] = GetOperand (&v [1]);
	Type [2] = GetOperand (&v [2]);

	if (Debug)
		printf ("\t\t\tType is '%3.3s', %ld %ld %ld\n",
			Type, v [0], v [1], v [2]);

	if (strnicmp (Type, OpCodes [Opcode].Mask, 3) != 0 && Pass == 1)
	{
		Warn ("Wrong operands found\n", TRUE);

		sprintf (Txt, "Expected <%3.3s>, found <%3.3s>\n",
			OpCodes [Opcode].Mask, Type);

		strcpy (Txt, "Expected ");
		SetOps (Txt, OpCodes [Opcode].Mask);
		strcat (Txt, " - found ");
		SetOps (Txt, Type);
		strcat (Txt, "\n");
		Warn (Txt, FALSE);
		return;
	}

	// Adjust incorrect double precision reg numbers

	for (i = 0; i < 3; i++)
	{
		if (OpCodes [Opcode].Mask [i] == 'F' && (v [i] % 2) == 1)
		{
			if (Pass == 1)
				Warn ("Invalid reg no. for double precision (adjusted - 1)", TRUE);

			v [i]--;
		}
	}

	if (Pass == 2)
	{
		Instr = OpCodes [Opcode].OpCode;

		for (i = 0; i < 3; i++)
		{
			switch (OpCodes [Opcode].Type)
			{
				case 'I' :
					switch (OpCodes [Opcode].Mask [i + 3])
					{
						case 'd' :
							Instr |= ((v [i] & 0x1F) << 16);
							break;

						case '1' :
							Instr |= ((v [i] & 0x1F) << 21);
							break;

						case 'o' :
						case 'i' :
							Instr |= (v [i] & 0xFFFF);
					}

					break;

				case 'B' :
					switch (OpCodes [Opcode].Mask [i + 3])
					{
						case '1' :
							Instr |= ((v [i] & 0x1F) << 21);
							break;

						case 'o' :
							Offset = (LONG) (v [i] - Addr - 4);
							Instr |= (Offset & 0xFFFF);
					}

					break;

				case 'R' :
					switch (OpCodes [Opcode].Mask [i + 3])
					{
						case 'd' :
							Instr |= ((v [i] & 0x1F) << 11);
							break;

						case '1' :
							Instr |= ((v [i] & 0x1F) << 21);
							break;

						case '2' :
							Instr |= ((v [i] & 0x1F) << 16);
					}

					break;

				case 'J' :
					switch (OpCodes [Opcode].Mask [i + 3])
					{
						case 'o' :
							Offset = (LONG) (v [i] - Addr - 4);
							Instr |= (Offset & 0x3FFFFFF);
					}

					break;

				case 'T' :
					switch (OpCodes [Opcode].Mask [i + 3])
					{
						case 'o' :
							Instr |= (v [i] & 0x3FFFFFF);

							if (Instr & 0x3)
							{
								Warn ("TRAP value not long word aligned (DAsm has adjusted it)", TRUE);
								Instr &= 0xFFFFFFFC;
							}
					}
			}
		}

		if (Listing)
		{
			if (LPage == 0)
				ListHeader ();

			fprintf (Lst, "%08lX : %08lX ", Addr, Instr);
		}

		if (Listing)
		{
			if (strlen (Line) > 61)
			{
				fprintf (Lst, "\n");
				IncLine ();
				fprintf (Lst, "	 ");
			}

			fprintf (Lst, Line);
			IncLine ();
		}
	}

	OutputLong (Instr);
}

/////////////////////////////////////////////////////////////////////////////////////////
// Assemble a line
/////////////////////////////////////////////////////////////////////////////////////////

VOID Assemble ()
{
	WORD	Opcode, Count = 0;
	BOOL	Loop = TRUE;

	WasSym = FALSE;

	do {
		Opcode = LookForOp ();

		if (Opcode)
		{
			if (++Count == 1)   // Only allow one opcode per line
				ProcessOpcode (Opcode);

			Loop = FALSE;
		}
		else	// It's a symbol of some sort?
		{
			WasSym = FALSE;

			switch (*ptr)
			{
				case ';' : case '\n' : case 0 :
					Loop = FALSE;
					break;

				default :
					Loop = HandleSymbol ();
			}
		}

	} while (Loop);
}

/////////////////////////////////////////////////////////////////////////////////////////
// Process a single file
/////////////////////////////////////////////////////////////////////////////////////////

VOID ProcessFile (STRPTR File, BOOL List)
{
	WORD	l, i;
	BOOL	Found = FALSE;

	strcpy (FName, File);
	l = strlen (FName);

	for (i = 0; i < l; i++)
		if (FName [i] == '.')
			Found = TRUE;

	if (!Found)
		strcat (FName, ".a");

	fprintf (stderr, "Processing %s\n", FName);

	// Generate output name

	strcpy (OName, FName);
	strcpy (GName, FName);
	l = strlen (OName);

	while (l && OName [l - 1] != '.')
	{
		OName [l - 1] = 0;
		GName [l - 1] = 0;
		l--;
	}

	if (strlen (OName) == 0)
	{
		strcpy (OName, FName);
		strcat (OName, ".o");
		strcpy (GName, FName);
		strcat (GName, ".i");
	}
	else
	{
		strcat (OName, "o");
		strcat (GName, "i");
	}

	GenInc = FALSE;
	OpenedGenInc = FALSE;
	GenIncNext = FALSE;

	// Do the assembly

	for (Pass = 1; Pass < 3; Pass++)
	{
		fprintf (stderr, "Pass %d\n", Pass);

		In = fopen (FName, "r");

		if (In)
		{
			MainLoop = TRUE;
			CodeGen = FALSE;
			LineNo = 0;
			Addr = 0;
			Addr2 = 0;

			if (Pass == 2)
			{
				Listing = List;

				Op = fopen (OName, "wb");

				if (!Op)
					Error ("Could not open output file", FALSE);
			}

			// Loop until no more input

			do {
				switch (GetLine ())
				{
					case COMMENT :
						ListLine ();
						break;

					case ENDCOND :
						break;

					case VALID :
						Assemble ();
						break;

					case SETADDR :
						GetAddr ();
				}
			} while (MainLoop);

			fclose (In);

			if (Pass == 2)
			{
				fclose (Op);

				if (OpenedGenInc)
				{
					fprintf (GI, "\n\tDEBUG\n\n\n");
					fclose (GI);
				}
			}
		}
		else
		{
			fprintf (stderr, "Could not open %s\n", FName);
			Pass = 3;
		}

		if (Pass == 1)
		{
			if (!Symbols)
				DisplaySymbolTable (TRUE);
			else
			{
				printf ("\nSymbol table for %s\n\n", FName);
				DisplaySymbolTable (FALSE);
				printf ("\n\nEnd of symbol table\n");
			}
		}
	}

	DeleteSymbolTable ();
}

/////////////////////////////////////////////////////////////////////////////////////////
// Main
/////////////////////////////////////////////////////////////////////////////////////////

int main (int argc, char **argv)
{
	ULONG   i;
	BOOL	List = FALSE, Done = FALSE, FList = FALSE;
	UBYTE   LI [4], Name [92];
	FILE	*FL;

	// Assign Lst here because gcc can't handle it where Lst is defined!

	Lst = stdout;

	i = 0x01020304;
	memcpy (LI, &i, 4);

	if (LI [0] == 4)
		LittleEndian = TRUE;

	fprintf (stderr, "\nDLX Assembler, Version %s.%d (%s)\n", Version, LinkNo, LinkDate);

	if (LittleEndian)
		fprintf (stderr, ">>> Little Endian System <<<\n");

	if (argc == 1)
	{
		printf ("Usage: %s [params] <Filename>\n", argv [0]);
		printf ("  params:  -d      Turn on debug\n");
		printf ("           -e      Display symbol table on error\n");
		printf ("           -fxxx   File xxx contains list to assemble\n");
		printf ("           -l<xxx> Turn on output listing (to file xxx)\n");
		printf ("           -s      Output Symbol table\n");
		exit (0);
	}

	for (i = 1; i < argc; i++)
	{
		if (argv [i][0] == '-')
		{
			switch (argv [i][1])
			{
				case 'd' :
					Debug = TRUE;
					DebugSave = TRUE;
					break;

				case 'e' :
					ErrorSym = TRUE;
					break;

				case 'f' :
					FL = fopen (&argv [i][2], "r");

					if (FL)
						FList = TRUE;

					break;

				case 'l' :
					List = TRUE;
					ListSave = TRUE;

					if (argv [i][2] > ' ')
					{
						strcpy (LName, &argv [i][2]);
						Lst = fopen (LName, "w");

						if (!Lst)
							Lst = stdout;
						else
							DoList = TRUE;
					}

					break;

				case 's' :
					Symbols = TRUE;
					break;
			}
		}
	}

	// Initialise the array of name pointers

	SetOffsets ();

	// Process file containing list of files

	if (FList)
	{
		while (!feof (FL))
		{
			Name [0] = 0;
			fgets (Name, 90, FL);

			if (strlen (Name))
			{
				Name [strlen (Name) - 1] = 0;   // Strip of CR
				ProcessFile (Name, List);
				Done = TRUE;
			}
		}

		fclose (FL);
	}

	// Process each command line file

	for (i = 1; i < argc; i++)
	{
		if (argv [i][0] != '-')
		{
			ProcessFile (argv [i], List);
			Done = TRUE;
		}
	}

	if (!Done)
		fprintf (stderr, "No input files specified\n");

	if (DoList)
		fclose (Lst);

	return 0;
}

/////////////////////////////////////////////////////////////////////////////////////////


