package de.slesa.jlo.pms.persistence;

import java.util.List;

import org.hibernate.SessionFactory;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Repository;
import de.slesa.jlo.pms.model.Currency;

@Repository
public class CurrencyDao implements ICurrencyDao {

	@Autowired
	private SessionFactory sessionFactory;
	
//	@PersistenceContext
//	private EntityManager em;
	
	
	@SuppressWarnings("unchecked")
	public List<Currency> findAll() {
		return sessionFactory.getCurrentSession().createCriteria(Currency.class).list();
//		return em.createQuery("from Currency c").getResultList(); 
	}

	public void save(Currency currency) {
		sessionFactory.getCurrentSession().saveOrUpdate(currency);
//		em.persist(currency);
	}

}
