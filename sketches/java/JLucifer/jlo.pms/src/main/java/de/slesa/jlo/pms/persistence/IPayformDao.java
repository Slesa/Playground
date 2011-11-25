package de.slesa.jlo.pms.persistence;

import java.util.List;

import de.slesa.jlo.pms.model.Payform;

public interface IPayformDao {

	List<Payform> findAll();
	void save(Payform payform);
}
