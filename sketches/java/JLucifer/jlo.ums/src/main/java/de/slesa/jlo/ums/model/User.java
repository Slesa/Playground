package de.slesa.jlo.ums.model;

import java.util.HashSet;
import java.util.Set;

import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.Id;
import javax.persistence.OneToMany;

import lombok.Data;

@Data
@Entity
public class User {

	@Id
	@GeneratedValue
	protected Long id;
	protected String name;
	
	@OneToMany
	protected Set<UserRole> roles = new HashSet<UserRole>();
	
	public User(String name) {
		this.name = name;
	}
}
