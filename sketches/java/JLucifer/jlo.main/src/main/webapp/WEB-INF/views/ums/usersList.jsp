<%@ taglib uri="http://java.sun.com/jsp/jstl/core" prefix="c"%>

<html>

<head>
	<title>All users</title>
</head>

<body>

	<h1>List users</h1>

	<a href="users/add.html">Add user</a>

	<c:if test="${!empty users}">
	<table>
		<tr>
			<th>ID</th>
			<th>Name</th>
		</tr>

		<c:forEach items="${users}" var="user">
		<tr>
			<td><c:out value="${user.id}"/></td>
			<td><c:out value="${user.name}"/></td>
		</tr>
		</c:forEach>
	</table>
	</c:if>

</body>
</html>