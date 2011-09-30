package de.tutorials.user.service;

import java.util.List;

import de.tutorials.user.User;

public interface IUserService {
	void register(User user);

	List<User> findAllUsers();
}
