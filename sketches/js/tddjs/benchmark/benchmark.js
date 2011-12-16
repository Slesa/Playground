var ol;

function runBenchmark(name, test) {
	if (!ol) {
		ol = document.createElement("ol");
		document.body.appendChild(ol);
	}

	setTimeout(function () {
		var start = new Date().getTime();
		test();
		var total = new Date().getTime() - start;

		var li = document.createElement("li");
		li.innerHTML = name+": " + total + " ms";
		ol.appendChild(li);
	}, 15);

}

