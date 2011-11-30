package de.slesa.jlo.pms.persistence;

import java.util.List;

import org.hibernate.SessionFactory;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Repository;

import de.slesa.jlo.pms.model.SalesFamily;

@Repository
public class SalesFamilyDao implements ISalesFamilyDao {

	@Autowired
	private SessionFactory sessionFactory;
	
//	@PersistenceContext
//	EntityManager em;

	@SuppressWarnings("unchecked")
	public List<SalesFamily> findAll() {
		return sessionFactory.getCurrentSession().createCriteria(SalesFamily.class).list();
//		return em.createQuery("From SalesFamily sf").getResultList();
	}

	public void save(SalesFamily salesFamily) {
		sessionFactory.getCurrentSession().saveOrUpdate(salesFamily);
//		em.persist(salesFamily);
	}
	
}
