isc.DynamicForm.create({
    ID: "orderItemCriteriaForm",
    numCols: 2, width: 400,
    autoDraw: false,
    fields: [
        {name: "startDate", type: "date", title: "Start&nbsp;Date", defaultValue: new Date(2009, 1, 1)},
        {name: "endDate", type: "date", title: "End&nbsp;Date", defaultValue: new Date(2009, 1, 28)},
        {
            name: "filterButton",
            title: "Filter",
            type: "button",
            endRow: false, startRow: false,
            click: function() {
                var criteria = orderItemSummaryList.getFilterEditorCriteria(true);
                if (!criteria) criteria = {};
                criteria = isc.DataSource.combineCriteria(criteria, orderItemCriteriaForm.getValuesAsCriteria());
                orderItemSummaryList.data.invalidateCache();
                orderItemSummaryList.filterData(criteria);
            }
        }
    ]
});

isc.ListGrid.create({
    ID: "orderItemSummaryList",
    width:650, height:184, alternateRecordStyles:true, 
    autoDraw: false,
    dataSource: dynamicReporting_orderItem,
    fetchOperation: "summary",
    dataPageSize: 15,  // Deliberately small, to show server-side paging and filtering
    drawAllMaxCells: 0,  // Disable this performance feature, again to force server visits
    fields:[
        {name: "itemID", displayField: "itemName", align: "left",
         title: "Item Name", width: "50%", 
         filterEditorType: "TextItem", 
         filterEditorProperties: {fetchMissingValues: false}
        },
        {name: "SKU"},
        {name: "unitCost"},
        {name: "quantity", title: "Total qty"},
        {name: "totalSales", formatCellValue: function(value) {
            return Math.round(value*100)/100;
        }
        }
    ],
    showFilterEditor: true
});

isc.IButton.create({
	ID: "orderItemExportButton",
	title: "Export Data",
	click: "orderItemSummaryList.exportData({operationId: 'summary'});"
});

isc.VLayout.create({
    membersMargin: 20,
    members: [orderItemCriteriaForm, orderItemSummaryList, orderItemExportButton]
});

orderItemSummaryList.fetchData(orderItemCriteriaForm);
