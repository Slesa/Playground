isc.IButton.create({
    ID: "stretchButton",
    width: 150,
    showRollOver: true,
    showDisabled: true,
    showDown: true,
    title: "Stretch Button",
    titleStyle: "stretchTitle",
    icon: "icons/16/find.png"
});

isc.Button.create({
    ID: "cssButton",
    baseStyle: "cssButton",
    left: 200,
    showRollOver: true,
    showDisabled: true,
    showDown: true,
    title: "CSS Button",
    icon: "icons/16/icon_add_files.png"
});

isc.ImgButton.create({
    ID: "imgButton",
    left: 400,
    width:18,				
	height:18,
    showRollOver: true,
    showDisabled: true,
    showDown: true
});

isc.Button.create({
    title: "Disable All",
    width: 120,
    left: 190,
    top: 45,
    click : function () {
        if (cssButton.isDisabled()) {
            cssButton.enable();
            stretchButton.enable();
            imgButton.enable();
            this.setTitle("Disable All");
        } else {
            cssButton.disable();
            stretchButton.disable();
            imgButton.disable();
            this.setTitle("Enable All");
        }
    }
});
    