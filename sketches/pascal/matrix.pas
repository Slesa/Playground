program MatrixSummen(input,output);
{ Ueberprueft bei einer Matrix von Interger-Zahlen, ob
  jede Spaltensumme groesser ist als die Zeilensumme
  einer angegebenen Zeile }
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

function ZeilenSummeKleiner(SelektZeile : integer) : boolean;
{ Liefert true, wenn alle Spaltensummen groesser sind
  als die Zeilensumme der uebergebenen Zeile }
var
	Spalte, Zeile : integer;
	ZeilenSumme, SpaltenSumme : integer;
	Fertig : boolean;
begin

	ZeilenSummeKleiner := true;
	Fertig := false;

	ZeilenSumme := 0;
	for Spalte:=1 to SPALTENMAX do
		ZeilenSumme := ZeilenSumme + Matrix[SelektZeile, Spalte];

	writeln ('Zeilensumme der Zeile ',SelektZeile:1,': ', ZeilenSumme);
	writeln ('Spaltensummen: ');

	Spalte := 1;
	repeat
		SpaltenSumme := 0;
		for Zeile:=1 to ZEILENMAX do
			SpaltenSumme := SpaltenSumme + Matrix[Zeile,Spalte];

		write ('Spalte ', Spalte , ': ', SpaltenSumme);
		if SpaltenSumme<=ZeilenSumme then 
		begin
			writeln (', ', SpaltenSumme, ' <= ', ZeilenSumme);
			ZeilenSummeKleiner := false;
			Fertig := true;
		end else
			writeln (', ', SpaltenSumme, ' > ', ZeilenSumme);
		Spalte := Spalte+1;
	until Fertig or (Spalte>=SPALTENMAX);

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

	
