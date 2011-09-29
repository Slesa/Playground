import java.util.Date;
import java.util.List;

import org.hibernate.Session;
import org.hibernate.SessionFactory;
import org.hibernate.cfg.Configuration;


public class HelloHibernate {

	static private SessionFactory sessionFactory;
	/**
	 * @param args
	 */
	public static void main(String[] args) {

		try {
			init_session_factory();
			
			create_values();
			show_list_of_values();
			
			shutdown_session_factory();
		} catch (Exception e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		
	}
	
	private static void show_list_of_values() {
		System.out.println();
		System.out.print("Result:");
		Session session = sessionFactory.openSession();
        session.beginTransaction();
        List result = session.createQuery( "from Event" ).list();
		for ( Event event : (List<Event>) result ) {
			System.out.println( "Event (" + event.getDate() + ") : " + event.getTitle() );
		}
        session.getTransaction().commit();
        session.close(); 
		System.out.println();
	}

	private static void create_values() {
		System.out.print("creating values...");
		Session session = sessionFactory.openSession();
		session.beginTransaction();
		session.save( new Event( "Our very first event!", new Date() ) );
		session.save( new Event( "A follow up event", new Date() ) );
		session.getTransaction().commit();
		session.close(); 		
		System.out.println("done");
	}

	private static void init_session_factory() throws Exception {
		System.out.print("creating session...");
		sessionFactory = new Configuration()
			.configure()
			.buildSessionFactory();
		System.out.println("done");
	}

	private static void shutdown_session_factory() throws Exception {
		System.out.print("shutting down session...");
		if( sessionFactory!=null )
			sessionFactory.close();
		System.out.println("done");
	}

}
