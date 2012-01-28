function fibonacciSlow(x) {
	if (x<2) {
		return 1;
	}
	return fibonacciSlow(x-1) + fibonacciSlow(x-2);
}

var fibonacciFast = (function () {
	var cache = {};
	
	function fibonacci(x) {
		if( x<2) {
			return 1;
		}
		if (!cache[x]) {
			cache[x] = fibonacci(x-1) + fibonacci(x-2);
		}
		return cache[x];
	}
	
	return fibonacci;
}());

if (!Function.prototype.memoize) {
	Function.prototype.memoize = function () {
		var cache = {};
		var func = this;
		
		return function (x) {
			if (!(x in cache)) {
				cache[x] = func.call(this, x);
			}
			return cache[x];
		};
	};
}
