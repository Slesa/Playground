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
	public void it_should_return_front() {
		DPosition<Integer> front = subject.front();
		assertEquals(front, subject); 
	}

}
