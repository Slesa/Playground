isc.ListGrid.create({
    ID: "countryList",
    width:500, height:250, top:50, alternateRecordStyles:true,
    dataSource: worldDSExport,
    autoFetchData: true,
    fields:[
        {name:"countryName", title:"Country"},
        {name:"capital", title:"Capital"},
        {name:"continent", title:"Continent"},
        {name:"independence", title:"Nationhood", 
            formatCellValue: function (value, record) {
                if (!value) return value;
                return value.toShortDate("toString");
            }
        },
        {name:"population", title:"Population", formatCellValue:"isc.Format.toUSString(value)"},
        {name:"gdp_percap", title:"GDP (per capita)",
            canEdit:false,
            align:"right",
            canSort: false,
            formatCellValue: function (value, record) {
                if (!isc.isA.Number(record.gdp) || !isc.isA.Number(record.population)) return "N/A";
                var gdpPerCapita = Math.round(record.gdp*1000000/record.population);
                return isc.Format.toUSDollarString(gdpPerCapita);
            },
            sortNormalizer: function (record) {
                return record.gdp/record.population;
            }
        }

    ],
    showFilterEditor: true
});

isc.DynamicForm.create({
    ID: "exportForm",
    width:300,
    fields: [
        { name: "exportType", title: "Export Type", type:"select", width:"*",
            defaultToFirstOption: true,
            valueMap: { 
                "csv" : "CSV" , 
                "xml" : "XML", 
                "json" : "JSON",
                "xls" : "XLS (Excel97)",
                "ooxml" : "OOXML (Excel2007)"
            }
        },
        { name: "showInWindow", title: "Show in Window", type: "boolean", align:"left" }
    ]
});

isc.Button.create({
   ID: "exportButton",
   title: "Export",
   left: 320,
   click: function () {
       var exportAs = exportForm.getField("exportType").getValue();
       var showInWindow = exportForm.getField("showInWindow").getValue();
       if (exportAs == "json") {
           // JSON exports are server-side only
           isc.say("For security reasons, client-sourced requests for JSON exports are not " +
                   "allowed.  Please use a different format.");
       } else {
           countryList.exportClientData({ exportAs: exportAs,
               exportDisplay: showInWindow ? "window" : "download", lineBreakStyle: "dos"
           });
       }
   }
});

