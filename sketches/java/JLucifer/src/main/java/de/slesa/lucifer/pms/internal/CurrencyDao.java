package de.slesa.lucifer.pms.internal;

import java.util.List;

import javax.persistence.EntityManager;
import javax.persistence.PersistenceContext;

import org.springframework.stereotype.Repository;

import de.slesa.lucifer.pms.model.Currency;
import de.slesa.lucifer.pms.persistence.ICurrencyDao;

@Repository
public class CurrencyDao implements ICurrencyDao {

	@PersistenceContext
	private EntityManager em;
	
	@SuppressWarnings("unchecked")
	public List<Currency> findAllCurrencies() {
	
		return em.createQuery("from Currency c").getResultList();
	}

	public void save(Currency currency) {
		
		em.persist(currency);
	}

}
