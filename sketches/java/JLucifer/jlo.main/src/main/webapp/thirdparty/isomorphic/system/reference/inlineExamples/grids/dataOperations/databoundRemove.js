isc.ListGrid.create({
    ID: "countryList",
    width:500, height:224, alternateRecordStyles:true,
    dataSource: worldDS,
    // display a subset of fields from the datasource
    fields:[
        {name:"countryCode"},
        {name:"countryName"},
        {name:"capital"},
        {name:"continent"}
    ],
    sortFieldNum: 0, // sort by countryCode
    dataPageSize: 50,
    autoFetchData: true
})


isc.IButton.create({
    left:0, top:240, width:140,
    title:"Remove first",
    click:"countryList.removeData(countryList.data.get(0))"
})


isc.IButton.create({
    left:160, top:240, width:140,
    title:"Remove first selected",
    click: function () {
        if (countryList.getSelectedRecord()) {
            countryList.removeData(countryList.getSelectedRecord())
        }
    }
})


isc.IButton.create({
    left:320, top:240, width:140,
    title:"Remove all selected",
    click: function () {
        if (countryList.getSelection().getLength() > 0) {
            countryList.getSelection().map(function (item) { countryList.removeData(item) });
        }
    }
})

