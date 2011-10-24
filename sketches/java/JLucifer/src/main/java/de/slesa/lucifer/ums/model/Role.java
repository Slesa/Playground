package de.slesa.lucifer.ums.model;

import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.Id;

import lombok.Data;

@Data
@Entity
public class Role {

	@Id
	@GeneratedValue
	protected Long id;
	protected String name;

	public Role(String name) {
		this.name = name;
	}
}
