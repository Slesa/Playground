isc.Window.create({
    ID: "theWindow",
    title: "Window with footer",
    width: 200, height: 200,
    canDragResize:true,
    showFooter: true,
    items: [
        isc.Label.create({
            contents: "Click me",
            align: "center",
            padding: 5,
            height: "100%",
            click : function () {
                theWindow.setStatus("Click at: "+isc.Event.getX()+", "+isc.Event.getY());
            }
        })
    ]

});
