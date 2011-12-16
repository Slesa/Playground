TestCase("strftimeTest", {
	setUp: function () {
		this.date = new Date(2009, 9, 2, 22, 14, 45);
	},

	tearDown: function () {
		delete this.date;
	},

	"test %Y should return full year": function () {
		var year = Date.formats.Y(this.date);
		assertNumber(year);
		assertEquals(2009, year);
	},

	"test %m should return month": function () {
		var month = Date.formats.m(this.date);
		assertString(month);
		assertEquals("10", month);
	},

	"test %d should return day": function () {
		assertEquals("02", Date.formats.d(this.date));
	},

	"test %y should return year as two digits": function () {
		assertEquals("09", Date.formats.y(this.date));
	},

	"test %F should act as %Y-%m-%d": function () {
		assertEquals("2009-10-02", this.date.strftime("%F"));
	}
});
