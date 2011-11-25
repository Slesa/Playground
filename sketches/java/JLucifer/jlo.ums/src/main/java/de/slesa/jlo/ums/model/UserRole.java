package de.slesa.jlo.ums.model;

import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.Id;

import lombok.Data;

@Data
@Entity
public class UserRole {

	@Id
	@GeneratedValue
	protected Long id;
	protected String name;
	
	public UserRole(String name) {
		this.name = name;
	}
}
