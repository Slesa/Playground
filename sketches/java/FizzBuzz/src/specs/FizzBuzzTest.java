package specs;
import org.junit.Before;

import Main.FizzBuzz;
import static org.junit.Assert.assertEquals;


public abstract class FizzBuzzTest {

	protected FizzBuzz _fizzBuzz;
	
	@Before
	public void setUp() throws Exception {
		_fizzBuzz = new FizzBuzz();
	}
	
	protected void expect(int number, String expect) {
		
		String result = _fizzBuzz.get_output(number);
		assertEquals(result, expect);
	}
}