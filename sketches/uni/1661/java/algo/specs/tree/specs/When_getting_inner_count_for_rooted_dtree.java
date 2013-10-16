package tree.specs;

import static org.junit.Assert.assertEquals;
import org.junit.Test;
import tree.DTree;

public class When_getting_inner_count_for_rooted_dtree {

	private DTree<Integer> subject;

	public When_getting_inner_count_for_rooted_dtree() {
		subject = new DTree<Integer>(42);
	}
	
	@Test
	public void it_should_be_zero() {
		int inners = subject.getInnerCount();
		assertEquals(0, inners); 
	}
}