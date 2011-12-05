/*
 * Isomorphic SmartClient
 * Version 8.1 (2011-08-02)
 * Copyright(c) 1998 and beyond Isomorphic Software, Inc. All rights reserved.
 * "SmartClient" is a trademark of Isomorphic Software, Inc.
 *
 * licensing@smartclient.com
 *
 * http://smartclient.com/license
 */

if(window.isc&&window.isc.module_Core&&!window.isc.module_RichTextEditor){isc.module_RichTextEditor=1;isc._moduleStart=isc._RichTextEditor_start=(isc.timestamp?isc.timestamp():new Date().getTime());if(isc._moduleEnd&&(!isc.Log||(isc.Log && isc.Log.logIsDebugEnabled('loadTime')))){isc._pTM={ message:'RichTextEditor load/parse time: ' + (isc._moduleStart-isc._moduleEnd) + 'ms', category:'loadTime'};
if(isc.Log && isc.Log.logDebug)isc.Log.logDebug(isc._pTM.message,'loadTime')
else if(isc._preLog)isc._preLog[isc._preLog.length]=isc._pTM
else isc._preLog=[isc._pTM]}isc.definingFramework=true;isc.ClassFactory.defineClass("RichTextCanvas","Canvas");isc.A=isc.RichTextCanvas;isc.A.FULL="full";isc.A.unsupportedErrorMessage="Rich text editing not supported in this browser";isc.A=isc.RichTextCanvas.getPrototype();isc.A.editable=true;isc.A.canSelectText=true;isc.A.canFocus=true;isc.A.$lq=false;isc.A.overflow=isc.Canvas.AUTO;isc.A.showCustomScrollbars=false;isc.A.fullSyntaxHiliteDelay=3000;isc.A.contents="";isc.A=isc.RichTextCanvas;isc.B=isc._allFuncs;isc.C=isc.B._maxIndex;isc.D=isc._funcClasses;isc.D[isc.C]=isc.A.Class;isc.B.push(isc.A.supportsRichTextEditing=function isc_c_RichTextCanvas_supportsRichTextEditing(){var _1=((isc.Browser.isSafari&&isc.Browser.safariVersion>=312)||(isc.Browser.isIE)||(isc.Browser.isMoz&&!isc.Browser.isCamino)||isc.Browser.isOpera);return _1}
);isc.B._maxIndex=isc.C+1;isc.A=isc.RichTextCanvas.getPrototype();isc.B=isc._allFuncs;isc.C=isc.B._maxIndex;isc.D=isc._funcClasses;isc.D[isc.C]=isc.A.Class;isc.A.$16k="<BR>";isc.A.$15u="Enter";isc.A.ignoreKeys=["Arrow_Up","Arrow_Down","Arrow_Left","Arrow_Right","Ctrl","Alt"];isc.B.push(isc.A.initWidget=function isc_RichTextCanvas_initWidget(){if(!isc.RichTextCanvas.supportsRichTextEditing()){var _1=isc.RichTextCanvas.unsupportedErrorMessage;this.logError(_1)}
if(this.overflow!=isc.Canvas.AUTO){this.logWarn('RichTextCanvas class currently only supports an overflow property of "auto"');this.overflow=isc.Canvas.AUTO}
if(this.$322()){this._useNativeTabIndex=false}
this.Super("initWidget",arguments)}
,isc.A.$vb=function isc_RichTextCanvas__getHandleOverflow(){if(this.$322()){var _1;if(this.$lv){_1="-moz-scrollbars-none";this.$vw=true}else{_1=this.$q5}
return _1}else return this.Super("$vb",arguments)}
,isc.A.getInnerHTML=function isc_RichTextCanvas_getInnerHTML(){if(this.$322()&&!this.isPrinting){return this.getIFrameHTML()}
return this.getContents(true)}
,isc.A.$322=function isc_RichTextCanvas__useDesignMode(){return isc.Browser.isMoz||isc.Browser.isSafari}
,isc.A.getIFrameHTML=function isc_RichTextCanvas_getIFrameHTML(){var _1=isc.Browser.isSafari,_2=_1?isc.Page.getBlankFrameURL():null,_3=this.getContentFrameWidth()+isc.px,_4=this.getContentFrameHeight()+isc.px,_5=["<IFRAME STYLE='margin:0px;padding:0px;border:0px;width:",_3,";height:",_4,";'",(_1||true?" src='"+isc.Page.getURL("[HELPERS]empty.html")+"'":null)," ONLOAD='",this.getID(),".$323();'"," TABINDEX=",this.getTabIndex()," ID='",this.getIFrameID(),"'></IFRAME>"];return _5.join(isc.emptyString)}
,isc.A.$y4=function isc_RichTextCanvas__setHandleTabIndex(_1){if(this.$322()){var _2=this.getContentFrame();if(_2!=null)_2.tabIndex=_1}else{return this.Super("$y4",arguments)}}
,isc.A.getBrowserSpellCheck=function isc_RichTextCanvas_getBrowserSpellCheck(){return true}
,isc.A.$323=function isc_RichTextCanvas__frameLoaded(){if(!this.$324)return;delete this.$324;if(!this.isDrawn())return;this.$325()}
,isc.A.getIFrameID=function isc_RichTextCanvas_getIFrameID(){return this.getID()+"$326"}
,isc.A.getContentDocument=function isc_RichTextCanvas_getContentDocument(){if(isc.Browser.isIE)return document;var _1=this.getContentWindow(),_2=_1?_1.document:null;if(_2==null){this.logDebug("Unable to get pointer to content document. Content may not be written out")}
return _2}
,isc.A.getContentBody=function isc_RichTextCanvas_getContentBody(){var _1=this.getContentDocument();if(_1)return _1.body;return null}
,isc.A.getContentWindow=function isc_RichTextCanvas_getContentWindow(){var _1=this.getContentFrame();return _1?_1.contentWindow:null}
,isc.A.getContentFrame=function isc_RichTextCanvas_getContentFrame(){if(!this.$322()||!this.isDrawn())return null;return isc.Element.get(this.getIFrameID())}
,isc.A.setOverflow=function isc_RichTextCanvas_setOverflow(){}
,isc.A.getScrollHandle=function isc_RichTextCanvas_getScrollHandle(){if(this.$322())return this.getContentBody();return this.Super("getScrollHandle",arguments)}
,isc.A.$yg=function isc_RichTextCanvas___adjustOverflow(){this.Super("$yg",arguments);if(!this.$322()||this.overflow!=isc.Canvas.AUTO)return;var _1=this.getScrollHeight(),_2=this.getScrollWidth(),_3=this.getHeight(),_4=this.getWidth(),_5=this.getScrollbarSize(),_6=false,_7=false;if(_1>_3)_7=true;if(_6)_4-=_5;if(_2>_4)_6=true;if(_6&&!_7&&(_1>_3-_5))_7=true;this.hscrollOn=_6;this.vscrollOn=_7}
,isc.A.getContentFrameWidth=function isc_RichTextCanvas_getContentFrameWidth(){return this.getWidth()-this.getHMarginBorderPad()}
,isc.A.getContentFrameHeight=function isc_RichTextCanvas_getContentFrameHeight(){return this.getHeight()-this.getHMarginBorderPad()}
,isc.A.$ue=function isc_RichTextCanvas__setHandleRect(_1,_2,_3,_4){this.Super("$ue",arguments);if(this.$322()){var _5=this.getContentFrame();if(_5!=null){var _6=this.getContentFrameWidth(),_7=this.getContentFrameHeight();_5.style.width=_6+"px";_5.style.height=_7+"px"}}}
,isc.A.getScrollWidth=function isc_RichTextCanvas_getScrollWidth(_1){if((this.$wc&&!_1)||!this.$322())
return this.Super("getScrollWidth",arguments);var _2=this.getContentBody();if(!_2)return this.Super("getScrollWidth",arguments);this.$wc=isc.Element.getScrollWidth(_2);return this.$wc}
,isc.A.getScrollHeight=function isc_RichTextCanvas_getScrollHeight(_1){if((this.$wg&&!_1)||!this.$322())
return this.Super("getScrollHeight",arguments);var _2=this.getContentBody();if(!_2)return this.Super("getScrollHeight",arguments);this.$wg=isc.Element.getScrollHeight(_2);return this.$wg}
,isc.A.$327=function isc_RichTextCanvas__rememberSelection(){if(!isc.Browser.isIE)return;if(!this.$328())return;this.$329=document.selection.createRange();this.$33a=this.$329.text}
,isc.A.$328=function isc_RichTextCanvas__hasSelection(){if(!this.isDrawn())return false
if(!isc.Browser.isIE)return;if(this.$322()){return(this.getActiveElement()==this.getContentFrame())}
var _1=this.getHandle();if(!_1)return false;var _2=isc.Element.$no();if(!_2)return false;return(_1==_2||_1.contains(_2))}
,isc.A.selectionChange=function isc_RichTextCanvas_selectionChange(){if(!this.$33b)this.$327()}
,isc.A.$33c=function isc_RichTextCanvas__resetSelection(){if(!this.editable||!this.isDrawn()||!this.isVisible())return;if(isc.Browser.isIE){if(!this.$329)return;if(this.$33a!=this.$329.text){this.$329.collapse(false)}
isc.EH.$nn=true;this.$329.select();delete isc.EH.$nn}}
,isc.A.setFocus=function isc_RichTextCanvas_setFocus(_1){this.$33b=true;this.Super("setFocus",arguments);this.$33b=false;if(this.$322()){var _2=this.getContentWindow();if(!_2)return;if(_1)_2.focus()
else window.focus()}else{if(_1){this.$33c()}}}
,isc.A.draw=function isc_RichTextCanvas_draw(){this.Super("draw",arguments);if(!isc.Browser.isSafari&&this.$322())
isc.EventHandler.registerMaskableItem(this,true);if(this.$322()){this.$324=true}else{this.$325()}}
,isc.A.redraw=function isc_RichTextCanvas_redraw(){var _1=this.$322();if(_1)this.$33d();this.Super("redraw",arguments);if(_1)this.$324=true}
,isc.A.$325=function isc_RichTextCanvas__setupEditArea(){var _1=this.$322();if(_1){if(!this.$33e){this.$33e=new Function("event","var returnValue="+this.getID()+".$33f(event);"+"if(returnValue==false && event.preventDefault)event.preventDefault()")}
if(!this.$33g){this.$33g=new Function("event","var returnValue="+this.getID()+".$33h(event);"+"if(returnValue==false && event.preventDefault)event.preventDefault()")}
if(!this.$33i){this.$33i=new Function("event","var returnValue="+this.getID()+".$33j(event);"+"if(returnValue==false && event.preventDefault)event.preventDefault()")}
if(!this.$33k){this.$33k=new Function("event","var returnValue="+this.getID()+".$33l(event);"+"if(returnValue==false && event.preventDefault)event.preventDefault()")}
if(!this.$748){this.$748=new Function("event",this.getID()+".$749();")}
if(!this.$75a){this.$75a=new Function("event",this.getID()+".$75b();")}
var _2=this.getContentWindow();_2.addEventListener("keypress",this.$33e,false);_2.addEventListener("keydown",this.$33g,false);_2.addEventListener("keyup",this.$33i,false);_2.addEventListener("scroll",this.$33k,false);_2.addEventListener("focus",this.$748,false);_2.addEventListener("blur",this.$75a,false);var _3=this.getContentBody().style;_3.margin="0px";var _4=isc.Element.getStyleDeclaration(this.className);if(_4!=null){var _5=isc.Canvas.textStyleAttributes;for(var i=0;i<_5.length;i++){var _7=_5[i];_3[_7]=_4[_7]}}}
if(isc.Browser.isMoz){this.getContentBody().spellcheck=(!!this.getBrowserSpellCheck())}
var _8=(this.editable&&!this.isDisabled());if(!_1)this.$33m(_8);else{this.delayCall("$33m",[_8,true],0)}
if(this.syntaxHiliter&&!this.formattedOnce){this.formattedOnce=true;this.contents=this.hiliteAndCount(this.contents)}
this.$33n(this.contents)}
,isc.A.$33f=function isc_RichTextCanvas__iFrameKeyPress(_1){isc.EH.getKeyEventProperties(_1);return isc.EH.handleKeyPress(_1,{keyTarget:this})}
,isc.A.$33h=function isc_RichTextCanvas__iFrameKeyDown(_1){isc.EH.getKeyEventProperties(_1);return isc.EH.handleKeyDown(_1,{keyTarget:this})}
,isc.A.$33j=function isc_RichTextCanvas__iFrameKeyUp(_1){isc.EH.getKeyEventProperties(_1);return isc.EH.handleKeyUp(_1,{keyTarget:this})}
,isc.A.$33l=function isc_RichTextCanvas__iFrameScroll(_1){return this.$mm(_1)}
,isc.A.$749=function isc_RichTextCanvas__iFrameOnFocus(){if(this.destroyed)return;isc.EH.focusInCanvas(this,true);return true}
,isc.A.$75b=function isc_RichTextCanvas__iFrameOnBlur(){if(this.destroyed)return;isc.EH.blurFocusCanvas(this,true);return true}
,isc.A.handleKeyPress=function isc_RichTextCanvas_handleKeyPress(_1,_2){var _3=isc.EH.getKey();if(this.ignoreKeys.contains(_3))return isc.EH.STOP_BUBBLING;if(this.countLines)this.rememberSelectionStartLine();this.$33o();var _4=this.Super("handleKeyPress",arguments);if(isc.Browser.isIE&&this.$33p){isc.Timer.clearTimeout(this.$33p);delete this.$33p}
if(_4!=false&&isc.Browser.isIE&&_3==this.$15u){this.$327();this.$329.pasteHTML(this.$16k);this.$329.collapse(true);this.$329.select();_4=false}
return _4}
,isc.A.$33o=function isc_RichTextCanvas__queueContentsChanged(){if(!this.$33q){this.$33q=true;if(!this.$33r)this.$33r="$33s";isc.Page.setEvent(isc.EH.IDLE,this,isc.Page.FIRE_ONCE,this.$33r)}}
,isc.A.$33s=function isc_RichTextCanvas__contentsChanged(){delete this.$33q;var _1=this.contents,_2=this.getContents();if(_1==_2)return;if(this.countLines&&this.selectionIsCollapsed())this.doLinesChanged(_1,_2);this.adjustOverflow("edited");if(this.changed!=null)this.changed(_1,_2);this.contents=_2}
,isc.A.setSyntaxHiliter=function isc_RichTextCanvas_setSyntaxHiliter(_1){if(_1==null){this.removeSyntaxHiliter();return}
this.syntaxHiliter=_1;this.countLines=true;var _2=this.getContents()||isc.emptyString;this.setContents(_2)}
,isc.A.removeSyntaxHiliter=function isc_RichTextCanvas_removeSyntaxHiliter(){var _1=this.getContents()||isc.emptyString;delete this.syntaxHiliter;delete this.countLines;this.setContents(_1)}
,isc.A.doLinesChanged=function isc_RichTextCanvas_doLinesChanged(_1,_2){var _3=this.getLastSelectionStartLine();if(_3==null)return;var _4=this.getLine(_3);var _5=isc.emptyString;var _6=this.markCurrentSelection();if(isc.Browser.isIE){if(!_4){this.getLineContainer().innerHTML=isc.emptyString;var _7=this.createLine();this.getLineContainer().appendChild(_7);var _8=document.selection.createRange();_8.moveToElementText(_7);_8.collapse();_8.select();_6=this.markCurrentSelection();_3=0;_4=this.getLine(0)}
_5=_4.innerHTML}else{var _9=this.getSelectionStartLine();var _10=this.getLineNumber(_9);if(_10<_3){_4=_9;_3=_10}
var _11=_4;var _12=0;while(_11&&_11!=_9){if(_11.innerHTML){_5+=_11.innerHTML}
_12++;_11=_11.nextSibling}
var _13=_9.nextSibling;if(_13&&_13.tagName.toLowerCase()=="br"){_13.parentNode.removeChild(_13);_9.appendChild(_13)}
_5+=_9.innerHTML;if(!_5.replace(/\n|\r/g,isc.emptyString).match(/<br>$/i)){if(_9.nextSibling){_5+=_9.nextSibling.innerHTML;_12++}}}
if(!_1){_1=this.contents;_2=this.getContents()}
if(this.linesChanged){this.linesChanged(_1,_2,_3,_12,_5,_6)}else if(this.syntaxHiliter){this.doSyntaxHilite(_1,_2,_3,_12,_5,_6)}}
,isc.A.doSyntaxHilite=function isc_RichTextCanvas_doSyntaxHilite(_1,_2,_3,_4,_5,_6){var _7=this.removeMarkup(_5,true);var _8=this.getSelectionMarkerIndex(_7);if(_8==-1){this.doFullSyntaxHilite();return}
_7=this.removeMarkup(_5);var _9=this.syntaxHiliter.hilite(_7,true,_8,this.$33t(_6));this.overwriteLines(_3,_4,_9);this.moveSelectionToMarker(_6)}
,isc.A.doFullSyntaxHilite=function isc_RichTextCanvas_doFullSyntaxHilite(){var _1=this.markCurrentSelection();var _2=this.$33u();var _3=this.removeMarkup(_2,true);var _4=this.getSelectionMarkerIndex(_3);if(_4==-1){_4=_2.length}
_3=this.removeMarkup(_2);this.setContents(_3,true,_4,this.$33t(_1));this.moveSelectionToMarker(_1);delete this.fullHiliteTimer}
,isc.A.queueFullHilite=function isc_RichTextCanvas_queueFullHilite(){if(this.fullHiliteTimer)isc.Timer.clearTimeout(this.fullHiliteTimer);this.fullHiliteTimer=this.delayCall("doFullSyntaxHilite",[],this.fullSyntaxHiliteDelay)}
,isc.A.selectionIsCollapsed=function isc_RichTextCanvas_selectionIsCollapsed(){if(isc.Browser.isIE){var _1=document.selection.createRange();return _1.text.length==0}else if(isc.Browser.isMoz){var _2=this.getContentWindow().getSelection();return _2.isCollapsed}}
,isc.A.rememberSelectionStartLine=function isc_RichTextCanvas_rememberSelectionStartLine(){this.startLineNum=this.getLineNumber(this.getSelectionStartLine())}
,isc.A.getLastSelectionStartLine=function isc_RichTextCanvas_getLastSelectionStartLine(){return this.startLineNum}
,isc.A.$33v=function isc_RichTextCanvas__setPasteTimer(){this.$33p=this.delayCall("doLinesChanged",[],0)}
,isc.A.$33w=function isc_RichTextCanvas__getOnBeforePaste(){if(!this.$33x)
this.$33x=this.getID()+".rememberSelectionStartLine();event.returnValue=true";return this.$33x}
,isc.A.$33y=function isc_RichTextCanvas__getOnPaste(){if(!this.$33z)this.$33z=this.getID()+".$33v();event.returnValue=true"
return this.$33z}
,isc.A.$330=function isc_RichTextCanvas__getLineSpanHTML(){if(!this.$331){this.$331="<span isLine='true'";if(this.syntaxHiliter&&!this.syntaxHiliter.autoWrap)
this.$331+=" style='white-space:nowrap'";if(isc.Browser.isIE){this.$331+=" onbeforepaste='"+this.$33w()+"' onpaste='"+this.$33y()+"'"}
this.$331+=">$1</span>"}
return this.$331}
,isc.A.createLine=function isc_RichTextCanvas_createLine(_1){var _2=this.getContentDocument();var _3=_2.createElement("span");_3.setAttribute("isLine","true");if(this.syntaxHiliter&&!this.syntaxHiliter.autoWrap)
_3.setAttribute("style","white-space:nowrap");if(isc.Browser.isIE){_3.setAttribute("onbeforepaste",this.$33w());_3.setAttribute("onpaste",this.$33y())}
_3.innerHTML=_1?_1:"<br>";return _3}
,isc.A.$332=function isc_RichTextCanvas__getNextSelectionId(){if(!this.selectionIdSequence)this.selectionIdSequence=0;return this.getID()+"_selection_"+this.selectionIdSequence++}
,isc.A.getSelectionStartLine=function isc_RichTextCanvas_getSelectionStartLine(){var _1=this.getContentDocument();var _2;if(isc.Browser.isIE){var _3=this.$332();var _4=_1.selection.createRange();_4.collapse();_4.pasteHTML("<span id='"+_3+"'></span>");var _5=_1.getElementById(_3);_2=_5.parentNode;_2.removeChild(_5)}else if(isc.Browser.isMoz){var _6=this.getContentWindow().getSelection();var _2=_6.anchorNode}
var _7=_2;while(_2.parentNode!=null){if(_2.getAttribute&&_2.getAttribute("isLine")!=null)_7=_2;_2=_2.parentNode}
return _7}
,isc.A.$33t=function isc_RichTextCanvas__getSelectionSpanHTML(_1){return"<span isSelectionSpan='true' id='"+_1+"'></span>"}
,isc.A.markCurrentSelection=function isc_RichTextCanvas_markCurrentSelection(){var _1=this.$332();var _2=this.getContentDocument();if(isc.Browser.isIE){var _3=_2.selection.createRange();_3.collapse();_3.pasteHTML(this.$33t(_1))}else if(isc.Browser.isMoz){var _4=_2.createElement("span");_4.setAttribute('isSelectionSpan',"true");_4.setAttribute('id',_1);var _5=this.getContentWindow().getSelection();var _3=_5.getRangeAt(0);if(_5.isCollapsed){_3.insertNode(_4)}else{var _6=_3.cloneRange();_6.collapse(false);_6.insertNode(_4);_6.detach()}}
return _1}
,isc.A.overwriteLines=function isc_RichTextCanvas_overwriteLines(_1,_2,_3){if(!isc.isAn.Array(_3))_3=[_3];var _4=this.getLine(_1);while(_1>=0&&(!_4||!_4.getAttribute||!_4.getAttribute("isLine"))){_4=this.getLine(_1);_1--}
if(_1<0){this.getLineContainer().innerHTML=isc.emptyString;_4=this.createLine();this.getLineContainer().appendChild(_4);if(isc.Browser.isMoz)_1++}
var _5=_4.parentNode;_4.innerHTML=_3[0];while(_2!=null&&_2-->0){var _6=this.getLine(_1+1);if(_6){_5.removeChild(_6)}}
for(var i=1;i<_3.length;i++){if(_3[i]!=-1)this.addLineAfter(_1+i-1,_3[i])}}
,isc.A.addLineAfter=function isc_RichTextCanvas_addLineAfter(_1,_2){var _3=this.getLine(_1);var _4=this.getNextLine(_3);_2=this.createLine(_2);if(_4){_4.parentNode.insertBefore(_2,_4)}else{_3.parentNode.appendChild(_2)}}
,isc.A.escapeSelection=function isc_RichTextCanvas_escapeSelection(_1,_2){if(_2==null)_2=isc.emptyString;return _1.replace(/<span [^>]*isSelectionSpan[^>]*><\/span>/gi,_2)}
,isc.A.getSelectionMarkerIndex=function isc_RichTextCanvas_getSelectionMarkerIndex(_1){var _2=new RegExp("<span [^>]*isSelectionSpan[^>]*>","i");var _3=_2.exec(_1);if(_3)return _3.index;return-1}
,isc.A.getLineNumber=function isc_RichTextCanvas_getLineNumber(_1){var _2=_1.parentNode.childNodes;for(var i=0;i<_2.length;i++)
if(_2[i]==_1)return i}
,isc.A.getPreviousLine=function isc_RichTextCanvas_getPreviousLine(_1){return _1.previousSibling}
,isc.A.getNextLine=function isc_RichTextCanvas_getNextLine(_1){return _1.nextSibling}
,isc.A.getLineContainer=function isc_RichTextCanvas_getLineContainer(){return isc.Browser.isIE?this.getHandle():this.getContentBody()}
,isc.A.getLine=function isc_RichTextCanvas_getLine(_1){return this.getLineContainer().childNodes[_1]}
,isc.A.getLineHTML=function isc_RichTextCanvas_getLineHTML(_1){return _1.innerHTML}
,isc.A.getLineContents=function isc_RichTextCanvas_getLineContents(_1){return this.removeMarkup(this.getLineHTML(_1))}
,isc.A.removeMarkup=function isc_RichTextCanvas_removeMarkup(_1,_2){if(_2){_1=_1.replace(/\n|\r|(<\/?(?!br|BR|([^>]*isSelectionSpan)).*?>)/gi,isc.emptyString)}else{_1=_1.replace(/\n|\r|(<\/?(?!br|BR).*?>)/gi,isc.emptyString)}
_1=_1.unescapeHTML();if(isc.Browser.isOpera){var _3=new RegExp(String.fromCharCode(160),"g");_1=_1.replace(_3," ")}
return _1}
,isc.A.moveSelectionToMarker=function isc_RichTextCanvas_moveSelectionToMarker(_1){var _2=this.getContentDocument();var _3=_2.getElementById(_1);if(isc.Browser.isIE){var _4=_2.selection.createRange();_4.moveToElementText(_3);_4.collapse();_4.select()}else if(isc.Browser.isMoz){var _5=this.getContentWindow().getSelection();_5.removeAllRanges();var _4=_2.createRange();_4.setStartBefore(_3);_4.setEndBefore(_3);_5.addRange(_4)}
this.destroySelectionMarker(_1)}
,isc.A.destroySelectionMarker=function isc_RichTextCanvas_destroySelectionMarker(_1){var _2=this.getContentDocument();var _3=_2.getElementById(_1);if(_3)_3.parentNode.removeChild(_3)}
,isc.A.setEditable=function isc_RichTextCanvas_setEditable(_1){if(_1==this.editable)return;this.editable=_1;this.$33m(_1)}
,isc.A.$33m=function isc_RichTextCanvas__setHandleEditable(_1,_2){if(this.$322()){var _3=this.getContentDocument();if(_3){if(_1||_2)_3.designMode="on";if(isc.Browser.isMoz)_3.execCommand("readonly",false,_1);if(!_1)_3.designMode="off"}}else{var _4=this.getHandle();if(_4!=null){_4.contentEditable=(_1?true:"inherit");if(isc.Browser.isIE){if(!this.isVisible()&&this.$328())
this.$333();else if(isc.Browser.version<6)
this.$327()}}}}
,isc.A.parentVisibilityChanged=function isc_RichTextCanvas_parentVisibilityChanged(_1){if(!this.$322()&&isc.Browser.isIE&&(_1==isc.Canvas.HIDDEN)&&this.$328())
{this.$333()}
return this.Super("parentVisibilityChanged",arguments)}
,isc.A.$333=function isc_RichTextCanvas__emptySelectionForHide(){document.body.focus();var _1=isc.EH.getFocusCanvas();if(_1!=this&&_1!=null){_1.focus()}}
,isc.A.disableKeyboardEvents=function isc_RichTextCanvas_disableKeyboardEvents(_1){this.Super("disableKeyboardEvents",arguments);if(this.editable)this.$33m(_1?false:true)}
,isc.A.$33d=function isc_RichTextCanvas__rememberContents(){if(!this.isDrawn()||this.$324)return;var _1=this.$33u();if(_1!=null)this.contents=_1}
,isc.A.$33u=function isc_RichTextCanvas__getContents(){var _1;if(this.$322()){var _2=this.getContentBody();if(!_2)return;_1=_2.innerHTML}else{var _3=this.getHandle();if(_3)_1=_3.innerHTML}
return _1}
,isc.A.getContents=function isc_RichTextCanvas_getContents(_1){this.$33d();if((this.syntaxHiliter||this.countLines)&&!_1){return this.removeMarkup(this.contents)}else{return this.contents}}
,isc.A.setContents=function isc_RichTextCanvas_setContents(_1,_2,_3,_4){if(_1==this.contents&&!_2)return;this.contents=_1;if(!this.isDrawn()||this.$324)return;this.$33n(this.hiliteAndCount(_1,_3,_4))}
,isc.A.$33n=function isc_RichTextCanvas__setContents(_1){this.contents=_1;if(!this.isDrawn())return;if(this.$322()){var _2=this.getContentBody();if(!_2)return;_2.innerHTML=_1}else{var _3=this.getHandle();if(_3)_3.innerHTML=_1}
this.adjustOverflow()}
,isc.A.hiliteAndCount=function isc_RichTextCanvas_hiliteAndCount(_1,_2,_3){if(this.syntaxHiliter){_1=this.syntaxHiliter.hilite(_1,false,_2,_3)}
if(this.countLines){if(_1==isc.emptyString)_1="<BR>";_1=_1.replace(/((?:.*?<br>)|(?:.+$))/gi,this.$330())}
return _1}
,isc.A.appendContents=function isc_RichTextCanvas_appendContents(_1,_2,_3){_1=this.hiliteAndCount(_1,_2,_3);var _4=this.$322()?this.getContentBody():this.getHandle();_4.innerHTML+=_1;this.adjustOverflow()}
,isc.A.$334=function isc_RichTextCanvas__execCommand(_1,_2){if(!this.isDrawn()||!this.editable)return;if(!isc.Page.isLoaded()){this.logWarn("Unsupported attempt to manipulate RichTextCanvas content style "+"before page load: postponed until the page has done loading.");isc.Page.setEvent("Load",this.getID()+".$334('"+_1+"','"+_2+"');");return}
this.focus();var _3=this.$322(),_4=_3?this.getContentDocument():document;if(!_4)return;if(!this.$335(_1))return false;try{_4.execCommand(_1,false,_2)}catch(e){return false}
if(_3){var _5=this.getContentWindow();_5.focus()}else{this.$327()}
this.$33s()}
,isc.A.$335=function isc_RichTextCanvas__commandEnabled(_1){try{var _2=this.$322()?this.getContentDocument():document;if(!_2)return false;if(!_2.queryCommandEnabled(_1))return false}catch(e){return false}
return true}
,isc.A.boldSelection=function isc_RichTextCanvas_boldSelection(){this.$334("bold")}
,isc.A.italicSelection=function isc_RichTextCanvas_italicSelection(){this.$334("italic")}
,isc.A.underlineSelection=function isc_RichTextCanvas_underlineSelection(){this.$334("underline")}
,isc.A.strikethroughSelection=function isc_RichTextCanvas_strikethroughSelection(){this.$334("strikethrough")}
,isc.A.showClipboardDisabledError=function isc_RichTextCanvas_showClipboardDisabledError(){var _1="Your browser does not allow web pages to access the clipboard programmatically.";isc.warn(_1)}
,isc.A.copySelection=function isc_RichTextCanvas_copySelection(){if(this.$334("copy")==false)this.showClipboardDisabledError()}
,isc.A.cutSelection=function isc_RichTextCanvas_cutSelection(){if(this.$334("cut")==false)this.showClipboardDisabledError();}
,isc.A.pasteOverSelection=function isc_RichTextCanvas_pasteOverSelection(){if(this.$334("paste")==false)this.showClipboardDisabledError()}
,isc.A.deleteSelection=function isc_RichTextCanvas_deleteSelection(){this.$334("delete")}
,isc.A.indentSelection=function isc_RichTextCanvas_indentSelection(){this.$334("indent")}
,isc.A.outdentSelection=function isc_RichTextCanvas_outdentSelection(){this.$334("outdent")}
,isc.A.justifySelection=function isc_RichTextCanvas_justifySelection(_1){if(_1==isc.RichTextCanvas.CENTER){this.$334("justifycenter")}else if(_1==isc.RichTextCanvas.FULL){this.$334("justifyfull")}else if(_1==isc.RichTextCanvas.RIGHT){this.$334("justifyright")}else if(_1==isc.RichTextCanvas.LEFT){this.$334("justifyleft")}}
,isc.A.setSelectionColor=function isc_RichTextCanvas_setSelectionColor(_1){this.$334("forecolor",_1)}
,isc.A.setSelectionBackgroundColor=function isc_RichTextCanvas_setSelectionBackgroundColor(_1){var _2=isc.Browser.isMoz?"hilitecolor":"backcolor";this.$334(_2,_1)}
,isc.A.setSelectionFont=function isc_RichTextCanvas_setSelectionFont(_1){this.$334("fontname",_1)}
,isc.A.setSelectionFontSize=function isc_RichTextCanvas_setSelectionFontSize(_1){this.$334("fontsize",_1)}
,isc.A.createLink=function isc_RichTextCanvas_createLink(_1){this.$334("CreateLink",_1)}
);isc.B._maxIndex=isc.C+101;isc.RichTextCanvas.registerStringMethods({changed:"oldValue,newValue"});isc.ClassFactory.defineClass("RichTextEditor","VLayout");isc.A=isc.RichTextEditor.getPrototype();isc.A.editAreaConstructor="RichTextCanvas";isc.A.editAreaBackgroundColor="white";isc.A.editAreaClassName="normal";isc.A.value="";isc.A.toolbarConstructor="HLayout";isc.A.toolbarHeight=24;isc.A.toolbarBackgroundColor="#CCCCCC";isc.A.toolbarSeparatorSrc="[SKIN]/RichTextEditor/separator.png";isc.A.controlButtonWidth=20;isc.A.defaultControlConstructor=isc.Button;isc.A.controlGroups=["fontControls","formatControls","styleControls","colorControls"];isc.A.styleControls=["boldSelection","italicSelection","underlineSelection"];isc.A.fontPrompt="Set Font ...";isc.A.fontSizePrompt="Set Font Size ...";isc.A.linkUrlTitle="Hyperlink URL:";isc.A.boldSelectionDefaults={title:"<b>B</b>",prompt:"Make selection bold"};isc.A.italicSelectionDefaults={title:"<i>I</i>",prompt:"Make selection italic"};isc.A.underlineSelectionDefaults={title:"<u>U</u>",prompt:"Make selection underlined"};isc.A.strikethroughSelectionDefaults={title:"<del>S</del>",prompt:"Strike through selection"};isc.A.fontControls=["fontSelector","fontSizeSelector"];isc.A.fontSelectorConstructor=isc.DynamicForm;isc.A.fontSizeSelectorConstructor=isc.DynamicForm;isc.A.fontNames={"arial,helvetica,sans-serif":"Arial",'courier new,courier,monospace':"Courier New",'georgia,times new roman,times,serif':"Georgia",'tahoma,arial,helvetica,sans-serif':"Tahoma",'times new roman,times,serif':"Times New Roman",'verdana,arial,helvetica,sans-serif':"Verdana","impact":"Impact"};isc.A.fontSizes={"1":"1 (8 pt)","2":"2 (10 pt)","3":"3 (12 pt)","4":"4 (14 pt)","5":"5 (18 pt)","6":"6 (24 pt)","7":"7 (36 pt)"};isc.A.editControls=["copySelection","cutSelection","pasteSelection"];isc.A.copySelectionDefaults={icon:"[SKIN]/RichTextEditor/copy.png",prompt:"Copy Selection"};isc.A.cutSelectionDefaults={icon:"[SKIN]/RichTextEditor/cut.png",prompt:"Cut Selection"};isc.A.pasteSelectionDefaults={icon:"[SKIN]/RichTextEditor/paste.png",prompt:"Paste"};isc.A.formatControls=["alignLeft","alignRight","alignCenter","justify"];isc.A.alignLeftDefaults={icon:"[SKIN]/RichTextEditor/text_align_left.png",prompt:"Left align selection",click:function(){this.creator.fireAction('justifySelection','left')}};isc.A.alignCenterDefaults={icon:"[SKIN]/RichTextEditor/text_align_center.png",prompt:"Center selection",click:function(){this.creator.fireAction('justifySelection','center')}};isc.A.alignRightDefaults={icon:"[SKIN]/RichTextEditor/text_align_right.png",prompt:"Right align selection",click:function(){this.creator.fireAction('justifySelection','right')}};isc.A.justifyDefaults={icon:"[SKIN]/RichTextEditor/text_align_justified.png",prompt:"Full justify selection",click:function(){this.creator.fireAction('justifySelection','full')}};isc.A.indentSelectionDefaults={icon:"[SKIN]/RichTextEditor/indent.png",prompt:"Indent selection"};isc.A.outdentSelectionDefaults={icon:"[SKIN]/RichTextEditor/outdent.png",prompt:"Decrease selection indent"};isc.A.colorControls=["color","backgroundColor"];isc.A.colorDefaults={icon:"[SKIN]/RichTextEditor/text_color.gif",prompt:"Set selection color",click:"this.creator.chooseTextColor()"};isc.A.backgroundColorDefaults={icon:"[SKIN]/RichTextEditor/background_color.gif",prompt:"Set selection background color",click:"this.creator.chooseBackgroundColor()"};isc.A.insertControls=["link"];isc.A.linkDefaults={icon:"[SKIN]/RichTextEditor/link_new.png",prompt:"Edit hyperlink",click:"this.creator.createLink()"};isc.A.canFocus=true;isc.A.$lq=false;isc.A._useNativeTabIndex=false;isc.A=isc.RichTextEditor;isc.B=isc._allFuncs;isc.C=isc.B._maxIndex;isc.D=isc._funcClasses;isc.D[isc.C]=isc.A.Class;isc.B.push(isc.A.$336=function isc_c_RichTextEditor__canvasContentsChanged(_1,_2){this.creator.$337(_1,_2)}
);isc.B._maxIndex=isc.C+1;isc.A=isc.RichTextEditor.getPrototype();isc.B=isc._allFuncs;isc.C=isc.B._maxIndex;isc.D=isc._funcClasses;isc.D[isc.C]=isc.A.Class;isc.A.dragStartDistance=1;isc.B.push(isc.A.initWidget=function isc_RichTextEditor_initWidget(){this.Super("initWidget",arguments);this.createChildren()}
,isc.A.doWarn=function isc_RichTextEditor_doWarn(){isc.logWarn("Warning: Not all Rich Text Editing features are supported in this browser.")}
,isc.A.createChildren=function isc_RichTextEditor_createChildren(){if(!this.richEditorSupported())this.delayCall("doWarn");if(!this.autoChildDefaults)this.autoChildDefaults={};this.autoChildDefaults.width=this.controlButtonWidth;this.autoChildDefaults.click=function(){if(this.isControl&&isc.isA.StatefulCanvas(this))this.creator.fireAction(this.controlName)}
if(this.toolbarHeight>0)this.$338();this.addAutoChild("editArea",{top:this.toolbarHeight,className:this.editAreaClassName,backgroundColor:this.editAreaBackgroundColor,left:0,width:"100%",height:"*",contents:this.value,tabIndex:-1,getTabIndex:function(){var _1=(this.parentElement)?this.parentElement.getTabIndex():-1;this.tabIndex=_1;return _1},$lh:function(_2,_3){if(this.parentElement!=null){return this.parentElement.$lh(_2,_3)}else{return this.Super("$lh",arguments)}},changed:isc.RichTextEditor.$336,focusChanged:function(){if(this.parentElement!=null)this.parentElement.editAreaFocusChanged()},getBrowserSpellCheck:function(){return this.parentElement.getBrowserSpellCheck()}})}
,isc.A.editAreaFocusChanged=function isc_RichTextEditor_editAreaFocusChanged(){}
,isc.A.richEditorSupported=function isc_RichTextEditor_richEditorSupported(){return!(isc.Browser.isSafari||isc.Browser.isOpera)}
,isc.A.getBrowserSpellCheck=function isc_RichTextEditor_getBrowserSpellCheck(){return this.browserSpellCheck}
,isc.A.$338=function isc_RichTextEditor__createToolbar(){this.addAutoChild("toolbar",{top:0,left:0,shouldPrint:false,width:"100%",height:this.toolbarHeight,overflow:isc.Canvas.VISIBLE,backgroundColor:this.toolbarBackgroundColor});for(var i=0;i<this.controlGroups.length;i++){if(i>0)
this.toolbar.addMember(this.$339());var _2=this[this.controlGroups[i]];if(!_2){this.logWarn("Unable to find countrol group '"+this.controlGroups[i]+"'. This group should be specified as an array of "+"control names, but is not present");continue}
for(var j=0;j<_2.length;j++){this.addAutoChild(_2[j],{canFocus:false,isControl:true,controlName:_2[j],layoutAlign:isc.Canvas.CENTER},this.defaultControlConstructor,this.toolbar)}}}
,isc.A.$339=function isc_RichTextEditor__createToolbarSeparator(){if(!this.$34a)this.$34a={autoDraw:false,width:12,height:"100%",src:this.toolbarSeparatorSrc};return isc.Img.create(this.$34a)}
,isc.A.setFocus=function isc_RichTextEditor_setFocus(_1){var _2=this.editArea;if(!_2)return;return _2.setFocus(_1)}
,isc.A.$y6=function isc_RichTextEditor__setTabIndex(_1,_2){this.Super("$y6",arguments);if(this.editArea)this.editArea.$y6(this.getTabIndex(),_2)}
,isc.A.$34b=function isc_RichTextEditor__makeFontMap(_1,_2){var _3={$34c:_1};return isc.addProperties(_3,_2)}
,isc.A.$34d=function isc_RichTextEditor__makeFontNamesMap(){return this.$34b(this.fontPrompt,this.fontNames)}
,isc.A.$34e=function isc_RichTextEditor__makeFontSizesMap(){return this.$34b(this.fontSizePrompt,this.fontSizes)}
,isc.A.fontSelector_autoMaker=function isc_RichTextEditor_fontSelector_autoMaker(_1){isc.addProperties(_1,{numCols:1,cellPadding:1,items:[{type:"select",name:"fontname",showTitle:false,tabIndex:-1,pickListProperties:{cellHeight:16,getCellValue:function(_4,_5,_6){var _2=this.Super("getCellValue",arguments),_3=_4?_4.fontname:null;if(_3&&_3!="$34c"){_2="<SPAN style='font-family:"+_3+";'>"+_2+"</SPAN>"}
return _2}},defaultValue:"$34c",valueMap:this.$34d(),pickValue:function(_4){this.Super("pickValue",arguments);if(_4!="$34c"){this.form.creator.fireAction('setSelectionFont',_4)}}}]});return this.createAutoChild("fontSelector",_1)}
,isc.A.fontSizeSelector_autoMaker=function isc_RichTextEditor_fontSizeSelector_autoMaker(_1){isc.addProperties(_1,{numCols:1,cellPadding:1,items:[{type:"select",name:"fontsize",showTitle:false,tabIndex:-1,defaultValue:"$34c",valueMap:this.$34e(),pickValue:function(_2){this.Super("pickValue",arguments);if(_2!="$34c"){this.form.creator.fireAction('setSelectionFontSize',_2)}}}]});return this.createAutoChild("fontSizeSelector",_1)}
,isc.A.fireAction=function isc_RichTextEditor_fireAction(_1,_2){var _3=this.editArea;if(!_3||!_1||!_3[_1]||!isc.isA.Function(_3[_1]))
return;this.editArea[_1](_2)}
,isc.A.chooseColor=function isc_RichTextEditor_chooseColor(_1){this.colorChooser=isc.ColorPicker.getSharedColorPicker({creator:this,ID:this.getID()+"$34f",showNullValue:false,colorSelected:function(_2){this.creator.$34g(_2)},cancel:function(){this.Super("cancel",arguments);this.creator.editArea.focus()}})
this.$34h=_1;this.colorChooser.show()}
,isc.A.$34g=function isc_RichTextEditor__colorSelected(_1){var _2=this.$34h?"setSelectionColor":"setSelectionBackgroundColor";delete this.$34h;this.fireAction(_2,_1)}
,isc.A.chooseTextColor=function isc_RichTextEditor_chooseTextColor(){this.chooseColor(true)}
,isc.A.chooseBackgroundColor=function isc_RichTextEditor_chooseBackgroundColor(){this.chooseColor(false)}
,isc.A.createLink=function isc_RichTextEditor_createLink(){var _1=this;isc.askForValue(this.linkUrlTitle,function(_2){if(_2==null)return;_1.fireAction("createLink",_2)},{defaultValue:"http://",width:320})}
,isc.A.$337=function isc_RichTextEditor__valueChanged(_1,_2){if(this.valueChanged)this.valueChanged(_1,_2)}
,isc.A.getValue=function isc_RichTextEditor_getValue(){if(this.editArea)this.value=this.editArea.getContents();return this.value}
,isc.A.setValue=function isc_RichTextEditor_setValue(_1){this.value=_1;if(this.editArea)this.editArea.setContents(this.value)}
);isc.B._maxIndex=isc.C+24;isc.RichTextEditor.registerStringMethods({valueChanged:"oldValue,newValue"});isc.ClassFactory.defineClass("RichTextItem",isc.CanvasItem);isc.A=isc.RichTextItem.getPrototype();isc.A.canFocus=true;isc.A.shouldSaveValue=true;isc.A.showTitle=false;isc.A.startRow=true;isc.A.endRow=true;isc.A.colSpan="*";isc.A.width=550;isc.A.canvasConstructor="RichTextEditor";isc.A=isc.RichTextItem.getPrototype();isc.B=isc._allFuncs;isc.C=isc.B._maxIndex;isc.D=isc._funcClasses;isc.D[isc.C]=isc.A.Class;isc.B.push(isc.A.$27k=function isc_RichTextItem__createCanvas(){var _1=this.getValue();_1=this.mapValueToDisplay(_1);var _2={ID:this.getID()+"$34i",width:this.width,height:this.height,getBrowserSpellCheck:function(){return this.canvasItem.getBrowserSpellCheck()},value:_1,valueChanged:"this.canvasItem.updateValue()",editAreaFocusChanged:function(){this.canvasItem.editAreaFocusChanged()}}
var _3=this.controlGroups;if(_3!=null){var _4="Properties",_5="_autoMaker",_6="Constructor";_2.controlGroups=_3;for(var i=0;i<_3.length;i++){if(this[_3[i]]){var _8=_3[i],_9=this[_8];_2[_8]=_9;for(var _10=0;_10<_9.length;_10++){var _11=_9[_10]+_4,_12=_9[_10]+_5,_13=_9[_10]+_6;if(this[_11])_2[_11]=this[_11];if(this[_12])_2[_12]=this[_12];if(this[_13])
_2[_13]=this[_13]}}}}
if(this.defaultControlConstructor!=null)
_2.defaultControlConstructor=this.defaultControlConstructor;this.canvas=_2;this.Super("$27k",arguments)}
,isc.A.editAreaFocusChanged=function isc_RichTextItem_editAreaFocusChanged(){if(this.canvas.editArea.hasFocus)this.elementFocus();else this.elementBlur()}
,isc.A.mapValueToDisplay=function isc_RichTextItem_mapValueToDisplay(_1){var _2=this.Super("mapValueToDisplay",_1);if(_2==null)return"";return _2}
,isc.A.setValue=function isc_RichTextItem_setValue(_1){this.Super("setValue",arguments);_1=this._value;this.canvas.setValue(this.mapValueToDisplay(_1));this.updateValue()}
,isc.A.getValue=function isc_RichTextItem_getValue(){if(this.canvas)this.updateValue();return this.Super("getValue",arguments)}
,isc.A.updateValue=function isc_RichTextItem_updateValue(){if(!this.canvas)return
var _1=this.canvas.getValue();return this.$17r(_1)}
);isc.B._maxIndex=isc.C+6;isc._moduleEnd=isc._RichTextEditor_end=(isc.timestamp?isc.timestamp():new Date().getTime());if(isc.Log&&isc.Log.logIsInfoEnabled('loadTime'))isc.Log.logInfo('RichTextEditor module init time: ' + (isc._moduleEnd-isc._moduleStart) + 'ms','loadTime');delete isc.definingFramework;}else{if(window.isc && isc.Log && isc.Log.logWarn)isc.Log.logWarn("Duplicate load of module 'RichTextEditor'.");}
/*
 * Isomorphic SmartClient
 * Version 8.1 (2011-08-02)
 * Copyright(c) 1998 and beyond Isomorphic Software, Inc. All rights reserved.
 * "SmartClient" is a trademark of Isomorphic Software, Inc.
 *
 * licensing@smartclient.com
 *
 * http://smartclient.com/license
 */

