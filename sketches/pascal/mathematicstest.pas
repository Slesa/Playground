unit mathematicstest;

{$mode objfpc}{$H+}

interface

uses 
	Classes, mathematics, fpcunit;

type

	TMathematicsTest = class(TTestCase)
	private
		mathematics: TMathematics;
	protected
		procedure SetUp; override;
		{procedure TearDown; override;}
	published
		procedure testGcd;
	end;

implementation

procedure TMathematicsTest.SetUp;
begin
	mathematics := TMathematics.Create();
end;

procedure TMathematicsTest.testGcd;
var result: Integer;
begin
	result := mathematics.gcd(12, 18);
	AssertEquals('gcd is not correct', 6, result);
end;

end.

