<%@ page contentType="text/html" %>
<%@ taglib prefix="tiles" 
    uri="http://jakarta.apache.org/struts/tags-tiles" %>

<table width="100%">
  <tr>
    <td valign="top">
      <h3>Recent Rantz:</h3>
      <tiles:insert template="rantList.jsp" />
    </td>
  </tr>
</table>
