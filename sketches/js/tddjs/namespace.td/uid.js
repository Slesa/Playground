(function() {
	var id = 0;
	
	function uid(object) {
		if (typeof object.__uid != "number") {
			object.__uid = id++;
		}
		return object.__uid;
	}
	
	if (typeof tddjs=="object") {
		tddjs.uid = uid;
	}
}());