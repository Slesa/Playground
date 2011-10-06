package de.slesa.lucifer.pms.model;

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
	protected Long id;
	protected String name;
	
	@OneToOne
	protected SalesFamily salesFamily;
}
