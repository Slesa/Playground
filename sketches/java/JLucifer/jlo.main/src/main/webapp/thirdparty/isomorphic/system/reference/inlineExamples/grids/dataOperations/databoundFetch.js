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
    sortFieldNum: 1, // sort by countryName
    dataPageSize: 50,
    drawAheadRatio: 4
})


isc.IButton.create({
    left:0, top:240, width:160,
    title:"Fetch Code: US",
    click:"countryList.fetchData({countryCode:'US'})"
})


isc.IButton.create({
    left:170, top:240, width:160,
    title:"Fetch Continent: Europe",
    click:"countryList.fetchData({continent:'Europe'})"
})


isc.IButton.create({
    left:340, top:240, width:160,
    title:"Fetch All",
    click:"countryList.fetchData()"
})
