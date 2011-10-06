package de.slesa.lucifer.pms.persistence;

import java.util.List;

import de.slesa.lucifer.pms.model.Currency;

public interface ICurrencyDao {

	List<Currency> findAllCurrencies();
	
	void save(Currency currency);
}
