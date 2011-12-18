(function () {
	function iterator(collection) {
		var index = 0;
		var length = collection.length;
		
		function next() {
			var item = collection[index++];
			return item;
		}
		
		function hasNext() {
			return index<length;
		}
		
		return {
			next: next,
			hasNext: hasNext 
		};
	}
	
	if (typeof tddjs === "object") {
		tddjs.iterator = iterator;
	}
}());
