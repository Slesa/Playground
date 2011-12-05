isc.DynamicForm.create({
        width: 300,
        fields: [
            { defaultValue:"Item", type:"section", sectionExpanded:true,
                itemIds: ["itemName", "description", "price"] 
            },
            { name:"itemName", type:"text", title:"Item"},
            { name:"description", type:"textArea", title:"Description"},
            { name:"price", type:"float", title:"Price", defaultValue:"low"},
            { defaultValue:"Stock", type:"section", sectionExpanded:false,
                itemIds: ["inStock", "nextShipment"]
            },
            { name:"inStock", type:"checkbox", title:"In Stock" },
            { name:"nextShipment", type:"date", title:"Next Shipment", useTextField:true}
        ]
});
