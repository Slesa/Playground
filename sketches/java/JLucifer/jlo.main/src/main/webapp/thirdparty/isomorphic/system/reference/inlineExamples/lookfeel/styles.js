exampleText = "When in the Course of human events, it becomes necessary for one people to dissolve the political bands which have connected them with another, and to assume among the powers of the earth, the separate and equal station to which the Laws of Nature and of Nature's God entitle them, a decent respect to the opinions of mankind requires that they should declare the causes which impel them to the separation."


isc.HTMLFlow.create({
    ID:"textBox",
    left:100, width:300,
    contents:exampleText,
    styleName:"exampleStyleOnline"
})

isc.FormLayout.create({
    items:[{
        type:"radioGroup",
        showTitle:false,
        valueMap:{
            "exampleStyleOnline":"Online",
            "exampleStyleLegal":"Legal",
            "exampleStyleCode":"Code",
            "exampleStyleInformal":"Informal"
        },
        defaultValue:"exampleStyleOnline",
        change:"textBox.setStyleName(value); textBox.markForRedraw()"
    }]
})

