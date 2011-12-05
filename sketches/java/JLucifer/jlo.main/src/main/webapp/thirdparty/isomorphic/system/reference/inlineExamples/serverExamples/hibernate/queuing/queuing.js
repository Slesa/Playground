isc.DynamicForm.create({
    ID: "findOrdersForm",
    width: 400, 
    fields: [
        { name: "userID", title: "User ID", defaultValue: 1, width: 50,
          hint: "Note:&nbsp;sample&nbsp;data&nbsp;only exists for user ID 1" },
        { type: "button", title: "Find Orders", click: "findOrdersForm.findOrders()"}
    ],
    findOrders: function () {

        sectionStack.draw();
        
        isc.RPCManager.startQueue();
        userDetailViewer.fetchData(findOrdersForm);
        ordersListGrid.fetchData(findOrdersForm);
        isc.RPCManager.sendQueue();
    }

});

isc.SectionStack.create({
    ID: "sectionStack",
    visibilityMode: "multiple",
    autoDraw: false,
    top: 100, width: 400, height: 300,
    sections: [
        { 
            title: "User Details",
            expanded: true,
            items: [
                isc.DetailViewer.create({
                    ID: "userDetailViewer",
                    autoDraw: false,
                    dataSource: "queuing_userHB",
                    useAllDataSourceFields: true
                })
            ]
        },
        {
            title: "Orders",
            expanded: true,
            items: [
                isc.ListGrid.create({
                    ID: "ordersListGrid",
                    autoDraw: false,
                    dataSource: "queuing_order",
                    showFilterEditor: true,
                    fields: [
                        { name: "orderID" },
                        { name: "customerName" },
                        { name: "orderDate" },
                        { name: "totalValue" }
                    ],
                    dataPageSize: 10,  // Deliberately small, to force paging
                    // Disable draw ahead to force paging with tiny dataPageSize
                    drawAheadRatio: 1.0,
                    drawAllMaxCells: 0  
                })
            ]
        } 
    ]
});


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
    top: 100, padding: 10, left: 420,
    width: 300, height: 40,
    border: "1px solid grey",
    contents: "<b>Number of server trips: 0<br>No queues sent</b>",
    count: 0,
    incrementAndUpdate: function (requests) {
        this.count++;
        this.setContents("<b>Number of server trips: " + this.count + 
                         "<br>Last queue contained " + requests.length + " request(s)</b>"); 
    }
});

