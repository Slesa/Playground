package de.slesa.jlo.pms.persistence;

import java.util.List;

import org.hibernate.SessionFactory;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Repository;

import de.slesa.jlo.pms.model.Payform;

@Repository
public class PayformDao implements IPayformDao {

	@Autowired
	private SessionFactory sessionFactory;
	
//	@PersistenceContext
//	EntityManager em;
	
	@SuppressWarnings("unchecked")
	public List<Payform> findAll() {
		return sessionFactory.getCurrentSession().createCriteria(Payform.class).list();
//		return em.createQuery("From Payform p").getResultList();
	}

	public void save(Payform payform) {
		sessionFactory.getCurrentSession().saveOrUpdate(payform);
//		em.persist(payform);
	}

}
