
isc.SimpleType.create({
    name:"currency",
    inheritsFrom:"float",
    
    normalDisplayFormatter:function(value) {
        return isc.isA.Number(value) ? value.toCurrencyString() : value;
    },
    shortDisplayFormatter:function(value) {
        return isc.isA.Number(value) ? value.toCurrencyString() : value;
    },
    editFormatter:function (value) {
        return isc.isA.Number(value) ? value.toFixed(2) : value;
    },
    parseInput:function(value) {
        var fVal = parseFloat(value);
        if (!isNaN(fVal)) return fVal;
        return value;
    },
    
    validators:[
        {type:"floatRange", min:0, errorMessage:"Please enter a valid (positive) dollar value."},
        {type:"floatPrecision", precision:2, roundToPrecision:true}
    ]
    
});

        

isc.VLayout.create({
    width:"100%",
    membersMargin:3,
    members:[
        isc.ListGrid.create({
            ID:"itemGrid",
             height:200,
            canEdit:true,
            dataSource:"supplyItemCurrency",
            autoFetchData:true,
            recordClick:"itemForm.editRecord(record)"
        }),
        
        isc.DynamicForm.create({
            ID:"itemForm",
            numCols:4,
            dataSource:"supplyItemCurrency"
        }),
        
        isc.IButton.create({
            ID:"saveBtn",
            title:"Save",
            layoutAlign:"center",
            click:"itemForm.saveData();"
        })
    ]
});