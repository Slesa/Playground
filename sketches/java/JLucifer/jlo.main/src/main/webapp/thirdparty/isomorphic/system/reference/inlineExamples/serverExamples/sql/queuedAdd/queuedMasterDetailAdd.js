isc.DynamicForm.create({
    ID: "orderForm",
    autoDraw: false,
    dataSource: queuedAdd_order,
    useAllDataSourceFields: true
});

isc.ListGrid.create({
    ID: "orderItemsList",
    width: 500,
    height: 224,
    dataSource: queuedAdd_orderItem,
    canEdit: true,
    autoDraw: false,
    autoSaveEdits: false,
    fields: [
        {name:"quantity", title:"Qty", type:"integer", width:30},
        {name:"categoryName", title:"Category", editorType:"select", 
         changed:"this.grid.clearEditValue(this.rowNum, 'itemName');",
         editorProperties : { optionDataSource:"supplyCategory" }
        },
        {name: "itemID", title:"Item", editorType: "select", 
         optionDataSource: "supplyItem", valueField: "itemID", displayField: "itemName",
         editorProperties:{
             getPickListFilterCriteria : function () {
                var category = this.grid.getEditedCell(this.rowNum, "categoryName");
                return {category:category};
             }
         }
        }
        
    ]
});

isc.IButton.create({
    ID: "addButton",
    autoDraw: false,
    width: 110,
    title: "Add Order Line",
    click: "orderItemsList.startEditingNew({quantity:1})"
});

isc.IButton.create({
    ID: "saveButton",
    autoDraw: false,
    width: 100,
    title: "Save Order",
    click: function() {
        startTracking();
        isc.RPCManager.startQueue();
        orderForm.saveData();
        orderItemsList.saveAllEdits();
        isc.RPCManager.sendQueue();
        stopTracking();
        orderForm.editNewRecord();
        orderItemsList.setData(null);
    }
});

isc.VLayout.create({
    membersMargin: 20,
    members: [ 
        orderForm, 
        orderItemsList, 
        isc.HLayout.create({membersMargin:10, members:[addButton, saveButton]})
    ]
});

// ---------------------------------------------------------------------------------------
// The code that follows is just to illustrate when SmartClient has contacted the server,
// to underline the point about queuing. It is not part of the example.

isc.Label.create({
    ID: "serverCount",
    top: 140, padding: 10, left: 520,
    width: 260, height: 40,
    border: "1px solid grey",
    contents: "<b>Number of server saves: 0<br>No queues sent</b>",
    count: 0,
    incrementAndUpdate: function (requests) {
        this.count++;
        this.setContents("<b>Number of server saves: " + this.count + 
                         "<br>Last queue contained " + requests.length + " request(s)</b>"); 
    }
});

function startTracking() {
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
}

function stopTracking() {
    delete isc.RPCManager.queueSent;
    delete isc.RPCManager.updateServerContactLabel;
}

stopTracking();