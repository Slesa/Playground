program KuerzenUndAusgeben (input, output);
{$R+}
{$B+}

procedure KuerzenUndAusgebenA;

  type
  tNatZahlPlus = 1..maxint;

  var
  Zaehler,
  Nenner : tNatZahlPlus;

  procedure Ausgabe (inZaehler, 
            inNenner : tNatZahlPlus);
  {Gibt den gekuerzten Bruch aus}

  begin
    writeln('gekuerzter Bruch:',inZaehler,
            '/', inNenner)
  end; {Ausgabe}

  procedure Kuerzen (var ioZaehler,
                    ioNenner : tNatZahlPlus);
  {Kuerzt den Bruch}
  
    var
    Teiler : tNatZahlPlus;
  
    function GGT (
        inZahl1, 
        inZahl2 : tNatZahlPlus) : tNatZahlPlus;
    {liefert den groessten gemeinsamen 
    Teiler von inZahl1 und inZahl2}

      var
      Hilf1,
      Hilf2 : tNatZahlPlus;

    begin
      Hilf1 := inZahl1;
      Hilf2 := inZahl2;
      while Hilf1 <> Hilf2 do
        if Hilf1 > Hilf2 then 
          Hilf1 := Hilf1 - Hilf2
        else
          Hilf2 := Hilf2 - Hilf1;
      GGT := Hilf1
    end; {GGT}
    
  begin {Kuerzen}
    Teiler := GGT (ioZaehler, ioNenner);
    ioZaehler := ioZaehler div Teiler;
    ioNenner := ioNenner div Teiler
  end; {Kuerzen}

begin{KuerzenUndAusgebenA}
  writeln ('*** A ***');
  writeln ('Geben Sie den Zaehler ein');
  read (Zaehler);
  writeln ('Geben Sie den Nenner ein');
  read (Nenner);
  Kuerzen (Zaehler, Nenner);
  Ausgabe (Zaehler, Nenner)
end;{KuerzenUndAusgebenA}

(*
procedure KuerzenUndAusgebenB;

  type
  tNatZahlPlus = 1..maxint;

  var
  Zaehler,
  Nenner : tNatZahlPlus;
  
  procedure Kuerzen (
            var ioZaehler, 
            ioNenner : tNatZahlPlus);

    var
    Teiler : integer;
  
    function GGT (
      inZahl1, 
      inZahl2 : tNatZahlPlus) : tNatZahlPlus;

      var
      Hilf1,
      Hilf2 : tNatZahlPlus;

    begin
      Hilf1 := inZahl1;
      Hilf2 := inZahl2;
      while Hilf1 <> Hilf2 do
        if Hilf1 > Hilf2 then 
          Hilf1 := Hilf1 - Hilf2
        else
          Hilf2 := Hilf2 - Hilf1;
      GGT := Hilf1
    end; 

      procedure Ausgabe (
            inZaehler, 
            inNenner : tNatZahlPlus);
  
      begin
        writeln('gekuerzter Bruch:',inZaehler,
                '/', inNenner)
      end; 

  begin 
    Teiler := GGT (ioZaehler, ioNenner);
    ioZaehler := ioZaehler div Teiler;
    ioNenner := ioNenner div Teiler
  end; 

begin 
  writeln ('*** B ***');
  writeln ('Geben Sie den Zaehler ein');
  read (Zaehler);
  writeln ('Geben Sie den Nenner ein');
  read (Nenner);
  Kuerzen (Zaehler, Nenner);
  Ausgabe (Zaehler, Nenner)
end; 
*)

procedure KuerzenUndAusgebenC;

  type
  tNatZahlPlus = 1..maxint;

  var
  Zaehler,
  Nenner,
  Teiler : tNatZahlPlus;

  procedure Ausgabe (inZaehler, 
            inNenner : tNatZahlPlus);
  {Gibt den gekuerzten Bruch aus}

  begin
    writeln('gekuerzter Bruch:',inZaehler,
            '/', inNenner)
  end; {Ausgabe}

  procedure GGT (inZahl1, 
                 inZahl2 : tNatZahlPlus);
  {bestimmt den groessten gemeinsamen 
  Teiler von inZahl1 und inZahl2}
    
    var
    Hilf1,
    Hilf2 : tNatZahlPlus;
    
  begin
    Hilf1 := inZahl1;
    Hilf2 := inZahl2;
    while Hilf1 <> Hilf2 do
      if Hilf1 > Hilf2 then 
        Hilf1 := Hilf1 - Hilf2
      else
        Hilf2 := Hilf2 - Hilf1;
    Teiler := Hilf1
  end; {GGT}

  procedure Kuerzen (inZaehler, 
                     inNenner : tNatZahlPlus);
  {Kuerzt den Bruch}
    
    var
    Temp1,
    Temp2 : tNatZahlPlus;

  begin
    Temp1 := inZaehler;
    Temp2 := inNenner;
    inZaehler := Temp1 div Teiler;
    inNenner := Temp2 div Teiler
  end; {Kuerzen}

begin {KuerzenUndAusgebenC}
  writeln ('*** C ***');
  writeln ('Geben Sie den Zaehler ein');
  read (Zaehler);
  writeln ('Geben Sie den Nenner ein');
  read (Nenner);
  GGT(Zaehler,Nenner);
  Kuerzen (Zaehler,Nenner);
  Ausgabe (Zaehler,Nenner)
end;{KuerzenUndAusgebenC}

begin
	KuerzenUndAusgebenA;
	KuerzenUndAusgebenC;
end.

