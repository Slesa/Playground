<%@ attribute name="state" required="true" %>
<%@ attribute name="number" required="true" %>
<%-- TODO: Fix hacky call to getRGBForState() --%>
<div class="plateDiv" style="background-image:url(images/plates/${state}_sm.jpg);
        color:<%=com.roadrantz.mvc.PlateTextColorHelper.getRGBForState(state) %>;">
  <span style="font-size:16pt;line-height:50px;">${number}</span>
</div>