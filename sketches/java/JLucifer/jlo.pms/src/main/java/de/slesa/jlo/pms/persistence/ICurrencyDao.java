package de.slesa.jlo.pms.persistence;

import java.util.List;

import de.slesa.jlo.pms.model.Currency;

public interface ICurrencyDao {

	List<Currency> findAll();
	void save(Currency currency);
}
