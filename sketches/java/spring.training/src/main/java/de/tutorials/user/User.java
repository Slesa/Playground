package de.tutorials.user;

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
	protected Set<Role> roles = new HashSet<Role>();
}
