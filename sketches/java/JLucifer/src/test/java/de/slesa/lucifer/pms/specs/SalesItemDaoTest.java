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
import de.slesa.lucifer.pms.model.SalesItem;
import de.slesa.lucifer.pms.persistence.ISalesFamilyDao;
import de.slesa.lucifer.pms.persistence.ISalesItemDao;

@TransactionConfiguration
@ContextConfiguration("classpath:context.xml")
@Transactional
@RunWith(SpringJUnit4ClassRunner.class)
public class SalesItemDaoTest {

	@Resource
	ISalesItemDao salesItemDao;
	@Resource
	ISalesFamilyDao salesFamilyDao;
	
	@Test public void saveSalesItem() {
		SalesItem salesItem = new SalesItem();
		salesItem.setName("Steak");
		salesItemDao.save(salesItem);
		
		List<SalesItem> salesItems = salesItemDao.findAllSalesItems();
		SalesItem saved = salesItems.get(0);
		Assert.assertEquals("There should be one saved sales item",1,salesItems.size());
		Assert.assertEquals("Sales item should be equal to saved one", salesItem, saved);
	}
	
	@Test public void saveSalesItemWithFamily() {
		
		SalesFamily salesFamily = new SalesFamily();
		salesFamily.setName("Food");
		
		SalesItem salesItem = new SalesItem();
		salesItem.setName("Steak");
		salesItem.setSalesFamily(salesFamily);
		salesItemDao.save(salesItem);
		
		List<SalesItem> salesItems = salesItemDao.findAllSalesItems();
		SalesItem saved = salesItems.get(0);
		Assert.assertEquals("There should be one saved sales item",1,salesItems.size());
		Assert.assertEquals("Sales item should be equal to saved one", salesItem, saved);
	}
}
