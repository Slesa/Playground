
/*

  SmartClient Ajax RIA system
  Version 8.1/LGPL Development Only (2011-08-02)

  Copyright 2000 and beyond Isomorphic Software, Inc. All rights reserved.
  "SmartClient" is a trademark of Isomorphic Software, Inc.

  LICENSE NOTICE
     INSTALLATION OR USE OF THIS SOFTWARE INDICATES YOUR ACCEPTANCE OF
     ISOMORPHIC SOFTWARE LICENSE TERMS. If you have received this file
     without an accompanying Isomorphic Software license file, please
     contact licensing@isomorphic.com for details. Unauthorized copying and
     use of this software is a violation of international copyright law.

  DEVELOPMENT ONLY - DO NOT DEPLOY
     This software is provided for evaluation, training, and development
     purposes only. It may include supplementary components that are not
     licensed for deployment. The separate DEPLOY package for this release
     contains SmartClient components that are licensed for deployment.

  PROPRIETARY & PROTECTED MATERIAL
     This software contains proprietary materials that are protected by
     contract and intellectual property law. You are expressly prohibited
     from attempting to reverse engineer this software or modify this
     software for human readability.

  CONTACT ISOMORPHIC
     For more information regarding license rights and restrictions, or to
     report possible license violations, please contact Isomorphic Software
     by email (licensing@isomorphic.com) or web (www.isomorphic.com).

*/

var isc = window.isc ? window.isc : {};if(window.isc&&!window.isc.module_History){isc.module_History=1;isc._moduleStart=isc._History_start=(isc.timestamp?isc.timestamp():new Date().getTime());if(isc._moduleEnd&&(!isc.Log||(isc.Log && isc.Log.logIsDebugEnabled('loadTime')))){isc._pTM={ message:'History load/parse time: ' + (isc._moduleStart-isc._moduleEnd) + 'ms', category:'loadTime'};
if(isc.Log && isc.Log.logDebug)isc.Log.logDebug(isc._pTM.message,'loadTime')
else if(isc._preLog)isc._preLog[isc._preLog.length]=isc._pTM
else isc._preLog=[isc._pTM]}isc.definingFramework=true;var isc=window.isc?window.isc:{};isc.$a=new Date().getTime();isc.version="8.1/LGPL Development Only";isc.versionNumber="8.1";isc.buildDate="2011-08-02";isc.expirationDate="${expiration}";isc.licenseType="LGPL";isc.licenseCompany="";isc.licenseSerialNumber="925ae72b9e3cbaf60a54637146649e5d";isc.licensingPage="http://smartclient.com/product/";isc.$b={SCServer:{present:"false",name:"SmartClient Server",serverOnly:true,isPro:true},Drawing:{present:"false",name:"Drawing Module"},PluginBridges:{present:"${includePluginBridges}",name:"PluginBridges Module"},RichTextEditor:{present:"${includeRichTextEditor}",name:"RichTextEditor Module"},Calendar:{present:"${includeCalendar}",name:"Calendar Module"},Analytics:{present:"false",name:"Analytics Module"},Tools:{present:"${includeTools}",name:"Portal and Tools Module"},NetworkPerformance:{present:"false",name:"Network Performance Module"},FileLoader:{present:"false",name:"Network Performance Module"},RealtimeMessaging:{present:"false",name:"RealtimeMessaging Module"},serverCriteria:{present:"${includeServerCriteria}",name:"Server Advanced Filtering",serverOnly:true,isFeature:true},customSQL:{present:"${includeCustomSQL}",name:"SQL Templating",serverOnly:true,isFeature:true},chaining:{present:"${includeChaining}",name:"Transaction Chaining",serverOnly:true,isFeature:true},batchDSGenerator:{present:"${includeBatchDSGenerator}",name:"Batch DS-Generator",serverOnly:true,isFeature:true},batchUploader:{present:"${includeBatchUploader}",name:"Batch Uploader",serverOnly:true,isFeature:true},transactions:{present:"${includeTransactions}",name:"Automatic Transaction Management",serverOnly:true,isFeature:true}};isc.canonicalizeModules=function(_1){if(!_1)return null;if(isc.isA.String(_1)){if(_1.indexOf(",")!=-1){_1=_1.split(",");var _2=/^\s+/,_3=/\s+$/;for(var i=0;i<_1.length;i++){_1[i]=_1[i].replace(_2,"").replace(_3,"")}}else _1=[_1]}
return _1};isc.hasOptionalModules=function(_1){if(!_1)return true;_1=isc.canonicalizeModules(_1);for(var i=0;i<_1.length;i++)if(!isc.hasOptionalModule(_1[i]))return false;return true};isc.getMissingModules=function(_1){var _2=[];_1=isc.canonicalizeModules(_1);for(var i=0;i<_1.length;i++){var _4=_1[i];if(!isc.hasOptionalModule(_4))_2.add(isc.$b[_4])}
return _2};isc.hasOptionalModule=function(_1){var v=isc.$b[_1];if(!v){if(isc.Log)isc.Log.logWarn("isc.hasOptionalModule - unknown module: "+_1);return false}
return v.present=="true"||v.present.charAt(0)=="$"};isc.getOptionalModule=function(_1){return isc.$b[_1]};isc.$d=window.isc_useSimpleNames;if(isc.$d==null)isc.$d=true;if(window.OpenAjax){isc.$e=isc.versionNumber.replace(/[a-zA-Z_]+/,".0");OpenAjax.registerLibrary("SmartClient","http://smartclient.com/SmartClient",isc.$e,{namespacedMode:!isc.$d,iscVersion:isc.version,buildDate:isc.buildDate,licenseType:isc.licenseType,licenseCompany:isc.licenseCompany,licenseSerialNumber:isc.licenseSerialNumber});OpenAjax.registerGlobals("SmartClient",["isc"])}
isc.$f=window.isc_useLongDOMIDs;isc.$g="isc.";isc.addGlobal=function(_1,_2){if(_1.indexOf(isc.$g)==0)_1=_1.substring(4);isc[_1]=_2;if(isc.$d)window[_1]=_2}
isc.onLine=true;isc.isOffline=function(){return!isc.onLine};isc.goOffline=function(){isc.onLine=false};isc.goOnline=function(){isc.onLine=true};if(window.addEventListener){window.addEventListener("online",isc.goOnline,false);window.addEventListener("offline",isc.goOffline,false)}
isc.addGlobal("Browser",{isSupported:false});isc.Browser.isOpera=(navigator.appName=="Opera"||navigator.userAgent.indexOf("Opera")!=-1);isc.Browser.isNS=(navigator.appName=="Netscape"&&!isc.Browser.isOpera);isc.Browser.isIE=(navigator.appName=="Microsoft Internet Explorer"&&!isc.Browser.isOpera);isc.Browser.isMSN=(isc.Browser.isIE&&navigator.userAgent.indexOf("MSN")!=-1);isc.Browser.minorVersion=parseFloat(isc.Browser.isIE?navigator.appVersion.substring(navigator.appVersion.indexOf("MSIE")+5):navigator.appVersion);isc.Browser.version=parseInt(isc.Browser.minorVersion);isc.Browser.isIE6=isc.Browser.isIE&&isc.Browser.version<=6;isc.Browser.isMoz=(navigator.userAgent.indexOf("Gecko")!=-1)&&(navigator.userAgent.indexOf("Safari")==-1)&&(navigator.userAgent.indexOf("AppleWebKit")==-1);isc.Browser.isCamino=(isc.Browser.isMoz&&navigator.userAgent.indexOf("Camino/")!=-1);if(isc.Browser.isCamino){isc.Browser.caminoVersion=navigator.userAgent.substring(navigator.userAgent.indexOf("Camino/")+7)}
isc.Browser.isFirefox=(isc.Browser.isMoz&&navigator.userAgent.indexOf("Firefox/")!=-1);if(isc.Browser.isFirefox){isc.Browser.firefoxVersion=navigator.userAgent.substring(navigator.userAgent.indexOf("Firefox/")+8)}
if(isc.Browser.isMoz){isc.Browser.$h=navigator.userAgent.indexOf("Gecko/")+6;isc.Browser.geckoVersion=parseInt(navigator.userAgent.substring(isc.Browser.$h,isc.Browser.$h+8));if(isc.Browser.isFirefox){if(isc.Browser.firefoxVersion.match(/^1\.0/))isc.Browser.geckoVersion=20050915;else if(isc.Browser.firefoxVersion.match(/^2\.0/))isc.Browser.geckoVersion=20071108}}
isc.Browser.isStrict=document.compatMode=="CSS1Compat";if(isc.Browser.isStrict&&isc.Browser.isMoz){isc.Browser.$i=document.doctype.publicId;isc.Browser.$j=document.doctype.systemId;isc.Browser.isTransitional=isc.Browser.$i.indexOf("Transitional")!=-1||isc.Browser.$i.indexOf("Frameset")!=-1}
isc.Browser.isIE7=isc.Browser.isIE&&isc.Browser.version==7;isc.Browser.isIE8=isc.Browser.isIE&&isc.Browser.version>=8&&document.documentMode==8
isc.Browser.isIE8Strict=isc.Browser.isIE&&isc.Browser.isStrict&&document.documentMode>=8;isc.Browser.isIE9=isc.Browser.isIE&&isc.Browser.version>=9&&document.documentMode>=9;isc.Browser.isAIR=(navigator.userAgent.indexOf("AdobeAIR")!=-1);isc.Browser.AIRVersion=(isc.Browser.isAIR?navigator.userAgent.substring(navigator.userAgent.indexOf("AdobeAir/")+9):null);isc.Browser.isWebKit=navigator.userAgent.indexOf("WebKit")!=-1;isc.Browser.isSafari=isc.Browser.isAIR||navigator.userAgent.indexOf("Safari")!=-1||navigator.userAgent.indexOf("AppleWebKit")!=-1;isc.Browser.isChrome=isc.Browser.isSafari&&(navigator.userAgent.indexOf("Chrome/")!=-1);if(isc.Browser.isSafari){if(isc.Browser.isAIR){isc.Browser.safariVersion=530}else{if(navigator.userAgent.indexOf("Safari/")!=-1){isc.Browser.rawSafariVersion=navigator.userAgent.substring(navigator.userAgent.indexOf("Safari/")+7)}else if(navigator.userAgent.indexOf("AppleWebKit/")!=-1){isc.Browser.rawSafariVersion=navigator.userAgent.substring(navigator.userAgent.indexOf("AppleWebKit/")+12)}else{isc.Browser.rawSafariVersion="530"}
isc.Browser.safariVersion=(function(){var _1=isc.Browser.rawSafariVersion,_2=_1.indexOf(".");if(_2==-1)return parseInt(_1);var _3=_1.substring(0,_2+1),_4;while(_2!=-1){_2+=1;_4=_1.indexOf(".",_2);_3+=_1.substring(_2,(_4==-1?_1.length:_4));_2=_4}
return parseFloat(_3)})()}}
isc.Browser.isWin=navigator.platform.toLowerCase().indexOf("win")>-1;isc.Browser.isWin2k=navigator.userAgent.match(/NT 5.01?/)!=null;isc.Browser.isMac=navigator.platform.toLowerCase().indexOf("mac")>-1;isc.Browser.isUnix=(!isc.Browser.isMac&&!isc.Browser.isWin);isc.Browser.isAndroid=navigator.userAgent.indexOf("Android")>-1;isc.Browser.isMobileWebkit=(isc.Browser.isSafari&&navigator.userAgent.indexOf(" Mobile/")>-1||isc.Browser.isAndroid);isc.Browser.isMobile=(isc.Browser.isMobileWebkit);isc.Browser.isTouch=(isc.Browser.isMobileWebkit);isc.Browser.isIPhone=(isc.Browser.isMobileWebkit&&navigator.userAgent.indexOf("AppleWebKit"));isc.Browser.isHandset=(isc.Browser.isMobileWebkit&&navigator.userAgent.indexOf("iPad")==-1);isc.Browser.isIPad=(isc.Browser.isIPhone&&navigator.userAgent.indexOf("iPad"));isc.Browser.isTablet=(isc.Browser.isIPad);isc.Browser.isBorderBox=(isc.Browser.isIE&&!isc.Browser.isStrict);isc.Browser.lineFeed=(isc.Browser.isWin?"\r\n":"\r");isc.Browser.$k=false;isc.Browser.isDOM=(isc.Browser.isMoz||isc.Browser.isOpera||isc.Browser.isSafari||(isc.Browser.isIE&&isc.Browser.version>=5));isc.Browser.isSupported=((isc.Browser.isIE&&isc.Browser.minorVersion>=5.5&&isc.Browser.isWin)||isc.Browser.isMoz||isc.Browser.isOpera||isc.Browser.isSafari||isc.Browser.isAIR);isc.Browser.allowsXSXHR=((isc.Browser.isFirefox&&isc.Browser.firefoxVersion>="3.5")||(isc.Browser.isChrome)||(isc.Browser.isSafari&&isc.Browser.safariVersion>=531));if(isc.addProperties==null){isc.addGlobal("addProperties",function(_1,_2){for(var _3 in _2)
_1[_3]=_2[_3];return _1})}
isc.addGlobal("evalSA",function(_1){if(isc.eval)isc.eval(_1);else eval(_1)});isc.addGlobal("defineStandaloneClass",function(_1,_2){if(isc[_1])return;isc.addGlobal(_1,_2);isc.addProperties(_2,{$l:_1,fireSimpleCallback:function(_3){_3.method.apply(_3.target?_3.target:window,_3.args?_3.args:[])},logMessage:function(_3,_4,_5){if(isc.Log){isc.Log.logMessage(_3,_4,_5);return}
if(!isc.$m)isc.$m=[];isc.$m[isc.$m.length]={priority:_3,message:_4,category:_5,timestamp:new Date()}},logWarn:function(_3){this.logMessage(3,_3,this.$l)},logInfo:function(_3){this.logMessage(4,_3,this.$l)},logDebug:function(_3){this.logMessage(5,_3,this.$l)},isAString:function(_3){if(_3==null)return false;if(_3.constructor&&_3.constructor.$n!=null){return _3.constructor.$n==4}
return typeof _3=="string"}});_2.isAn=_2.isA;return _2});isc.defineStandaloneClass("SA_Page",{$o:false,$p:[],isLoaded:function(){return this.$o},onLoad:function(_1,_2,_3){this.$p.push({method:_1,target:_2,args:_3});if(!this.$q){this.$q=true;if(isc.Browser.isIE||isc.Browser.isOpera){window.attachEvent("onload",function(){isc.SA_Page.$r()})}else{window.addEventListener("load",function(){isc.SA_Page.$r()},true)}}},$r:function(){if(!window.isc||this.$o)return;this.$o=true;for(var i=0;i<this.$p.length;i++){var _2=this.$p[i];this.fireSimpleCallback(_2)}
delete this.$p}});isc.SA_Page.onLoad(function(){this.$o=true},isc.SA_Page);isc.defineStandaloneClass("History",{registerCallback:function(_1,_2){this.$s=_1;this.$t=_2},getCurrentHistoryId:function(){var _1=this.$u(location.href);if(_1=="$v")return null;return _1},getHistoryData:function(_1){return this.historyState?this.historyState.data[_1]:null},setHistoryTitle:function(_1){this.historyTitle=_1},addHistoryEntry:function(_1,_2,_3){this.logDebug("addHistoryEntry: id="+_1+" data="+isc.echoAll(_3));if(_1==null)_1="";if(isc.Browser.isSafari&&isc.Browser.safariVersion<500){return}
if(!isc.SA_Page.isLoaded()){this.logWarn("You must wait until the page has loaded before calling "+"isc.History.addHistoryEntry()");return}
var _4=this.$u(location.href);var _5;if(_3===_5)_3=null;if(_4==_1){this.historyState.data[_1]=_3;this.$w();return}
while(this.historyState.stack.length){var _6=this.historyState.stack.pop();if(_6==_4){this.historyState.stack.push(_6);break}
delete this.historyState.data[_6]}
this.historyState.stack.add(_1);this.historyState.data[_1]=_3;this.logDebug("historyState[id]: "+isc.echoAll(this.historyState.data[_1]));this.$w();if(isc.Browser.isIE){if(_1!=null&&document.getElementById(_1)!=null){this.logWarn("Warning - attempt to add synthetic history entry with id that conflicts"+" with an existing DOM element node ID - this is known to break in IE")}
if(_4==null){var _7=location.href;var _8=document.getElementsByTagName("title");if(_8.length)_7=_8[0].innerHTML;this.$x("$v",_7)}
this.$x(_1,_2)}else{location.href=this.$y(location.href,_1)}
this.$z=location.href},$x:function(_1,_2){this.$0=true;var _3=!this.isAString(_1)?_1:_1.replace(/\\/g,"\\\\").replace(/\"/g,"\\\"").replace(/\t/g,"\\t").replace(/\r/g,"\\r").replace(/\n/g,"\\n");var _4="<HTML><HEAD><TITLE>"+(_2!=null?_2:this.historyTitle!=null?this.historyTitle:_1)+"</TITLE></HEAD><BODY><SCRIPT>var pwin = window.parent;if (pwin && pwin.isc)pwin.isc.History.historyCallback(window,\""+_3+"\");</SCRIPT></BODY></HTML>";var _5=this.$1.contentWindow;_5.document.open();_5.document.write(_4);_5.document.close()},haveHistoryState:function(_1){if(isc.Browser.isIE&&!isc.SA_Page.isLoaded()){this.logWarn("haveHistoryState() called before pageLoad - this always returns false"+" in IE because state information is not available before pageLoad")}
var _2;return this.historyState&&this.historyState.data[_1]!==_2},$2:function(){return window.isomorphicDir?window.isomorphicDir:"../isomorphic/"},$3:function(){this.logInfo("History initializing");if(this.$4)return;this.$4=true;if(isc.Browser.isSafari&&isc.Browser.safariVersion<500)return;var _1="<form style='position:absolute;top:-1000px' id='isc_historyForm'>"+"<textarea id='isc_historyField' style='display:none'></textarea></form>";document.write(_1);if(isc.Browser.isIE){var _2="<iframe id='isc_historyFrame' src='"+this.getBlankFrameURL()+"' style='position:absolute;visibility:hidden;top:-1000px'></iframe>";document.write(_2);this.$1=document.getElementById('isc_historyFrame');document.write("<span id='isc_history_buffer_marker' style='display:none'></span>")}
if(isc.Browser.isIE){isc.SA_Page.onLoad(function(){this.$5()},this)}else if(isc.Browser.isMoz||isc.Browser.isOpera||(isc.Browser.isSafari&&isc.Browser.safariVersion>=500)){this.$5()}},getBlankFrameURL:function(){if(isc.Page)return isc.Page.getBlankFrameURL();if(isc.Browser.isIE&&("https:"==window.location.protocol||document.domain!=location.hostname))
{var _1,_2=window.isomorphicDir;if(_2&&(_2.indexOf("/")==0||_2.indexOf("http")==0))
{_1=_2}else{_1=window.location.href;if(_1.charAt(_1.length-1)!="/"){_1=_1.substring(0,_1.lastIndexOf("/")+1)}
_1+=(_2==null?"../isomorphic/":_2)}
_1+="system/helpers/empty.html";return _1}
return"about:blank"},$6:function(){var _1=document.getElementById("isc_historyField");return _1?_1.value:null},$7:function(_1){var _2=document.getElementById("isc_historyField");if(_2)_2.value=_1},$5:function(){var _1=this.$6();if(_1){_1=new Function("return ("+_1+")")()}
if(!_1)_1={stack:[],data:{}};this.historyState=_1;this.logInfo("History init complete");this.$z=location.href;this.$8=window.setInterval("isc.History.$9()",this.$aa);if(isc.Browser.isIE||isc.Browser.isMoz||isc.Browser.isOpera||(isc.Browser.isSafari&&isc.Browser.safariVersion>=500))
{isc.SA_Page.onLoad(this.$ab,this)}},$ab:function(){if(this.$ac)return;if(this.$s&&isc.SA_Page.isLoaded()){this.$ac=true;var _1=this.$u(location.href);this.$ad(_1)}},$y:function(_1,_2){var _3=_1.match(/([^#]*).*/);return _3[1]+"#"+encodeURI(_2)},$u:function(_1){var _2=location.href.match(/([^#]*)#(.*)/);return _2?decodeURI(_2[2]):null},$aa:100,$w:function(){if(isc.Comm){this.$7(isc.Comm.serialize(this.historyState))}},$9:function(){if(location.href!=this.$z){var _1=this.$u(location.href);this.$ad(_1)}
this.$z=location.href},historyCallback:function(_1,_2){if(_2=="$v")_2="";var _3=this.$y(location.href,_2);if(isc.SA_Page.isLoaded()){location.href=_3;this.$z=_3}else{isc.SA_Page.onLoad(function(){location.href=this.$y(location.href,_2);this.$z=_3},this)}
if(this.$0){this.$0=false;return}
if(isc.SA_Page.isLoaded()){this.$ad(_2)}else{isc.SA_Page.onLoad(function(){this.$ad(_2)},this)}},$ad:function(_1){if(this.$ae==_1){if(this.$72a)return}
this.$72a=true;if(!this.$s){this.logWarn("ready to fire history callback, but no callback registered."+"Please call isc.History.registerCallback() before pageLoad."+" If you can't register your callback before pageLoad, you"+" can call isc.History.getCurrentHistoryId() to get the ID"+" when you're ready.");return}
if(_1=="$v")_1=null;var _2=this.$s;var _3;if(!this.haveHistoryState(_1)){if(this.$t){this.logWarn("User navigated to URL associated with synthetic history ID:"+_1+". This ID is not associated with any synthetic history entry generated via "+"History.addHistoryEntry(). Not firing registered historyCallback as "+"callback was registered with parameter requiring a data object.  "+"This can commonly occur when the user navigates to a stored history entry "+"via a bookmarked URL.");return}}else{_3=this.historyState.data[_1]}
this.$ae=_1;this.logDebug("history callback: "+_1);if(isc.Class&&this.isAString(_2)){isc.Class.fireCallback(_2,["id","data"],[_1,_3])}else{_2=isc.addProperties({},_2);_2.args=[_1,_3];this.fireSimpleCallback(_2)}}});isc.History.$3();isc._moduleEnd=isc._History_end=(isc.timestamp?isc.timestamp():new Date().getTime());if(isc.Log&&isc.Log.logIsInfoEnabled('loadTime'))isc.Log.logInfo('History module init time: ' + (isc._moduleEnd-isc._moduleStart) + 'ms','loadTime');delete isc.definingFramework;}else{if(window.isc && isc.Log && isc.Log.logWarn)isc.Log.logWarn("Duplicate load of module 'History'.");}

/*

  SmartClient Ajax RIA system
  Version 8.1/LGPL Development Only (2011-08-02)

  Copyright 2000 and beyond Isomorphic Software, Inc. All rights reserved.
  "SmartClient" is a trademark of Isomorphic Software, Inc.

  LICENSE NOTICE
     INSTALLATION OR USE OF THIS SOFTWARE INDICATES YOUR ACCEPTANCE OF
     ISOMORPHIC SOFTWARE LICENSE TERMS. If you have received this file
     without an accompanying Isomorphic Software license file, please
     contact licensing@isomorphic.com for details. Unauthorized copying and
     use of this software is a violation of international copyright law.

  DEVELOPMENT ONLY - DO NOT DEPLOY
     This software is provided for evaluation, training, and development
     purposes only. It may include supplementary components that are not
     licensed for deployment. The separate DEPLOY package for this release
     contains SmartClient components that are licensed for deployment.

  PROPRIETARY & PROTECTED MATERIAL
     This software contains proprietary materials that are protected by
     contract and intellectual property law. You are expressly prohibited
     from attempting to reverse engineer this software or modify this
     software for human readability.

  CONTACT ISOMORPHIC
     For more information regarding license rights and restrictions, or to
     report possible license violations, please contact Isomorphic Software
     by email (licensing@isomorphic.com) or web (www.isomorphic.com).

*/

