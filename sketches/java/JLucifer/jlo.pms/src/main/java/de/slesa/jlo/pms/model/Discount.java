package de.slesa.jlo.pms.model;

import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.Id;

import lombok.Data;

@Data
@Entity
public class Discount {

	@Id
	@GeneratedValue
	protected long id;
	protected String name;
	protected double rate;
	
	public Discount(String name, double rate) {
		this.name = name;
		this.rate = rate;
	}
}
