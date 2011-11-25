package de.slesa.jlo.ums.persistence;

import java.util.List;

import javax.persistence.EntityManager;
import javax.persistence.PersistenceContext;

import de.slesa.jlo.ums.model.User;

public class UserDao implements IUserDao {

	@PersistenceContext
	EntityManager em;
	
	@SuppressWarnings("unchecked")
	public List<User> findAll() {
		return em.createQuery("FROM User u").getResultList();
	}

	public void save(User user) {
		em.persist(user);
	}

}
