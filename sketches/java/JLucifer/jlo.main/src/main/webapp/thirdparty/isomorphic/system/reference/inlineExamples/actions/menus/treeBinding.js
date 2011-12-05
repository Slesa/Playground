isc.Tree.create({
    ID: "menuTree",
    root: {name: "root", children: [
        {name: "Marketing", children: [
            {name: "Advertising"},
            {name: "Community Relations"}
        ]},
        {name: "Sales", children: [
            {name: "Channel Sales"},
            {name: "Direct Sales"}
        ]},
        {name: "Manufacturing", children: [
            {name: "Design"},
            {name: "Development"},
            {name: "QA"}
        ]},
        {name: "Services", children: [
            {name: "Support"},
            {name: "Consulting"}
        ]}
    ]}
});

isc.MenuButton.create({
    title: "Go to department",
    width: 140,
    menu: isc.Menu.create({
        data: menuTree,
        canSelectParentItems: true,
        itemClick: function (item) {
            isc.say("You picked the \""+item.name+"\" department.");
        }
    })
});

isc.MenuButton.create({
    title: "Go to category",
    width: 140,
    top: 30,
    menu: isc.Menu.create({
        dataSource: "supplyCategory",
        canSelectParentItems: true,
        itemClick: function (item) { 
            isc.say("You picked the \""+item.categoryName+"\" category.");
        }
    })
});

