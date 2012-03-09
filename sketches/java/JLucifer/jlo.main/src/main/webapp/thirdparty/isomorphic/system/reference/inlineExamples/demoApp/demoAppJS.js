
// User Interface
// ---------------------------------------------------------------------

isc.TreeGrid.create({
	ID:"categoryTree",
    dataSource:"supplyCategory",
	nodeClick:"findForm.findItems(node.categoryName)",
    showHeader:false,
    leaveScrollbarGap:false,
    animateFolders:true,
	canAcceptDroppedRecords:true,
    canReparentNodes:false,
    selectionType:"single",
    animateRowsMaxTime:750
});

isc.HTMLPane.create({
	ID:"helpCanvas",
	contentsURL:isc.Page.getIsomorphicDocsDir() + "/inlineExamples/demoApp/demoApp_helpText.html",
	overflow:"auto",
    styleName:"defaultBorder",
	padding:10
});

isc.IButton.create({
	ID:"findButton",
	title:"Find",
	left:25,
	top:16,
	width:80,
	click:"findForm.findItems()",
	icon:"demoApp/icon_find.png",
	iconWidth:24
});


isc.SearchForm.create({
	ID:"findForm",
	dataSource:"supplyItem",
	left:130,
    top:10,
	cellPadding:4,
	numCols:6,
	fields:[
		{name:"SKU"},
        {name:"itemName", editorType:"comboBox", optionDataSource:"supplyItem", 
        pickListWidth:250},
		{name:"findInCategory", editorType:"checkbox", 
            title:"Use category", defaultValue:true, shouldSaveValue:false}
	],
    
    // Function to actually find items
    findItems : function (categoryName) {
    	var findValues;
    
    	if (this.getValue('findInCategory') && categoryTree.selection.anySelected()) {
    		// use tree category and form values
    		if (categoryName == null) categoryName = categoryTree.getSelectedRecord().categoryName;
    		findValues = {category:categoryName};
    		isc.addProperties(findValues, this.getValues());
    		
    	} else if (categoryName == null) {
    		// use form values only
    		findValues = this.getValues();
    		
    	} else {
    		// use tree category only
    		findValues = {category:categoryName};
    	}
    	
    	itemList.filterData(findValues);
    	
    	itemDetailTabs.clearDetails();
    }
});

isc.ListGrid.create({
	ID:"itemList",
	dataSource:"supplyItem",
	useAllDataSourceFields:true,
	fields:[
		{name:"itemName", title:"Name", showHover:true},
		{name:"unitCost", 
         formatCellValue:"return isc.Format.toCurrencyString(parseFloat(value))", 
         editorType:"spinner", editorProperties:{step:0.01}},
		{name:"SKU", canEdit:false},
		{name:"description", showHover:true},
		{name:"category", canEdit:false},
        {name:"inStock", width:55, align:"center",
            formatCellValue : function (value, record, field, rowNum, colNum) {
                if (value) return isc.Canvas.imgHTML("demoApp/checked.png",13,13);
                else return isc.Canvas.imgHTML("demoApp/unchecked.png",13,13)
            }},
        {name:"nextShipment", showIf:"false"}
	],
	recordClick:"this.updateDetails()",
	canEdit:true,
    modalEditing:true,
	cellChanged:"this.updateDetails()",
	alternateRecordStyles:true,
	canDragRecordsOut:true,
	hoverWidth:200,
    hoverHeight:20,
	selectionType:"single",
	cellContextClick:"return itemListMenu.showContextMenu()",
    
    // Function to update details based on selection
    updateDetails : function () {
        var record = this.getSelectedRecord();
        if (record == null) return itemDetailTabs.clearDetails();
        
        if (itemDetailTabs.getSelectedTabNumber() == 0) {
            // View tab: show selected record
            itemViewer.setData(record) 
        } else {
            // Edit tab: edit selected record
            itemDetailTabs.updateTab("editTab", editForm);
            editForm.editRecord(record);
        }
    }

});

isc.Menu.create({
	ID:"itemListMenu",
    cellHeight:22,
	data:[
        {title:"Add New Item",
			icon:"demoApp/icon_add.png",
            click:function () {
                itemList.selection.deselectAll();
                itemDetailTabs.selectTab(1);
                itemList.updateDetails();
            }
        },
        {isSeparator:true},
		{title:"Show Details",
			icon:"demoApp/icon_view.png",
			click:"itemDetailTabs.selectTab(0); itemList.updateDetails()"},
		{title:"Edit Item",
			icon:"demoApp/icon_edit.png",
			click:"itemDetailTabs.selectTab(1); itemList.updateDetails()"},
		{title:"Delete Item",
			icon:"demoApp/icon_delete.png",
			click:"itemList.removeSelectedData(); itemDetailTabs.clearDetails()"}
	]
});


isc.DetailViewer.create({
	ID:"itemViewer",
	dataSource:"supplyItem",
	width:"100%",
	margin:"25",
    emptyMessage:"Select an item to view its details"
});

isc.DynamicForm.create({
	ID:"editForm",
	dataSource:"supplyItem",
	useAllDataSourceFields:true,
	fields:[
		{name:"SKU"},
		{name:"description", rowSpan:3, width:200},
		{name:"category", editorType:"pickTree", dataSource:"supplyCategory",
         emptyMenuMessage:"No Sub Categories", canSelectParentItems:true},
		{name:"unitCost", editorType:"spinner", step:0.01},
        {name:"inStock"},
        {name:"nextShipment", useTextField:true},
		{name:"savebtn", editorType:"button", align:"center", 
            width:100, colSpan:4, title:"Save Item", click:"editForm.saveData()"}
	],
	width:650,
	numCols:4,
	colWidths:[100,200,100,200],
    margin:25,
	cellPadding:5,
	autoFocus:false
});

isc.Label.create({
    ID:"editorMessage",
    autoDraw: false,
    width:"100%",
    height:"100%",
    align:"center",
    contents:"Select a record to edit, or a category to insert a new record into"
});

isc.TabSet.create({
	ID:"itemDetailTabs",
	tabs:[
		{title:"View", pane:itemViewer, ID:"viewTab", width:70, icon:"demoApp/icon_view.png"},
		{title:"Edit", pane:editForm, ID:"editTab", width:70, icon:"demoApp/icon_edit.png"}
	],

	tabSelected:"itemList.updateDetails()",

    // Function to clear out selected items' details
    clearDetails : function () {
        var selectedTab = this.getSelectedTabNumber();
        if (selectedTab == 0) {
            // View tab: show empty message
        	itemViewer.setData();
        } else if (selectedTab == 1) {
            // Edit tab: show new record editor, or empty message
            if (categoryTree.getSelectedRecord() != null) {
                this.updateTab("editTab", editForm);
                editForm.editNewRecord({category:categoryTree.getSelectedRecord().categoryName});
            } else {
                this.updateTab("editTab", editorMessage);
            }
        }
    }

});

// Define application layout
// ---------------------------------------------------------------------

isc.HLayout.create({
	ID:"pageLayout",
	width:"100%",
	height:"100%",
    layoutMargin:20,
	members:[
		isc.SectionStack.create({
			ID:"leftSideLayout",
			width:280,
			showResizeBar:true,
			visibilityMode:"multiple",
            animateSections:true,
			sections:[
				{title:"Office Supply Categories", autoShow:true, items:[categoryTree]},
				{title:"Instructions", autoShow:true, items:[helpCanvas]}
			]
		}),
		isc.SectionStack.create({
			ID:"rightSideLayout",
			visibilityMode:"multiple",
            animateSections:true,
			sections:[
				{title:"Find Items", autoShow:true, items:[
					isc.Canvas.create({
						ID:"findPane",
						height:60,
						overflow:"auto",
                        styleName:"defaultBorder",
						children:[findForm,findButton]
					})				
				]},
				{title:"Office Supply Items", autoShow:true, items:[itemList]},
				{title:"Item Details", autoShow:true, items:[itemDetailTabs]}
			]
		})
	]
});

isc.Page.setEvent("load", "pageLayout.draw()");

// Custom logic: 
// When showing options in the combo-box, only show the options from the selected category
// if appropriate
findForm.getItem("itemName").addProperties({
    getPickListFilterCriteria : function () {
        var criteria = this.Super("getPickListFilterCriteria", arguments);
        if (this.form.getValue('findInCategory') && categoryTree.selection.anySelected()) {
            criteria.category = categoryTree.getSelectedRecord().categoryName;
        }
        return criteria
     }
     
});



//	Call fetchData() on the tree to load the initially visible categories
// ---------------------------------------------------------------------

categoryTree.fetchData();
