
var ds = isc.DataSource.get("countryDS");

// HTML for a "warning" icon to appear after certain hilites
var iHTML = isc.Canvas.imgHTML("[SKIN]/actions/exclamation.png");

// array of hilite-objects to apply to the grid
var hiliteArray =  
    [
        {
            fieldName: "area", 
            criteria: {
                fieldName: "area", 
                operator: "greaterThan", 
                value: 5000000
            }, 
            textColor: "#FF0000", 
            cssText: "color:#FF0000;", 
            id: 0
        }, 
        {
            fieldName:[
                "area", 
                "gdp"
            ], 
            textColor: "#FFFFFF", 
            backgroundColor: "#639966", 
            criteria: {
                _constructor: "AdvancedCriteria", 
                operator: "and", 
                criteria: [
                    {
                        fieldName: "gdp", 
                        operator: "greaterThan", 
                        value: 1000000
                    }, 
                    {
                        fieldName: "area", 
                        operator: "lessThan", 
                        value: 500000
                    }
                ]
            }, 
            cssText: "color:#3333FF;background-color:#639966;", 
            htmlAfter: iHTML,
            id:1
        }
    ]
;

isc.VLayout.create({
	ID:"layout",
	width:500, height:250,
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
                        // don't include the hilite array in the create statement to demonstrate
                        // re-application from state via setHiliteState()
						recreateListGrid(false);
				        countryList.setHiliteState(state);
				    }
				})
			]
		})
	]
});

// Create the initial ListGrid.  See comment below for parameter description.
recreateListGrid(true);

// Function to create a new ListGrid.  On the first call, passes "true" as the parameter, such
// that the hiliteArray above is included in the create() statement.  Subsequent calls pass false, 
// meaning no hiliteArray is included and a seperate call to setHiliteState() is used instead.
function recreateListGrid(includeHilites) {
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
		hilites: includeHilites ? hiliteArray : null
	}));

}
