
var _today = new Date;
var _start = _today.getDate() - _today.getDay();
var _month = _today.getMonth();
var _year = _today.getFullYear();
var eventOverlapData = [
{
    eventId: 1, 
    name: "Meeting",
    description: "Shareholders meeting: monthly forecast report",
    startDate: new Date(_year, _month, _start + 3, 9),
    endDate: new Date(_year, _month, _start + 3, 14)
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
    startDate: new Date(_year, _month, _start + 4, 8),
    endDate: new Date(_year, _month, _start + 4, 12)
},
{
    eventId: 4, 
    name: "Sleep",
    description: "Catch up on sleep",
    startDate: new Date(_year, _month, _start + 4, 9),
    endDate: new Date(_year, _month, _start + 4, 11)
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
    name: "Dinner Party",
    description: "Prepare elaborate meal for friends",
    startDate: new Date(_year, _month, _start + 4, 11),
    endDate: new Date(_year, _month, _start + 4, 14)
},
{
    eventId: 7,
    name: "Poker",
    description: "Poker at Steve's house",
    startDate: new Date(_year, _month, _start + 4, 13),
    endDate: new Date(_year, _month, _start + 4, 16)
}
];
