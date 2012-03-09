package de.slesa.jlo.ums.persistence;

import java.util.List;

import org.hibernate.SessionFactory;
import org.springframework.beans.factory.annotation.Autowired;

import de.slesa.jlo.ums.model.UserRole;

public class UserRoleDao implements IUserRoleDao {

	@Autowired
	private SessionFactory sessionFactory;
	
//	@PersistenceContext
//	EntityManager em;
	
	@SuppressWarnings("unchecked")
	public List<UserRole> findAll() {
		return sessionFactory.getCurrentSession().createCriteria(UserRole.class).list();
//		return em.createQuery("FROM UserRole r").getResultList();
	}
	
	public void save(UserRole role) {
		sessionFactory.getCurrentSession().saveOrUpdate(role);
//		em.persist(role);
	}
}
