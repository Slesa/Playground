isc.ListGrid.create({
    ID: "countryList",
    width:500, height:224, alternateRecordStyles:true,
    data: countryData,
    fields:[
        {name:"countryCode", title:"Flag", width:50, type:"image", imageURLPrefix:"flags/16/", imageURLSuffix:".png"},
        {name:"countryName", title:"Country"},
        {name:"population", title:"Population", formatCellValue:"isc.Format.toUSString(value)"},
        {name:"area", title:"Area (km&sup2;)", formatCellValue:"isc.Format.toUSString(value)"}
    ],
    // initial sort on Population, high-to-low
    sortFieldNum: 2,
    sortDirection: "descending",
    showSortArrow: "corner" // other values: "field", "both", "none"
})
