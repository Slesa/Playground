package rangier.specs;

import static org.junit.Assert.*;
import org.junit.Before;
import org.junit.Test;
import rangier.Bahnhof;
import rangier.WagonTarget;

public class When_switching_waggons {

	private Bahnhof subject;
	private int moves;
	
	public When_switching_waggons() {
		this.subject = new Bahnhof();
		this.subject.AddWagonToG(WagonTarget.A).AddWagonToG(WagonTarget.A).AddWagonToG(WagonTarget.B);
		this.subject.AddWagonToH(WagonTarget.B).AddWagonToH(WagonTarget.A).AddWagonToH(WagonTarget.B).AddWagonToH(WagonTarget.A);
	}
	
	@Before
	public void Because_of() {
		this.moves = this.subject.SwitchWagons();
	}
	
	@Test
	public void it_should_leave_track_i_empty() {
		assertTrue(this.subject.getTrackI().isEmpty());
	}
	
	@Test
	public void it_should_leave_all_b_on_track_h() {
		while(!this.subject.getTrackH().isEmpty()) {
			assertEquals(WagonTarget.B, this.subject.getTrackH().pop());
		}
	}
	
	@Test
	public void it_should_leave_all_a_on_track_g() {
		while(!this.subject.getTrackG().isEmpty()) {
			assertEquals(WagonTarget.A, this.subject.getTrackG().pop());
		}
	}
	
	@Test
	public void it_should_take_14_moves() {
		assertEquals(14, this.moves);
	}
}
