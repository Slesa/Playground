TestCase("iteratorTest", {
	"test next should return first item": function () {
		var collection = [1, 2, 3, 4, 5];
/*		var iterator = tddjs.iterator(collection);
	
		assertSame(collection[0], iterator.next());
		assertTrue(iterator.hasNext());*/
		var next = tddjs.iterator(collection);
		
		assertSame(collection[0], next());
		assertTrue(next.hasNext);
	},
	
	"test hasNext should be false after last item": function () {
		var collection = [1, 2];
/*		var iterator = tddjs.iterator(collection);
		
		iterator.next();
		iterator.next();
		
		assertFalse(iterator.hasNext());*/
		var next = tddjs.iterator(collection);
		next();
		next();
		assertFalse(next.hasNext);
	},
	
	"test should loop collection with iterator": function () {
		var collection = [1, 2, 3, 4, 5];
		var result = [];
/*		var it = tddjs.iterator(collection);
		
		while (it.hasNext()) {
			result.push(it.next());
		}*/
		var next = tddjs.iterator(collection);
		while (next.hasNext)
			result.push(next());
			
		assertEquals(collection, result);
	}
});