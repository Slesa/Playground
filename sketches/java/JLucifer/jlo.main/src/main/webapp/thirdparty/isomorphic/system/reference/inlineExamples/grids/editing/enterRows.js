isc.ListGrid.create({
    ID: "countryList",
    width:500, height:224, alternateRecordStyles:true, cellHeight:22,
    // use server-side dataSource so edits are retained across page transitions
    dataSource: countryDS,
    // display a subset of fields from the datasource
    fields:[
        {name:"countryName"},
        {name:"continent"},
        {name:"member_g8"},
        {name:"population", 
         formatCellValue:"isc.Format.toUSString(parseInt(value))"},
        {name:"independence"}
    ],
    autoFetchData: true,
    canEdit: true,
    editEvent: "click",
    listEndEditAction: "next"
})

isc.IButton.create({
    top:250,
    title:"Edit New",
    click:"countryList.startEditingNew()"
});

