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

import de.slesa.lucifer.pms.model.SalesFamily;
import de.slesa.lucifer.pms.persistence.ISalesFamilyDao;

@TransactionConfiguration
@ContextConfiguration("classpath:context.xml")
@Transactional
@RunWith(SpringJUnit4ClassRunner.class)
public class SalesFamilyDaoTest {

	@Resource
	ISalesFamilyDao salesFamilyDao;

	@Test public void saveSalesFamily() {
		
		SalesFamily salesFamily = new SalesFamily();
		salesFamily.setName("Food");
		salesFamilyDao.save(salesFamily);
		
		List<SalesFamily> salesFamilies = salesFamilyDao.findAllSalesFamilies();
		SalesFamily saved = salesFamilies.get(0);
		Assert.assertEquals("There should be one saved sales family",1,salesFamilies.size());
		Assert.assertEquals("Sales family should be equal to saved one", salesFamily, saved);
	}
}
