<%@ taglib prefix="c" uri="http://java.sun.com/jsp/jstl/core" %>
<div class="signInBox">
  <h3>Welcome to RoadRantz</h3>
  <form method="POST" action="<c:url value='j_spring_security_check'/>">
    <b>Username:  </b><input type="text" name="j_username"><br>
    <b>Password:  </b><input type="password" name="j_password"><br>
    <input id="remember_me" type="checkbox" name="_spring_security_remember_me"><b>Remember Me</b><br>
    <input type="submit" value="Login">
  </form>
  <p>New to RoadRantz? <a href="register.htm">Register</a> now!</p>
</div>