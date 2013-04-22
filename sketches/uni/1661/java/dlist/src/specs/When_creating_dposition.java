package specs;
import static org.junit.Assert.*;
import org.junit.Test;

import dlist.DPosition;

public class When_creating_dposition {

	private DPosition<Integer> subject;
	
	public When_creating_dposition() {
		this.subject = new DPosition<Integer>();
	}
	
	@Test
	public void it_should_have_no_value() {
		assertNull(this.subject.getValue());
	}

	@Test
	public void it_should_have_no_succ() {
		assertNull(this.subject.getSucc());
	}
	
	@Test
	public void it_should_have_no_pred() {
		assertNull(this.subject.getPred());
	}
}
