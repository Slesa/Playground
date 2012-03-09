package de.slesa.jlo.pms.controllers;

import java.util.List;

import de.slesa.jlo.pms.model.Currency;

public interface ICurrencyService {

	void addCurrency(Currency currency);
	List<Currency> listCurrencies();
}
