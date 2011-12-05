isc.Img.create({
    left:50, top:50, width:200, height:200, overflow:"hidden",
    showEdges:true, padding:20,
    src:"other/cpu.jpg", imageType:"normal",
    canDrag: true,
    cursor: "all-scroll",
    dragAppearance: "none",
    dragStart: function () {
        this.startScrollLeft = this.getScrollLeft();
        this.startScrollTop = this.getScrollTop();
    },
    dragMove: function () {
        this.scrollTo(
            this.startScrollLeft - isc.Event.lastEvent.x + isc.Event.mouseDownEvent.x,
            this.startScrollTop - isc.Event.lastEvent.y + isc.Event.mouseDownEvent.y
        )
    }
})
