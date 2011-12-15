YUI({
	combine: true,
	timeout:10000
}).use("node", "console", "test", function (Y) {
	var assert = Y.Assert;

	var strftimeTestCase = new Y.Test.Case({
		name: "Date.prototype.strftime Tests",

		setUp: function () {
			this.date = new Date(2009, 9, 2, 22, 14, 15);
		},

		tearDown: function () {
			delete this.date;
		},

		"test %Y should return full year": function () {
			var year = Date.formats.Y(this.date);
			assert.isNumber(year);
			assert.areEqual(2009, year);
		},

		"test %m should return month": function () {
			var month = Date.formats.m(this.date);
			assert.isString(month);
			assert.areEqual("10", month);
		},

		"test %d should return day" : function () {
			assert.areEqual("02", Date.formats.d(this.date));
		},

		"test %y should return year as two digits" : function () {
			assert.areEqual("09", Date.formats.y(this.date));
		},

		"test %f should act as %Y-%m-%d" : function () {
			assert.areEqual("2009-10-02", this.date.strftime("%F"));
		}
	});

	var r = new Y.Console({
		newestOnTop: false,
		style: 'block'
	});

	r.render("#testReport");
	Y.Test.Runner.add(strftimeTestCase);
	Y.Test.Runner.run();
});
