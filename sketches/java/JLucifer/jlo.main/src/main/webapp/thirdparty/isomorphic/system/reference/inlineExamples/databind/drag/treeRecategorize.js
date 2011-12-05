isc.TreeGrid.create({
    autoDraw: false,
    ID: "categoryTree",
    width: "30%",
    dataSource: "supplyCategory",
    canAcceptDroppedRecords:true,
    showResizeBar: true,
    autoFetchData: true
});

isc.ListGrid.create({
    autoDraw: false,
    ID: "itemList",
    width: "70%",
    dataSource: "supplyItem",
    canDragRecordsOut:true,
    autoFetchData: true,
    fields: [
        {name: "itemName"},
        {name: "SKU"},
        {name: "category"}
    ]
});

isc.HLayout.create({
    height: 300,
    width: 800,
    members: [
        categoryTree, itemList
    ]
})
