isc.Button.create({
    title: "Multilevel Sort",
    autoFit: true,
    click: function () {
        isc.logWarn(isc.echoAll(supplyList.getSort()));
        isc.MultiSortDialog.askForSort(
            supplyList,
            supplyList.getSort(),
            function (sortLevels) {
                if (sortLevels) supplyList.setSort(sortLevels);
            }
        );
    }
});

isc.ListGrid.create({
    ID: "supplyList",
    top: 30, width:500, height:300, 
    alternateRecordStyles:true,
    dataSource: supplyItem,
    autoFetchData: true
})

