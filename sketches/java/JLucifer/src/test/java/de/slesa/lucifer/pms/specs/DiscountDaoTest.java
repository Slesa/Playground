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

import de.slesa.lucifer.pms.model.Discount;
import de.slesa.lucifer.pms.persistence.IDiscountDao;

@TransactionConfiguration
@ContextConfiguration("classpath:context.xml")
@Transactional
@RunWith(SpringJUnit4ClassRunner.class)
public class DiscountDaoTest {

	@Resource
	IDiscountDao discountDao;
	
	@Test public void saveDiscount() {
		Discount discount = new Discount();
		discount.setName("25 percent");
		discount.setRate(0.25);
		discountDao.save(discount);
		
		List<Discount> discounts = discountDao.findAllDiscounts();
		Discount saved = discounts.get(0);
		Assert.assertEquals("There should be one saved discount",1,discounts.size());
		Assert.assertEquals("Discount should be equal to saved discount", discount, saved);
	}
}
