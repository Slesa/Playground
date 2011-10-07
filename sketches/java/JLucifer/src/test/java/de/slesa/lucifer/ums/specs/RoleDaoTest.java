package de.slesa.lucifer.ums.specs;

import java.util.List;

import junit.framework.Assert;
import javax.annotation.Resource;

import org.junit.Test;
import org.junit.runner.RunWith;
import org.springframework.test.context.ContextConfiguration;
import org.springframework.test.context.junit4.SpringJUnit4ClassRunner;
import org.springframework.test.context.transaction.TransactionConfiguration;
import org.springframework.transaction.annotation.Transactional;

import de.slesa.lucifer.ums.model.Role;
import de.slesa.lucifer.ums.persistence.IRoleDao;

@TransactionConfiguration
@ContextConfiguration("classpath:context.xml")
@Transactional
@RunWith(SpringJUnit4ClassRunner.class)
public class RoleDaoTest {

	@Resource
	IRoleDao roleDao;
	
	@Test public void saveRole() {
		Role role = new Role("Admin");
		roleDao.save(role);
		
		List<Role> roles = roleDao.findAllRoles();
		Role saved = roles.get(0);
		Assert.assertEquals("There should be one saved role",1,roles.size());
		Assert.assertEquals("Role should be equal to saved one", role, saved);
	}
}
