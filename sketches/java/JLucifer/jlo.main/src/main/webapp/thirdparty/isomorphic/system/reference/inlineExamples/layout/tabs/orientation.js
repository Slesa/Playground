isc.TabSet.create({
    ID: "topTabSet",
    tabBarPosition: "top",
    width: 400,
    height: 200,
    tabs: [
        {title: "Blue", icon: "pieces/16/pawn_blue.png", iconSize:16,
         pane: isc.Img.create({autoDraw: false, width: 48, height: 48, src: "pieces/48/pawn_blue.png"})},
        {title: "Green", icon: "pieces/16/pawn_green.png", iconSize:16,
         pane: isc.Img.create({autoDraw: false, width: 48, height: 48, src: "pieces/48/pawn_green.png"})}
    ]
});

isc.TabSet.create({
    ID:"leftTabSet",
    tabBarPosition: "left",
    width: 400,
    height: 200,
    top: 250,
    tabs: [
        {icon: "pieces/16/pawn_blue.png", iconSize:16,
         pane: isc.Img.create({autoDraw: false, width: 48, height: 48, src: "pieces/48/pawn_blue.png"})},
        {icon: "pieces/16/pawn_green.png", iconSize:16,
         pane: isc.Img.create({autoDraw: false, width: 48, height: 48, src: "pieces/48/pawn_green.png"})}
    ]
});

isc.IButton.create({
    title: "Select Blue",
    top: 215,
    click: function () {
        topTabSet.selectTab(0);
        leftTabSet.selectTab(0);
    }
});

isc.IButton.create({
    title: "Select Green",
    top: 215,
    left: 110,
    click: function () {
        topTabSet.selectTab(1);
        leftTabSet.selectTab(1);
    }
});
