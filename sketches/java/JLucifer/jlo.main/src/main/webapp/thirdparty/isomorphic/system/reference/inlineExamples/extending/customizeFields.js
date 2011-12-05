isc.DataSource.create({
    ID:"dsField",
    fields : [
        { name:"name" },
        { name:"title" },
        { name:"type", valueMap:["text","boolean","date","int","decimal","link"]},
        { name:"required", title:"req", type:"boolean", width:40},
        { name:"hidden", title:"hide", type:"boolean", width:40}
    ]
});

isc.ListGrid.create({
    ID:"customFieldsGrid",
    dataSource:"dsField",
    canEdit:true, listEndEditAction: "next", editEvent:"click", modalEditing:true,
    saveLocally:true, cellChanged : "bindButton.rebind()",
    canReorderRecords:true,
    data : [
        { name:"nextShipment", required:true },
        { name:"customField", title:"Order Quantity", type:"int" }
    ],
    extraSpace:5
});
customFieldsGrid.delayCall("startEditing");

isc.DynamicForm.create({ ID:"sampleView", useAllDataSourceFields:true, titleWidth:150 });

isc.IButton.create({
    ID:"bindButton",
    title:"Try it",
    click : "this.rebind()",
    rebind : function () {
        sampleView.setDataSource("supplyItem", customFieldsGrid.getData());
    }
});

bindButton.rebind();

isc.SectionStack.create({
    width:"100%", height:"100%", visibilityMode:"multiple",
    sections : [
        { title:"Field Editing", items:[customFieldsGrid, bindButton], expanded:true },
        { title:"Sample Binding", items:[sampleView], expanded:true }
    ]
});

