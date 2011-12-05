isc.DynamicForm.create({
    ID: "filterForm",
    width: 300,
    operator: "and",
    saveOnEnter: true,
    dataSource: worldDS,
    submit: function () {
        filterGrid.filterData(filterForm.getValuesAsCriteria());
    },
    fields: [
        {name: "countryName",
         title: "Country Name contains",
         type: "text"
        },
        {type: "blurb",
         defaultValue: "<b>AND</b>"
        },
        {name: "population",
         title: "Population smaller than",
         type: "number",
         operator: "lessThan"
        },
        {type: "blurb",
         defaultValue: "<b>AND</b>"
        },
        {name: "independence",
         title: "Nationhood later than",
         type: "date",
         useTextField: true,
         operator: "greaterThan"
        }
    ]
});

isc.ListGrid.create({
    ID: "filterGrid",
    top: 150,
    width:750, height:300, alternateRecordStyles:true,
    dataSource: worldDS,
    autoFetchData: true,
    useAllDataSourceFields: true
})

isc.IButton.create({
    left: 320,
    title: "Filter",
    click: function () {
        filterForm.submit();
    }
});
