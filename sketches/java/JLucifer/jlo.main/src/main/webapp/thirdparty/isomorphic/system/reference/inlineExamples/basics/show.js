isc.Label.create({
    ID:"textbox", align:"center", showEdges:true, padding:5,
    left:50, top:50, width:200,
    contents:"The future has a way of arriving unannounced.",
    visibility:"hidden"
})

isc.IButton.create({
    title:"Show", left:40,
    click:"textbox.show();"
})

isc.IButton.create({
    title:"Hide", left:160,
    click:"textbox.hide();"
})
