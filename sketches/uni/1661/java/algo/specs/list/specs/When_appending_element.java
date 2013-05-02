package list.specs;

import static org.junit.Assert.*;
import org.junit.Before;
import org.junit.Test;
import list.DList;
import list.DPosition;

public class When_appending_element {

	private DList<Integer> Subject;
	
	public When_appending_element() {
		Subject = new DList<Integer>();
	}
	
	@Before
	public void Because_of() {
		Subject.append(42);
	}
	
	@Test
	public void It_should_return_the_element_as_head() {
		DPosition<Integer> element = Subject.getHead();
		assertEquals(element.getValue().intValue(), 42);
	}
	
	@Test
	public void It_should_return_the_element_as_tail() {
		DPosition<Integer> element = Subject.getTail();
		assertEquals(element.getValue().intValue(), 42);
	}
}
