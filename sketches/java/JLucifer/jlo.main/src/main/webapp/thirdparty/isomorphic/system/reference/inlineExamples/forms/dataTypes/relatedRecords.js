isc.DynamicForm.create({
    fields: [
        { type:"header", defaultValue:"Order Supply Items"},
        { name: "itemID", title:"Item", type:"select",
          optionDataSource:"supplyItem",
          displayField:"itemName", 
          change:"label1.setContents('Selected itemID:' + value)"},
        { name:"Quantity", editorType:"SpinnerItem", defaultValue:1, min:1}
    ]
});

isc.Label.create({
    align:"center",
    border:"1px solid blue",
    top:75, 
    height:50, width:250, margin:10,
    ID:"label1",
    contents:"Select an item to order"
})

// -------------------------------------------------------------------------------------------
// Variation with multi column picker:


isc.DynamicForm.create({
    left:300,
    fields: [
        { type:"header", defaultValue:"Order Supply Items"},
        { name: "itemSKU", title:"Item", type:"select",
          optionDataSource:"supplyItem",
          valueField:"SKU", displayField:"itemName",
          change:"label2.setContents('Selected SKU:' + value)",
          pickListWidth:250,
          pickListFields: [
              { name:"itemName", width:125 },
              { name:"units" },
              { name:"unitCost" }
          ]
        },
        { name:"Quantity", editorType:"SpinnerItem", defaultValue:1, min:1}
    ]
});

isc.Label.create({
    align:"center",
    border:"1px solid blue",
    left:300, top:75,
    height:50, width:250, margin:10,
    ID:"label2",
    contents:"Select an item to order"
});
