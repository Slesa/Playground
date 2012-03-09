isc.ListGrid.create({
    ID: "countryList",
    width:500, height:224, alternateRecordStyles:true, canDragSelect: true,
    sortFieldNum: 1,
    data: countryData,
    fields:[
        {name:"countryCode", title:"Flag", width:50, type:"image", imageURLPrefix:"flags/16/", imageURLSuffix:".png"},
        {name:"countryName", title:"Country"},
        {name:"capital", title:"Capital"},
        {name:"population", title:"Population", type:"number", formatCellValue:"isc.Format.toUSString(value)"}
    ],
    
    getCellCSSText: function (record, rowNum, colNum) {
        if (this.getFieldName(colNum) == "population") {
            if (record.population > 1000000000) {
                return "font-weight:bold; color:red;";
            } else if (record.population < 50000000) {
                return "font-weight:bold; color:blue;";
            }
        }
    }

})
