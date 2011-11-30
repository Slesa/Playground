<%@ page language="java" contentType="text/html; charset=ISO-8859-1" pageEncoding="ISO-8859-1"%>
<%@ taglib prefix="c" uri="http://java.sun.com/jsp/jstl/core"%>
<%@ taglib prefix="form" uri="http://www.springframework.org/tags/form"%>
<!DOCTYPE htlm PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN" "http://www.w3.org/TR/html4/loose.dtd">
<html>
	<head>
		<meta http-equiv="Content-Type" content="text/html; charset=ISO-8859-1">
		<title>Add currency</title>
	</head>
	<body>
	
		<h1>Add currency</h1>
		
		<c:url var="viewCurrenciesUrl" value="/forms/pms/currencies" />
		<a href="${viewCurrenciesUrl}">Show all currencies</a>

		<br />
		<br />
		
		<c:url var="saveCurrencyUrl" value="/forms/pms/currencies/save" />
				
		<form:form modelAttribute="currency" method="POST" action="${saveCurrencyUrl}">
		<form:label path="name">Currency name:</form:label>
		<form:input path="name" />
		
		<br />
		
		<form:label path="contraction">Contraction:</form:label>
		<form:input path="contraction" />
		
		<br />
		
		<form:label path="rate">Rate:</form:label>
		<form:input path="rate" />
		
		<form:label path="decimalPosition">Decimal position:</form:label>
		<form:input path="decimalPosition" />
		
		<br />
		
		<input type="submit" value="Save currency" />
		
		<!--  table>
			<tr>
				<td>Currency name:<font color="red"><form:errors path="name" /></font></td>
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
 