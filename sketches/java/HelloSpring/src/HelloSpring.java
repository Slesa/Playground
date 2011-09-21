import org.springframework.context.support.ClassPathXmlApplicationContext;

public class HelloSpring {

	/**
	 * @param args
	 */
	public static void main(String[] args) {
		
		try {

			System.out.println("Hello spring started");
			
			ClassPathXmlApplicationContext appContext = new ClassPathXmlApplicationContext(new String[] {"hellospring.xml"});
			
			System.out.println("Classpath loaded");
			
			System.out.println("Done");
			
		}
		catch(Exception e) {
			
			e.printStackTrace();
		}
	}

}
