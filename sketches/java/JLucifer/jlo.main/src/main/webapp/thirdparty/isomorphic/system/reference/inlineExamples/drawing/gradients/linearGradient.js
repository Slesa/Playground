isc.DrawPane.create({
    showEdges: true,
    autoDraw:  true,
    ID:        "mainPane",
    width:     '400',
    height:    '400',
    overflow:  "hidden",
    cursor:    "auto"
});

isc.DynamicForm.create({
    ID:     "dataForm",
    width:  250,
    left:   420,
    fields: [
        {name: "startColor", title: "Start Color",       type: "color",  defaultValue: "#ff0000"},
        {name: "stop1Color", title: "First Stop Color",  type: "color",  defaultValue: "#ffff00"},
        {name: "stop2Color", title: "Second Stop Color", type: "color",  defaultValue: "#00ff00"},
        {name: "endColor",   title: "End Color",         type: "color",  defaultValue: "#0000ff"},
    ],
    itemChanged : drawShapes
});

isc.DynamicForm.create({
    ID:         "slidersForm",
    width:      290,
    left:       420,
    top:        120,
    titleWidth: 20,
    fields: [
        {name: "x1", type: "slider", defaultValue: 20, min: 0, max: 100, step: 1, height: 20},
        {name: "y1", type: "slider", defaultValue: 20, min: 0, max: 100, step: 1, height: 20},
        {name: "x2", type: "slider", defaultValue: 80, min: 0, max: 100, step: 1, height: 20},
        {name: "y2", type: "slider", defaultValue: 80, min: 0, max: 100, step: 1, height: 20}
    ],
    itemChanged : drawShapes
});

function drawShapes(){
    mainPane.erase();
    
    var linearGradient = mainPane.createLinearGradient( "lg", { 
        x1: slidersForm.values.x1 + '%', 
        y1: slidersForm.values.y1 + '%', 
        x2: slidersForm.values.x2 + '%', 
        y2: slidersForm.values.y2 + '%' ,
        colorStops: [
            {color: dataForm.values.startColor, offset: 0.00},
            {color: dataForm.values.stop1Color, offset: 0.33},
            {color: dataForm.values.stop2Color, offset: 0.66},
            {color: dataForm.values.endColor,   offset: 1.00}
        ] 
    });
    
    isc.DrawTriangle.create({
        autoDraw:     true,
        drawPane:     mainPane,
        fillGradient: linearGradient,
        points:       [[100,50],[150,150],[50,150]]
    });
    
    isc.DrawCurve.create({
        autoDraw:      true,
        drawPane:      mainPane,
        fillGradient:  linearGradient,
        startPoint:    [200, 50],
        endPoint:      [340, 150],
        controlPoint1: [270, 0],
        controlPoint2: [270, 200]
    });
    
    isc.DrawOval.create({
        autoDraw:     true,
        drawPane:     mainPane,
        fillGradient: linearGradient,
        left:         50,
        top:          200,
        width:        100,
        height:       150
    });
    
    isc.DrawRect.create({
        autoDraw:     true,
        drawPane:     mainPane,
        fillGradient: linearGradient,
        left:         200,
        top:          225,
        width:        150,
        height:       100
    });
}

drawShapes();