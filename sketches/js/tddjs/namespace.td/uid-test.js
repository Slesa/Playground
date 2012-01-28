TestCase("UidTest", {
	"test should return numeric id": function () {
		var id = tddjs.uid({});
		assertNumber(id);
	},
	
	"test should return consistent id for object": function () {
		var object = {};
		var id = tddjs.uid(object);
		
		assertSame(id, tddjs.uid(object));
	},
	
	"test should return unique id": function () {
		var object = {};
		var object2 = {};
		var id = tddjs.uid(object);
		
		assertNotEquals(id, tddjs.uid(object2));
	},
	
	"test should return consistent id for function": function () {
		var func = function () {};
		var id = tddjs.uid(func);
		
		assertSame(id, tddjs.uid(func));
	},
	
	"test should return undefined for primitive": function () {
		var str = "my string";
		
		assertUndefined(tddjs.uid(str));	
	}
});