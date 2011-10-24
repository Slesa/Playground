program finden(output);

const
	FELDGROESSE = 10;
	FELDGROESSEPLUSEINS = 11;

type
	tIndex = 1..FELDGROESSE;
	tIndexObenUnten = 0..FELDGROESSEPLUSEINS;

var
	unten,
	oben : tIndexObenUnten;
	Mitte : tIndex;
	Feld : array [tIndex] of integer;
	Suchwert : integer;
    i : integer;
	Ende : boolean;	

function findenA(Suchwert : integer) : boolean;
begin
	unten := 1;
	oben := FELDGROESSE;
	repeat
		Mitte := (oben + unten) div 2;
		if Suchwert > Feld[Mitte] then
			unten := Mitte + 1
			else
			oben := Mitte - 1
	until (Feld[Mitte] = Suchwert ) or (unten > oben);
	if Feld[Mitte] = Suchwert then
		findenA := true
	else
		findenA := false;
end;

function findenB(Suchwert : integer) : boolean;
var gefunden : boolean;
begin
	unten := 1;
	oben := FELDGROESSE;
	gefunden := false;
	repeat
		Mitte := (oben + unten) div 2;
		if Suchwert = Feld[Mitte] then
			gefunden := true
		else
			if Suchwert > Feld[Mitte] then
				unten := Mitte
			else
				oben := Mitte
	until gefunden or (unten >= oben);
	findenB := gefunden
end;

function findenC(Suchwert : integer) : boolean;
begin
	unten := 1;
	oben := FELDGROESSE;
	repeat
		Mitte := (oben + unten) div 2;
		if Suchwert <= Feld[Mitte] then
			oben := Mitte - 1;
		if Feld[Mitte] <= Suchwert then
			unten := Mitte + 1;
	until unten > oben;
	findenC := Feld[Mitte] = Suchwert;
end;

function findenD(Suchwert : integer) : boolean;
var gefunden : boolean;
begin
	gefunden := false;
	unten := 1;
	oben := FELDGROESSE;
	while (unten<oben) and not gefunden do
	begin
		Mitte := (oben + unten) div 2;
		if Suchwert = Feld[Mitte] then
			gefunden := true
		else
			if Suchwert < Feld[Mitte] then
				oben := Mitte - 1
			else
				unten := Mitte + 1
	end;
	findenD := gefunden
end;

function findenE(Suchwert : integer) : boolean;
var gefunden : boolean;
begin
	gefunden := false;
	unten := 1;
	oben := FELDGROESSE;
	while (unten<=oben) and not gefunden do
	begin
		Mitte := (oben + unten) div 2;
		if Suchwert = Feld[Mitte] then
			gefunden := true
		else
			if Suchwert < Feld[Mitte] then
				oben := Mitte - 1
			else
				unten := Mitte + 1
	end;
	findenE := gefunden
end;

procedure hatGefunden(name : String; gefunden : boolean);
begin
	write ('Funktion ', name);
	if gefunden then 
		writeln (', Suchwert gefunden')
	else
		writeln (', Suchwert kommt nicht vor');
end;

begin

	for i:=1 to FELDGROESSE do Feld[i] := i*2-1;
	Ende := false;

	repeat
		write ('Suchwert: ');
		readln(Suchwert);
		if Suchwert=0 then Ende := true
		else begin
			hatGefunden ( 'A', findenA(Suchwert) );
			{ hatGefunden ( 'B', findenB(Suchwert) ); }
			hatGefunden ( 'C', findenC(Suchwert) );
			hatGefunden ( 'D', findenD(Suchwert) );
			hatGefunden ( 'E', findenE(Suchwert) );
		end;

	until Ende;

end.

