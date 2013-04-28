package tree.specs;

import static org.junit.Assert.assertTrue;
import org.junit.Test;
import tree.DTree;

public class When_getting_element_depth_for_notexisting_element {
	
	private DTree<Integer> subject;
	
	public When_getting_element_depth_for_notexisting_element() {
		this.subject = TreeCreator.CreateTree();
	}
	
	@Test
	public void it_should_be_below_zero() {
		int depth = this.subject.getDepthOf(42);
		assertTrue(depth<0); 
	}
}
