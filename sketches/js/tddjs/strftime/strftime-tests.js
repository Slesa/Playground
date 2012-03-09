function assert(message, expr) {
	if (!expr) {
		throw new Error(message);
	}
	assert.count += 1;
	return true;
}

assert.count = 0;

function output(text, color) {
	var p = document.createElement("p");
	p.innerHTML = text;
	p.style.color = color;
	document.body.appendChild(p);
}

function testCase(name, tests) {
	assert.count = 0;
	var successful = 0;
	var testCount = 0;
	var hasSetup = typeof tests.setUp == "function";
	var hasTeardown = typeof tests.tearDown == "function";

	for (var test in tests) {
		if (!/^test/.test(test)) {
			continue;
		}

		testCount += 1;

		try {
			if (hasSetup) {
				tests.setUp();
			}

			tests[test]();
			output(test, "#0x0c0");

			if (hasTeardown) {
				tests.tearDown();
			}

			successful += 1;
		} catch (e) {
			output (test + " failed: " + e.message, "#c00");
		}
	}

	var color = successful == testCount ? "#0c0" : "0xc00";
	output("<strong>" + testCount + " tests, " 
		+ (testCount-successful) + " failures</strong>", color);
}



testCase("strftime test", {

	setUp: function() {
		this.date = new Date(2009, 9, 2, 22, 14, 45);
	},

	"test format specifier %d": function () {
		assert("%d should return day", this.date.strftime("%d")=="02");
	},

	"test format specifier %m": function () {
		assert("%m should return month", this.date.strftime("%m")=="10");
	},

	"test format specifier %y": function () {
		assert("%y should return year as two digits", this.date.strftime("%y")=="09");
	},

	"test format specifier %Y": function () {
		assert("%Y should return full year", this.date.strftime("%Y")=="2009");
	},

	"test format specifiert %F": function() {
		assert("%F should act as %Y-%m-%d", this.date.strftime("%F")=="2009-10-02");
	},

	"test format specifiert %D": function() {
		assert("%D should act as %m/%d/%y", this.date.strftime("%D")=="10/02/09");
	}
});
