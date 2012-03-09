package de.slesa.jlo.pms.persistence;

import java.util.List;

import de.slesa.jlo.pms.model.SalesFamily;

public interface ISalesFamilyDao {

	List<SalesFamily> findAll();
	void save(SalesFamily salesFamily);
}
