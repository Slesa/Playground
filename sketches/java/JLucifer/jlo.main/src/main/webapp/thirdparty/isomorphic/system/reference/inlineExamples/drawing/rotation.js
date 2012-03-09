isc.DrawPane.create({
    showEdges: true,
    autoDraw:  true,
    ID:        "mainPane",
    width:     400,
    height:    300,
    top:       120,
    overflow:  "hidden",
    cursor:    "auto"
});

isc.DrawTriangle.create({
    ID:       "drawTriangle",
    autoDraw: true,
    drawPane: mainPane,
    points:   [[75,50],[100,100],[50,100]]
});

isc.DrawCurve.create({
    ID:            "drawCurve",
    autoDraw:      true,
    drawPane:      mainPane,
    startPoint:    [125, 50],
    endPoint:      [175, 100],
    controlPoint1: [150, 25],
    controlPoint2: [150, 125]
});

isc.DrawLinePath.create({
    ID:         "drawLinePath",
    autoDraw:   true,
    drawPane:   mainPane,
    startPoint: [200, 50],
    endPoint:   [250, 100]
});

isc.DrawPath.create({
    ID:       "drawPath",
    autoDraw: true,
    drawPane: mainPane,
    points:   [[275, 50], [287,50], [300,62], [312,62], 
              [325,62],   [325,87], [312,87], [300,87], 
              [287,100],  [275,100]]
});

isc.DrawOval.create({
    ID:       "drawOval",
    autoDraw: true,
    drawPane: mainPane,
    left:     50,
    top:      150,
    width:    50,
    height:   75
});

isc.DrawRect.create({
    ID:       "drawRect",
    autoDraw: true,
    drawPane: mainPane,
    left:     150,
    top:      175,
    width:    75,
    height:   50
});

isc.DrawLine.create({
    ID:         "drawLine",
    autoDraw:   true,
    drawPane:   mainPane,        
    startPoint: [275, 175],
    endPoint:   [325,225]        
});

isc.Slider.create({
    ID:            "shapesRotation",
    minValue:      0,
    maxValue:      360,
    numValues:     360,
    width:         400,
    left:          0,
    value:         0,
    title:         "Rotate Shapes",
    vertical:      false,
    valueChanged : function (value) {
        drawTriangle.rotateTo(value);
        drawCurve.rotateTo(value);
        drawLinePath.rotateTo(value);
        drawPath.rotateTo(value);
        drawOval.rotateTo(value);
        drawRect.rotateTo(value);
        drawLine.rotateTo(value);
    }
});

isc.Slider.create({
    ID:            "paneRotation",
    minValue:      0,
    maxValue:      360,
    numValues:     360,
    width:         400,
    left:          0,
    top:           50,
    value:         0,
    title:         "Rotate Pane",
    vertical:      false,
    valueChanged : function (value) {
        mainPane.rotate(value);
    }
});