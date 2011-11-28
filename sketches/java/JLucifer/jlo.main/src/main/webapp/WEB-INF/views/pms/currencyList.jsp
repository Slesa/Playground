<%@ taglib uri="http://java.sun.com/jsp/jstl/core" prefix="c"%>

<html>

<head>
	<title>All currencies</title>
</head>

<body>

	<h1>List currencies</h1>

	<a href="currencies/add.html">Add currency</a>

	<c:if test="${!empty currencies}">
	<table>
		<tr>
			<th>Currency ID</th>
			<th>Currency Name</th>

		</tr>

		<c:forEach items="${currencies}" var="currency">
		<tr>
			<td><c:out value="${currency.Id}"/></td>
			<td><c:out value="${currency.Name}"/></td>
		</tr>
		</c:forEach>
	</table>
	</c:if>

</body>
</html>