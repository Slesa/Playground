<%@ taglib uri="/WEB-INF/iscTaglib.xml" prefix="isomorphic" %>
<!--  jsp:forward page="pms/currency.jsp" / -->

<!--  %@ page language="java" contentType="text/html; charset=ISO-8859-1"
    pageEncoding="ISO-8859-1"% -->
<!-- %@ taglib uri="isomorphic" prefix="isomorphic" % -->
<!--  jsp:include page="initlibs.jsp" / -->

<!DOCTYPE html PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN" "http://www.w3.org/TR/html4/loose.dtd">
<html>
<head>
	<isomorphic:loadISC skin="Mobile"/>
	<meta http-equiv="Content-Type" content="text/html; charset=ISO-8859-1">
	<title>Lucifer Office</title>
</head>
<body>

<SCRIPT>
isc.IButton.create({
	title:"Hello",
	click:"isc.say('Hello World')"
});
 
</SCRIPT>

<h1>Lucifer Office</h1>
<ul>
	<li><a href="forms/ums/users.html">Users</a></li>
	<li><a href="forms/pms/currencies">Currencies</a></li>
</ul>
</body>
</html>
