(function () {
	function iterator(collection) {
		var index = 0;
		var length = collection.length;
/**/		
		function next() {
			var item = collection[index++];
			next.hasNext = index < length;
			return item;
		}
		
		next.hasNext = index < length;
		return next;
/**/
		/*
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
		*/
	}
	
	if (typeof tddjs === "object") {
		tddjs.iterator = iterator;
	}
}());
