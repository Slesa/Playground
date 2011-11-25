package de.slesa.jlo.pms.persistence.tests;

import java.util.List;

import javax.annotation.Resource;

import junit.framework.Assert;

import org.junit.Test;
import org.junit.runner.RunWith;
import org.springframework.test.context.ContextConfiguration;
import org.springframework.test.context.junit4.SpringJUnit4ClassRunner;
import org.springframework.test.context.transaction.TransactionConfiguration;
import org.springframework.transaction.annotation.Transactional;

import de.slesa.jlo.pms.model.Currency;
import de.slesa.jlo.pms.persistence.ICurrencyDao;

@TransactionConfiguration
@ContextConfiguration("classpath:context.xml")
@Transactional
@RunWith(SpringJUnit4ClassRunner.class)
public class CurrencyDaoTest {

	@Resource
	ICurrencyDao currencyDao;
	
	@Test public void saveCurrency() {
		Currency currency = new Currency("Euro", "€");
		currencyDao.save(currency);
		
		List<Currency> currencies = currencyDao.findAll();
		Assert.assertEquals("There should be one saved currency", 1, currencies.size());
		Currency saved = currencies.get(0);
		Assert.assertEquals("Currency should be equal to saved one", currency, saved);
	}
}
