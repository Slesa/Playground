package tree.specs;

import static org.junit.Assert.assertEquals;
import org.junit.Test;
import tree.DTree;

public class When_getting_leaf_count_for_filled_dtree {

	private DTree<Integer> subject;

	public When_getting_leaf_count_for_filled_dtree() {
		this.subject = TreeCreator.CreateTree();
	}
	
	@Test
	public void it_should_calculate_leafs() {
		int nodes = this.subject.getLeafCount();
		assertEquals(6, nodes); 
	}
}
