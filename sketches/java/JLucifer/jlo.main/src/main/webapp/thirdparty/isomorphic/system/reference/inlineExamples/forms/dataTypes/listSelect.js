isc.DynamicForm.create({
    width: 500,
    fields: [{
        name: "shipTo", title: "Ship to", type: "select",
        hint: "<nobr>Overnight shipping available for countries in bold</nobr>",
        valueMap: {
            "US" : "<b>United States</b>",
            "CH" : "China",
            "JA" : "<b>Japan</b>",
            "IN" : "India",
            "GM" : "Germany",
            "FR" : "France",
            "IT" : "Italy",
            "RS" : "Russia",
            "BR" : "<b>Brazil</b>",
            "CA" : "Canada",
            "MX" : "Mexico",
            "SP" : "Spain"
        },
        imageURLPrefix:"flags/16/",
        imageURLSuffix:".png",
        valueIcons: {
            "US" : "US",
            "CH" : "CH",
            "JA" : "JA",
            "IN" : "IN",
            "GM" : "GM",
            "FR" : "FR",
            "IT" : "IT",
            "RS" : "RS",
            "BR" : "BR",
            "CA" : "CA",
            "MX" : "MX",
            "SP" : "SP"
        }
    }]
});
