program temperatur(input,output);

var
	eingabe: integer;

function getCelsius(fahrenheit: integer) : real;
begin
	getCelsius := (5*(fahrenheit-32)) / 9;
end;

begin
	write('Fahrenheit: ');
	readln(eingabe);
	writeln('In celsius: ', getCelsius(eingabe):10:2);
end.
