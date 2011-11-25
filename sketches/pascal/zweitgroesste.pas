program testeFeldZweitMax (input, output);
{ testet die Funktion FeldZweitMax }

	const
	FELDGROESSE = 10;

	type
	tIndex = 1..FELDGROESSE;
	tFeld = array [tIndex] of integer;

	var 
	Feld : tFeld;
	i : integer;


	function FeldZweitMax (var inFeld : tFeld) : integer;
	{ berechnet die zweitgroesste Zahl in inFeld }

	var
	max1, max2: integer;
	i: integer;

	begin
		max1 := -maxint-1;
		max2 := max1;
		for i:=1 to FELDGROESSE do
		begin
			if inFeld[i]>max1 then
			begin
				max2 := max1;
				max1 := inFeld[i]
			end
			else
			begin
				if inFeld[i]>max2 then
					max2 := inFeld[i]
			end 
		end;
		FeldZweitMax := max2
	end;
 
begin { Testprogramm }
	writeln('Bitte geben Sie ', FELDGROESSE, ' Zahlen ein:');
	for i := 1 to FELDGROESSE do
		read (Feld [i]);
	writeln('Die zweitgroesste Zahl ist ', FeldZweitMax (Feld), '.');
end. { testeFeldZweitMax }

