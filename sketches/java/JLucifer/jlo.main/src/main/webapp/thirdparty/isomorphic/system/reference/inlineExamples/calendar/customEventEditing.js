
isc.DataSource.create({
    ID: "eventDS",
    fields:[
        {name:"eventId", primaryKey: true, type: "sequence"},
        {name:"name"},
        {name:"description"},
        {name:"startDate"},
        {name:"endDate"}
    ],
    clientOnly: true,
    testData: eventData
        
});     

isc.Calendar.create({
    ID: "eventCalendar", 
    dataSource: eventDS, 
    autoFetchData: true,
    eventEditorFields: [
        // specify the last field from the default fields so that subsequent fields come after the 
        // default fields
        {name: "description"},
        // custom fields below
        {type:"header", defaultValue:"Event Options"},
        {name: "repeats", title: "Repeats", type: "select", colSpan: 4, defaultToFirstOption: true,
            valueMap: ["Does not repeat", "Daily","Weekly", "Monthly", "Yearly"]
        },
        {name: "reminderType", title: "Reminder", type: "select", width: 70, defaultToFirstOption: true,
            valueMap: ["Pop-up", "Email"]
        },
        {name: "reminderValue", showTitle: false, type: "text", width: 60, defaultValue:10},
        {name: "reminderUnits", showTitle: false, type: "select", width: 70, defaultToFirstOption: true,
            valueMap: ["minutes", "hours", "days"]
        }
    ],
    eventDialogFields: [
        {name: "name"},
        {name: "sharing", title: "Sharing", type: "radioGroup", 
            valueMap: ["Public", "Private"], vertical: false, width: 50
        }
    ]
    
});
