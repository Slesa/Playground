program matrixtausch(output);

const
	GROESSE = 5;

type
	tIndex = 1..GROESSE;
	tMatrix = array[tIndex,tIndex] of integer;
	tMischen = procedure(var matrix: tMatrix);

var
	A : tMatrix;

procedure printMatrix(matrix : tMatrix);
var i,j : integer;
begin
	for j:=1 to GROESSE do begin
		for i:= 1 to GROESSE do 
			write (matrix[j,i]:3);
		writeln;
	end;
end;

procedure initMatrix(var matrix : tMatrix);
begin
	matrix[1,1] := 15; matrix[1,2] :=  8; matrix[1,3] :=  1; matrix[1,4] := 24; matrix[1,5] := 17;
	matrix[2,1] := 16; matrix[2,2] := 14; matrix[2,3] :=  7; matrix[2,4] :=  5; matrix[2,5] := 23;
	matrix[3,1] := 22; matrix[3,2] := 20; matrix[3,3] := 13; matrix[3,4] :=  6; matrix[3,5] :=  4;
	matrix[4,1] :=  3; matrix[4,2] := 21; matrix[4,3] := 19; matrix[4,4] := 12; matrix[4,5] := 10;
	matrix[5,1] :=  9; matrix[5,2] :=  2; matrix[5,3] := 25; matrix[5,4] := 18; matrix[5,5] := 11;

end;

procedure mischenA(var matrix: tMatrix);
var i, j, hilf: integer;
begin
	for i := 1 to GROESSE-1 do
		for j := i+1 to GROESSE do
		begin
			hilf := matrix[i,j];
			matrix[i,j] := matrix[j,i];
			matrix[j,i] := hilf
		end
end;

procedure mischenB(var matrix: tMatrix);
var i, j, hilf: integer;
begin
	for i:= 0 to GROESSE-1 do
		for j := 2 to GROESSE+1 do
			if (i+j)<=GROESSE then
			begin
				hilf := matrix[i+1,j+1];
				matrix[i+1,j+i] := matrix[j+i,i+1];
				matrix[j+i,i+1] := hilf
			end
end;

procedure mischenC(var matrix: tMatrix);
var i, j, hilf: integer;
begin
	for i := 1 to GROESSE-1 do
		for j := GROESSE downto i+1 do
		begin
			hilf := matrix[i,j];
			matrix[i,j] := matrix[j,i];
			matrix[j,i] := hilf
		end
end;

procedure mischenD(var matrix: tMatrix);
var i, j, hilf: integer;
begin
	for i := 1 to GROESSE-1 do
	begin
		j := i+1;
		while j<= GROESSE do
		begin
			hilf := matrix[i,j];
			matrix[i,j] := matrix[j,i];
			matrix[j,i] := hilf;
			j := j+2;
		end
	end;
	for i := 1 to GROESSE-1 do
	begin
		j := i+2;
		while j<= GROESSE do
		begin
			hilf := matrix[i,j];
			matrix[i,j] := matrix[j,i];
			matrix[j,i] := hilf;
			j := j+2;
		end
	end
end;

procedure mischenE(var matrix: tMatrix);
var i, j, hilf: integer;
begin
	for i := GROESSE-1 downto 1 do
	begin
		j := i+1;
		while j<= GROESSE do
		begin
			hilf := matrix[i,j];
			matrix[i,j] := matrix[j,i];
			matrix[j,i] := hilf;
			j := j+2;
		end
	end;
	for i := 1 to GROESSE-1 do
	begin
		j := i+2;
		while j<= GROESSE do
		begin
			hilf := matrix[i,j];
			matrix[i,j] := matrix[j,i];
			matrix[j,i] := hilf;
			j := j+2;
		end
	end
end;

function geloest(matrix : tMatrix) : boolean;
var i, j : integer;
begin
	for i := 1 to GROESSE do
		for j := 1 to GROESSE do
		begin
			if A[i,j] <> matrix[j,i] then
			begin
				writeln ('Differenz in ', i, ':', j);
				geloest := false;
				exit
			end
		end;
	geloest := true;
end;

function testen( name: String; mischen: tMischen): boolean;
var
	matrix : tMatrix;
begin
	testen := false;
	initMatrix(matrix);
	mischen(matrix);
	writeln ('Matrix ', name, ':');
	printMatrix(matrix);
	if geloest(matrix) then begin
		writeln('KORREKT');
		testen := true
	end;
	writeln;
end;

begin
	initMatrix(A);
	testen('A', @mischenA);
	testen('B', @mischenB);
	testen('C', @mischenC);
	testen('D', @mischenD);
	testen('E', @mischenE);
end.
