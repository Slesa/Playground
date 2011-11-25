package de.slesa.jlo.pms.model;

import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.Id;

import lombok.Data;

@Data
@Entity
public class Currency {

	@Id
	@GeneratedValue
	protected long id;
	protected String name;
	protected String contraction;
	protected String symbol;
	protected double rate;
	protected int decimalPosition;
	protected char decimalChar;
	protected char thousandChar;
	
	public Currency(String name, String contraction) {
		this.name = name;
		this.contraction = contraction;
	}
}
