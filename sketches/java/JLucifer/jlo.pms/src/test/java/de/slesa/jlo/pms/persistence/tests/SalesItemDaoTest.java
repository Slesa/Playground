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

import de.slesa.jlo.pms.model.SalesItem;
import de.slesa.jlo.pms.persistence.ISalesItemDao;

@TransactionConfiguration
@ContextConfiguration("classpath:context.xml")
@Transactional
@RunWith(SpringJUnit4ClassRunner.class)
public class SalesItemDaoTest {
	
	@Resource
	ISalesItemDao salesItemDao;
	
	@Test public void saveSalesitem() {
		SalesItem salesItem = new SalesItem("Steak");
		salesItemDao.save(salesItem);
		
		List<SalesItem> salesItems = salesItemDao.findAll();
		Assert.assertEquals("There should be one saved sales item", 1, salesItems.size());
		SalesItem saved = salesItems.get(0);
		Assert.assertEquals("Sales item should equal to saved one", salesItem, saved);
	}

}
