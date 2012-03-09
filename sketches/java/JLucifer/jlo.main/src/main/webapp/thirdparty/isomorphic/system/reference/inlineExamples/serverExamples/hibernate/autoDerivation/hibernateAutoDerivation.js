var grid = isc.ListGrid.create({
    ID: "supplyItemList",
    width:700,
    height:224,
    alternateRecordStyles:true,
    dataSource: supplyItemHBAutoDerive,
    autoFetchData: true,
    showFilterEditor: true,
    canEdit: true,
    editEvent: "click",
    canRemoveRecords: true
});

var button = isc.Button.create({
    ID: "newButton",
    top: 230,
    title: "Add New",
    click: "supplyItemList.startEditingNew();"
});

