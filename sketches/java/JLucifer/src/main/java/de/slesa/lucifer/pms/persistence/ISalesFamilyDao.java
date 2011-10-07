package de.slesa.lucifer.pms.persistence;

import java.util.List;

import de.slesa.lucifer.pms.model.SalesFamily;

public interface ISalesFamilyDao {

	List<SalesFamily> findAllSalesFamilies();
	
	void save(SalesFamily salesFamily);
}
