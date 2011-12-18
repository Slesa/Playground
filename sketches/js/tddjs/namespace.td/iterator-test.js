TestCase("iteratorTest", {
	"test next should return first item": function () {
		var collection = [1, 2, 3, 4, 5];
		var iterator = tddjs.iterator(collection);
	
		assertSame(collection[0], iterator.next());
		assertTrue(iterator.hasNext());
	},
	
	"test hasNext should be false after last item": function () {
		var collection = [1, 2];
		var iterator = tddjs.iterator(collection);
		
		iterator.next();
		iterator.next();
		
		assertFalse(iterator.hasNext());
	},
	
	"test should loop collection with iterator": function () {
		var collection = [1, 2, 3, 4, 5];
		var it = tddjs.iterator(collection);
		var result = [];
		
		while (it.hasNext()) {
			result.push(it.next());
		}
			
		assertEquals(collection, result);
	}
});