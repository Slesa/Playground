<%@ taglib prefix="c" uri="http://java.sun.com/jsp/jstl/core" %>
<%@ taglib prefix="fn" uri="http://java.sun.com/jsp/jstl/functions" %>
<%@ taglib prefix="rr" tagdir="/WEB-INF/tags" %>

<div class="rantList">
   <c:forEach items="${rantList}" var="rant">
     <rr:plate state="${fn:toLowerCase(rant.vehicle.state)}" number="${rant.vehicle.plateNumber}" />
     <div style="min-height:50px;vertical-align:middle;"><c:out value="${rant.rantText}"/></div><br/>
   </c:forEach>
</div>