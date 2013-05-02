package tree.specs;

import org.junit.Test;
import static org.junit.Assert.*;
import tree.DNode;

public class When_creating_dnode {

	private DNode<Integer> subject;
	
	public When_creating_dnode() {
		subject = new DNode<Integer>(42);
	}
	
	@Test
	public void it_should_have_no_left() {
		DNode<Integer> left = subject.getLeft();
		assertEquals(null, left); 
	}
	
	@Test
	public void it_should_have_no_right() {
		DNode<Integer> right = subject.getRight();
		assertEquals(null, right); 
	}
	
	@Test
	public void it_should_have_value() {
		int val = subject.getValue();
		assertEquals(val, 42); 
	}
	
}
