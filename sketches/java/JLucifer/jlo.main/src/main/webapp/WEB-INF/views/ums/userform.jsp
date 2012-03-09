<%@ page language="java" contentType="text/html; charset=ISO-8859-1" pageEncoding="ISO-8859-1"%>
<%@ taglib prefix="c" uri="http://java.sun.com/jsp/jstl/core"%>
<%@ taglib prefix="form" uri="http://www.springframework.org/tags/form"%>
<!DOCTYPE htlm PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN" "http://www.w3.org/TR/html4/loose.dtd">
<html>
	<head>
		<meta http-equiv="Content-Type" content="text/html; charset=ISO-8859-1">
		<title>Add user</title>
	</head>
	<body>
	
		<h1>Add user</h1>
	
		<c:url var="viewUsersUrl" value="/forms/ums/users" />
		<a href="${viewUsersUrl}">Show all users</a>

		<br />
		<br />

		<c:url var="saveUserUrl" value="/forms/ums/users/save" />
	
		<form:form modelAttribute="user" method="POST" action="${saveUserUrl}">
		<form:label path="name">User name:</form:label>
		<form:input path="name" />

		<br />
	
		<!--  table>
			<tr>
				<td>User name:<font color="red"><form:errors path="name" /></font></td>
			</tr>
			<tr>
				<td><form:input path="name" /></td>
			</tr>
			<tr>
				<td><input type="submit" value="Submit" /></td>
			</tr>
		</table -->
		</form:form>
	</body>
</html>
 