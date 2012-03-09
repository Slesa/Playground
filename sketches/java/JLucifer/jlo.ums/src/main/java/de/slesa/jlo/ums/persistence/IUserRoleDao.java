package de.slesa.jlo.ums.persistence;

import java.util.List;

import de.slesa.jlo.ums.model.UserRole;

public interface IUserRoleDao {

	List<UserRole>	findAll();
	void save(UserRole role);
}
