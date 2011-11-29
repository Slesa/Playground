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
			<th>Contraction</th>
			<th>Symbol</th>
			<th>Rate</th>
			<th>Dec. position</th>
			<th>Dec. char</th>
			<th>Ths. char</th>
		</tr>

		<c:forEach items="${currencies}" var="currency">
		<tr>
			<td><c:out value="${currency.Id}"/></td>
			<td><c:out value="${currency.Name}"/></td>
			<td><c:out value="${currency.Contraction}"/></td>
			<td><c:out value="${currency.Rate}"/></td>
			<td><c:out value="${currency.DecimalPosition}"/></td>
			<td><c:out value="${currency.DecimalChar}"/></td>
			<td><c:out value="${currency.ThousandChar}"/></td>
		</tr>
		</c:forEach>
	</table>
	</c:if>

</body>
</html>