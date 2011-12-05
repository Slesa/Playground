
var firstTime=true;

isc.DynamicForm.create({
    ID: "uploadForm",
    dataSource: mediaLibraryHB,
    fields: [
        { name: "title" },
        { name: "image", type: "imageFile", hint: "Maximum file-size is 50k" },
        { title: "Save", type: "button", 
            click: function () {
                this.form.saveData("if(dsResponse.status>=0) uploadForm.editNewRecord()");
            }
        }
    ]
});

isc.DynamicForm.create({
    ID: "searchForm",
    width: "100%",
    numCols: 3,
    colWidths: [60, 200, "*"],
    saveOnEnter:true, 
    fields: [
        { name: "title", title: "Title", type: "text", width: "*" },
        { name: "search", title: "Search", type: "SubmitItem",
            startRow: false, endRow: false
        }
    ],
    submit : function () {
        mediaTileGrid.fetchData(this.getValuesAsCriteria(), null, {textMatchStyle:"substring"});
    } 
});

isc.IButton.create({
    ID: "viewAsTiles",
    title: "View as Tiles",
    autoFit: true,
    icon: "[ISO_DOCS_SKIN]/images/silkicons/application_view_tile.png",
    value: true,
    radioGroup: "views",
    actionType: "checkbox",
    click: function(){
        showTileGrid();
    }
});

isc.IButton.create({
    ID:"viewAsList",
    title: "View as List",
    autoFit: true,
    icon: "[ISO_DOCS_SKIN]/images/silkicons/application_view_detail.png",
    radioGroup: "views",
    actionType: "checkbox",
    click: function(){
        showListGrid();
    }
});

isc.HLayout.create({
    ID: "buttons",
    width: 500,
    membersMargin: 5,
    padding: 5,
    members: [viewAsTiles, viewAsList]
});

isc.TileGrid.create({
        ID: "mediaTileGrid",
        width: "100%",
        height: 224,
        tileWidth: 100,
        tileHeight: 150,
        dataSource: mediaLibraryHB,
        autoFetchData: true
});

isc.ListGrid.create({
        ID: "mediaListGrid",
        width: "100%",
        height: 224,
        alternateRecordStyles: true,
        dataSource: mediaLibraryHB
});

isc.VLayout.create({
    ID:"mainLayout",
    left: 350,
    width:500,
    height:250,
    members:[searchForm, buttons, mediaTileGrid, mediaListGrid]
});

viewAsTiles.click();

function showTileGrid() {
    mediaListGrid.hide();
    mediaTileGrid.show();
}

function showListGrid() {
    if (firstTime) {
        firstTime = false;
        mediaListGrid.setData(mediaTileGrid.getData());
    }
    mediaTileGrid.hide();
    mediaListGrid.show();
}
