isc.ListGrid.create({
    ID: "categoryList",
    width:500, height:300, 
    alternateRecordStyles:true,
    dataSource: supplyCategory,
    autoFetchData: true,
    canExpandRecords: true,
    expansionMode: "related",
    detailDS:"supplyItem"
});

