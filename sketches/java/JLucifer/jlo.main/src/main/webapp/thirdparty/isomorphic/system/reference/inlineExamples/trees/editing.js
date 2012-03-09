isc.TreeGrid.create({
    ID: "employeeTree",
    width: 500,
    height: 250,
    dataSource: "employees",
    autoFetchData: true,
    canEdit: true,
    canReorderRecords: true,
    canAcceptDroppedRecords: true,
    nodeIcon: "icons/16/person.png",
    folderIcon: "icons/16/person.png",
    showOpenIcons: false,
    showDropIcons: false,
    closedIconSuffix: "",
    fields: [
        {name: "Name"},
        {name: "Job"},
        {name: "Salary"}
    ]
});

isc.SearchForm.create({
    top: 270,
    width: 200,
    height: 30,
    fields:[
        {editorType: "pickTree", showTitle: false, canSelectParentItems: true,
         dataSource: "employees", displayField: "Name", valueField: "EmployeeId",
         change: "employeeGrid.fetchData({ReportsTo: value})"}
    ]
});

isc.ListGrid.create({
    ID: "employeeGrid",
    top: 300,
    width: 500,
    height: 250,
    dataSource: "employees",
    canEdit: true,
    fields: [
        {name: "Name"},
        {name: "Job"},
        {name: "Salary"}
    ]
});