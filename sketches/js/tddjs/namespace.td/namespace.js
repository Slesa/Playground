var tddjs = (function () {
	function namespace(string) {
		var object = this;
		var levels = string.split(".");
		
		for (var i=0, l=levels.length; i<l; i++) {
			if (typeof object[levels[i]] == "undefined") {
				object[levels[i]] = {};
			}
			
			object = object[levels[i]];
		}
			
		return object;
	}
		
	return {
		namespace : namespace
	};
}());