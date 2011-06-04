/////////////////////////////////////////////////////////////////////////////////////////
//
//
//	        DLX RISC Simulator - Memory I/O
//	        Note that 8-byte accesses are via strings
//
//	        D. J. Viner
//
/////////////////////////////////////////////////////////////////////////////////////////

#include "cpu.h"

/////////////////////////////////////////////////////////////////////////////////////////
// Store Val at address Addr making sure it is within range. Like a real machine there
// will be no error if the user tries to store into a valid memory address when there is
// no actual memory at that location. There are four versions of Store for Byte, Word (2-
// byte), Long (4-byte) and an eight byte version for use with double precision fp
// values. Note that neither StoreB or StoreW can store into the timer area as these are
// all Long word values (conversely all Fetch routines can read from the timers).
/////////////////////////////////////////////////////////////////////////////////////////

VOID StoreB (ULONG Addr, UBYTE Val)
{
	if (Addr < dlx.SizeOfMem)
		dlx.Memory [Addr] = Val;
}

/////////////////////////////////////////////////////////////////////////////////////////

VOID StoreW (ULONG Addr, UWORD Val)
{
	if (Addr < dlx.SizeOfMem - 1)
	{
		if (LittleEndian)
		{
			UBYTE   V [2], i;

			memcpy (V, &Val, 2);

			for (i = 0; i < 2; i++)
				dlx.Memory [Addr + (1 - i)] = V [i];
		}
		else
			memcpy (&dlx.Memory [Addr], &Val, 2);
	}
}

/////////////////////////////////////////////////////////////////////////////////////////

VOID StoreL (ULONG Addr, ULONG Val)
{
	if (Addr < dlx.SizeOfMem - 3)
	{
		if (LittleEndian)
		{
			UBYTE   V [4], i;

			memcpy (V, &Val, 4);

			for (i = 0; i < 4; i++)
				dlx.Memory [Addr + (3 - i)] = V [i];
		}
		else
			memcpy (&dlx.Memory [Addr], &Val, 4);
	}
	else
	if ((dlx.Type & TIMERON) && Addr >= T1_LATCH && Addr <= T3_STATUS)
	{
		switch (Addr)
		{
			case T1_LATCH :
				timer.Timer1Latch = Val;
				timer.Timer1Count = 0;
				break;

			case T1_STATUS :
				timer.Timer1Status = Val;
				break;

			case T2_LATCH :
				timer.Timer2Latch = Val;
				timer.Timer2Count = 0;
				break;

			case T2_STATUS :
				timer.Timer2Status = Val;
				break;

			case T3_LATCH :
				timer.Timer3Latch = Val;
				timer.Timer3Count = 0;
				break;

			case T3_STATUS :
				timer.Timer3Status = Val;
		}
	}
}

/////////////////////////////////////////////////////////////////////////////////////////

VOID Store8 (ULONG Addr, STRPTR Val)
{
	if (Addr < dlx.SizeOfMem - 7)
	{
		if (LittleEndian)
		{
			UBYTE   i;

			for (i = 0; i < 8; i++)
				dlx.Memory [Addr + (7 - i)] = Val [i];
		}
		else
			memcpy (&dlx.Memory [Addr], Val, 8);
	}
}

/////////////////////////////////////////////////////////////////////////////////////////
// Get a value from memory, return 0 if there is no memory at the point requested. Like
// Store, there are four versions. Note that 2 to 8 byte reads that cross the EndOfMem
// boundary will contain garbage values.
/////////////////////////////////////////////////////////////////////////////////////////

UBYTE FetchB (ULONG Addr)
{
	UBYTE   Val = 0xFF;
	STRPTR  p;

	if (Addr < dlx.SizeOfMem)
		Val = dlx.Memory [Addr];
	else
	if ((dlx.Type & TIMERON) && Addr >= T1_LATCH && Addr <= T3_STATUS + 7)
	{
		p = (STRPTR) &timer;
		Val = (Addr - 0xFF000000);

		if (LittleEndian)   // Swap byte access around
		{
			UBYTE   Off = (Val / 4) * 4;

			Val = Off + 3 - (Val - Off);
		}

		p += Val;
		Val = *p;
	}
	else
	if (Addr >= MEM_BASE && Addr <= MEM_BASE + 3)
	{
		int ad = (3 - (Addr - MEM_BASE)) * 8;
		Val = (dlx.SizeOfMem >> ad) & 0xFF;
	}

	return Val;
}

/////////////////////////////////////////////////////////////////////////////////////////

UWORD FetchW (ULONG Addr)
{
	UWORD   Val = 0xFFFF;


	if (Addr < dlx.SizeOfMem)
	{
		if (LittleEndian)
		{
			UBYTE   V [2], i;

			for (i = 0; i < 2; i++)
				V [i] = dlx.Memory [Addr + (1 - i)];

			memcpy (&Val, V, 2);
		}
		else
			memcpy (&Val, &dlx.Memory [Addr], 2);
	}
	else
	if ((dlx.Type & TIMERON) && (Addr >= T1_LATCH && Addr <= T3_STATUS) || (Addr >= MEM_BASE && Addr <= MEM_BASE + 3))
		Val = FetchB (Addr) + 256 * FetchB (Addr + 1);

	return Val;
}

/////////////////////////////////////////////////////////////////////////////////////////

ULONG FetchL (ULONG Addr)
{
	ULONG   Val = 0xFFFFFFFF;


	if (Addr < dlx.SizeOfMem)
	{
		if (LittleEndian)
		{
			UBYTE   V [4], i;

			for (i = 0; i < 4; i++)
				V [i] = dlx.Memory [Addr + (3 - i)];

			memcpy (&Val, V, 4);
		}
		else
			memcpy (&Val, &dlx.Memory [Addr], 4);
	}
	else
	if ((dlx.Type & TIMERON) && Addr >= T1_LATCH && Addr <= T3_STATUS)
	{
		switch (Addr)
		{
			case T1_LATCH :
				Val = timer.Timer1Latch;
				break;

			case T1_COUNT :
				Val = timer.Timer1Count;
				break;

			case T1_STATUS :
				Val = timer.Timer1Status;
				timer.Timer1Status &= 0xFFFFFF7F;
				break;

			case T2_LATCH :
				Val = timer.Timer2Latch;
				break;

			case T2_COUNT :
				Val = timer.Timer2Count;
				break;

			case T2_STATUS :
				Val = timer.Timer2Status;
				timer.Timer2Status &= 0xFFFFFF7F;
				break;

			case T3_LATCH :
				Val = timer.Timer3Latch;
				break;

			case T3_COUNT :
				Val = timer.Timer3Count;
				break;

			case T3_STATUS :
				Val = timer.Timer3Status;
				timer.Timer3Status &= 0xFFFFFF7F;
		}
	}
	else
	if (Addr == MEM_BASE)
		Val = dlx.SizeOfMem;

	return Val;
}

/////////////////////////////////////////////////////////////////////////////////////////

STRPTR Fetch8 (ULONG Addr)
{
	static  UBYTE   Val [8] = { 0xFF, 0xFF, 0xFF, 0xFF,
								0xFF, 0xFF, 0xFF, 0xFF };


	if (Addr < dlx.SizeOfMem)
	{
		if (LittleEndian)
		{
			UBYTE   i;

			for (i = 0; i < 8; i++)
				Val [i] = dlx.Memory [Addr + (7 - i)];
		}
		else
			memcpy (Val, &dlx.Memory [Addr], 8);
	}

	return Val;
}

/////////////////////////////////////////////////////////////////////////////////////////


