
var ajaxDefinition = 
    "<span style='font-size:22px;'>Ajax</span> " +
    "<b>A</b>synchronous <b>J</b>avaScript <b>A</b>nd <b>X</b>ML (AJAX) is a " +
    "Web development technique for creating interactive <b>web applications</b>. " +
    "The intent is to make web pages feel more responsive by exchanging small " +
    "amounts of data with the server behind the scenes, so that the entire Web " +
    "page does not have to be reloaded each time the user makes a change. " +
    "This is meant to increase the Web page's <b>interactivity</b>, <b>speed</b>, " +
    "and <b>usability</b>.<br>" +
    "(Source: <a href='http://www.wikipedia.org' title='Wikipedia' target='_blank'>Wikipedia</a>)";

isc.VLayout.create({
    width:400,
    membersMargin:5,
    members:[
        isc.RichTextEditor.create({
            autoDraw:false,
            ID:"contentEditor",
            height:155,
            
            overflow:"hidden",
            canDragResize:true, showEdges:true,

             // Standard control group options include
             // "fontControls", "formatControls", "styleControls" and "colorControls"
             controlGroups:["fontControls", "styleControls"],
             value:ajaxDefinition
        }),
        
        isc.IButton.create({
            autoDraw:false,
            title:"Set Canvas HTML", width:150, 
            click:"htmlCanvas.setContents(contentEditor.getValue())"
        }),
        
        isc.LayoutSpacer.create({height:10}),
        
        isc.HTMLFlow.create({
            ID:"htmlCanvas",
            height:130, padding:2, overflow:"auto",
            canDragResize:true, showEdges:true,
            contents:"Click <b>Set Canvas HTML</b> to display edited content."
        })
    ]
})

