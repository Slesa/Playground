(function(global) {
	"use strict";
	
	TestCase("FizzBuzz", {
	"test should print out integer": function() {
		var fizzBuzz = new FizzBuzzer();
		var spy = sinon.spy(fizzBuzz, "examine");
		var result = spy.withArgs(2);
		assertEqual("2", result);
	}
});

}(this));
