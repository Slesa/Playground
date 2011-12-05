isc.IButton.create({
    ID: "exportButton",
    title: "Do Custom Export",
    autoFit: true,
    click : function () {
       supplyItemExport.exportData(null, { operationId: "customExport" });
    }
});

