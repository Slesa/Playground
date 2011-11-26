<%@ page language="java" contentType="text/html; charset=ISO-8859-1" pageEncoding="ISO-8859-1"%>
<%@taglib prefix="form" uri="http://springframework.org/tags/form" %>
<!DOCTYPE htlm PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN" "http://www.w3.org/TR/html4/loose.dtd">
<html>
	<head>
		<meta http-equiv="Content-Type" content="text/html; charset=ISO-8859-1">
		<title>Edit currency</title>
	</head>
	<body>
		<form:form method="POST" action="validationform.html" commandName="validationform">
		<table>
			<tr>
				<td>Currency name:<font color="red"><form:errors path="currencyName" /></font></td>
			</tr>
			<tr>
				<td><form:input path="currencyName" /></td>
			</tr>
			<tr>
				<td><input type="submit" value="Submit" /></td>
			</tr>
		</table>
		</form:form>
	</body>
</html>
 