package de.slesa.lucifer.pms.persistence;

import java.util.List;

import de.slesa.lucifer.pms.model.Discount;

public interface IDiscountDao {

	List<Discount> findAllDiscounts();
	
	void save(Discount discount);
}
