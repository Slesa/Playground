package de.tutorials.user.service.internal;

import java.util.Iterator;
import java.util.List;

import javax.annotation.Resource;

import junit.framework.Assert;

import org.junit.Test;
import org.junit.runner.RunWith;
import org.springframework.test.context.ContextConfiguration;
import org.springframework.test.context.junit4.SpringJUnit4ClassRunner;
import org.springframework.test.context.transaction.TransactionConfiguration;
import org.springframework.transaction.annotation.Transactional;

import de.tutorials.user.Role;
import de.tutorials.user.User;
import de.tutorials.user.service.IUserService;


@TransactionConfiguration
@ContextConfiguration({"classpath:context.xml"})
@Transactional
@RunWith(SpringJUnit4ClassRunner.class)
public class UserServiceTest {
	
	@Resource
	IUserService userService;
	
	@Test public void registerUser(){
		User user = new User();
		user.setName("bubu");
		userService.register(user);
		
		List<User> allUsers = userService.findAllUsers();
		User savedUser = allUsers.get(0);
		Assert.assertEquals("There should be one saved user",1,allUsers.size());
		Assert.assertEquals("User should be equal to saved user",user, savedUser);
	}
	
	@Test public void registerUserWithRoles(){
		User user = new User();
		
		Role roleAdmin = new Role("admin");
		Role roleUser = new Role("user");
		
		user.getRoles().add(roleAdmin);
		user.getRoles().add(roleUser);
		user.setName("bubu");
		
		userService.register(user);
		
		List<User> allUsers = userService.findAllUsers();
		User savedUser = allUsers.get(0);
		
		Assert.assertEquals("The user should be equal to saved user",user, savedUser);
		
		Assert.assertEquals("The user should have 2 roles",2, savedUser.getRoles().size());
		Assert.assertTrue("The user should have role user", savedUser.getRoles().contains(roleUser));
		Assert.assertTrue("The user should have role admin", savedUser.getRoles().contains(roleAdmin));
		
		Iterator<Role> iterRoles = savedUser.getRoles().iterator();
		
		Assert.assertEquals("First role should be admin",roleAdmin, iterRoles.next());
		Assert.assertEquals("Second role should be user",roleUser, iterRoles.next());
		
	}
}
