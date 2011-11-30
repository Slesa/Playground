package de.slesa.jlo.ums.persistence;

import java.util.List;

import javax.persistence.EntityManager;
import javax.persistence.PersistenceContext;

import org.hibernate.SessionFactory;
import org.springframework.beans.factory.annotation.Autowired;

import de.slesa.jlo.ums.model.User;

public class UserDao implements IUserDao {

	@Autowired
	private SessionFactory sessionFactory;

//	@PersistenceContext
//	EntityManager em;
	
	@SuppressWarnings("unchecked")
	public List<User> findAll() {
		return sessionFactory.getCurrentSession().createCriteria(User.class).list();
//		return em.createQuery("FROM User u").getResultList();
	}

	public void save(User user) {
		sessionFactory.getCurrentSession().saveOrUpdate(user);
//		em.persist(user);
	}

}
