<%
String isomorphicDir = pageContext.getServletContext().getContextPath()+"/thirdparty/isomorphic/";

String isomorphicScriptPath = isomorphicDir + "system/modules/"; //minified version
//String isomorphicScriptPath = isomorphicDir + "client/modules/";   //developer version
%>
<script>var isomorphicDir="<%=isomorphicDir%>";</script> 

<script	SRC="<%=isomorphicScriptPath%>ISC_Core.js"></script> 
<script	SRC="<%=isomorphicScriptPath%>ISC_Foundation.js"></script>
<script SRC="<%=isomorphicScriptPath%>ISC_Containers.js"></script>
<script SRC="<%=isomorphicScriptPath%>ISC_Grids.js"></script>
<script SRC="<%=isomorphicScriptPath%>ISC_Forms.js"></script>

<!-- // Load smart client dacos skin. -->
<script type="text/javascript" src="<%=isomorphicDir%>skins/Mobile/load_skin.js"></script>

<!-- script type="text/javascript" src="${pageContext.request.contextPath}/thirdparty/json2.js"></script -->
<!-- script type="text/javascript" src="${pageContext.request.contextPath}/thirdparty/RoboHelp_CSH.js"></script -->
<!-- script type="text/javascript" src="${pageContext.request.contextPath}/thirdparty/xregexp/xregexp-min.js"></script -->
<!-- script type="text/javascript" src="${pageContext.request.contextPath}/thirdparty/xregexp/xregexp-unicode-base.js"></script -->
<!-- script type="text/javascript" src="${pageContext.request.contextPath}/thirdparty/xregexp/xregexp-unicode-blocks.js"></script -->
<!-- script type="text/javascript" src="${pageContext.request.contextPath}/thirdparty/xregexp/xregexp-unicode-categories.js"></script -->
<!-- script type="text/javascript" src="${pageContext.request.contextPath}/thirdparty/xregexp/xregexp-unicode-scripts.js"></script -->

