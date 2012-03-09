isc.TileGrid.create({
    ID:"boundList",
    tileWidth:150,
    tileHeight:205,
    width: "100%",
    height:"100%",
    dataSource:"animals",
    autoFetchData:true,
    animateTileChange:true,
    fields: [
        {name:"picture"},
        {name:"commonName", cellStyle: "commonName"},
        {name:"lifeSpan", formatCellValue: "return 'Lifespan: ' + value;"},
        {   name:"status", 
            getCellStyle: function (value, field, record, viewer) {
                if (value == "Endangered") return "endangered";
                else if (value == "Threatened") return "threatened";
                else if (value == "Not Endangered") return "notEndangered";
                else return viewer.cellStyle;
            }
        }
    ],

    getTile : function (record) {
        // override getTile() and add a "Remove" button 
        var canvas = this.Super("getTile", arguments);
        canvas.addChild(this.getRemoveButton(this.getRecord(record)));
        return canvas;
    },
    
    getRemoveButton : function (record) {
        var removeButton = isc.ImgButton.create({
            src: "[SKINIMG]/Tab/left/close.png",
            showHover: true,
            prompt: "Remove tile",
            size: 15,
            showFocused: false,
            showRollOver: false,
            snapTo: "TR",
            showDown: false,
            margin: 2,
            tileGrid: this,
            record: record,
            click : function () {
                animals.removeData(this.record);
            }
        });

        return removeButton;
    }
});

