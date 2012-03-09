package de.slesa.jlo.pms.persistence;

import java.util.List;

import de.slesa.jlo.pms.model.SalesItem;

public interface ISalesItemDao {

	List<SalesItem> findAll();
	void save(SalesItem salesItem);
}
