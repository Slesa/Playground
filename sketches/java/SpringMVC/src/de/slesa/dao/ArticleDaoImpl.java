package de.slesa.dao;

import java.util.Date;
import java.util.List;

import org.hibernate.SessionFactory;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Repository;

import de.slesa.model.Article;

@Repository("articleDao")
public class ArticleDaoImpl implements ArticleDao {

	@Autowired
	private SessionFactory sessionFactory;
	
	public void saveArticle(Article article) {
		article.setAddedDate(new Date());
		sessionFactory.getCurrentSession().saveOrUpdate(article);
	}
	
	@SuppressWarnings("unchecked")
	public List<Article> listArticles() {
		return (List<Article>) sessionFactory.getCurrentSession().createCriteria(Article.class).list();
	}
}
