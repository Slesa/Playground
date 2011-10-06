program merge(input, output);

const 
	GRENZE1 = 5;
	GRENZE2 = 8;
	GRENZE  = 13;
	GRENZE1PLUS1 = 6;
	GRENZE2PLUS1 = 9;
	GRENZEPLUS1  = 14;

type
	tIndex1 = 1..GRENZE1;
	tIndex2 = 1..GRENZE2;
	tIndex = 1..GRENZE;
	tIndex1Plus1 = 1..GRENZEPLUS1;
	tIndex2Plus1 = 1..GRENZE2PLUS1;
	tIndexPlus1 = 1..GRENZEPLUS1;
	tFeld1 = array [tIndex1] of integer;
	tFeld2 = array [tIndex2] of integer;
	tFeld = array [tIndex] of integer;

var
	Feld1 : tFeld1;
	Feld2 : tFeld2;
	Feld : tFeld;
	i : tIndex1Plus1;
	j : tIndex2Plus1;
	k : tIndexPlus1;
	erstes : boolean;

begin
	writeln ('Bitte', GRENZE1:2, ' Werte des ersten Feldes sortiert eingeben!');
	for i:=1 to GRENZE1 do
		readln (Feld1[i]);
	
	writeln ('Bitte', GRENZE2:2, ' Werte des zweiten Feldes sortiert eingeben!');
	for j:=1 to GRENZE2 do
		readln (Feld2[j]);

	i := 1;
	j := 1;
	k := 1;

	repeat

		Feld[k] := maxint;
		erstes := false;

		{Erstmal den Wert aus dem ersten Feld nehmen}
		{sofern noch Werte übrig}
		if i<GRENZE1PLUS1 then begin
			Feld[k] := Feld1[i];
			erstes := true;
		end;

		{Falls im zweiten Feld noch Werte übrig sind}
		if j<GRENZE2PLUS1 then begin
			{und der zugeordnete Wert größer als der des zweiten Feldes}
			if Feld[k]>Feld2[j] then begin
				{dann nimm den Wert doch aus dem zweiten Feld}
				Feld[k] := Feld2[j];
				erstes := false
			end
		end;

		{Je nach Fall den passenden Index erhöhen}
		if erstes then
			i := i+1
		else
			j := j+1;
		
		k := k+1;

	until k=GRENZEPLUS1;

	writeln ('Das Ergenisfeld ist:');
	for k:=1 to GRENZE do write (Feld[k]:8);
	writeln
end.
