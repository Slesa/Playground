<%@ page language="java" contentType="text/html; charset=ISO-8859-1"
    pageEncoding="ISO-8859-1"%>
<%@ taglib prefix="core" uri="http://java.sun.com/jsp/jstl/core"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN" "http://www.w3.org/TR/html4/loose.dtd">
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=ISO-8859-1">
<title>Login success</title>
</head>
<body>

	<h3>Welcome <core:out value="${loginForm.userName}" /></h3>

	<table>
		<tr>
			<td><a href="loginform.html">Back</a></td>
		</tr>
	</table>

</body>
</html>