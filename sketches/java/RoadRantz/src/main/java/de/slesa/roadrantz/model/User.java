package de.slesa.roadrantz.model;

import java.util.HashSet;
import java.util.Set;

import javax.persistence.CascadeType;
import javax.persistence.Entity;
import javax.persistence.FetchType;
import javax.persistence.GeneratedValue;
import javax.persistence.Id;
import javax.persistence.OneToMany;

import lombok.Data;

@Entity
@Data
public class User {

	@Id
	@GeneratedValue
	private Long Id;
	private boolean enabled = true;
	private String firstName;
	private String lastName;
	private String userName;
	private String password;
	@OneToMany(cascade=CascadeType.ALL, fetch=FetchType.LAZY, mappedBy="user")
	private Set<UserPrivilege> privileges;
	
	public User() {
		privileges = new HashSet<UserPrivilege>();
	}
}
