isc.ListGrid.create({
    ID: "countryList",
    width:500, height:224, alternateRecordStyles:true,
    data: countryData,
    fields:[
        {name:"countryCode", title:"Flag", width:50, type:"image", imageURLPrefix:"flags/16/", imageURLSuffix:".png"},
        {name:"countryName", title:"Country"},
        {name:"independence", title:"Nationhood", type:"date",
            formatCellValue: function (value) {
                if (value) {
                    return value.getShortMonthName()+' '+value.getDate()+', '+value.getFullYear();
                }
            }
        },
        {name:"area", title:"Area", type:"number",
            formatCellValue: "isc.Format.toUSString(value) + ' km&sup2;'"
        }
    ]
})
