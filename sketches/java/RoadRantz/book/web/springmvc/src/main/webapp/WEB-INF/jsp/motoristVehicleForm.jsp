<%@ page contentType="text/html" %>
<%@ taglib prefix="c" uri="http://java.sun.com/jsp/jstl/core" %>
<%@ taglib prefix="fn" uri="http://java.sun.com/jsp/jstl/functions" %>
<%@ taglib prefix="form" uri="http://www.springframework.org/tags/form" %>
<%@ taglib prefix="spring" uri="http://www.springframework.org/tags" %>
<%@ taglib prefix="rr" tagdir="/WEB-INF/tags" %>

    <h2>Motorist Vehicles vehicles (Step 2 of 3)</h2>

    <form:form commandName="motoristForm" method="POST" action="register.htm">
      <input type="hidden" name="_flowExecutionKey" value="${flowExecutionKey}"><br/>    
      <input type="hidden" value="<c:out value="${nextVehicle}" />" /><br/>
      Motorist: <c:out value="${motoristForm.motorist.firstName}" />&nbsp;<c:out value="${motoristForm.motorist.lastName}" /><br/>
      E-mail: <c:out value="${motoristForm.motorist.username}" /><br/>
      Vehicles: <br/>
      <ul>
      <c:forEach items="${motoristForm.scrubbedVehicles}" var="vehicle">
      <li><c:out value="${vehicle.state}" /> - <c:out value="${vehicle.plateNumber}" /></li>
      </c:forEach>
      </ul>
      
      <hr />
      State: <rr:stateSelection path="nextVehicle.state" /><br/>
      Plate: <form:input path="nextVehicle.plateNumber" /><br/>
      <input type="submit" name="_eventId_back" value="Back"/>&nbsp;
      <input type="submit" name="_eventId_add" value="Add Vehicle"/>&nbsp;
      <input type="submit" name="_eventId_continue" value="Next"/>
    </form:form>
