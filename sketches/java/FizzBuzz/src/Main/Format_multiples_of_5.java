package Main;

public class Format_multiples_of_5 implements INumberFormatter {

	@Override
	public boolean can_handle(int number) {
		
		return number%5==0;
	}

	@Override
	public String handle(int number) {
		
		return "Buzz";
	}
	
}