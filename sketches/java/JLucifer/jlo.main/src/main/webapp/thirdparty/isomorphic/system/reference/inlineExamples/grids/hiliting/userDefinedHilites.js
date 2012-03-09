
var ds = isc.DataSource.get("countryDS");

isc.VLayout.create({
	ID:"layout",
	width:600, height:250,
	members: [
		isc.HLayout.create({
			ID:"buttonLayout",
			width:"*", height:30,
			membersMargin: 10,
			members: [
				isc.IButton.create({
				    ID: "editHilitesButton",
				    autoFit: true,
				    title: "Edit Hilites",
				    click: "countryList.editHilites();"
				}),
				isc.IButton.create({
				    ID: "stateButton",
				    autoFit: true,
				    title: "Recreate from State",
				    click: function () {
				        var state = countryList.getHiliteState();

						countryList.destroy();
						recreateListGrid();
				        countryList.setHiliteState(state);
				    }
				})
			]
		})
	]
});

// create the initial ListGrid
recreateListGrid();

// function to create a new ListGrid
function recreateListGrid() {
	layout.addMember(isc.ListGrid.create({
	    ID: "countryList",
	    width:"100%", height:"*",
	    alternateRecordStyles:true, cellHeight:22,
	    dataSource: ds,
	    autoFetchData: true,
	    canAddFormulaFields: true,
	    canAddSummaryFields: true,
        useAllDataSourceFields:true,
	    fields:[
	        {name:"countryCode", title:"Flag", width:40, type:"image", imageURLPrefix:"flags/16/", 
	            imageURLSuffix:".png"
	        },
    
	        {name:"population", title:"Population", formatCellValue:"isc.Format.toUSString(value)"},
	        {name:"area", title:"Area (km&sup2;)", formatCellValue:"isc.Format.toUSString(value)"},
	        {name:"gdp", formatCellValue:"isc.Format.toUSString(value)"},
            {name:"capital", hidden:true},
            {name:"government", hidden:true}
	    ]
	}));

}
