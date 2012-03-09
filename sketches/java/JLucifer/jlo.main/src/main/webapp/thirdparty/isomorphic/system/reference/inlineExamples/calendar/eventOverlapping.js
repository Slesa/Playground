isc.Calendar.create({
    ID: "eventCalendar", 
    data: eventOverlapData,
// the following are the Calendar's defaults and would still have been set without this code
    eventAutoArrange: true,
    eventOverlap: true,
    eventOverlapPercent: "10",             
    eventOverlapIdenticalStartTimes: false
});
