package de.slesa.jlo.ums.persistence;

import java.util.List;

import javax.persistence.EntityManager;
import javax.persistence.PersistenceContext;

import de.slesa.jlo.ums.model.UserRole;

public class UserRoleDao implements IUserRoleDao {

	@PersistenceContext
	EntityManager em;
	
	@SuppressWarnings("unchecked")
	public List<UserRole> findAll() {
		return em.createQuery("FROM UserRole r").getResultList();
	}
	
	public void save(UserRole role) {
		em.persist(role);
	}
}
