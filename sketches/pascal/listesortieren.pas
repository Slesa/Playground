program TesteSortiereListe(input, output);
{$B+}
{$R+}

	type
	tNatZahl = 0..maxint;
	tRefListe = ^tListe;
	tListe = record
			 info : tNatZahl;
			 next : tRefListe;
		   end;

	var
	RefListe : tRefListe;

	procedure SortiereListe (var ioRefListe : tRefListe);
	{ sortiert eine lineare Liste aufsteigend }

	var
	root: tRefListe;
	current: tRefListe;
	previous: tRefListe;
	tmpCurrent, tmpPrevious: tRefListe;

	begin
		if ioRefListe<>nil then 
		{ Eingangsliste nicht leer }
		begin
			root := ioRefListe;
			if root^.next<>nil then 
			begin
			   { mehr als 2 Eintraege, Sortierung moeglich }
				current:= root^.next;
				previous:= root;
				while current <> nil do 
				begin
					if current^.info<root^.info then
					{ current ist kleiner als bisheriges root-Element }
					begin
						previous^.next := current^.next;
						current^.next:= root;
						root := current;
						current := previous^.next;
					end
					else 
					if current^.info >= previous^.info then
					{ current is groesser als das bisherige groesste Element }
					begin
						previous:= current;
						current:= current^.next;
					end
					else 
					{ current muss weiter vorne eingefuegt werden }
					begin
						tmpPrevious := root;
						tmpCurrent := root^.next;
						while tmpCurrent^.info <= current^.info do 
						{ Solange die Vergleichszahl kleiner ist, ruecke die Zeiger vor }
						begin
							tmpPrevious:= tmpCurrent;
							tmpCurrent:= tmpCurrent^.next;
						end;
						{ current ersetzt jetzt tmpCurrent }
						tmpPrevious^.next:= current;
						previous^.next:= current^.next;
						current^.next:= tmpCurrent;
						current:= previous^.next
					end
				end { while current<>nil }
			end; { if root^.next<>nil }
			ioRefListe := root
		end { if ioRefListe<>nil }
	end; {SortiereListe}

	procedure Anhaengen(var ioListe : tRefListe;
							inZahl : tNatZahl);
	{ Haengt inZahl an ioListe an }
	  var Zeiger : tRefListe;
	begin
	  Zeiger := ioListe;
	  if Zeiger = nil then
	  begin
		new(ioListe);
		ioListe^.info := inZahl;
		ioListe^.next := nil;
	  end
	  else
	  begin
		while Zeiger^.next <> nil do
		  Zeiger := Zeiger^.next;
		{ Jetzt zeigt Zeiger auf das letzte Element }
		new(Zeiger^.next);
		Zeiger := Zeiger^.next;
		Zeiger^.info := inZahl;
		Zeiger^.next := nil;
	  end;
	end;

	procedure ListeEinlesen(var outListe:tRefListe);
	{ liest eine durch Leerzeile abgeschlossene Folge von Integer-
	  Zahlen ein und speichert diese in der linearen Liste RefListe. }
	  var
	  Liste : tRefListe;
	  Zeile : string;
	  Zahl, Code : integer;
	begin
	  writeln('Bitte geben Sie die zu sortierenden Zahlen ein.');
	  writeln('Beenden Sie Ihre Eingabe mit einer Leerzeile.');
	  Liste := nil;
	  readln(Zeile);
	  val(Zeile, Zahl, Code); { val konvertiert String nach Integer }
	  while Code=0 do
	  begin
		Anhaengen(Liste, Zahl);
		readln(Zeile);
		val(Zeile, Zahl, Code);
	  end; { while }
	  outListe := Liste;
	end; { ListeEinlesen }

	procedure GibListeAus(inListe : tRefListe);
	{ Gibt die Elemente von inListe aus }
	  var Zeiger : tRefListe;
	begin
	  Zeiger := inListe;
	  while Zeiger <> nil do
	  begin
		writeln(Zeiger^.info);
		Zeiger := Zeiger^.next;
	  end; { while }
	end; { GibListeAus }

begin
  ListeEinlesen(RefListe);
  SortiereListe(RefListe);
  GibListeAus(RefListe)
end.

