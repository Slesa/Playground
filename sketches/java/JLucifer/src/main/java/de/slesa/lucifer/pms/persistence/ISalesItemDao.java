package de.slesa.lucifer.pms.persistence;

import java.util.List;

import de.slesa.lucifer.pms.model.SalesItem;

public interface ISalesItemDao {

	List<SalesItem> findAllSalesItems();
	
	void save(SalesItem salesItem);
}
