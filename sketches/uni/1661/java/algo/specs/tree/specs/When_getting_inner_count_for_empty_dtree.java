package tree.specs;

import static org.junit.Assert.assertEquals;
import org.junit.Test;
import tree.DTree;

public class When_getting_inner_count_for_empty_dtree {
	
	private DTree<Integer> subject;
	
	public When_getting_inner_count_for_empty_dtree() {
		subject = new DTree<Integer>();
	}
	
	@Test
	public void it_should_be_zero() {
		int inners = subject.getInnerCount();
		assertEquals(0, inners); 
	}
}
