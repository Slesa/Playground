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

import de.slesa.jlo.pms.model.Payform;
import de.slesa.jlo.pms.persistence.IPayformDao;

@TransactionConfiguration
@ContextConfiguration("classpath:context.xml")
@Transactional
@RunWith(SpringJUnit4ClassRunner.class)
public class PayformDaoTest {

	@Resource
	IPayformDao payformDao;
	
	@Test public void savePayform() {
		Payform payform = new Payform("Cash");
		payformDao.save(payform);
		
		List<Payform> payforms = payformDao.findAll();
		Assert.assertEquals("There should be one saved payform", 1, payforms.size());
		Payform saved = payforms.get(0);
		Assert.assertEquals("Payform should be equal to saved one", payform ,saved);
	}
}
