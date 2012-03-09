
// ---------------------------------------------------------------------------------------
// Local Data Example

isc.Label.create({
    contents: "Local Data",
    width: "90%",
    height: 25,
    autoDraw: true,
    baseStyle: "exampleSeparator"
});

isc.ListGrid.create({
    top: 40,
    width: 500,
    height: 200,
    canEdit:true,
    autoDraw: true,
    data:[
        {employee:"Richard Smith", division:"Marketing", department:"Community Relations"},
        {employee:"Sam Edwards", division:"Services", department:"Support"}
    ],
    fields: [
        {name:"employee", title:"Name", canEdit:false},
        {name: "division",
         title: "Division",
         editorType: "select",
         valueMap: ["Marketing", "Sales", "Manufacturing", "Services"],
         // Calling 'setValueMap()' will force the 'getEditorValueMap' method to be
         // re-evaluated for the department field
         changed:"item.grid.setValueMap('department')"
        },
        {name: "department",
         title: "Department",
         editorType: "select",
         getEditorValueMap : function (values, field, grid) {
             var division = values.division;
             return field.departments[division];
         },
         departments: {
            Marketing: ["Advertising", "Community Relations"],
            Sales: ["Channel Sales", "Direct Sales"],
            Manufacturing: ["Design", "Development", "QA"],
            Services: ["Support", "Consulting"]
         },
         editorProperties:{
             addUnknownValues:false
         }
        }
    ]

});


// ---------------------------------------------------------------------------------------
// Remote Data Example

isc.Label.create({
    contents: "Remote Data",
    top: 280,
    width: "90%",
    height: 25,
    autoDraw: true,
    baseStyle: "exampleSeparator"
});

isc.ListGrid.create({
    top: 320,
    width: 500,
    height:200,
    canEdit:true,
    autoDraw: true,
    ID:"orderList",
    
    fields: [
        {name:"quantity", title:"Qty", type:"integer", width:30},
        {name:"categoryName", title:"Category", editorType:"select", 
         changed:"this.grid.clearEditValue(this.rowNum, 'itemName');",
         editorProperties : { optionDataSource:"supplyCategory" }
        },
        {name: "itemName", title:"Item", editorType: "select", 
         editorProperties:{
             optionDataSource:"supplyItem", 
             getPickListFilterCriteria : function () {
                var category = this.grid.getEditedCell(this.rowNum, "categoryName");
                return {category:category};
             }
         }
        }
        
    ]
});

isc.IButton.create({
    top: 535,
    width: 110,
    title:"Order New Item",
    click:"orderList.startEditingNew({quantity:1})"
});

