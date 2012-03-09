
var _today = new Date;
var _start = _today.getDate() - _today.getDay();
var _month = _today.getMonth();
var _year = _today.getFullYear();
var eventData = [
{
    eventId: 1, 
    name: "Meeting",
    description: "Shareholders meeting: monthly forecast report",
    startDate: new Date(_year, _month, _start + 2, 9),
    endDate: new Date(_year, _month, _start + 2, 14)
},
{
    eventId: 2,
    name: "Realtor",
    description: "Breakfast with realtor to discuss moving plans",
    startDate: new Date(_year, _month, _start + 3, 8 ),
    endDate: new Date(_year, _month, _start + 3, 10)
},
{
    eventId: 3,
    name: "Soccer",
    description: "Little league soccer finals",
    startDate: new Date(_year, _month, _start + 4, 13),
    endDate: new Date(_year, _month, _start + 4, 16)
},
{
    eventId: 4, 
    name: "Sleep",
    description: "Catch up on sleep",
    startDate: new Date(_year, _month, _start + 4, 5),
    endDate: new Date(_year, _month, _start + 4, 9)
},
{
    eventId: 5,
    name: "Inspection",
    description: "Home inspector coming",
    startDate: new Date(_year, _month, _start + 4, 10),
    endDate: new Date(_year, _month, _start + 4, 12),
    eventWindowStyle: "testStyle",
    canEdit: false
},
{
    eventId: 6,
    name: "Airport run",
    description: "Pick James up from the airport",
    startDate: new Date(_year, _month, _start + 4, 1),
    endDate: new Date(_year, _month, _start + 4, 3)
},
{
    eventId: 7,
    name: "Dinner Party",
    description: "Prepare elaborate meal for friends",
    startDate: new Date(_year, _month, _start + 4, 17),
    endDate: new Date(_year, _month, _start + 4, 20)
},
{
    eventId: 8,
    name: "Poker",
    description: "Poker at Steve's house",
    startDate: new Date(_year, _month, _start + 4, 21),
    endDate: new Date(_year, _month, _start + 4, 23)
},
{
    eventId: 9,
    name: "Meeting",
    description: "Board of directors meeting: discussion of next months strategy",
    startDate: new Date(_year, _month, _start + 5, 11),
    endDate: new Date(_year, _month, _start + 5, 15)
}
];
