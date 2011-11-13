<%@ taglib prefix="tiles" 
    uri="http://jakarta.apache.org/struts/tags-tiles" %>
<%@ taglib prefix="security" uri="http://www.springframework.org/security/tags" %>
<%@ taglib prefix="c" uri="http://java.sun.com/jsp/jstl/core" %>

<html>
  <head>
    <title><tiles:getAsString name="title"/></title>
    <link href="css/main.css" rel="stylesheet" type="text/css"> 
  
   <script>
     function highlightField(field)
      {
        field.style.backgroundColor="#009900";
        field.style.border="1px solid white;"
      }

     function unhighlightField(field)
      {
        field.style.backgroundColor="#006e00";
        field.style.border="1px solid #008800;"
      }

      function highlightAndShift(field) {
//     highlightField(field);
       field.style.marginTop  = "-3px;";
        field.style.marginLeft = "3px;";
      }

      function unhighlightAndShiftBack(field) {
//     unhighlightField(field);
       field.style.marginTop  = "0px;";
        field.style.marginLeft = "0px;";
      }
   </script>
  </head>
  <body style="margin:0px;">
   <div style="width:100%;height:120px;background-image: url(images/trafficbanner.png);">
      <table width="100%" style="height:124px;"><tr>
         <td><a href="<c:url value='home.htm' />"><img src="images/rr-logo.png" border="0"/></a></td>
         <td align="right" valign="bottom">
            <div style="text-align:left;width:300px;height:109px;background-image: url(images/freewaysign.png);">
            <security:authorize ifAnyGranted="ROLE_ANONYMOUS">
               <form style="font-family:arial;color:white;font-size:9pt;width:230px;margin-left:10px;" action="<c:url value='j_spring_security_check'/>" method="POST">
                  <br/><img src="images/LoginToRoadRantz.png" />
                  <table border="0" cellspacing="0" cellpadding="0">
                     <tr>
                        <td><b style="font-family:arial;color:white;font-size:9pt;">Username:</b></td>
                        <td><input type="text" name="j_username" style="background-color:#006e00;border:1px solid #006e00;color:white;width:130px;" onmouseover="highlightField(this);" onfocus="highlightField(this);" onblur="unhighlightField(this);" onmouseout="unhighlightField(this);" tabindex="1"/></td>
                        <td rowspan="3" style="padding-left:8px;"><input type="image" src="images/exit-arrow-right.png" onmouseover="highlightAndShift(this);" onfocus="highlightAndShift(this);" onblur="unhighlightAndShiftBack(this);" onmouseout="unhighlightAndShiftBack(this);" tabindex="3"/></td>
                     </tr>
                     <tr>
                        <td><b style="font-family:arial;color:white;font-size:9pt;">Password:</b></td>
                        <td><input type="password" name="j_password" style="background-color:#006e00;border:1px solid #006e00;color:white;width:130px;" onmouseover="highlightField(this);" onfocus="highlightField(this);" onblur="unhighlightField(this);" onmouseout="unhighlightField(this);" tabindex="2"/></td>
                     </tr>
                     <tr>
                        <td colspan="2">
                          <input type="checkbox" name="_spring_security_remember_me" style="width:10px;height:10px;"><span style="font-family:arial;color:white;font-size:9pt;">Remember me</span></input>
                        </td>
                     </tr>
                   </table>
                </form>
            </security:authorize>
            <security:authorize ifNotGranted="ROLE_ANONYMOUS">
              <div style="font-family:arial;color:white;font-size:10pt;width:230px;margin-left:10px;text-align:center;">
                <br/>
                <p>Welcome, <security:authentication property="principal.username"/>!</p>
                <a href="j_spring_security_logout" style="color:white;">Log out</a>
              </div>
            </security:authorize>
            </div>
         </td>
      </tr>
      </table>
    </div>
    <div style="width:100%;min-width:700px;background-color:#006e00;color:white;font-size:12pt;height:20px;padding-top:4px;border-top:1px solid white;border-bottom:1px solid:white;">
      <a href="<c:url value='addRant.htm'/>" style="margin-right:20px;"><img src="images/addARant.png" border="0"/></a>
      <security:authorize ifAnyGranted="ROLE_ANONYMOUS"><a href="<c:url value='register.htm'/>" style="margin-left:20px;margin-right:20px;"><img src="images/register.png" border="0"/></a></security:authorize>
      <a href="#" style="margin-left:20px;"><img src="images/about.png" border="0"/></a>
    </div>
    <center>    
       <div class="contentArea">
         <tiles:insert name="content"/>
       </div>
    </center>
       <div class="footer">
         <tiles:insert name="footer"/>
       </div>
       <span style="color:#000066;font-size:7pt;"><i>License plate images provided courtesy of <a href="http://www.rtbrandon.com/blankplates/" style="color:#000066;text-decoration:none;">R.T.'s Blank Plates</a>.</i></span>
  </body>
</html>
