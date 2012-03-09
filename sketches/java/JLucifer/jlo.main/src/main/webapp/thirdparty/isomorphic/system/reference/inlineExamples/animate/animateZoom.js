isc.Img.create({
    ID:"magnifier",
    src:"other/magnifier.png",
    left:100, top:125, width:48, height:48,
    animateTime:500
})

isc.IButton.create({
    title:"Zoom",
    click:"magnifier.animateRect(0,50,248,248)"
})

isc.IButton.create({
    title:"Shrink", left:120,
    click:"magnifier.animateRect(100,125,48,48)"
})
