isc.ListGrid.create({
    width: 550,
    height: 224,
    showAllRecords: true,  
    canResizeFields: true,

    showRecordComponents: true,    
    showRecordComponentsByCell: true,  
    canRemoveRecords: true,  

    data: countryData,

    fields: [
        { name: "countryCode", type: "image", title: "Flag", width: 40, align: "center",
            imageURLPrefix: "flags/16/", imageURLSuffix: ".png" 
        },
        { name: "countryName", title: "Country" },
        { name: "capital", title: "Capital" },
        { name: "continent", title: "Continent" },
        { name: "buttonField", title: "Info", align: "center" },
        { name: "iconField", title: "Comments/Stats", width: 100 }
    ],

    createRecordComponent : function (record, colNum) {  
        var fieldName = this.getFieldName(colNum);  

        if (fieldName == "iconField") {  
            var recordCanvas = isc.HLayout.create({
                height: 22,
                align: "center"
            });

            var editImg = isc.ImgButton.create({
                showDown: false,
                showRollOver: false,
                layoutAlign: "center",
                src: "icons/16/comment_edit.png",
                prompt: "Edit Comments",
                height: 16,
                width: 16,
                grid: this,
                click : function () {
                    isc.say("Edit Comment Icon Clicked for country : " + record["countryName"]);  
                }
            });

            var chartImg = isc.ImgButton.create({
                showDown: false,
                showRollOver: false,
                layoutAlign: "center",
                src: "icons/16/chart_bar.png",
                prompt: "View Chart",
                height: 16,
                width: 16,
                grid: this,
                click : function () {
                    isc.say("Chart Icon Clicked for country : " + record["countryName"]);  
                }
            });

            recordCanvas.addMember(editImg);  
            recordCanvas.addMember(chartImg);  
            return recordCanvas;  
        } else if (fieldName == "buttonField") {  
            var button = isc.IButton.create({
                height: 18,
                width: 65,
                icon: "flags/16/" + record["countryCode"] + ".png",
                title: "Info",
                click : function () {
                    isc.say(record["countryName"] + " info button clicked.");
                }
            });
            return button;  
        } else {  
            return null;  
        }  
    }

});

