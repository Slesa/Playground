
public class Format_multiples_of_5 implements INumberFormatter {

	public boolean can_handle(int number) {
		
		return number%5==0;
	}

	public String handle(int number) {
		
		return "Buzz";
	}

}
