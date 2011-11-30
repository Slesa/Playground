package de.slesa.jlo.pms.persistence;

import java.util.List;

import org.hibernate.SessionFactory;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Repository;

import de.slesa.jlo.pms.model.Discount;

@Repository
public class DiscountDao implements IDiscountDao {

	@Autowired
	private SessionFactory sessionFactory;
	
//	@PersistenceContext
//	EntityManager em;
	
	@SuppressWarnings("unchecked")
	public List<Discount> findAll() {
		return sessionFactory.getCurrentSession().createCriteria(Discount.class).list();
//		return em.createQuery("From Discount d").getResultList();
	}

	public void save(Discount discount) {
		sessionFactory.getCurrentSession().saveOrUpdate(discount);
//		em.persist(discount);
	}

}
