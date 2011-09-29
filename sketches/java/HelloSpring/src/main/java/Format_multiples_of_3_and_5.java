
public class Format_multiples_of_3_and_5 implements INumberFormatter {

	public boolean can_handle(int number) {
		
		return number%3==0 && number%5==0;
	}

	public String handle(int number) {
		
		return "FizzBuzz";
	}
}
