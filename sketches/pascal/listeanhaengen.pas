program TesteAnhaengen(input, output);
{ testet die Prozedur anhaengen }

	type
	tRefListe = ^tListe;
	tListe = record
		info : integer;
		next : tRefListe
	end;

	var
	Liste1,
	Liste2,
	Gesamtliste : tRefListe;

	procedure anhaengen( inRefAnfang1, 
		inRefAnfang2 : tRefListe;
		var outRefAnfang : tRefListe);
	{ Haengt die lineare Liste mit Anfangszeiger inRefAnfang2 an
	  die Liste mit Anfangszeiger inRefAnfang1 an.
	  Zurueckgegeben wird outRefAnfang, der Anfang der Ergebnisliste. }

	var
	current : tRefListe;

	begin
		if inRefAnfang1=nil then 
			outRefAnfang := inRefAnfang2
		else 
		begin
			outRefAnfang := inRefAnfang1;
			current := inRefAnfang1;
			while current^.next<>nil do
				current := current^.next;
			current^.next := inRefAnfang2
		end
	end;


	procedure LiesListe(var outListe : tRefListe);
	{ Liest eine (evtl. leere) Liste ein und gibt deren Anfangszeiger outListe zurueck. }

	var
	Anzahl : integer;
	i : integer;
	neueZahl : integer;
	Listenanfang,
	Listenende : tRefListe;

	begin
		Listenanfang := nil;
		repeat
			write ('Wie viele Zahlen wollen Sie eingeben? ');
			readln (Anzahl);
		until Anzahl >= 0;

		write ('Bitte geben Sie ', Anzahl, ' Zahlen ein: ');

		{ Liste aufbauen }
		for i := 1 to Anzahl do
		begin
			read (neueZahl);
			if Listenanfang = nil then
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
				Listenende^.info := neueZahl
			end  { if Liste = nil }
		end; { for }
		outListe := Listenanfang;
		writeln
	end; { LiesListe }


	procedure gibListeAus (inListe : tRefListe);
	{ gibt die Liste mit Anfangszeiger inListe aus }

	var
	ListenElement : tRefListe;

	begin
		ListenElement := inListe;
		while ListenElement <> nil do
		begin
			write(ListenElement^.info, ' ');
			ListenElement := ListenElement^.next
		end;
		writeln
	end; { gibListeAus }


begin { testeAnhaengen }
	writeln('Bitte geben Sie die erste Liste ein! ');
	LiesListe (Liste1);
	writeln;

	writeln('Bitte geben Sie die zweite Liste ein! ');
	LiesListe (Liste2);
	writeln;

	writeln('Die konkatenierte Liste lautet: ');
	Anhaengen (Liste1, Liste2, Gesamtliste);
	GibListeAus (Gesamtliste);
end. { TesteAnhaengen }
