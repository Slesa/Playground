isc.DynamicForm.create({
    width: 500,
    numCols: 4,
    fields : [{
        name: "bugStatus", title: "Bug Status", 
        editorType: "comboBox",
        valueMap : {
            "new" : "New",
            "active" : "Active",
            "revisit" : "Revisit",
            "fixed" : "Fixed",
            "delivered" : "Delivered",
            "resolved" : "Resolved",
            "reopened" : "Reopened"
        }
    },{
        name: "itemName", title: "Item Name", editorType: "comboBox", 
        optionDataSource: "supplyItem", pickListWidth: 250
    }]
});