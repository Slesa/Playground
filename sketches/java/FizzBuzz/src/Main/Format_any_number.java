package Main;

public class Format_any_number implements INumberFormatter {

	@Override
	public boolean can_handle(int number) {
		
		return true;
	}

	@Override
	public String handle(int number) {
		
		return String.format("%d", number);
	}
	
}