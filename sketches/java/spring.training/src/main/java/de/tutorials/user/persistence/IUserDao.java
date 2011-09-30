package de.tutorials.user.persistence;

import java.util.List;

import de.tutorials.user.User;

public interface IUserDao {

	List<User> findAllUsers();

	void save(User user);
}