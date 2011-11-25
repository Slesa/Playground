package de.slesa.jlo.pms.persistence;

import java.util.List;

import de.slesa.jlo.pms.model.Discount;

public interface IDiscountDao {

	List<Discount> findAll();
	void save(Discount discount);
}
