package de.slesa.lucifer.pms.specs;

import java.util.List;

import junit.framework.Assert;
import javax.annotation.Resource;

import org.junit.Test;
import org.junit.runner.RunWith;
import org.springframework.test.context.ContextConfiguration;
import org.springframework.test.context.junit4.SpringJUnit4ClassRunner;
import org.springframework.test.context.transaction.TransactionConfiguration;
import org.springframework.transaction.annotation.Transactional;

import de.slesa.lucifer.pms.model.Currency;
import de.slesa.lucifer.pms.persistence.ICurrencyDao;

@TransactionConfiguration
@ContextConfiguration({"classpath:context.xml"})
@Transactional
@RunWith(SpringJUnit4ClassRunner.class)
public class CurrencyDaoTest {

	@Resource
	ICurrencyDao currencyDao;
	
	@Test public void saveCurrency() {
		Currency currency = new Currency();
		currency.setName("Euro");
		currency.setContraction("€");
		currencyDao.save(currency);
		
		List<Currency> currencies = currencyDao.findAllCurrencies();
		Currency saved = currencies.get(0);
		Assert.assertEquals("There should be one saved currency",1,currencies.size());
		Assert.assertEquals("Currency should be equal to saved currency", currency, saved);
	}
}
