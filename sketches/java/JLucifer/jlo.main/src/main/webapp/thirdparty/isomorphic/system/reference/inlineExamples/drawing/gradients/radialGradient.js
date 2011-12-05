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
    width:  270,
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
        {name: "r", type: "slider", defaultValue: 100, min: 0, max: 100, step: 1, height: 20}
    ],
    itemChanged : drawShapes
});

function drawShapes(){
    mainPane.erase();
    
    var radialGradient = mainPane.createRadialGradient( "rg", { 
        cx: 0,
        cy: 0,
        r:  slidersForm.values.r + '%',
        fx: 0,
        fy: 0,
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
        fillGradient: radialGradient,
        points:       [[100,50],[150,150],[50,150]]
    });
    
    isc.DrawCurve.create({
        autoDraw:      true,
        drawPane:      mainPane,
        fillGradient:  radialGradient,
        startPoint:    [200, 50],
        endPoint:      [340, 150],
        controlPoint1: [270, 0],
        controlPoint2: [270, 200]
    });
    
    isc.DrawOval.create({
        autoDraw:     true,
        drawPane:     mainPane,
        fillGradient: radialGradient,
        left:         50,
        top:          200,
        width:        100,
        height:       150
    });
    
    isc.DrawRect.create({
        autoDraw:     true,
        drawPane:     mainPane,
        fillGradient: radialGradient,
        left:         200,
        top:          225,
        width:        150,
        height:       100
    });
}

drawShapes();