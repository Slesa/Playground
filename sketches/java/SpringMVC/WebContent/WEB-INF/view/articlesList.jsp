<%@ page language="java" contentType="text/html; charset=ISO-8859-1"
    pageEncoding="ISO-8859-1"%>
<%@ taglib uri="http://java.sun.com/jsp/jstl/core" prefix="c"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN" "http://www.w3.org/TR/html4/loose.dtd">
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=ISO-8859-1">
<title>List Articles</title>
</head>
<body>

	<h1>List Articles</h1>
	
	<a href="articles/add.html">Add Article</a>
	
	<c:if test="${!empty articles}">
		<table>
			<tr>
				<th>Article ID</th>
				<th>Article Name</th>
				<th>Description</th>
				<th>Added Date</th>
			</tr>
			<c:forEach items="${articles}" var="article">
			<tr>
				<td><c:out value="${article.articleId}"/></td>
				<td><c:out value="${article.articleName}"/></td>
				<td><c:out value="${article.articleDesc}"/></td>
				<td><c:out value="${article.addedDate}"/></td>
			</tr>
			</c:forEach>	
		</table>
	</c:if>
</body>
</html>