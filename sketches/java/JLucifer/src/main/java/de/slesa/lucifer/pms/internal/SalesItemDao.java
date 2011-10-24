package de.slesa.lucifer.pms.internal;

import java.util.List;

import javax.persistence.EntityManager;
import javax.persistence.PersistenceContext;

import org.springframework.stereotype.Repository;

import de.slesa.lucifer.pms.model.SalesItem;
import de.slesa.lucifer.pms.persistence.ISalesItemDao;

@Repository
public class SalesItemDao implements ISalesItemDao {

	@PersistenceContext
	EntityManager em;
	
	@SuppressWarnings("unchecked")
	public List<SalesItem> findAllSalesItems() {
		
		return em.createQuery("FROM SalesItem si").getResultList();
	}

	public void save(SalesItem salesItem) {
		
		em.persist(salesItem);
	}

}
