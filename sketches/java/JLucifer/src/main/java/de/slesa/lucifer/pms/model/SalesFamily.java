package de.slesa.lucifer.pms.model;

import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.Id;

import lombok.Data;

@Data
@Entity
public class SalesFamily {

	@Id
	@GeneratedValue
	protected Long id;
	protected String name;
}
