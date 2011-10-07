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

import de.slesa.lucifer.pms.model.Payform;
import de.slesa.lucifer.pms.persistence.IPayformDao;

@TransactionConfiguration
@ContextConfiguration("classpath:context.xml")
@Transactional
@RunWith(SpringJUnit4ClassRunner.class)
public class PayformDaoTest {

	@Resource
	IPayformDao payformDao;
	
	@Test public void savePayform() {
		Payform payform = new Payform();
		payform.setName("Cash");
		payformDao.save(payform);
		
		List<Payform> payforms = payformDao.findAllPayforms();
		Payform saved = payforms.get(0);
		Assert.assertEquals("There should be one saved payform",1,payforms.size());
		Assert.assertEquals("Payform should be equal to saved one", payform, saved);
		
	}
}
