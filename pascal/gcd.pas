{Greatest common divisor}
program gcd(output);

type
	TestFunction = function : boolean;

function gcd(m, n: integer) : integer;
begin
	while m<>n do
		if m>n then
			m := m-n
		else
			n := n-m;
	gcd := m;
end;

function gcdSimpleTest : boolean;
begin
	if gcd(18, 12) = 6 then
		gcdSimpleTest := true
	else
		gcdSimpleTest := false;
end;

function gcdSwapValues : boolean;
begin
	if gcd(12, 18) = 6 then
		gcdSwapValues := true
	else
		gcdSwapValues := false;
end;

procedure ExecuteTest(msg: string; func: TestFunction);
begin
	write(msg);
	if not func() then 
	begin
		writeln('failed');
		halt(0);
	end;
	writeln('passed');
end;

begin
	writeln('Greatest common divisor:'); 
	ExecuteTest('Simple test: ', @gcdSimpleTest);
	ExecuteTest('Swap values: ', @gcdSwapValues);
end.

