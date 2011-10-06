program MatrixSummen(input,output);

const
	ZEILENMAX = 3;
	SPALTENMAX = 4;

type
	tZeile = 1..ZEILENMAX;
	tSpalte = 1..SPALTENMAX;
	tMatrix = array [tZeile, tSpalte] of integer;

var
	Matrix : tMatrix;
	Zeile : tZeile;
	Spalte : tSpalte;
	Eingabe : integer;

function ZeilenSummeKleiner(Zeile : integer) : boolean;
Label 10;
var
	i,j : integer;
	summe, vergleich : integer;
begin
	summe := 0;
	for i:=1 to SPALTENMAX do
		summe := summe + Matrix[Zeile, i];

	writeln ('Zeilensumme der Zeile ',Zeile:1,': ', summe);
	writeln ('Spaltensummen: ');

	for i:=1 to SPALTENMAX do begin
		vergleich := 0;
		for j:=1 to ZEILENMAX do
			vergleich := vergleich + Matrix[j,i];

		write ('Spalte ', i, ': ', vergleich);
		if vergleich<=summe then begin
			writeln (', ', vergleich, ' <= ', summe);
			ZeilenSummeKleiner := false;
			goto 10
		end else
			writeln (', ', vergleich, ' > ', summe);
	end;

	ZeilenSummeKleiner := true;

	10:
end;

begin
	
	for Zeile := 1 to ZEILENMAX do
		for Spalte := 1 to SPALTENMAX do 
		begin
			write ('Lese MATRIX[ ',Zeile,' : ', Spalte, ']: ');
			readln (Matrix[Zeile, Spalte])
		end;
	
	repeat
		write ('Welche Zeile soll überprüft werden? (1..', ZEILENMAX:1, ') (anderes = Ende)');
		readln (Eingabe);

		if (Eingabe>0) and (Eingabe<=ZEILENMAX) then
		begin
			Zeile := Eingabe;
			if ZeilenSummeKleiner(Zeile) then
				writeln ('Jede Spaltensumme ist größer als die Zeilensumm der ',Zeile:1,'. Zeile.')
			else
				writeln ('Es sind nicht alle Spaltensumme größer als die Zeilensumme der ',Zeile:1,'. Zeile.')
		end;
	until (Eingabe<=0) or (Eingabe>ZEILENMAX)
end.

	
