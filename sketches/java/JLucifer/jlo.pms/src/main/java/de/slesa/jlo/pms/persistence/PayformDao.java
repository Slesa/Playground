package de.slesa.jlo.pms.persistence;

import java.util.List;

import javax.persistence.EntityManager;
import javax.persistence.PersistenceContext;

import org.springframework.stereotype.Repository;

import de.slesa.jlo.pms.model.Payform;

@Repository
public class PayformDao implements IPayformDao {

	@PersistenceContext
	EntityManager em;
	
	@SuppressWarnings("unchecked")
	public List<Payform> findAll() {
		return em.createQuery("From Payform p").getResultList();
	}

	public void save(Payform payform) {
		em.persist(payform);
	}

}
