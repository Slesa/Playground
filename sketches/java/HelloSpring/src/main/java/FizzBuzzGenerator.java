import java.util.List;


public class FizzBuzzGenerator {

	List<INumberFormatter> _formatters;
	
	public FizzBuzzGenerator(List<INumberFormatter> formatters) {
		_formatters = formatters;
	}
	
	String get_output(int maximum) {
		
		String result = new String();
		for(int i=1; i<=maximum; i++)
			result += get_output_for(i) + " ";
		return result;
	}
	
	String get_output_for(int number) {
		
		for(INumberFormatter formatter : _formatters) {
			if( formatter.can_handle(number))
				return formatter.handle(number);
		}
		
		return null;
	}
	
}
