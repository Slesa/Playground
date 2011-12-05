isc.ValuesManager.create({
        ID: "vm"        
});

isc.VLayout.create({
    width: 400,
    height: 300,
    membersMargin: 10,
    members : [
        isc.TabSet.create({
                ID: "theTabs",
            height: 250,
            tabs: [
                {title:"Item",
                    pane: isc.DynamicForm.create({
                        ID: "form0",
                        valuesManager:"vm",
                        fields: [
                            {name: "itemName", type:"text", title:"Item"},
                            {name: "description", type:"textArea", title:"Description"},
                            {name:"price", type:"float", title: "Price", defaultValue: "low"} 
                        ]
                    })
                },
                {title:"Stock", 
                    pane: isc.DynamicForm.create({
                        ID: "form1",
                        valuesManager:"vm",
                        fields: [
                            {name: "inStock", type:"checkbox", title:"In Stock"},
                            {name: "nextShipment", type:"date", title:"Next Shipment",
                                useTextField:"true", defaultValue:"256"
                            }                                
                        ]
                    })    
                }
            ]
        }),
        isc.Button.create({
            title:"Submit",
            click : function () {
                vm.validate();
                if (form1.hasErrors()) theTabs.selectTab(1);
                else theTabs.selectTab(0);
            }
        })    
    ]
});


