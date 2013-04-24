package specs;

import static org.junit.Assert.assertEquals;
import org.junit.Before;
import org.junit.Test;
import dlist.DList;
import dlist.DPosition;

public class When_prepending_elements {

	private DList<Integer> Subject;
	
	public When_prepending_elements() {
		Subject = new DList<Integer>();
	}
	
	@Before
	public void Because_of() {
		Subject.prepend(1).prepend(2).prepend(3);
	}
	
	@Test
	public void It_should_return_third_element_as_head() {
		DPosition<Integer> element = Subject.getHead();
		assertEquals(element.getValue().intValue(), 3);
	}
	
	@Test
	public void It_should_return_second_element_after_head() {
		DPosition<Integer> element = Subject.getHead().getNext();
		assertEquals(element.getValue().intValue(), 2);
	}
	
	@Test
	public void It_should_return_first_element_after_head() {
		DPosition<Integer> element = Subject.getHead().getNext().getNext();
		assertEquals(element.getValue().intValue(), 1);
	}
	
	@Test
	public void It_should_return_first_element_as_tail() {
		DPosition<Integer> element = Subject.getTail();
		assertEquals(element.getValue().intValue(), 1);
	}
	
	@Test
	public void It_should_return_second_element_before_tail() {
		DPosition<Integer> element = Subject.getTail().getPrevious();
		assertEquals(element.getValue().intValue(), 2);
	}
	
	@Test
	public void It_should_return_third_element_before_tail() {
		DPosition<Integer> element = Subject.getTail().getPrevious().getPrevious();
		assertEquals(element.getValue().intValue(), 3);
	}

}
