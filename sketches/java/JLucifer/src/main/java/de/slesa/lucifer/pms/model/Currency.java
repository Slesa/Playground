package de.slesa.lucifer.pms.model;

import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.Id;

import lombok.Data;

@Data
@Entity
public class Currency {

	@Id
	@GeneratedValue
	protected Long id;
	protected String name;
	protected String Contraction;
	protected String Symbol;
	protected double rate;
	protected int decimalPosition;
	protected char DecimalChar;
	protected char ThousandChar;
}
