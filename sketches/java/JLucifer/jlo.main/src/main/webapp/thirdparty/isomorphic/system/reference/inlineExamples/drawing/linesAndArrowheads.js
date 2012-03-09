isc.DrawPane.create({
    showEdges: true,
    autoDraw:  true,
    ID:        "mainPane",
    width:     500,
    height:    240,
    top:       100,
    overflow:  "hidden",
    cursor:    "auto"
});

isc.DynamicForm.create({
    ID:     "dataForm",
    width:  300,
    fields: [
        { 
            editorType:   "spinner", 
            name:         "lineWidth", 
            title:        "Line Width", 
            defaultValue: 3,
            min:          1, 
            max:          10, 
            step:         1, 
            width:        150
        }, 
        {
            type:         "select", 
            name:         "lineStyle", 
            title:        "Line Style", 
            width:        150,
            defaultValue: "solid",
            valueMap: {
                "solid":     "Solid",
                "dot":       "Dot",
                "dash":      "Dash",
                "shortdot":  "Short dot",
                "shortdash": "Short dash",
                "longdash":  "Long dash"
            }
        }, 
        {
            name:         "arrowheadStyle", 
            title:        "Arrowhead Style", 
            type:         "select", 
            width:        150,
            defaultValue: "round",
            valueMap: {
                "round":  "Round",
                "square": "Square",
                "butt":   "Butt"
            }
        }
    ],
    itemChanged : drawLines
});

function drawLines(){
    mainPane.erase();
    
    var lineWidth = dataForm.values["lineWidth"];
    var lineStyle = dataForm.values["lineStyle"];
    var arrowheadStyle = dataForm.values["arrowheadStyle"];
        
    isc.DrawLine.create({
        autoDraw:    true,
        drawPane:    mainPane,
        lineWidth:   lineWidth,
        linePattern: lineStyle, 
        lineCap:     arrowheadStyle,
        startPoint:  [20, 30],
        endPoint:    [180, 190]        
    });
    
    isc.DrawCurve.create({
        autoDraw:      true,
        drawPane:      mainPane,
        lineWidth:     lineWidth,
        linePattern:   lineStyle,
        lineCap:       arrowheadStyle,
        startPoint:    [250, 35],
        endPoint:      [250, 185],
        controlPoint1: [150, 85],
        controlPoint2: [350, 135]
    });
    
    isc.DrawLinePath.create({
        autoDraw:    true,
        drawPane:    mainPane,
        lineWidth:   lineWidth,
        linePattern: lineStyle,
        lineCap:     arrowheadStyle,
        startPoint:  [320, 40],
        endPoint:    [460, 190]
    });
}

drawLines();