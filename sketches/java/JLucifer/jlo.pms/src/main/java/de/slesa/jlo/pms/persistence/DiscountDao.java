package de.slesa.jlo.pms.persistence;

import java.util.List;

import javax.persistence.EntityManager;
import javax.persistence.PersistenceContext;

import org.springframework.stereotype.Repository;

import de.slesa.jlo.pms.model.Discount;

@Repository
public class DiscountDao implements IDiscountDao {

	@PersistenceContext
	EntityManager em;
	
	@SuppressWarnings("unchecked")
	public List<Discount> findAll() {
		return em.createQuery("From Discount d").getResultList();
	}

	public void save(Discount discount) {
		em.persist(discount);
	}

}
