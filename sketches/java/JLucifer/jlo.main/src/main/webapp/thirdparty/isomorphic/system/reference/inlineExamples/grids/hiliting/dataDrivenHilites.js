
var ds = isc.DataSource.get("countryDSHilites");

isc.VLayout.create({
	ID:"layout",
	width:500, height:250
});


// Function to create a new ListGrid.
function recreateListGrid() {
	layout.addMember(isc.ListGrid.create({
	    ID: "countryList",
	    width:"100%", height:"*",
	    alternateRecordStyles:true, cellHeight:22,
	    dataSource: ds,
	    autoFetchData: true,
	    canAddFormulaFields: true,
	    canAddSummaryFields: true,
	    fields:[
	        {name:"countryCode", title:"Flag", width:50, type:"image", imageURLPrefix:"flags/16/", 
	            imageURLSuffix:".png"
	        },
	        {name:"countryName", title:"Country"},
	        {name:"capital", title:"Capital"},
	        {name:"population", title:"Population", formatCellValue:"isc.Format.toUSString(value)"},
	        {name:"area", title:"Area (km&sup2;)", formatCellValue:"isc.Format.toUSString(value)"},
	        {name:"gdp", formatCellValue:"isc.Format.toUSString(value)"}
	    ],
		hilites:     [
            {
                fieldName: "area", 
                textColor: "#FF0000", 
                cssText: "color:#FF0000;", 
                id: 0
            }, 
            {
                fieldName:[
                    "area"
                    , 
                    "gdp"
                ], 
                textColor: "#FFFFFF", 
                backgroundColor: "#639966", 
                cssText: "color:#3333FF;background-color:#639966;", 
                id:1
            }
        ]
	}));

}

// Create the initial ListGrid.
recreateListGrid();

