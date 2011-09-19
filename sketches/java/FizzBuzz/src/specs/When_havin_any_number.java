package specs;

import org.junit.Test;


public class When_havin_any_number extends FizzBuzzTest {

	@Test
	public void it_should_return_same_as_string() {
		expect(7, "7");
	}

}
