var loopLength = 5000000;

var array = [];

for (var i=0; i<loopLength; i++) {
	array[i] = "item "+i;
}

function forLoop() {
	for (var i=0, item; i<array.length; i++) {
		item = array[i];
	}
}

function forLoopCachedLength() {
	for (var i=0, l=array.length, item; i<l; i++) {
		item = array[i];
	}
}

function forLoopDirectAccess() {
	for (var i=0, item; (item=array[i]); i++) {
	}
}

function whileLoop() {
	var i=0, item;
	while (i<array.length) {
		item = array[i];
		i++;
	}
}

function whileLoopCachedLength() {
	var i=0, l=array.length, item;
	while(i<l) {
		item = array[i];
		i++;
	}
}

function reversedWhileLoop() {
	var l = array.length, item;
	while(i--) {
		item = array[i];
	}
}

function doubleReversedWhileLoop() {
	var l = array.length, i=l, item;
	while(i--) {
		item = array[l-i-1];
	}
}

runBenchmark("for-loop", forLoop);
runBenchmark("for-loop, cached length", forLoopCachedLength);
runBenchmark("for-loop, direct array acces", forLoopDirectAccess);
runBenchmark("while-loop", whileLoop);
runBenchmark("while-loop, cached length", whileLoopCachedLength);
runBenchmark("while-loop, reversed", reversedWhileLoop);
runBenchmark("while loop, double reversed", doubleReversedWhileLoop);

