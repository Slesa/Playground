program maximum;

const
	GRENZE = 10;
type      
	tIndex = 1..GRENZE;
	tFeld = array [tIndex] of integer;
var
	Feld : tFeld;
	w,
	w1,
	w2 : integer;


function max ( ParFeld: tFeld; von, bis: integer): integer;
{ bestimmt das Maximum im Teilfeld von ParFeld[von] 
bis ParFeld[bis] }
var
	Wert : integer;
	i : integer;
begin
	if (von>GRENZE) or (von<=0) then
		writeln ('von OUT OF RANGE!');
	if (bis>GRENZE) or (bis<=0) then
		writeln ('bis OUT OF RANGE!');
	Wert := ParFeld[von];
	for i := von + 1 to bis do begin
		if (i>GRENZE) or (i<=0) then
			writeln ('OUT OF RANGE!');
		if ParFeld[i] > Wert then
			Wert := ParFeld[i];
	end;
	max := Wert
end; { max }


begin
	for w:=1 to GRENZE do Feld[w] := 1000+w;

	writeln ('A:');
	w := max (Feld, Feld[1], Feld[GRENZE]);

	writeln ('B:');
	w := max (Feld, (GRENZE-1) div 2, (GRENZE-1) div 2);

	writeln ('C:');
	if max (Feld, 1, (GRENZE-1) div 2) > max (Feld, (GRENZE+1) div 2, GRENZE) then
		w := max (Feld, 1, (GRENZE-1) div 2)
	else
		w := max (Feld, (GRENZE+1) div 2, GRENZE);

	writeln ('D:');
	w := max (Feld, 1, GRENZE);
	if w <= GRENZE then
	  write (max (Feld, w, w));

	writeln ('E:');
	w1 := max (Feld, 1, GRENZE);
	w2 := max (Feld, 4, GRENZE-1);
	if (0 < w2) and (w1 <= GRENZE) then
	begin
		w := max (Feld, 2, GRENZE);
		w := max (Feld, 1, w)
	end; 
end.

