program Maximum(input, output);

var 
	max: integer;
	anz: integer;
	eingabe : integer;

begin

	max := 0;
	anz := 0;
	writeln ('Geben Sie ganze Zahlen ein, deren Maximum bestimmt werden soll.');

	repeat
		anz := anz + 1;
		write(anz, '. Wert: ');
		readln(eingabe);

		if eingabe<> 0 then begin
			if (anz=1) or (eingabe>max) then
				max := eingabe;
		end;

	until eingabe=0; 

	if max=0 then 
		writeln('Leere Eingabefolge!')
	else
		writeln('Maximum: ', max);
end.

