program feldermax(input,output);
{$R+} {$B+}

const 
 UNTEN = 0;
 OBEN = 5;

type
 tIndex = UNTEN..OBEN;
 tFeld = array[tIndex] of integer;

var
 i: tIndex;
 feld: tFeld;
 value: integer;
 a, b, c, d, e: integer;


function FeldMaxA (
          var inFeld : tFeld;
              inUnten,
              inOben : tIndex) : integer;
{ bestimmt rekursiv das Maximum in einem Feld
    mit den Grenzen inUnten und inOben }

  var
  Mitte : tIndex;
  MaxL,
  MaxR : integer;

begin
  if inUnten = inOben then
    FeldMaxA := inFeld[inUnten]
  else
  begin
    Mitte := (inUnten + inOben) div 2;
    MaxL := FeldMaxA (inFeld,inUnten,Mitte);
    MaxR := FeldMaxA (inFeld,Mitte+1,inOben);
    if MaxL > MaxR then
      FeldMaxA := MaxL
    else
      FeldMaxA := MaxR
  end
end; { FeldMaxA }


function FeldMaxB (
          var inFeld : tFeld;
              inUnten,
              inOben : tIndex) : integer;
{ bestimmt rekursiv das Maximum in einem
    Feld mit den Grenzen inUnten und inOben }

  var
  Mitte : tIndex;
  MaxL,
  MaxR : integer;

begin
  if inUnten = inOben then
    FeldMaxB := inFeld[inUnten]
  else
  begin
    Mitte := (inUnten + inOben) div 2;
    MaxL := FeldMaxB (inFeld,inUnten,Mitte);
    MaxR := FeldMaxB (inFeld,Mitte,inOben);
    if MaxL > MaxR then
      FeldMaxB := MaxL
    else
      FeldMaxB := MaxR
  end
end; { FeldMaxB }


function FeldMaxC (
          var inFeld : tFeld;
              inUnten,
              inOben : tIndex) : integer;
{ bestimmt rekursiv das Maximum in einem Feld  
    mit den Grenzen inUnten und inOben }

  var
  Mitte : tIndex;
  MaxL,
  MaxR : integer;

begin
  if inUnten > inOben then
    FeldMaxC := inFeld[inUnten]
  else
  begin
    Mitte := (inUnten + inOben) div 2;
    MaxL := FeldMaxC (inFeld,inUnten,Mitte);
    MaxR := FeldMaxC (inFeld,Mitte+1,inOben);
    if MaxL > MaxR then
      FeldMaxC := MaxL
    else
      FeldMaxC := MaxR
  end
end; { FeldMaxC }


function FeldMaxD (
          var inFeld : tFeld;
              inUnten,
              inOben : tIndex) : integer;
{ bestimmt rekursiv das Maximum in einem Feld
    mit den Grenzen inUnten und inOben }

    var
    Mitte : tIndex;
    MaxL,
    MaxR : integer;

begin
  if inUnten > inOben then
    FeldMaxD := inFeld[inUnten]
  else
  begin
    Mitte := (inUnten + inOben) div 2;
    MaxL := FeldMaxD (inFeld,inUnten,Mitte);
    MaxR := FeldMaxD (inFeld,Mitte,inOben);
    if MaxL > MaxR then
      FeldMaxD := MaxL
    else
      FeldMaxD := MaxR
  end
end; { FeldMaxD }


function FeldMaxE (
          var inFeld : tFeld;
              inUnten,
              inOben : tIndex) : integer;
{ bestimmt iterativ das Maximum in einem Feld
    mit den Grenzen inUnten und inOben }

  var
  i : tIndex;
  HilfMax : integer; { Hilfsvariable }

begin
  HilfMax := 0;
  for i := inUnten to inOben do
    if inFeld[i] > HilfMax then
      HilfMax := inFeld[i];
  FeldMaxE := HilfMax
end; { FeldMaxE }


begin
 writeln(OBEN-UNTEN, ' Werte eingeben:');
 for i:=UNTEN to OBEN do begin
  readln(value);
  feld[i] := value;
 end;

 a:= FeldMaxA(feld, UNTEN, OBEN);
 writeln('Max a: ', a);

 (* b:= FeldMaxB(feld, UNTEN, OBEN); 
 writeln('Max b: ', b);*)

 (*c:= FeldMaxC(feld, UNTEN, OBEN);
 writeln('Max c: ', c);*)

 (*d:= FeldMaxD(feld, UNTEN, OBEN);
 writeln('Max d: ', d);*)

 e:= FeldMaxE(feld, UNTEN, OBEN);
 writeln('Max e: ', e);
end.

