package de.slesa.jlo.ums.persistence;

import java.util.List;

import de.slesa.jlo.ums.model.User;

public interface IUserDao {

	List<User> findAll();
	void save(User user);
}
