var projects = [
    "New Costing System", "Warehousing Improvements", 
    "Evaluate AJAX Frameworks", "Upgrade Postgres", 
    "Online Billing"];

isc.ListGrid.create({
    ID: "employeesList",
    width: 320, height: 264,
    dataSource: employees,
    canDragRecordsOut: true,
    dragDataAction: "copy",
    alternateRecordStyles: true,
    autoFetchData: false,
    fields: [
        {name: "EmployeeId", width: "30%"},
        {name: "Name"}
    ]
});

isc.ListGrid.create({
    ID: "projectList",
    width: 340, height: 264,
    dataSource: teamMembers,
    canAcceptDroppedRecords: true,
    canRemoveRecords: true,
    alternateRecordStyles: true,    
    autoFetchData: false,
    preventDuplicates: true,
    leaveScrollbarGap: false,
    fields: [
        {name: "employeeId", width: "25%"},
        {name: "employeeName", width: "40%"},
        {name: "projectCode"}
    ]
});

isc.HStack.create({membersMargin:10, height:160, members:[
    isc.VStack.create({
        members: [
            isc.LayoutSpacer.create({height: 30}),
            employeesList
        ]
    }),
    isc.Img.create({src:"icons/32/arrow_right.png", width:32, height:32, layoutAlign:"center",
        click:"projectList.transferSelectedData(employeesList)"
    }),
    isc.VStack.create({
        members: [
            isc.DynamicForm.create({
                width: 300, height: 30,
                fields: [
                    {
    					ID: "projectSelector",
                        name: "projectCode",
                        type: "select",
                        title: "Team for Project",
                        defaultValue: projects[0],
                        valueMap: projects,
                        changed: function(){
                            var crit = this.form.getValuesAsCriteria();
                            projectList.fetchData(crit);
                        }
                    }
                ]
            }),
            projectList
		]
    })
]});

employeesList.fetchData();
projectSelector.changed();

// ---------------------------------------------------------------------------------------
// The code that follows is just to illustrate when SmartClient has contacted the server,
// to underline the point about queuing. It is not part of the example.

isc.RPCManager.addClassProperties({
    queueSent: function (requests) {
        if (serverCount) this.updateServerContactLabel(requests);
    },
    updateServerContactLabel: function (requests) {
        serverCount.incrementAndUpdate(requests);
        // Flash the label
        var bgColor = serverCount.backgroundColor;
        serverCount.setBackgroundColor("ffff77");
        isc.Timer.setTimeout("if (serverCount) serverCount.setBackgroundColor('" + bgColor +"');", 500);
    }

});

isc.Label.create({
    ID: "serverCount",
    top: 315, padding: 10, left: 220,
    width: 260, height: 40,
    border: "1px solid grey",
    contents: "<b>Number of server trips: 0<br>No queues sent</b>",
    count: 0,
    incrementAndUpdate: function (requests) {
        this.count++;
        this.setContents("<b>Number of server trips: " + this.count + 
                         "<br>Last queue contained " + requests.length + " request(s)</b>"); 
    }
});

