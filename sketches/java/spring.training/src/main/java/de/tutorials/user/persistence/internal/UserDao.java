package de.tutorials.user.persistence.internal;

import java.util.List;

import javax.persistence.EntityManager;
import javax.persistence.PersistenceContext;

import org.springframework.stereotype.Repository;

import de.tutorials.user.User;
import de.tutorials.user.persistence.IUserDao;

@Repository
public class UserDao implements IUserDao {

	@PersistenceContext
	private EntityManager em;

	@SuppressWarnings("unchecked")
	public List<User> findAllUsers() {
		return em.createQuery("from User u").getResultList();
	}

	public void save(User user) {
		em.persist(user);
	}

}
