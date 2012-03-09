isc.Img.create({
    ID:"eyesImg",
    width:360, height:188,
    src:"other/eyes.jpg",
    showEdges:true,
    animateTime:1200
})

isc.IButton.create({
    title:"Fade out", left:60, top:200,
    click:"eyesImg.animateFade(0)"
})

isc.IButton.create({
    title:"Fade in", left:200, top:200,
    click:"eyesImg.animateFade(100)"
})
