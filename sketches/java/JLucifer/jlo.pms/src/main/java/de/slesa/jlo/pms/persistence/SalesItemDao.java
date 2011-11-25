package de.slesa.jlo.pms.persistence;

import java.util.List;

import javax.persistence.EntityManager;
import javax.persistence.PersistenceContext;

import org.springframework.stereotype.Repository;

import de.slesa.jlo.pms.model.SalesItem;

@Repository
public class SalesItemDao implements ISalesItemDao {

	@PersistenceContext
	EntityManager em;

	@SuppressWarnings("unchecked")
	public List<SalesItem> findAll() {
		return em.createQuery("From SalesItem si").getResultList();
	}

	public void save(SalesItem salesItem) {
		em.persist(salesItem);
	}
	
}
