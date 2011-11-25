package de.slesa.jlo.pms.persistence;

import java.util.List;

import javax.persistence.EntityManager;
import javax.persistence.PersistenceContext;

import org.springframework.stereotype.Repository;

import de.slesa.jlo.pms.model.Currency;

@Repository
public class CurrencyDao implements ICurrencyDao {

	@PersistenceContext
	private EntityManager em;
	
	@SuppressWarnings("unchecked")
	public List<Currency> findAll() {
		return em.createQuery("from Currency c").getResultList(); 
	}

	public void save(Currency currency) {
		em.persist(currency);
	}

}
