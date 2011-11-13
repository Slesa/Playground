program testeListenMaxBestimmen(input, output);
{ Testet die Prozedur ListenMaxBestimmen }

	type
	tRefListe = ^tListe;
	tListe = record
		info : integer;
		next : tRefListe;
	end;

	var
	Liste : tRefListe;
	Max : integer;
	Ok : boolean;

	procedure ListenmaxBestimmen( inRefAnfang : tRefListe;
		var outMax : integer;
		var outOk : boolean);
	var
	max : integer;
	current : tRefListe;

	begin
		if inRefAnfang=nil then 
			outOk := false
		else
		begin
			outOk := true;
			max := -maxint-1;
			current := inRefAnfang;
			while current<>nil do begin
				if current^.info>max then
					max := current^.info;
				current := current^.next;
			end
		end;
		outMax := max
	end;

	procedure LiesListe(var outListe : tRefListe);
	{ Liest eine (evtl. leere) Liste ein und gibt deren Anfangszeiger outListe zurueck. }
		var
		Anzahl : integer;
		i : integer;
		neueZahl : integer;
		Listenanfang, Listenende : tRefListe;

	begin
		Listenanfang := nil;
		repeat
			write ('Wie viele Zahlen wollen Sie eingeben? ');
			readln (Anzahl);
		until Anzahl >= 0;

		write ('Bitte geben Sie ', Anzahl, ' Zahlen ein: ');

		{ Liste aufbauen }
		for i:=1 to Anzahl do
		begin
			read(neueZahl);
			if Listenanfang=nil then
			begin
				new (Listenanfang);
				Listenanfang^.next := nil;
				Listenanfang^.info := neueZahl;
				Listenende := Listenanfang;
			end
			else
			begin
				new (Listenende^.next);
				Listenende := Listenende^.next;
				Listenende^.next := nil;
				Listenende^.info := neueZahl;
			end { if Liste=nil }
		end; { for }
		outListe := Listenanfang;
		writeln
	end;		

begin
	LiesListe(Liste);
	ListenMaxBestimmen(Liste, Max, Ok);
	if Ok then
		writeln ('Das Maximum der Liste ist ', Max, '.')
	else
		writeln ('Leere Eingabefolge!')
end. { testeListenMaxBestimmen }
