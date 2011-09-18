package specs;
import org.junit.Test;

public class When_divisible_by_3 extends FizzBuzzTest {

	@Test
	public void it_should_return_fizz_for_3() {
		expect(3, "Fizz");
	}
	
	@Test
	public void it_should_return_fizz_for_6() {
		expect(6, "Fizz");
	}
}