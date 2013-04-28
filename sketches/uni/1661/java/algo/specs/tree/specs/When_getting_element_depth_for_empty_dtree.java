package tree.specs;

import static org.junit.Assert.assertTrue;
import org.junit.Test;
import tree.DTree;

public class When_getting_element_depth_for_empty_dtree {
	
	private DTree<Integer> subject;
	
	public When_getting_element_depth_for_empty_dtree() {
		subject = new DTree<Integer>();
	}
	
	@Test
	public void it_should_be_below_zero() {
		int depth = subject.getDepthOf(42);
		assertTrue(depth<0); 
	}
}
