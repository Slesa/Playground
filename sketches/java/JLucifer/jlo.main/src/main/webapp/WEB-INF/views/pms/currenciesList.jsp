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
			<th>ID</th>
			<th>Name</th>
			<th>Contraction</th>
			<th>Symbol</th>
			<th>Rate</th>
			<th>Dec. position</th>
			<th>Dec. char</th>
			<th>Ths. char</th>
		</tr>

		<c:forEach items="${currencies}" var="currency">
		<tr>
			<td><c:out value="${currency.id}"/></td>
			<td><c:out value="${currency.name}"/></td>
			<td><c:out value="${currency.contraction}"/></td>
			<td><c:out value="${currency.rate}"/></td>
			<td><c:out value="${currency.decimalPosition}"/></td>
			<td><c:out value="${currency.decimalChar}"/></td>
			<td><c:out value="${currency.thousandChar}"/></td>
		</tr>
		</c:forEach>
	</table>
	</c:if>

</body>
</html>