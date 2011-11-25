package de.slesa.jlo.pms.persistence;

import java.util.List;

import javax.persistence.EntityManager;
import javax.persistence.PersistenceContext;

import org.springframework.stereotype.Repository;

import de.slesa.jlo.pms.model.SalesFamily;

@Repository
public class SalesFamilyDao implements ISalesFamilyDao {

	@PersistenceContext
	EntityManager em;

	@SuppressWarnings("unchecked")
	public List<SalesFamily> findAll() {
		return em.createQuery("From SalesFamily sf").getResultList();
	}

	public void save(SalesFamily salesFamily) {
		em.persist(salesFamily);
	}
	
}
