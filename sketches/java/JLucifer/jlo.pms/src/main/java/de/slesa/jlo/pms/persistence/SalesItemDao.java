package de.slesa.jlo.pms.persistence;

import java.util.List;

import org.hibernate.SessionFactory;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Repository;

import de.slesa.jlo.pms.model.SalesItem;

@Repository
public class SalesItemDao implements ISalesItemDao {

	@Autowired
	private SessionFactory sessionFactory;
	
//	@PersistenceContext
//	EntityManager em;

	@SuppressWarnings("unchecked")
	public List<SalesItem> findAll() {
		return sessionFactory.getCurrentSession().createCriteria(SalesItem.class).list();
//		return em.createQuery("From SalesItem si").getResultList();
	}

	public void save(SalesItem salesItem) {
		sessionFactory.getCurrentSession().saveOrUpdate(salesItem);
//		em.persist(salesItem);
	}
	
}
