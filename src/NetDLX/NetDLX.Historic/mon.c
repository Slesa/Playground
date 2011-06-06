/*
		DLX RISC Simulator - Monitor Source Module 1

		D. J. Viner
*/

#include "cpu.h"
#include "dis.h"
#include "mon.h"
#include "io.h"

ULONG   LastAddr = 0;

BOOL CheckSize (WORD Size)
{
	if (Size == 1 || Size == 2 || Size == 4)
		return TRUE;

	printf ("Invalid value size (must be 1, 2 or 4)\n");
	return FALSE;
}

/*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/
/* Enter words into memory until blank input found                        */
/*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/

VOID EnterMemory (STRPTR Cmd, BOOL Playback, BOOL Record, FILE *Rec)
{
	ULONG   Addr, Val;
	WORD    j, Size, p = 1;
	UBYTE   Str [162];
	BOOL    Blank;


	Size = ExtractNo (Cmd, &p, &Blank);

	if (Blank || !CheckSize (Size))
		return;

	Addr = ExtractNo (Cmd, &p, &Blank);

	if (Blank)
		Addr = LastAddr;

	do {
		printf ("%08lX (", Addr);

		switch (Size)
		{
			case 1 :
				printf ("%02X", FetchB (Addr));
				break;

			case 2 :
				printf ("%04X", FetchW (Addr));
				break;

			case 4 :
				printf ("%08lX", FetchL (Addr));
		}

		printf (") > ");

		if (Playback)
		{
			fgets (Str, 160, Rec);
			printf ("%s", Str);
		}
		else
			fgets (Str, 160, stdin);

		if (Record)
			fputs (Str, Rec);

		if (strlen (Str) > 1)
		{
			for (j = 0; j < strlen (Str); j++)
				if (islower (Str [j]))
					Str [j] = toupper (Str [j]);

			if (Str [0] == '-')
				Addr -= Size;
			else
			if (Str [0] == '$')
			{
				p = 1;
				Addr = ExtractNo (Str, &p, &Blank);

				if (Blank)
					Addr = LastAddr;
			}
			else
			{
				p = 0;
				Val = ExtractNo (Str, &p, &Blank);

				if (!Blank)
				{
					switch (Size)
					{
						case 1 :
							StoreB (Addr, (UBYTE) Val);
							break;

						case 2 :
							StoreW (Addr, (UWORD) Val);
							break;

						case 4 :
							StoreL (Addr, Val);
					}
				}

				Addr += Size;
				LastAddr = Addr;
			}
		}

	} while (Str [0] != '\n');

	LastAddr = Addr;
}

/*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/
/* Fill memory with byte, word, long value                                */
/*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/

VOID FillMemory (STRPTR Cmd)
{
	ULONG   Start, End, Val, i;
	WORD    Size, Pos = 1;
	BOOL    Blank;


	Size = ExtractNo (Cmd, &Pos, &Blank);

	if (Blank || !CheckSize (Size))
		return;

	Start = ExtractNo (Cmd, &Pos, &Blank);

	if (Blank)
	{
		printf ("Start address missing\n");
		return;
	}

	End = ExtractNo (Cmd, &Pos, &Blank);

	if (Blank)
	{
		printf ("End address missing\n");
		return;
	}

	Val = ExtractNo (Cmd, &Pos, &Blank);

	if (Blank)
	{
		printf ("Value missing\n");
		return;
	}

	for (i = Start; i <= End; i += Size)
	{
		switch (Size)
		{
			case 1 :
				StoreB (i, Val);
				break;

			case 2 :
				StoreW (i, Val);
				break;

			case 4 :
				StoreL (i, Val);
		}
	}
}

/*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/
/* Dump a block of memory                                                 */
/*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/

VOID MemoryDump (STRPTR Cmd)
{
	ULONG   Start, End, i, j;
	UBYTE   Str [16], Val;
	BOOL    Blank;
	WORD    Pos = 1;


	Start = ExtractNo (Cmd, &Pos, &Blank);

	if (!Blank)
		End = ExtractNo (Cmd, &Pos, &Blank);
	else
		Start = LastAddr;

	if (Blank || End < Start)
		End = Start + 127;

	for (i = Start; i <= End; i += 16)
	{
		printf ("%08lX", i);
		printf (" : ");

		for (j = 0; j < 16; j++)
		{
			Val = FetchB (i + j);
			printf ("%02X", Val);

			if ((j + 1) % 4 == 0)
				putchar (' ');

			if (Val < 32 || Val > 126)
				Str [j] = '.';
			else
				Str [j] = Val;
		}

		printf (": %16.16s\n", Str);
	}

	LastAddr = i;
}

VOID OnOff (ULONG Flag)
{
	if (DebugLevel & Flag)
		printf ("<<ON>>\n");
	else
		printf ("off\n");
}

VOID SetDebugLevel (STRPTR Cmd)
{
	WORD    i;


	for (i = 1; Cmd [i] > 31; i++)
	{
		switch (Cmd [i])
		{
			case 'D' :
				DebugLevel |= DB_DISASSEM;
				break;

			case 'P' :
				DebugLevel |= DB_PIPEREGS;
				break;

			case 'M' :
				DebugLevel |= DB_MICROCODE;
				break;

			case 'S' :
				DebugLevel |= DB_PIPESTAGES;
				break;

			case 'R' :
				DebugLevel |= DB_REGS;
				break;

			case 'E' :
				DebugLevel |= DB_EXONLY;
				break;

			case 'd' :
				DebugLevel &= ~DB_DISASSEM;
				break;

			case 'p' :
				DebugLevel &= ~DB_PIPEREGS;
				break;

			case 'm' :
				DebugLevel &= ~DB_MICROCODE;
				break;

			case 's' :
				DebugLevel &= ~DB_PIPESTAGES;
				break;

			case 'e' :
				DebugLevel &= ~DB_EXONLY;
				break;

			case 'r' :
				DebugLevel &= ~DB_REGS;
		}
	}

	printf ("Debug settings:\n\tD  Disassemble DLX Code    ");
	OnOff (DB_DISASSEM);
	printf ("\tM  Disassemble Microcode   ");
	OnOff (DB_MICROCODE);
	printf ("\tR  Display Registers       ");
	OnOff (DB_REGS);
	printf ("\tS  Display Pipeline Stages ");
	OnOff (DB_PIPESTAGES);
	printf ("\tE  Display only EX Stage   ");
	OnOff (DB_EXONLY);
	printf ("\tP  Display Pipeline Regs   ");
	OnOff (DB_PIPEREGS);
}

VOID SetPipelineControl (STRPTR Cmd)
{
	WORD    i;


	for (i = 1; Cmd [i] > 31; i++)
	{
		if (islower (Cmd [i]))
			Cmd [i] = toupper (Cmd [i]);

		switch (Cmd [i])
		{
			case 'B' : /* Branch */
				dlx.Type &= 0xFFF0FFFF;
				dlx.Type |= ((atol (&Cmd [++i]) & 3) << 16);
				break;

			case 'F' : /* Forwarding */
				dlx.Type &= 0xFF0FFFFF;
				dlx.Type |= ((atol (&Cmd [++i]) & 3) << 20);
				break;

			case 'L' : /* Load Hazard */
				dlx.Type &= 0xF0FFFFFF;
				dlx.Type |= ((atol (&Cmd [++i]) & 3) << 24);
		}
	}

	switch (BRANCHTYPE)
	{
		case BR_NONE :
			printf ("\tB0  No Branch hazard control\n");
			break;

		case BR_STALL :
			printf ("\tB1  Stall on branch hazards\n");
			break;

		case BR_EARLY :
			printf ("\tB2  Early branch detection in ID enabled\n");
			break;

		case BR_DELAY :
			printf ("\tB3  Early branch detection plus branch delay slot\n");
	}

	switch (FEEDTYPE)
	{
		case FW_NONE :
			printf ("\tF0  No register forwarding or detection\n");
			break;

		case FW_STALL :
			printf ("\tF1  Stall on register data hazard\n");
			break;

		case FW_FEED :
			printf ("\tF2  Feed register values back to ID stage\n");
	}

	switch (LOADTYPE)
	{
		case LD_NONE :
			printf ("\tL0  No load hazard detection\n");
			break;

		case LD_HAZARD :
			printf ("\tL1  Load hazard enabled (stalls pipeline - note: to work\n");
			printf ("\t    correctly also turn on feed forwarding)\n");
	}
}

BOOL SetPC (STRPTR Cmd)
{
	WORD    Pos = 1;
	ULONG   Val;
	BOOL    Blank, Ok = TRUE;


	Val = ExtractNo (Cmd, &Pos, &Blank);

	if (!Blank)
	{
		if (Val % 4)
		{
			printf ("Invalid PC\n");
			Ok = FALSE;
		}
		else
			dlx.PC = Val;
	}

	return Ok;
}

VOID Performance ()
{
	DOUBLE   Perc = ((DOUBLE) 100) / ((DOUBLE) dlx.Instr);


	printf ("Performance of last run:\n");

	if (SIMTYPE == PIPELINED || dlx.IDone)
	{
		printf ("  No. of instructions\n");
		printf ("      Started   (IF) : %ld\n", dlx.IStart);
		printf ("      Executed  (EX) : %ld\n", dlx.Instr);
		printf ("      Completed (WB) : %ld\n", dlx.IDone);
	}
	else
		printf ("  No. of instructions executed   : %ld\n", dlx.Instr);

	if (!dlx.Instr)
	{
		if (SIMTYPE == PIPELINED && dlx.IStart)
			printf ("  No instructions have reached the EX unit\n");
		else
			printf ("  Program not run\n");

		return;
	}

	printf ("    Loads              %8ld (%05.2f%%)\n",
		dlx.Loads, (DOUBLE) (Perc * dlx.Loads));
	printf ("    Stores             %8ld (%05.2f%%)\n",
		dlx.Stores, (DOUBLE) (Perc * dlx.Stores));
	printf ("    ALU                %8ld (%05.2f%%)\n",
		dlx.ALU, (DOUBLE) (Perc * dlx.ALU));
	printf ("    Set                %8ld (%05.2f%%)\n",
		dlx.Set, (DOUBLE) (Perc * dlx.Set));
	printf ("    Move               %8ld (%05.2f%%)\n",
		dlx.Move, (DOUBLE) (Perc * dlx.Move));
	printf ("    Convert            %8ld (%05.2f%%)\n",
		dlx.Convert, (DOUBLE) (Perc * dlx.Convert));
	printf ("    Jumps              %8ld (%05.2f%%)\n",
		dlx.Jumps, (DOUBLE) (Perc * dlx.Jumps));
	printf ("    JALs               %8ld (%05.2f%%)\n",
		dlx.JALs, (DOUBLE) (Perc * dlx.JALs));

	if (SIMTYPE == MICROCODE)
	{
		printf ("    Branches           %8ld (%05.2f%%)\n",
			dlx.BranchTaken, (DOUBLE) (Perc * dlx.BranchTaken));
	}
	else
	{
		printf ("    Branch (taken)     %8ld (%05.2f%%)\n",
			dlx.BranchTaken, (DOUBLE) (Perc * dlx.BranchTaken));
		printf ("    Branch (not taken) %8ld (%05.2f%%)\n",
			dlx.BranchNotTaken, (DOUBLE) (Perc * dlx.BranchNotTaken));
	}

	printf ("    Trap/RFE           %8ld (%05.2f%%)\n\n",
		dlx.TrapRfe, (DOUBLE) (Perc * dlx.TrapRfe));
	printf ("  Total clock cycles   %8ld\n", dlx.Clock);

	if (SIMTYPE == PIPELINED)
	{
		printf ("  Average CPI executed  %8.3f\n", ((DOUBLE) dlx.Clock) /
			((DOUBLE) dlx.Instr));
		printf ("  Average CPI complete  ");

		if (dlx.IDone)
			printf ("%8.3f\n", ((DOUBLE) dlx.Clock) / ((DOUBLE) dlx.IDone));
		else
			printf ("(None completed)\n");
	}
	else
		printf ("  Average CPI          %8.3f\n", ((DOUBLE) dlx.Clock) /
			((DOUBLE) dlx.Instr));
}

VOID BPMessage ()
{
	ULONG   i;


	dlx.PC -= 4;

	for (i = 0; i < BP.Num; i++)
		if (dlx.PC == BP.BPs [i])
		{
			printf ("Breakpoint %d at $%08lX\n", i + 1, dlx.PC);
			break;
		}

	/* Restore original instruction at breakpoint address */

	StoreL (dlx.PC, BP.Copy);
	BP.Copy = 0;
	BP.PC = dlx.PC;
}

VOID HandleBreakpoints (STRPTR Cmd)
{
	ULONG   i, Val;
	WORD    pos;
	BOOL    Blank;


	if (islower (Cmd [1]))
		Cmd [1] = toupper (Cmd [1]);

	switch (Cmd [1])
	{
		case ' ' : /* Set next free breakpoint */
			pos = 1;
			Val = ExtractNo (Cmd, &pos, &Blank);

			if (!Blank)
			{
				if (Val % 4)
					printf ("Invalid breakpoint address\n");
				else
				{
					for (i = 0; i < MAXBREAKPOINTS; i++)
						if (BP.BPs [i] == ~0)
						{
							BP.BPs [i] = Val;
							printf ("Setting breakpoint %d to $%08lX\n",
								i + 1, Val);

							if (i + 1 > BP.Num)
								BP.Num = i + 1;

							break;
						}

					if (i == MAXBREAKPOINTS)
						printf ("No more breakpoints free\n");
				}

				break;
			}

			/* Drop through */

		case 0 : /* Display */
			if (BP.Num)
			{
				printf ("Breakpoints:\n");

				for (i = 0; i < BP.Num; i++)
					if (BP.BPs [i] != ~0)
						printf ("%4d  $%08lX\n", i + 1, BP.BPs [i]);
			}
			else
				printf ("No breakpoints set\n");

			break;

		case 'C' :
			printf ("Breakpoints cleared\n");

			for (i = 0; i < MAXBREAKPOINTS; i++)
				BP.BPs [i] = ~0;

			BP.Num = 0;
			break;

		default : /* Set specified breakpoint */
			if (isdigit (Cmd [1]))
			{
				pos = 1;
				i = (UWORD) ExtractDecNo (Cmd, &pos, &Blank);

				if (!Blank && i && i <= MAXBREAKPOINTS)
				{
					i--;
					Val = ExtractNo (Cmd, &pos, &Blank);

					if (!Blank)
					{
						if (Val % 4)
							printf ("Invalid breakpoint address\n");
						else
						{
							BP.BPs [i] = Val;
							printf ("Setting breakpoint %d to $%08lX\n",
								i + 1, Val);

							if (i + 1 > BP.Num)
								BP.Num = i + 1;
						}
					}
					else
					{
						BP.BPs [i] = ~0;

						if (i + 1 == BP.Num)
							BP.Num--;
					}
				}
			}
	}
}

VOID SetRegister (STRPTR Cmd)
{
	ULONG   i, Val;
	WORD    pos;
	BOOL    Blank;


	if (isdigit (Cmd [1]))  /* Integer Regs */
	{
		pos = 1;
		i = (UWORD) ExtractDecNo (Cmd, &pos, &Blank);

		if (!Blank && i && i < 32)  /* Don't change R0 */
		{
			Val = ExtractNo (Cmd, &pos, &Blank);

			if (!Blank)
				dlx.R [i] = Val;
		}
	}
	else
	{
		if (islower (Cmd [1]))
			Cmd [1] = toupper (Cmd [1]);

		pos = 2;
		i = (UWORD) ExtractDecNo (Cmd, &pos, &Blank);

		switch (Cmd [1])
		{
			case 'I' :
				Val = ExtractNo (Cmd, &pos, &Blank);

				if (!Blank)
					dlx.FP.I [i] = Val;

				break;

			case 'D' :
				if (!Blank)
					dlx.FP.D [i / 2] = (DOUBLE) atof (&Cmd [pos]);

				break;

			case 'F' :
				if (!Blank)
					dlx.FP.F [i] = (FLOAT) atof (&Cmd [pos]);
		}
	}
}

VOID AllocateMemory (STRPTR Cmd)
{
	ULONG   i, Val;
	WORD    pos;
	BOOL    Blank;
	STRPTR  NewMem;


	pos = 1;
	Val = (UWORD) ExtractDecNo (Cmd, &pos, &Blank);

	if (!Blank && Val)
	{
		Val = Val * 1024;

		/* Prevent memory overlapping simulated timer which sits at
		   0xFF000000 (yes, I know it seems pretty unlikely AT THE
		   MINUTE that a system will have 4GB RAM available - but
		   you never know!) */

		if (Val > 0xFF000000)
			Val = 0xFF000000;

		/* Grab the memory */

		NewMem = (STRPTR) calloc (1, Val);

		if (!NewMem)
			printf ("Could not allocate new memory\n");
		else
		{
			if (Val < dlx.SizeOfMem)
				i = Val;
			else
				i = dlx.SizeOfMem;

			memcpy (NewMem, dlx.Memory, i);
			free (dlx.Memory);
			dlx.Memory = NewMem;
			dlx.SizeOfMem = Val;
		}
	}

	printf ("Current memory size = %ldK\n", dlx.SizeOfMem / 1024);
}

/*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/
/* Main input 'event' loop                                                */
/*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/

VOID MainLoop ()
{
	ULONG   i, Val;
	WORD    pos;
	UBYTE   Cmd [162];
	FILE    *Rec = NULL, *Lg = stdout;
	BOOL    Loop = TRUE, Debug = FALSE, Blank, TraceLoop,
			Playback = FALSE, Record = FALSE;


	do {
		/* Prompt and get input */

		printf (">");

		if (Playback)
		{
			fgets (Cmd, 160, Rec);
			printf ("%s", Cmd);
		}
		else
			fgets (Cmd, 160, stdin);

		if (Record)
			fputs (Cmd, Rec);

		/* Make first chr upper case */

		if (islower (Cmd [0]))
			Cmd [0] = toupper (Cmd [0]);

		/* Strip off line feed at end */

		Cmd [strlen (Cmd) - 1] = 0;

		/* Handle command */

		switch (Cmd [0])
		{
			case 'A' : /* Allocate/Display memory size */
				AllocateMemory (Cmd);
				break;

			case 'B' : /* Breakpoints */
				HandleBreakpoints (Cmd);
				break;

			case 'C' : /* Continue Go (run) */
				if (Cmd [1] == 'D' || Cmd [1] == 'd')
					Debug = TRUE;

				if (dlx.PC < dlx.SizeOfMem)
				{
					Log = Lg;
					Run (Debug);
					Log = stdout;
				}

				if (BP.Copy)
					BPMessage ();
				else
					Debug = FALSE;

				break;

			case 'D' : /* Disassemble memory */
				if (Cmd [1] == 'M' || Cmd [1] == 'm')
					DisMicrocode ();
				else
					Disassemble (Cmd);

				break;

			case 'E' : /* Enter directly into memory */
				EnterMemory (Cmd, Playback, Record, Rec);
				break;

			case 'F' : /* Fill memory */
				FillMemory (Cmd);
				break;

			case 'H' : /* Go with Debug */
				Debug = TRUE;

				/* Drop through */

			case 'G' : /* Go */
				if (SetPC (Cmd))
				{
					if (dlx.PC < dlx.SizeOfMem)
					{
						ClearDLXRegs (FALSE);
						Log = Lg;
						Run (Debug);
						Log = stdout;

						if (BP.Copy)
							BPMessage ();
						else
							Debug = FALSE;
					}
					else
						printf ("No memory at PC\n");
				}

				break;

			case 'I' : /* Set/Display Load Address */
				pos = 1;
				Val = (UWORD) ExtractNo (Cmd, &pos, &Blank);

				if (!Blank && Val)
					DefLoad = Val;

				printf ("Current default load address = $%08lX\n", DefLoad);
				break;

			case 'J' :
				SetDebugLevel (Cmd);
				break;

			case 'K' :
				SetPipelineControl (Cmd);
				break;

			case 'L' : /* Load memory */
				DoLoad (&Cmd [1], TRUE);
				break;

			case 'M' : /* Memory dump */
				MemoryDump (Cmd);
				break;

			case 'O' : /* Display (output) registers */
				if (islower (Cmd [1]))
					Cmd [1] = toupper (Cmd [1]);

				switch (Cmd [1])
				{
					case 'D' :
						DisplayDPRegisters ();
						break;

					case 'F' :
						DisplaySPRegisters ();
						break;

					case 'I' :
						DisplayIRegisters ();
						break;

					case 'S' :
						DisplayStatRegisters ();
						break;

					default :
						DisplayRegisters ();
				}

				break;

			case ';' : /* Display registers */
				DisplayRegisters ();
				break;

			case '<' : /* Display SP registers */
				DisplaySPRegisters ();
				break;

			case '>' : /* Display DP registers */
				DisplayDPRegisters ();
				break;

			case '\\' : case '|' : /* Display I registers */
				DisplayIRegisters ();
				break;

			case '#' : /* Display Status registers */
				DisplayStatRegisters ();
				break;

			case 'P' : /* Set PC */
				SetPC (Cmd);
				ClearDLXRegs (FALSE);
				printf ("PC set to $%08lX\n", dlx.PC);
				break;

			case 'R' : /* Set Register */
				SetRegister (Cmd);
				break;

			case 'S' : /* Save memory */
				DoSave (&Cmd [1]);
				break;

			case 'U' : /* Trace with Debug */
				Debug = TRUE;

				/* Drop through */

			case 'T' : /* Trace */
				pos = 1;
				i = (UWORD) ExtractDecNo (Cmd, &pos, &Blank);

				if (Blank)
					i = 1;

				TraceLoop = TRUE;
				Log = Lg;

				do {
					TraceLoop = RunOneInstr (Debug);
				} while (--i > 0 && TraceLoop);

				Log = stdout;

				if (BP.Copy)
					BPMessage ();
				else
					Debug = FALSE;

				break;

			case 'V' : /* Change or display settings/display title */
				if (islower (Cmd [1]))
					Cmd [1] = toupper (Cmd [1]);

				switch (Cmd [1])
				{
					case 'H' :
						dlx.Type = (dlx.Type & 0xFFFFFFF8) | HARDWIRED;
						break;

					case 'M' :
						dlx.Type = (dlx.Type & 0xFFFFFFF8) | MICROCODE;
						break;

					case 'P' :
						dlx.Type = (dlx.Type & 0xFFFFFFF8) | PIPELINED;
				}

				if (Cmd [1] == 'T')
					DisplayTitle ();
				else
					DisplaySettings ();

				break;

			case 'W' : /* Debugging to log file */
				if (Cmd [1] == '-') /* Turn off trace to a file */
				{
					if (Lg != stdout)
					{
						fclose (Lg);
						Lg = stdout;
						Log = stdout;   /* Just in case */
						printf ("Log file closed\n");
					}

					Debug = FALSE;
				}
				else    /* Trace to a file */
				{
					strcat (Cmd, ".log");

					if (Cmd [1] == '+')
						Lg = fopen (&Cmd [2], "a");
					else
						Lg = fopen (&Cmd [1], "w");

					if (!Lg)
					{
						Lg = stdout;
						printf ("Could not open log file");
					}
					else
						printf ("Opened log file");

					printf (" %s\n", &Cmd [1]);
					Debug = FALSE;
				}

				break;

			case 'Q' : /* Quit or */
			case 'X' : /* Exit */
				Loop = FALSE;
				break;

			case 'Y' : /* Display performance statistics */
				Performance ();
				break;

			case 'Z' : /* Zero registers */
				if (islower (Cmd [1]))
					Cmd [1] = toupper (Cmd [1]);

				switch (Cmd [1])
				{
					case 'T' : /* Timer */
						memset (&timer, 0, sizeof (TIMER));
						printf ("Timer reset\n");
						break;

					default :
						ClearDLXRegs (TRUE);
						printf ("Registers cleared\n");
				}

				break;

			case 0 : case ' ' : /* No command */
				break;

			case '[' : /* Start/end recording */
				if (Record || Playback)
				{
					if (Record)
						printf ("End recording\n");

					Playback = FALSE;
					Record = FALSE;
					fclose (Rec);
				}
				else
				{
					strcat (Cmd, ".rec");

					Rec = fopen (&Cmd [1], "w");

					if (Rec)
						Record = TRUE;
					else
						printf ("Could not open file '%s' for record\n",
							&Cmd [1]);
				}

				break;

			case ']' : /* Play back recording */
				if (!Record)
				{
					strcat (Cmd, ".rec");

					Rec = fopen (&Cmd [1], "r");

					if (Rec)
						Playback = TRUE;
					else
						printf ("Could not open file '%s' for playback\n",
							&Cmd [1]);
				}

				break;

			case '.' : /* Insert pause during playback */
				if (Playback)
					PauseForKey (2);

				break;

			case '@' : /* System command */
				system (&Cmd [1]);
				break;

			case '^' : /* Load microcode */
				DoMcLoad (&Cmd [1]);
				break;

			case '?' :
				if (Cmd [1] > ' ')
				{
					DoFullHelp (&Cmd [1]);
					break;
				}

				/* Drop through */

			default : /* Anything else gives standard help */
				if (Cmd [0] != '?')
					printf ("Command not recognised - available commands are:\n\n");

				DoHelp ();
		}

	} while (Loop);
}

int main ()
{
	ULONG   i;
	UBYTE   LE [4];


	/* First test to see whether running on a little endian system */

	i = 0x01020304;
	memcpy (LE, &i, 4);

	if (LE [0] == 4)
		LittleEndian = TRUE;

	Log = stdout;

	/* Say hello to the user */

	printf ("Please wait. Initialising...\n\n");

	/* Allow CPU to set itself up */

	if (!InitCpu ())
	{
		printf ("Could not allocate DLX memory\n");
		exit (20);
	}

	/* Initialise breakpoints */

	for (i = 0; i < MAXBREAKPOINTS; i++)
		BP.BPs [i] = ~0;

	BP.Copy = 0;
	BP.Num = 0;

	DisplayTitle ();

	/* Indicate help available */

	printf ("\nEnter ? for commands\n");

	MainLoop ();

	return 0;
}
