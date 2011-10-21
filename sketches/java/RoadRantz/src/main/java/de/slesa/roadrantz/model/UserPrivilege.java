package de.slesa.roadrantz.model;

import javax.persistence.Id;
import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.ManyToOne;

import lombok.Data;

@Entity
@Data
public class UserPrivilege {

	@Id
	@GeneratedValue
	private Long Id;
	private String userName;
	@ManyToOne
	private User user;
	private String privilege;
	
	public UserPrivilege() {
	}
	
	public UserPrivilege(String privilege) {
		this.privilege = privilege;
	}
}
