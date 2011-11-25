package de.slesa.jlo.pms.model;

import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.Id;

import lombok.Data;

@Data
@Entity
public class Payform {

	@Id
	@GeneratedValue
	protected long id;
	protected String name;
	
	public Payform(String name) {
		this.name = name;
	}
}
