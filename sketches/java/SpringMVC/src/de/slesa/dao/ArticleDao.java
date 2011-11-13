package de.slesa.dao;

import java.util.List;

import de.slesa.model.Article;

public interface ArticleDao {

	public void saveArticle(Article article);
	public List<Article> listArticles();
}
