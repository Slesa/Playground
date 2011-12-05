isc.DrawPane.create({
    showEdges: true,
    autoDraw:  true,
    ID:        "mainPane",
    width:     400,
    height:    400,
    top:       70,
    overflow:  "hidden",
    cursor:    "auto"
});

isc.DrawTriangle.create({
    autoDraw: true,
    drawPane: mainPane,
    points:   [[75,50],[100,100],[50,100]]
});

isc.DrawCurve.create({
    autoDraw:      true,
    drawPane:      mainPane,
    startPoint:    [125, 50],
    endPoint:      [175, 100],
    controlPoint1: [150, 25],
    controlPoint2: [150, 125]
});

isc.DrawLinePath.create({
    autoDraw:   true,
    drawPane:   mainPane,
    startPoint: [200, 50],
    endPoint:   [250, 100]
});

isc.DrawPath.create({
    autoDraw: true,
    drawPane: mainPane,
    points:   [[275, 50], [287,50], [300,62], [312,62], 
              [325,62],   [325,87], [312,87], [300,87], 
              [287,100],  [275,100]]
});

isc.DrawOval.create({
    autoDraw: true,
    drawPane: mainPane,
    left:     50,
    top:      150,
    width:    50,
    height:   75
});

isc.DrawRect.create({
    autoDraw: true,
    drawPane: mainPane,
    left:     150,
    top:      175,
    width:    75,
    height:   50
});

isc.DrawLine.create({
    autoDraw:   true,
    drawPane:   mainPane,        
    startPoint: [275, 175],
    endPoint:   [325,225]        
});

isc.Slider.create({
    ID:             "zoomSlider",
    minValue:       0.10,
    maxValue:       3.00,
    numValues:      300,
    roundValues:    false,
    roundPrecision: 2,
    width:          400,
    title:          "Zoom Shapes",
    vertical:       false,
    valueChanged : function (value) {
        mainPane.zoom(value);
    }
});