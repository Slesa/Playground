package de.slesa.lucifer.ums.persistence;

import java.util.List;

import de.slesa.lucifer.ums.model.Role;

public interface IRoleDao {

	List<Role> findAllRoles();
	
	void save(Role role);
}
