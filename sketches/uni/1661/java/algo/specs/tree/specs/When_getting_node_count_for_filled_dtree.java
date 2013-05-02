package tree.specs;

import static org.junit.Assert.assertEquals;
import org.junit.Test;
import tree.DTree;

public class When_getting_node_count_for_filled_dtree {

	private DTree<Integer> subject;

	public When_getting_node_count_for_filled_dtree() {
		this.subject = TreeCreator.CreateTree();
	}
	
	@Test
	public void it_should_calculate_nodes() {
		int nodes = this.subject.getNodeCount();
		assertEquals(11, nodes); 
	}
}
