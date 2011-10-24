package de.slesa.lucifer.ums.internal;

import java.util.List;

import javax.persistence.EntityManager;
import javax.persistence.PersistenceContext;

import org.springframework.stereotype.Repository;

import de.slesa.lucifer.ums.model.Role;
import de.slesa.lucifer.ums.persistence.IRoleDao;

@Repository
public class RoleDao implements IRoleDao {

	@PersistenceContext
	EntityManager em;
	
	@SuppressWarnings("unchecked")
	public List<Role> findAllRoles() {
		 
		return em.createQuery("FROM Role r").getResultList();
	}

	public void save(Role role) {
		
		em.persist(role);
	}

}
