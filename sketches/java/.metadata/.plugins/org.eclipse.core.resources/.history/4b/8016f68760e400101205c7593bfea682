package Main;

import java.util.ArrayList;
import java.util.List;

public class FizzBuzz {

	static List<INumberFormatter> _numberFormatter;
	
	public FizzBuzz() {
	
		_numberFormatter = new ArrayList<INumberFormatter>();
		_numberFormatter.add( new Format_multiples_of_3_and_5() );
		_numberFormatter.add( new Format_multiples_of_3() );
		_numberFormatter.add( new Format_multiples_of_5() );
		_numberFormatter.add( new Format_any_number() );
		
	}

	public String get_output(int number) {
		
		for(INumberFormatter numberFormatter : _numberFormatter) {
			if( numberFormatter.can_handle(number))
				return numberFormatter.handle(number);
		}
		
		return null;
	}
	
	/**
	 * @param args
	 */
	public static void main(String[] args) {
		
		FizzBuzz _fizzBuzz = new FizzBuzz();
		int max = 10;
		for(int i=0; i<max; i++)
		{
			System.out.println( _fizzBuzz.get_output(i) );
		}
	}

}
