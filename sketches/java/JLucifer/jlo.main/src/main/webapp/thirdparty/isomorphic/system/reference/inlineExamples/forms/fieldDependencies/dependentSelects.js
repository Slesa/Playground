
// ---------------------------------------------------------------------------------------
// Local Data Example

isc.Label.create({
    contents: "Local Data",
    width: "90%",
    height: 25,
    autoDraw: true,
    baseStyle: "exampleSeparator"
});

isc.DynamicForm.create({
    top: 45,
    width: 500,
    numCols: 4,
    autoDraw: true,
    fields: [
        {name: "division",
         title: "Division",
         type: "select",
         valueMap: ["Marketing", "Sales", "Manufacturing", "Services"],
         changed: "form.getField('department').setValueMap(item.departments[value])",
         departments: {
            Marketing: ["Advertising", "Community Relations"],
            Sales: ["Channel Sales", "Direct Sales"],
            Manufacturing: ["Design", "Development", "QA"],
            Services: ["Support", "Consulting"]
         }
        },
        {name: "department",
         title: "Department",
         type: "select",
         addUnknownValues:false
        }
    ]
});


// ---------------------------------------------------------------------------------------
// Remote Data Example

isc.Label.create({
    contents: "Remote Data",
    top: 120,
    width: "90%",
    height: 25,
    autoDraw: true,
    baseStyle: "exampleSeparator"
});

isc.DynamicForm.create({
    top: 165,
    width: 500,
    numCols: 4,
    autoDraw: true,
    fields: [
        {name:"categoryName", title:"Category", editorType:"select", 
         optionDataSource:"supplyCategory", changed:"form.clearValue('itemName');" 
        },
        {name: "itemName", title:"Item", editorType: "select", 
         optionDataSource:"supplyItem", 
         getPickListFilterCriteria : function () {
            var category = this.form.getValue("categoryName");
            return {category:category};
         }
        }
    ]
});
