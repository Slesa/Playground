{Money back}
program moneyback(input, output);

function getCoins(var amount: integer; coin: integer) : integer;
begin
	getCoins := amount div coin;
	amount := amount mod coin;
end;

procedure outputCoins(amount: integer);
var
	value : integer;
begin
	value := amount;
	write(getCoins(value, 50), ' '); 
	write(getCoins(value, 20), ' '); 
	write(getCoins(value, 10), ' '); 
	write(getCoins(value, 5), ' '); 
	write(getCoins(value, 2), ' '); 
	writeln(value);
end;

function readInput : integer;
var
	eingabe: integer;
begin
	write('Eingabe: ');
	readln(eingabe);
	if (eingabe<1) or (eingabe>99) then begin
		writeln('Ungültige Eingabe (1-99)');
		readInput := 0;
	end
	else
		readInput := eingabe;

end;

function readAmount : integer;
var 
	money : integer;
begin
	repeat
		money := readInput;
	until money>0;
	readAmount := money;
end;

begin
	outputCoins(readAmount);
end.
