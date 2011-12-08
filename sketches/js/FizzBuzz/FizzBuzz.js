// https://github.com/mroderick/PubSubJS/
var FizzBuzz = function() {
	var rules = [];

	this.examine = function(num) {
		var buffer;
		for ( var i = 0; i < rules.length; i++) {
			buffer = rules[i](num);
			if (buffer == null)
				continue;
			return buffer;
		}
		;
	};

	function divisionBy3(num) {
		if (num % 3 === 0)
			return "Fizz";
		return null;
	}

	function divisionBy5(num) {
		if (num % 5 === 0)
			return "Buzz";
		return null;
	}

	function divisionBy3And5(num) {
		if (num % 3 === 0 && num % 5 === 0)
			return "FizzBuzz";
		return null;
	}

	function defaultRule(num) {
		return num;
	}

	function init() {
		rules.push(divisionBy3And5);
		rules.push(divisionBy3);
		rules.push(divisionBy5);
		rules.push(defaultRule);
	}
	;

	init();
};

var Main = function() {

	function print(x) {
		document.write(x);
	}

	function println(x) {
		print(x + "<br>");
	}

	this.doOutput = function() {
		var fizzBuzz = new FizzBuzz();
		var max = 20;
		for ( var i = 1; i < max; i++) {
			var result = fizzBuzz.examine(i);
			println(result);
		}
		;
	};
};
