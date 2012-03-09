isc.FilterBuilder.create({
    ID:"advancedFilter",
    dataSource:"worldDS",
    criteria: { _constructor: "AdvancedCriteria",
        operator: "and", criteria: [
            {fieldName: "continent", operator: "equals", value: "Europe"},
            {operator: "or", criteria: [
                {fieldName: "countryName", operator: "iEndsWith", value: "land"},
                {fieldName: "population", operator: "lessThan", value: 3000000}
            ]}
        ]
    }
});

isc.ListGrid.create({
    ID: "countryList",
    width:550, height:224, alternateRecordStyles:true, 
    dataSource: worldDS,
    fields:[
        {name:"countryName"},
        {name:"continent"},
        {name:"population", formatCellValue:"isc.Format.toUSString(value)"},
        {name:"area", formatCellValue:"isc.Format.toUSString(value)"},
        {name:"gdp", formatCellValue:"isc.Format.toUSString(value)"},
        {name:"independence"}
    ]})

isc.IButton.create({
    ID:"filterButton",
    title:"Filter",
    click : function () {
        countryList.filterData(advancedFilter.getCriteria());
    }
})

isc.VStack.create({
    membersMargin:10,
    members:[ advancedFilter, filterButton, countryList ]
})

// Perform the initial filter
filterButton.click();