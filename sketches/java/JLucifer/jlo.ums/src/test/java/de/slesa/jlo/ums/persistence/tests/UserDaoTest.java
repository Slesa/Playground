package de.slesa.jlo.ums.persistence.tests;

import java.util.List;

import javax.annotation.Resource;

import junit.framework.Assert;

import org.junit.Test;
import org.junit.runner.RunWith;
import org.springframework.test.context.ContextConfiguration;
import org.springframework.test.context.junit4.SpringJUnit4ClassRunner;
import org.springframework.test.context.transaction.TransactionConfiguration;
import org.springframework.transaction.annotation.Transactional;

import de.slesa.jlo.ums.model.User;
import de.slesa.jlo.ums.persistence.IUserDao;

@TransactionConfiguration
@ContextConfiguration("classpath:context.xml")
@Transactional
@RunWith(SpringJUnit4ClassRunner.class)
public class UserDaoTest {

	@Resource
	IUserDao userDao;
	
	@Test public void saveUser() {
		User user = new User("Admin");
		userDao.save(user);
		
		List<User> users = userDao.findAll();
		Assert.assertEquals("There should be one saved user", 1, users.size());
		
		User saved = users.get(0);
		Assert.assertEquals("User should be equal to saved one", user, saved);
	}
}
