package de.slesa.lucifer.pms.internal;

import java.util.List;

import javax.persistence.EntityManager;
import javax.persistence.PersistenceContext;

import org.springframework.stereotype.Repository;

import de.slesa.lucifer.pms.model.SalesFamily;
import de.slesa.lucifer.pms.persistence.ISalesFamilyDao;

@Repository
public class SalesFamilyDao implements ISalesFamilyDao {

	@PersistenceContext
	EntityManager em;
	
	@SuppressWarnings("unchecked")
	public List<SalesFamily> findAllSalesFamilies() {
		
		return em.createQuery("FROM SalesFamily sf").getResultList();
	}

	public void save(SalesFamily salesFamily) {
		
		em.persist(salesFamily);
	}

}
