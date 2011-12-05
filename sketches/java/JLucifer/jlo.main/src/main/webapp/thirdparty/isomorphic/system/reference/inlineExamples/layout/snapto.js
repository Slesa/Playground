isc.Label.create({
    ID:"label1", 
	height: 80, width: 80,
    contents: "Top Left",
	align: "center",
	backgroundColor: "#FFAAAA",
	border: "1px solid black",
    snapTo:"TL"
});

isc.Label.create({
    ID:"label2", 
	height: 80, width: 80,
    contents: "Top Right",
	align: "center",
	backgroundColor: "#BEC9FE",
	border: "1px solid black",
    snapTo:"TR"
});

isc.Label.create({
    ID: "label3", 
	height: 80, width: 80,
    contents: "Bottom Left",
	align: "center",
	backgroundColor: "#D8D5D6",
	border: "1px solid black",
    snapTo:"BL"
});

isc.Label.create({
    ID: "label4", 
	height: 80, width: 80,
    contents: "Bottom Right",
	align: "center",
	backgroundColor: "#F8BFFB",
	border: "1px solid black",
    snapTo:"BR"
});

isc.Label.create({
    ID: "label5", 
	height: 80, width: 80,
    contents: "Left",
	align: "center",
	backgroundColor: "#CCFFCC",
	border: "1px solid black",
    snapTo:"L"
});

isc.Label.create({
    ID: "label6", 
	height: 80, width: 80,
    contents: "Right",
	align: "center",
	backgroundColor: "#AB5654",
	border: "1px solid black",
    snapTo:"R"
});

isc.Label.create({
    ID: "label7", 
	height: 80, width: 80,
    contents: "Bottom",
	align: "center",
	backgroundColor: "#DCEFEF",
	border: "1px solid black",
    snapTo:"B"
});

isc.Label.create({
    ID: "label8", 
	height: 80, width: 80,
    contents: "Top",
	align: "center",
	backgroundColor: "#FFCC99",
	border: "1px solid black",
    snapTo:"T"
});

isc.Label.create({
    ID: "label9", 
	height: 80, width: 80,
    contents: "Center",
	align: "center",
	backgroundColor: "#FFFF99",
	border: "1px solid black",
	snapTo:"C"
});
isc.Canvas.create({
    ID:"snapLayout", 
    height: 300, width: 400,
	showEdges:true,
    edgeImage:"edges/custom/sharpframe_10.png",
    edgeSize:10,
	canDragResize: true,
	children: [
				label1,
				label2,
				label3,
				label4,
				label5,
				label6,
				label7,
				label8,
				label9
		]
});
// Example using Offset and snapEdge with snapTo 
isc.Label.create({
    ID:"label1a", 
	height: 80, width: 80,
    contents: "Top Left",
	showHover: true,
	prompt: "SnapOffsetLeft and Top by pixels",
	align: "center",
	backgroundColor: "#FFAAAA",
	border: "1px solid black",
    snapTo:"TL",
	snapOffsetTop: 10,
	snapOffsetLeft: 20
});

isc.Label.create({
    ID:"label2a", 
	height: 80, width: 80,
    contents: "Top Right",
	showHover: true,
	prompt: "SnapTo Top Right and snapEdge Top",
	align: "center",
	backgroundColor: "#BEC9FE",
	border: "1px solid black",
    snapTo:"TR",
	snapEdge: "T"
});

isc.Label.create({
    ID: "label3a", 
	height: 80, width: 80,
    contents: "Bottom Left",
	showHover: true,
	prompt: "snapTo Bottom Left with SnapOffsetLeft as pixels",
	align: "center",
	backgroundColor: "#D8D5D6",
	border: "1px solid black",
    snapTo:"BL",
	snapOffsetLeft: 5
});

isc.Label.create({
    ID: "label4a", 
	height: 80, width: 80,
    contents: "Left",
	showHover: true,
	prompt: "SnapTo Left with SnapEdge Left",
	align: "center",
	backgroundColor: "#CCFFCC",
	border: "1px solid black",
    snapTo:"L",
	snapEdge: "L"
});

isc.Label.create({
    ID: "label5a", 
	height: 80, width: 80,
    contents: "Right",
	showHover: true,
	prompt: "snapTo Right with SnapEdge Right",
	align: "center",
	backgroundColor: "#AB5654",
	border: "1px solid black",
    snapTo: "R",
	snapEdge: "R"
});

isc.Label.create({
    ID: "label6a", 
	height: 80, width: 80,
    contents: "Bottom",
	showHover: true,
	prompt: "Bottom with SnapEdge Bottom",
	align: "center",
	backgroundColor: "#DCEFEF",
	border: "1px solid black",
    snapTo:"B",
	snapEdge: "B"
});

isc.Label.create({
    ID: "label7a", 
	height: 80, width: 80,
    contents: "Top",
	showHover: true,
	prompt: "SnapOffsetTop using percentage",
	align: "center",
	backgroundColor: "#FFCC99",
	border: "1px solid black",
    snapTo:"T",
	snapOffsetTop: "5%"
});
isc.Label.create({
    ID: "label8a", 
	height: 80, width: 80,
    contents: "Bottom Right",
	showHover: true,
	prompt: "Bottom Right with SnapEdge Left",
	align: "center",
	backgroundColor: "#F8BFFB",
	border: "1px solid black",
    snapTo:"BR",
	snapEdge: "L"
});

isc.Canvas.create({
    ID:"offsetLayout", 
    height: 300, width: 400,
	showEdges:true,
    edgeImage:"edges/custom/sharpframe_10.png",
    edgeSize:10,
	canDragResize: true,
	children: [
				label1a,
				label2a,
				label3a,
				label4a,
				label5a,
				label6a,
				label7a,
				label8a
		]
});

isc.LayoutSpacer.create({
ID: "spacer",
width: 100
});

isc.HLayout.create({
ID:	"controlLayout",
membersMargin: 20,
height: 800, width: 1000,
members: [
	snapLayout,
	spacer,
	offsetLayout
	]
});