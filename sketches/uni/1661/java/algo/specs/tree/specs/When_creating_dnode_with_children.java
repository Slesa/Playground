package tree.specs;

import static org.junit.Assert.assertEquals;
import org.junit.Test;
import tree.DNode;

public class When_creating_dnode_with_children {

	private DNode<Integer> subject;
	
	public When_creating_dnode_with_children() {
		subject = new DNode<Integer>(42, new DNode<Integer>(1), new DNode<Integer>(2));
	}
	
	@Test
	public void it_should_have_left() {
		DNode<Integer> left = subject.getLeft();
		assertEquals((Integer)1, (Integer)left.getValue()); 
	}
	
	@Test
	public void it_should_have_right() {
		DNode<Integer> right = subject.getRight();
		assertEquals((Integer)2, (Integer)right.getValue()); 
	}
	
	@Test
	public void it_should_have_value() {
		Integer val = subject.getValue();
		assertEquals((Integer)val, (Integer)42); 
	}

}
