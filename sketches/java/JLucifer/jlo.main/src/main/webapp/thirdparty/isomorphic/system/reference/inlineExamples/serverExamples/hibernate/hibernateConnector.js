
isc.ListGrid.create({
    ID: "worldList",
    width:700,
    height:224,
    alternateRecordStyles:true, 
    dataSource: worldHB,
    autoFetchData: true,
    showFilterEditor: true,
    canEdit: true,
    editEvent: "click",
    canRemoveRecords: true
});

isc.Button.create({
    ID: "newButton",
    top: 230,
    title: "Add New",
    click: "worldList.startEditingNew();"
});
