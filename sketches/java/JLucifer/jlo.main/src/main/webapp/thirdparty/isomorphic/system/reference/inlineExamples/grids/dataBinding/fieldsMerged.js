isc.ListGrid.create({
    ID: "countryList",
    width:500, height:224, alternateRecordStyles:true,
    fields:[
    // move countryCode before country name and replace with flag image/title
        {name:"countryCode", title:"Flag", width:40, type:"image", imageURLPrefix:"flags/16/", imageURLSuffix:".png"},
        {name:"countryName"},
    // change title and alignment of independence date
        {name:"independence", title:"Nationhood", align:"center"},
    // format population number
        {name:"population", formatCellValue:"isc.Format.toUSString(value)"},
    // format GDP as $M instead of $B
        {name:"gdp", title:"GDP ($M)", formatCellValue:"(value*1000).toUSString()"}
    ],
    dataSource: countryDS,
    autoFetchData: true
})
