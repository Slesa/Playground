
isc.VLayout.create({width:"100%", height:"100%", members:[

    isc.HStack.create({height:50, layoutMargin:10, membersMargin:10, members:[
        isc.IButton.create({title:"Yahoo", click:"myPane.setContentsURL('http://www.yahoo.com')"}),
        isc.IButton.create({title:"Google", click:"myPane.setContentsURL('http://www.google.com')"}),
        isc.IButton.create({title:"Wikipedia", click:"myPane.setContentsURL('http://www.wikipedia.org')"}),
        isc.IButton.create({title:"Baidu", click:"myPane.setContentsURL('http://www.baidu.com')"})
    ]}),
    
    isc.HTMLPane.create({
        ID:"myPane",
        showEdges:true,
        contentsURL:"http://www.google.com/",
        contentsType:"page"
    })

]})
