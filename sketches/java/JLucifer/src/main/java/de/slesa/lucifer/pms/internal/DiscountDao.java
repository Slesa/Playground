package de.slesa.lucifer.pms.internal;

import java.util.List;

import javax.persistence.EntityManager;
import javax.persistence.PersistenceContext;

import org.springframework.stereotype.Repository;

import de.slesa.lucifer.pms.model.Discount;
import de.slesa.lucifer.pms.persistence.IDiscountDao;

@Repository
public class DiscountDao implements IDiscountDao {

	@PersistenceContext
	EntityManager em;
	
	@SuppressWarnings("unchecked")
	public List<Discount> findAllDiscounts() {
		
		return em.createQuery("From Discount d").getResultList();
	}

	public void save(Discount discount) {
		
		em.persist(discount);
	}

}
