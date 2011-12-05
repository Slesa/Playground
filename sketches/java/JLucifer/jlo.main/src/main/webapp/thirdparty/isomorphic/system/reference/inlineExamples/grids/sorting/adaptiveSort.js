isc.ListGrid.create({
    ID: "supplyList",
    width:500, height:300, alternateRecordStyles:true,
    dataSource: supplyItem,
    fields:[
        {name:"SKU"},
        {name:"itemName"},
        {name:"description"},
        {name:"category"}
    ],
    autoFetchData: true,
    dataPageSize: 20
})

isc.DynamicForm.create({
    ID: "sortForm",
    width: 300,
    top: 310,
    fields: [
        {
            name: "restrict",
            title: "Limit to Electronics",
            type: "boolean",
            changed: function () {
                var criteria = null;
                if (this.getValue() == true) {
                    criteria = {category: "Office Machines and Electronics"};
                }
                supplyList.fetchData(criteria);
            }
        }
    ]
});


// ---------------------------------------------------------------------------------------
// The code that follows is just to illustrate when SmartClient has needed to contact the
// server. It is not part of the example.

supplyItem.addProperties({
    transformResponse: function () {
        serverCount.increment();
        // Flash the label
        serverCount.setBackgroundColor("ffff77");
        isc.Timer.setTimeout("if (serverCount) serverCount.setBackgroundColor('ffffff');", 500);
    }
})
isc.Label.create({
    ID: "serverCount",
    top: 350, padding: 10,
    width: 500, height: 30,
    border: "1px solid black",
    contents: "<b>Number of server trips: 0</b>",
    count: 0,
    increment: function () {
        this.count++;
        this.setContents("<b>Number of server trips: " + this.count + "</b>");
    }
});
