program testeZeigListMax(input, output);
{ testet die Funktion ZeigListMax }
{$R+}
{$B+}
  type
  tRefListe = ^tListe;
  tListe = record
             info : integer;
             next : tRefListe
           end;

  var
  Liste,
  MaxZeig : tRefListe;

  function ZeigListMax (inRefAnfang : tRefListe) : tRefListe;
  { bestimmt rekursiv einen Zeiger auf das Listenelement mit
    der groessten Zahl }
  var
    result,
    rest : tRefListe;
  begin
    result := inRefAnfang;
    if inRefAnfang<>nil then
    begin
      rest := ZeigListMax(inRefAnfang^.next);
      if rest<>nil then
        if rest^.info>inRefAnfang^.info then
          result := rest
    end;
    ZeigListMax := result
  end;

procedure LiesListe(var outListe : tRefListe);
  { Liest eine (evtl. leere) Liste ein und gibt deren
    Anfangszeiger outListe zurueck. }

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


begin 
  LiesListe (Liste);
  { Die zu testende Funktion wird zweimal aufgerufen, damit tatsaechlich
    ein Fehler auftritt, wenn die Liste durch den Aufruf zerstoert wird. }
  MaxZeig := ZeigListMax (Liste);
  MaxZeig := ZeigListMax (Liste);
  if MaxZeig = nil then
    writeln('Leere Eingabefolge!')
  else
    writeln ('Das Maximum ist ', MaxZeig^.info, '.')
end. { testeZeigListMax }

