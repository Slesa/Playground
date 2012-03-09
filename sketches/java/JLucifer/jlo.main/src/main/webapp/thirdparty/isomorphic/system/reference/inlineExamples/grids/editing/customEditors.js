isc.ListGrid.create({
    ID: "countryList",
    width:620, height:224, alternateRecordStyles:true, 
    cellHeight:42, wrapCells:true,
    dataSource: countryDS,
    fields:[
        {name:"countryName", width:100},
        {name:"government", width:175,
            editorType:"textArea",
            editorProperties:{height:40}
        },
        {name:"population", width:100, formatCellValue:"isc.Format.toUSString(value)",
            editorType:"spinner"
        },
        {name:"independence", width:225,
            editorProperties:{useTextField:false}
        }
    ],
    autoFetchData: true,
    canEdit: true,
    editEvent: "click"
})
