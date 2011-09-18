package specs;

import org.junit.Test;

public class When_divisible_by_3_and_5 extends FizzBuzzTest {
	
	@Test
	public void it_should_return_fizzbuzz_for_15() {
		expect(15, "FizzBuzz");
	}
}