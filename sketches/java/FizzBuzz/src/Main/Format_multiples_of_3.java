package Main;

public class Format_multiples_of_3 implements INumberFormatter {

	@Override
	public boolean can_handle(int number) {
		
		return number%3==0;
	}

	@Override
	public String handle(int number) {
		
		return "Fizz";
	}
 
}