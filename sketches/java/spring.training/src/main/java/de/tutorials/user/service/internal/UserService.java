package de.tutorials.user.service.internal;

import java.util.List;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

import de.tutorials.user.User;
import de.tutorials.user.persistence.IUserDao;
import de.tutorials.user.service.IUserService;

@Service
public class UserService implements IUserService{
	@Autowired
	IUserDao userDao;
	
	@Override
	public List<User> findAllUsers() {
		return userDao.findAllUsers();
	}

	@Override
	@Transactional
	public void register(User user) {
		userDao.save(user);
	}
}