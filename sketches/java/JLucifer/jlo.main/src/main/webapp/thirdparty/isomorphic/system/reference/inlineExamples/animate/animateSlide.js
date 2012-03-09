isc.Label.create({
    ID:"messageBox",
    left:35, top:50, width:150,
    styleName:"exampleTitle", align:"center", valign:"center", padding:10, showEdges:true,
    contents:"Vision is the<br>art of seeing the invisible.", backgroundColor:"#ffffd0",
    visibility:"hidden",
    animateTime:1200
})

isc.IButton.create({
    title:"Show",
    click:"messageBox.animateShow('slide')"
})

isc.IButton.create({
    title:"Hide", left:120,
    click:"messageBox.animateHide('slide')"
})
