package tree.specs;

import static org.junit.Assert.assertEquals;
import org.junit.Test;
import tree.DTree;

public class When_getting_node_count_for_rooted_dtree {

	private DTree<Integer> subject;

	public When_getting_node_count_for_rooted_dtree() {
		subject = new DTree<Integer>(42);
	}
	
	@Test
	public void it_should_count_one() {
		int nodes = subject.getNodeCount();
		assertEquals(1, nodes); 
	}
}
