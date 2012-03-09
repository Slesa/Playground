package de.slesa.jlo.ums.controllers;
import java.util.List;

import de.slesa.jlo.ums.model.User;
import de.slesa.jlo.ums.persistence.IUserDao;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Propagation;
import org.springframework.transaction.annotation.Transactional;

@Service("userService")
@Transactional(propagation=Propagation.REQUIRED)
public class UserService implements IUserService {

	@Autowired
	IUserDao userDao;

	public void addUser(User user) {
		userDao.save(user);
	}

	public List<User> listUsers() {
		return userDao.findAll();
	}
	
}
