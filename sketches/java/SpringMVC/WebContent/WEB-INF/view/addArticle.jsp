<%@ page language="java" contentType="text/html; charset=ISO-8859-1"
    pageEncoding="ISO-8859-1"%>
<%@ taglib uri="http://java.sun.com/jsp/jstl/core" prefix="c"%>
<%@ taglib uri="http://www.springframework.org/tags/form" prefix="form"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN" "http://www.w3.org/TR/html4/loose.dtd">
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=ISO-8859-1">
<title>Add Article</title>
</head>
<body>

	<h1>Add Article</h1>
	
	<c:url var="viewArticlesUrl" value="/articles.html" />
	<a href="${viewArticlesUrl}">Show All Articles</a>
	
	<br />
	<br />
	
	<c:url var="saveArticleUrl" value="/articles/save.html" />
	<form:form modelAttribute="article" method="POST" action="${saveArticleUrl}">
		<form:label path="articleName">Article Name:</form:label>
		<form:input path="articleName" />
		<br />
		<form:label path="articleDesc">Description:</form:label>
		<form:textarea path="articleDesc"/>
		<br />
		<input type="submit" value="Save Article" />
	</form:form>
	
</body>
</html>