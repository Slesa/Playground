isc.DynamicForm.create({
    ID:"testForm",
    width: 500,
   
    fields : [
    {
        name: "itemName", title: "Item Name", editorType: "comboBox", 
        optionDataSource: "supplyItem", 
        width: 250,
        pickListCellHeight: 50,
        pickListProperties: {
           
            formatCellValue : function (value, record, field, viewer) {               
               var descStr = record.description ? record.description : "[no descripton]";     
               var styleStr = "font-family:arial;font-size:11px;white-space:nowrap;overflow:hidden;";                                  
               var retStr = "<table>" + 
               "<tr><td ><span style='" + styleStr + "width:170px;float:left'>" + record.itemName + "<span></td>" +
               "<td align='right'><span style='" + styleStr + "width:50px;float:right;font-weight:bold'>" + record.unitCost + "<span></td></tr>" +
               "<tr><td colSpan=2><span style='" + styleStr + "width:220px;float:left'>" + descStr + "</span></td></tr></table>";
               return retStr;
            },
            canHover: true,
            showHover: true,
            cellHoverHTML : function (record) {
               
                return record.description ? record.description : "[no description]";    
            }
        }
    }]
});


