package de.slesa.lucifer.pms.persistence;

import java.util.List;

import de.slesa.lucifer.pms.model.Payform;

public interface IPayformDao {

	List<Payform> findAllPayforms();
	
	void save(Payform payform);
}
