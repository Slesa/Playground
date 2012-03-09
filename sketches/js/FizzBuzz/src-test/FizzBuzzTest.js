FizzBuzzTest = TestCase("FizzBuzz");

FizzBuzzTest.prototype.testStandardNumber = function() {
	var sut = new FizzBuzz();
	var result = sut.examine(2);
//	jstestdriver.console.log("JsTestDriver", result);
	assertEquals("2", result);
};

FizzBuzzTest.prototype.testFizz = function() {
	var sut = new FizzBuzz();
	var result = sut.examine(9);
	assertEquals("Fizz", result);
};

FizzBuzzTest.prototype.testBuzz = function() {
	var sut = new FizzBuzz();
	var result = sut.examine(10);
	assertEquals("Buzz", result);
};

FizzBuzzTest.prototype.testFizzBuzz = function() {
	var sut = new FizzBuzz();
	var result = sut.examine(15);
	assertEquals("FizzBuzz", result);
};
