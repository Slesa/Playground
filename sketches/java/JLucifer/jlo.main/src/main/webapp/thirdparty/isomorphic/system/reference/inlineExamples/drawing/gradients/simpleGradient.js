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
        {name: "startColor", title: "Start Color", type: "color",   defaultValue: "#0000ff"},
        {name: "endColor",   title: "End Color",   type: "color",   defaultValue: "#00ff00"},
        {name: "direction",  title: "Direction",   type: "spinner", defaultValue: 45,
            min: 0, max: 360, step: 1}
    ],
    itemChanged : drawShapes
});

function drawShapes(){
    mainPane.erase();
    
    var simpleGradient = {
        direction:  dataForm.values.direction,
        startColor: dataForm.values.startColor,
        endColor:   dataForm.values.endColor
    };
    
    isc.DrawTriangle.create({
        autoDraw:     true,
        drawPane:     mainPane,
        fillGradient: simpleGradient,
        points:       [[100,50],[150,150],[50,150]]
    });
    
    isc.DrawCurve.create({
        autoDraw:      true,
        drawPane:      mainPane,
        fillGradient:  simpleGradient,
        startPoint:    [200, 50],
        endPoint:      [340, 150],
        controlPoint1: [270, 0],
        controlPoint2: [270, 200]
    });
    
    isc.DrawOval.create({
        autoDraw:     true,
        drawPane:     mainPane,
        fillGradient: simpleGradient,
        left:         50,
        top:          200,
        width:        100,
        height:       150
    });
    
    isc.DrawRect.create({
        autoDraw:     true,
        drawPane:     mainPane,
        fillGradient: simpleGradient,
        left:         200,
        top:          225,
        width:        150,
        height:       100
    });
}

drawShapes();