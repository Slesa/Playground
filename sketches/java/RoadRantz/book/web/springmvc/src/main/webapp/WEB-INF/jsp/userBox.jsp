<%@ taglib prefix="security" uri="http://www.springframework.org/security/tags" %>
<div class="signInBox">
  <h3>Welcome <security:authentication property="principal.username"/></h3>
  <p><a href="j_spring_security_logout" style="font-size:12pt;color:black;text-decoration:none;">
  <img src="images/signs/exit.gif" border="0" width="50" align="middle"/> Logout of RoadRantz</a></p>
</div>