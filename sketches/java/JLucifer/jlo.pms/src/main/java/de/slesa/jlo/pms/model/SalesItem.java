package de.slesa.jlo.pms.model;

import javax.persistence.CascadeType;
import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.Id;
import javax.persistence.OneToOne;

import lombok.Data;

@Data
@Entity
public class SalesItem {

	@Id
	@GeneratedValue
	protected long id;
	protected String name;
	
	@OneToOne(cascade=CascadeType.ALL)
	protected SalesFamily salesFamily;
	
	public SalesItem(String name) {
		this.name = name;
	}
}
