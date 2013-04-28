package tree.specs;

import static org.junit.Assert.assertEquals;
import org.junit.Test;
import tree.DTree;

public class When_getting_leaf_count_for_abnormal_tree {

	private DTree<Integer> subject;

	public When_getting_leaf_count_for_abnormal_tree() {
		this.subject = TreeCreator.CreateAbnormalTree();
	}
	
	@Test
	public void it_should_calculate_leafs() {
		int leafs = this.subject.getLeafCount();
		assertEquals(1, leafs); 
	}
}
