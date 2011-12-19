TestCase("FibonacciTest", {
	"test calculate fibonacci number slow": function () {
		var start = new Date().getTime();
		var result = fibonacciSlow(30);
		var total = new Date().getTime() - start;
		jstestdriver.console.log("fib", "Elapsed time "+total+" ms");
		assertEquals(1346269, result);
	},

	"test calculate fibonacci number fast": function () {
		var start = new Date().getTime();
		var result = fibonacciFast(30);
		var total = new Date().getTime() - start;
		jstestdriver.console.log("fib", "Elapsed time "+total+" ms");
		assertEquals(1346269, result);
	},
	
	"test calculate fibonacci number accelerated": function () {
		var start = new Date().getTime();
		var fibonacci = fibonacciSlow.memoize();
		var result = fibonacci(30);
		var total = new Date().getTime() - start;
		jstestdriver.console.log("fib", "Elapsed time "+total+" ms");
		assertEquals(1346269, result);
	}
});