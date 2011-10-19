package de.slesa.model;

import java.util.Date;

import javax.persistence.Column;
import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.Id;
import javax.persistence.Table;

import lombok.Data;

@Entity
@Table(name = "articles")
@Data
public class Article {

	@Id
	@GeneratedValue
	@Column(name="article_id")
	private Long articleId;
	
	private String articleName;
	private String articleDesc;
	private Date addedDate;
	
	public Article() {
	}
	
}
