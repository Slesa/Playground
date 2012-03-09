
isc.Menu.create({
        ID: "testMenu",
        autoDraw:false,
        data: exampleData,
        fields:[
            {name:"partSrc", type:"image", width:40, imgDir:"pieces/16/"},
            {name:"partName"}     
        ],
        selectionType: "single",
        canDragRecordsOut: true,
        dragDataAction: "copy"
        
});
isc.MenuButton.create({
    ID: "menuButton",
    autoDraw:false,
    title: "Parts",
    width: 100,
    menu: testMenu
});

isc.HStack.create({membersMargin:50, height:160, members:[
    menuButton,
    isc.ListGrid.create({
        ID:"myList",
        autoDraw:false,
        canAcceptDroppedRecords: true,
        canReorderRecords: true,
        fields:[
            {name:"partSrc", type:"image", width:40, imgDir:"pieces/16/"},
            {name:"partName"},
            {name:"partNum", width:40}
        ],
        data: [
            {partName:"Blue", partSrc:"cube_blue.png", partNum:1},
            {partName:"Yellow", partSrc:"cube_yellow.png", partNum:2}
        ]
    })
]})


