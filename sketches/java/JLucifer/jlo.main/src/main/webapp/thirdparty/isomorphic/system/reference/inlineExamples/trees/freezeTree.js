isc.TreeGrid.create({
    ID: "employeeTree",
    width:500, height:224,
    dataSource: employees,
    autoFetchData:true,
    canFreezeFields:true,
    canReparentNodes:true,
    fields:[
        {name:"Name", frozen:true, width:150},
        
        {name:"Email", width:150},
        {name:"Job", width:150},
        {name:"EmployeeType", width:80},
        {name:"EmployeeStatus", width:80},
        {name:"Salary", width:80},
        {name:"Gender", width:80},
        {name:"MaritalStatus", width:80}
    ]
});