package tree.specs;

import static org.junit.Assert.*;
import org.junit.Test;
import tree.DTree;

public class When_getting_element_depth_for_filled_dtree {
	
	private DTree<Integer> subject;
	
	public When_getting_element_depth_for_filled_dtree() {
		this.subject = TreeCreator.CreateTree();
	}
	
	@Test
	public void it_should_find_1() {
		int depth = this.subject.getDepthOf(1);
		assertEquals(0, depth); 
	}
	
	@Test
	public void it_should_find_7() {
		int depth = this.subject.getDepthOf(7);
		assertEquals(2, depth); 
	}
	
	@Test
	public void it_should_find_8() {
		int depth = this.subject.getDepthOf(11);
		assertEquals(3, depth); 
	}
	
	@Test
	public void it_should_find_11() {
		int depth = this.subject.getDepthOf(11);
		assertEquals(3, depth); 
	}

}
