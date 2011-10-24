package de.slesa.service;

import java.util.List;

import de.slesa.model.Article;

public interface ArticleService {

	public void addArticle(Article article);
	public List<Article> listArticles();
	
}
