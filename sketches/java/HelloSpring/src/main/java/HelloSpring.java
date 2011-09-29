import org.springframework.context.support.ClassPathXmlApplicationContext;


public class HelloSpring {

	/**
	 * @param args
	 */
	public static void main(String[] args) {
		
		int max = 50;
		
		try {

			System.out.println("Hello spring started");
			ClassPathXmlApplicationContext appContext = new ClassPathXmlApplicationContext(new String[] {"HelloSpring.xml"});
			
			System.out.println("Classpath loaded");
			FizzBuzzGenerator generator = appContext.getBean(FizzBuzzGenerator.class);
			
			String output = generator.get_output(max);
			
			if( output==null )
				System.out.println("FATAL: no output");
			else
				System.out.println(output);
			
			System.out.println("Done");

		}

		catch(Exception e) {
			
			e.printStackTrace();
		}
	}

}
