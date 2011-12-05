// ---------------------------------------------------------------------------------------
// Seperate DynamicForm Example (DateRangeItem)

isc.Label.create({
    contents: "External DynamicForm (DateRangeItem)",
    top: 0,
    width: "90%",
    height: 25,
    autoDraw: true,
    baseStyle: "exampleSeparator"
});

isc.DynamicForm.create({
    ID: "form",
    top: 30, width: 500, 
    titleOrientation: "top",
    items: [
        { name: "independence", type: "DateRangeItem", showTitle: false, allowRelativeDates: true }
    ]
});

isc.Button.create({
    ID: "searchButton",
    title: "Filter",
    top: 120,
    autoFit: true,
    click: function () {
        var criteria = form.getField("independence").getCriterion();
        grid1.fetchData(criteria);
    }
});

// Create a ListGrid displaying data from the worldDS
isc.ListGrid.create({
    ID: "grid1",
    top: 150, width: 595, height: 100,
    dataSource: "worldDS"
});


// ---------------------------------------------------------------------------------------
// Inline FilterEditor Example (MiniDateRangeItem)

isc.Label.create({
    contents: "FilterEditor (MiniDateRangeItem)",
    top: 280,
    width: "90%",
    height: 25,
    autoDraw: true,
    baseStyle: "exampleSeparator"
});

// Create a ListGrid displaying data from the worldDS and also displaying a FilterEditor
isc.ListGrid.create({
    ID: "grid2",
    top: 330, width: 595, height: 100,
    dataSource: "worldDS",
    autoFetchData: true,
    showFilterEditor: true
});

// Give the DateRangeItem an initial value (see the DateRange object)
form.getItem("independence").setValue({ 
    start: { _constructor: "RelativeDate", value: "-1200m" }, 
    end: { _constructor: "RelativeDate", value: "-600m" }
});

searchButton.click();
