package tree.specs;

import static org.junit.Assert.assertEquals;
import org.junit.Test;
import tree.DNode;
import tree.DTree;

public class When_creating_dtree {

	private DTree<Integer> subject;

	public When_creating_dtree() {
		subject = new DTree<Integer>();
	}
	
	@Test
	public void it_should_have_no_root() {
		DNode<Integer> root = subject.getRoot();
		assertEquals(null, root); 
	}
	
}
