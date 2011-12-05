
isc.ListGrid.create({
    ID: "userListGrid",
    width: 600, height: 164,
    autoDraw: true,
    dataSource: "flattenedBeans_flatUserHB",
    autoFetchData: true,
    fields: [
        { name: "firstName" },
        { name: "surname" },
        { name: "email" },
        { name: "addressLine1" },
        { name: "city" },
        { name: "state" }
    ],
    selectionChanged: function (record, state) {
        if (state) {
            editorForm.editRecord(record);
        }
    }
});

isc.DynamicForm.create({
    ID: "editorForm",
    width: 280, top: 180,
    dataSource: "flattenedBeans_flatUserHB",
    fields: [
        { name: "firstName", title: "First Name" },
        { name: "surname", title: "Surname" },
        { name: "email", title: "Email address" },
        { name: "addressLine1", title: "Address Line 1" },
        { name: "city", title: "City" },
        { name: "state", title: "State" }
    ]

});

isc.IButton.create({
    title: "Add User", 
    top: 190, left: 300,
    click: "editorForm.editNewRecord();"
});

isc.IButton.create({
    title: "Save Changes", 
    top: 220, left: 300,
    click: "editorForm.saveData();"
});
