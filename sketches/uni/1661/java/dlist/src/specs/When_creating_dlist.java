package specs;

import static org.junit.Assert.*;
import org.junit.Test;
import dlist.DList;
import dlist.DPosition;

public class When_creating_dlist {

	private DList<Integer> subject;
	
	public When_creating_dlist() {
		subject = new DList<Integer>();
	}
	
	@Test
	public void it_should_have_no_head() {
		DPosition<Integer> head = subject.getHead();
		assertEquals(null, head); 
	}
	
	@Test
	public void it_should_have_no_tail() {
		DPosition<Integer> tail = subject.getTail();
		assertEquals(null, tail); 
	}

}
