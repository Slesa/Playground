<%@ page language="java" contentType="text/html; charset=ISO-8859-1" pageEncoding="ISO-8859-1"%>
<%@ taglib prefix="c" uri="http://java.sun.com/jsp/jstl/core"%>
<%@ taglib prefix="form" uri="http://www.springframework.org/tags/form"%>
<%@ taglib uri="/WEB-INF/iscTaglib.xml" prefix="isomorphic" %>
<!DOCTYPE htlm PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN" "http://www.w3.org/TR/html4/loose.dtd">

<html>
<head>
	<isomorphic:loadISC skin="Mobile"/>
	<meta http-equiv="Content-Type" content="text/html; charset=ISO-8859-1">
	<title>Add currency</title>
</head>
<body>
	<h1>Add currency</h1>
		
	<c:url var="viewCurrenciesUrl" value="/forms/pms/currencies" />
	<a href="/forms/pms/currencies">Show all currencies</a>
	<!--  a href="${viewCurrenciesUrl}">Show all currencies</a -->
		
	<Script>
	isc.DynamicForm.create({
		ID: "currencyForm",
		left: 50, top: 250,
		width: 300,
		fields: [
			{name: "name", title: "Name"},
			{name: "contraction", title: "Contraction:"}
		]
	});
	</Script>
	<!--  

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
		
		</form:form>
		 -->
</body>
</html>
 