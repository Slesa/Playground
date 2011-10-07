package de.slesa.lucifer.pms.internal;

import java.util.List;

import javax.persistence.EntityManager;
import javax.persistence.PersistenceContext;

import org.springframework.stereotype.Repository;

import de.slesa.lucifer.pms.model.Payform;
import de.slesa.lucifer.pms.persistence.IPayformDao;

@Repository
public class PayformDao implements IPayformDao {

	@PersistenceContext
	EntityManager em;
	
	@SuppressWarnings("unchecked")
	public List<Payform> findAllPayforms() {
		
		return em.createQuery("From Payform p").getResultList();
	}
	
	public void save(Payform payform) {
		em.persist(payform);
	}
}
