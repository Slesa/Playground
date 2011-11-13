<%@ page contentType="text/html" %>
<%@ taglib prefix="c" uri="http://java.sun.com/jsp/jstl/core" %>
<%@ taglib prefix="form" uri="http://www.springframework.org/tags/form" %>
<%@ taglib prefix="spring" uri="http://www.springframework.org/tags" %>

    <h2>Motorist info (Step 1 of 3)</h2>
    
    <form:form commandName="motoristForm" method="POST" action="register.htm">
      <input type="hidden" name="_flowExecutionKey" value="${flowExecutionKey}"><br/>    
      <spring:message code="field.email" /> <form:input path="motorist.username" /><br/>
      <spring:message code="field.password" />: <form:input path="motorist.password" /><br/>
      <spring:message code="field.firstName" /><form:input path="motorist.firstName" /><br/>
      <spring:message code="field.lastName" /><form:input path="motorist.lastName" /><br/>
      <input type="submit" name="_eventId_continue" value="Next"/>
    </form:form>
