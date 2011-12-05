isc.ListGrid.create({
    ID: "countryList",
    width:750, height:224, alternateRecordStyles:true,
    headerHeight: 40,
    dataSource: countryDS,
    autoFetchData: true,
    fields:[
        {name:"countryCode", title:"Flag", width:50, type:"image", imageURLPrefix:"flags/16/", imageURLSuffix:".png"},
        {name:"countryName", title:"Country"},
        {name:"capital"},
        {name:"government"},
        {name:"independence", title:"Nationhood"},
        {name:"population", title:"Population", formatCellValue:"isc.Format.toUSString(value)"},
        {name:"area", title:"Area (km&sup2;)", formatCellValue:"isc.Format.toUSString(value)"},
        {name:"gdp", formatCellValue:"isc.Format.toUSString(value)"}
    ],
    headerSpans: [
        {
            fields: ["countryCode", "countryName"], 
            title: "Identification"
        },
        {
            fields: ["capital", "government", "independence"], 
            title: "Government & Politics"
        },
        {
            fields: ["population", "area", "gdp"], 
            title: "Demographics"
        }
    ]
    
})
