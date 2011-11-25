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

import de.slesa.jlo.ums.model.UserRole;
import de.slesa.jlo.ums.persistence.IUserRoleDao;

@TransactionConfiguration
@ContextConfiguration("classpath:context.xml")
@Transactional
@RunWith(SpringJUnit4ClassRunner.class)
public class UserRoleDaoTest {
	
	@Resource
	IUserRoleDao userRoleDao;
	
	@Test public void saveUserRole() {
		UserRole userRole = new UserRole("Admin");
		userRoleDao.save(userRole);
		
		List<UserRole> userRoles = userRoleDao.findAll();
		Assert.assertEquals("There should be one saved user role", 1, userRoles.size());
		
		UserRole savedRole = userRoles.get(0);
		Assert.assertEquals("User role should be equal to saved one", userRole, savedRole);
	}

}
