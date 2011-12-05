isc.DynamicForm.create({
    ID: "dateForm",
    numCols: 4,    
    width: 600,
    fields: [
        {name:"pickListDate", title:"PickList Date", type:"date", change:"dateLabel.setContents(value)"},
        {name:"directInputDate", title:"Direct Input Date", type:"date", useTextField:true, change:"dateLabel.setContents(value)"}
    ]
});

isc.Label.create({
        ID: "dateLabel",
        top: 40,
        left: 100,
        width: 400
});

dateLabel.setContents(dateForm.getValue('pickListDate'));
