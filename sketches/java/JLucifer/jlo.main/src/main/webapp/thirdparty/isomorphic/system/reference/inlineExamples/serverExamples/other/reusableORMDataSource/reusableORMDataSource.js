isc.DynamicForm.create({
    values: {dataSource: "Select A DataSource"},
    items: [
        {name: "dataSource",
         width: 210,
         showTitle: false,
         editorType: "select",
         valueMap: ["reusableORMDataSource_supplyItem", "reusableORMDataSource_country"],
         change: function (form, item, value, oldValue) {
                    if (!this.valueMap.contains(value)) return false;
                    else {
                        list.setDataSource(value);
                        list.filterData();
                    }
                 }
        }
    ]
});

isc.ListGrid.create({
    ID: "list",
    width: 600,
    height: 224,
    top: 40,
    dataSource: reusableORMDataSource_supplyItem,
    canEdit: true,
    canRemoveRecords: true,
    autoFetchData: true,
    showFilterEditor: true,
    dataPageSize: 25   // keep this fairly small to more easily demonstrate that pagination and
                       // server-side sort/filter are working on the custom dataSource
});

isc.IButton.create({
    ID: "addButton",
    top: 280,
    width: 150,
    title: "Create new record",
    click: "list.startEditingNew();"
});
