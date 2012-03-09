isc.ListGrid.create({
    ID: "countryList",
    width:500, height:224, alternateRecordStyles:true,
    data: countryData,
    fields:[
        {name:"countryCode", title:"Flag", width:50, type:"image", imageURLPrefix:"flags/16/", imageURLSuffix:".png"},
        {name:"countryName", title:"Country"},
        {name:"independence", title:"Nationhood", type:"date"},
        {name:"area", title:"Area (km&sup2;)", type:"float", formatCellValue:"isc.Format.toUSString(value)"},
        {name:"gdp_percap", title:"GDP (per capita)", align:"right",
            formatCellValue: function (value, record) {
                var gdpPerCapita = Math.round(record.gdp*1000000000/record.population);
                return isc.Format.toUSDollarString(gdpPerCapita);
            },
            sortNormalizer: function (record) {
                return record.gdp/record.population;
            }
        }
    ],
    // initial sort on Area, high-to-low
    sortFieldNum: 3,
    sortDirection: "descending"
})
