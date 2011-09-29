
public class Format_number implements INumberFormatter {

	public boolean can_handle(int number) {
		
		return true;
	}

	public String handle(int number) {
		
		return String.format("%d", number);
	}

}
