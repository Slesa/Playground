isc.BatchUploader.create({
    ID:"uploader",
    height: 400,
    uploadDataSource: supplyItemHB,
    // BatchUploader supports extra fields, which are uploaded along with the file 
    // data and can be accessed from the HttpSession on the server side; here we
    // define some fields to be used in this way
    uploadFormFields: [
        { name: "stringValue", title: "String Value", type: "text" },
        { name: "numericValue", title: "Numeric Value", type: "number" }
    ],
    // We have limited vertical screen space when running in the Feature Explorer, 
    // so we force the embedded grid to be quite short in an attempt to avoid 
    // the need for page scrolling
    gridProperties: {
        height: 200
    }
});

isc.VStack.create({
    left:20,
    width:"70%",
    membersMargin:20,
    members:[

        isc.Label.create({
            ID:"helpText",
            contents: "<ul>" +
                "<li>Download the example <code>supplyItemTest.csv</code> file <a href=" + 
                isc.Page.getURL("[ISOMORPHIC]/system/reference") + 
                "/inlineExamples/serverExamples/other/batchUpload/supplyItemTest.csv>here</a>" +
                "<li>Click the 'Browse' button and use the file picker to select the " +
                "<code>supplyItemTest.csv</code> file that you just downloaded.</li>" +
                "<li>The BatchUploader will upload and validate the contents of that CSV file against the " +
                "DataSource declared on the BatchUploader, which in this case is supplyItemHB.</li>" +
                "<li>Validated data will then be streamed back down to the client and displayed in an " +
                "editable ListGrid, so you can review and correct errors.</li>" +
                "<li>Click 'Commit' to save the data back to the DataSource's persistent store (in this " +
                "case, a database table accessed via Hibernate).</li>" +
                "<li>This end-to-end functionality is encapsulated by the BatchUploader, and requires " + 
                "no application code.</li></ul>"
        }),
        
        uploader

    ]
});

