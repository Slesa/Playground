package de.slesa.jlo.ums.controllers;

import java.util.List;

import de.slesa.jlo.ums.model.User;

public interface IUserService {

	void addUser(User user);
	List<User> listUsers();
	
}
