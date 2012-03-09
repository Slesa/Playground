isc.ListGrid.create({
    ID: "countryList",
    width:500, height:300, 
    alternateRecordStyles:true,
    data: countryData,
    fields: [
        {name: "countryName", title: "Country"},
        {name: "capital", title: "Capital"},
        {name: "continent", title: "Continent"}
    ],
    canExpandRecords: true,
    expansionMode: "detailField",
    detailField: "background"
});
