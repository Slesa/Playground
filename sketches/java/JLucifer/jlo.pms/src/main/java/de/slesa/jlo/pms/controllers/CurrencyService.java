package de.slesa.jlo.pms.controllers;

import java.util.List;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Propagation;
import org.springframework.transaction.annotation.Transactional;

import de.slesa.jlo.pms.model.Currency;
import de.slesa.jlo.pms.persistence.ICurrencyDao;

@Service("currencyService")
@Transactional(propagation=Propagation.REQUIRED)
public class CurrencyService implements ICurrencyService {

	@Autowired
	ICurrencyDao currencyDao;
	
	public void addCurrency(Currency currency) {
		currencyDao.save(currency);
	}

	public List<Currency> listCurrencies() {
		return currencyDao.findAll();
	}

}
