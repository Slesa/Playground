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

import de.slesa.jlo.pms.model.SalesFamily;
import de.slesa.jlo.pms.persistence.ISalesFamilyDao;

@TransactionConfiguration
@ContextConfiguration("classpath:context.xml")
@Transactional
@RunWith(SpringJUnit4ClassRunner.class)
public class SalesFamilyDaoTest {

	@Resource
	ISalesFamilyDao salesFamilyDao;
	
	@Test public void saveSalesFamily() {
		SalesFamily salesFamily = new SalesFamily("Food");
		salesFamilyDao.save(salesFamily);
		
		List<SalesFamily> salesFamilies = salesFamilyDao.findAll();
		Assert.assertEquals("Theres should be one saved sales family", 1, salesFamilies.size());
		SalesFamily saved = salesFamilies.get(0);
		Assert.assertEquals("Sales family should be equal to saved one", salesFamily, saved);
	}
}
