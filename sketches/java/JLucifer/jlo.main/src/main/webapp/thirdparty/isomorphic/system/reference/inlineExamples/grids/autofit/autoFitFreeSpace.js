isc.ListGrid.create({
    ID: "listGrid",
    autoDraw: false,
    dataSource: supplyItem,
    useAllDataSourceFields: true,
    autoFetchData: true
});

isc.TabSet.create({
    ID: "tabSet",
    autoDraw: false,
    tabs: [
        {title: "View"},
        {title: "Edit"}
    ]
});

isc.TreeGrid.create({
    ID: "treeGrid",
    width: "30%",
    showConnectors: true,
    showResizeBar: true,
    data: isc.Tree.create({
        modelType: "child",
        root: {name: "root", children: [
            {name: "File"},
            {name: "Edit"},
            {name: "Search"},
            {name: "Project"},
            {name: "Tools"},
            {name: "Window"},
            {name: "Favorites"}
        ]}
    }),
    fields: [
        {name: "Navigation", formatCellValue: "record.name"}
    ]    
});

isc.HLayout.create({
    ID: "navLayout",
    members: [
        treeGrid, listGrid
    ]
});

isc.SectionStack.create({
    height: 400, width: 600,
    visibilityMode: "multiple",
    border:"1px solid blue",
    animateSections: true,
    overflow: "hidden",
    sections: [
        {title: "Summary", expanded: true, items: [navLayout]},
        {title: "Details", expanded: true, items: [tabSet]}
    ]
});
