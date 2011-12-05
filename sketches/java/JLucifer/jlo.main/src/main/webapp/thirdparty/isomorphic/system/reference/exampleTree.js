isc.ExampleTree.create({
    ID:"exampleTree",
    openProperty:"isOpen",
    nodeVisibility:"sdk",
    root:{
        name:"root/",
        children:[
            {
                icon:"[ISO_DOCS_SKIN]/images/silkicons/house.png",
                id:"Welcome",
                isOpen:true,
                screenshot:"screenshots/tabs_ds_code.png",
                screenshotHeight:"176",
                screenshotWidth:"291",
                showSkinSwitcher:"true",
                title:"Featured Samples",
                description:"\n    Welcome to the SmartClient Feature Tour!\n    <BR>\n    <BR>\n    Click on the name of an example in the tree on the left to load it.\n    <BR>\n    <BR>\n    With an example loaded, you can view source code by clicking on the tabs shown above the\n    running example.\n    <BR>\n    <BR>\n    For an overview of how to use this Feature Explorer, including specific instructions for\n    using code shown here in a standalone application, please see the <a target=_blank\n    href='${isc.ExampleViewer.getRefDocsURL()}#featureExplorerOverview'>\n    Feature Explorer Overview</a> topic in the Reference Docs.\n    \n      \n\n",
                children:[
                    {
                        jsURL:"welcome/helloButton.js",
                        title:"Hello World",
                        xmlURL:"welcome/helloButton.xml",
                        description:"\n        A SmartClient <code>IButton</code> component responds to mouse clicks by showing a\n        modal <code>Dialog</code> component with the \"Hello world!\" message.  Source code is\n        provided in both XML and JS formats.\n        "
                    },
                    {
                        jsURL:"welcome/helloStyled.js",
                        title:"Hello World (styling)",
                        visibility:"sdk",
                        xmlURL:"welcome/helloStyled.xml",
                        tabs:[
                            {
                                title:"CSS",
                                url:"welcome/helloStyled.css"
                            }
                        ],
                        description:"\n        This <code>Label</code> component is heavily styled with a combination of CSS class,\n        CSS attribute shortcuts, and SmartClient attributes.  Source code is\n        provided in both XML and JS formats.\n        "
                    },
                    {
                        jsURL:"welcome/helloForm.js",
                        title:"Hello You (form)",
                        visibility:"sdk",
                        xmlURL:"welcome/helloForm.xml",
                        description:"\n        This SmartClient <code>FormLayout</code> provides a text field and a button control.\n        Type your name in the field, then click the button for a personalized message.\n        Source code is provided in both XML and JS formats.\n        "
                    },
                    {
                        icon:"[ISO_DOCS_SKIN]/images/silkicons/cal.png",
                        ref:"databoundCalendar",
                        title:"Databound Calendar"
                    },
                    {
                        icon:"[ISO_DOCS_SKIN]/images/silkicons/application_put.png",
                        ref:"fetchOperation",
                        title:"Live Grid",
                        description:"\n        <p>Rows are fetched automatically as the user drags the scrollbar. Drag the \n        scrollbar quickly to the bottom to fetch a range near the end (a prompt will appear \n        during server fetch).</p>\n        <p>Scroll slowly back up to fill in the middle.</p>\n        <p>Another key unique feature of SmartClient is lazy rendering of <b>columns</b>. Most \n        browsers cannot handle displaying a large number of column and have serious \n        performance issues. SmartClient, however, does not render all columns outside the \n        visible area by default and only renders them as you scroll horizontally. You can \n        however disable this feature if desired.</p>\n        <p>You can control how far ahead of the currently visible area rows should be \n        rendered. This is expressed as a ratio from viewport size to rendered area size. \n        The default is 1.3.</p>\n        <p>Tweaking drawAheadRatio allows you to make tradeoffs between continuous \n        scrolling speed vs initial render time and render time when scrolling by large \n        amounts.</p> "
                    },
                    {
                        icon:"[ISO_DOCS_SKIN]/images/silkicons/application_view_detail.png",
                        ref:"adaptiveFilter",
                        title:"Adaptive Filter"
                    },
                    {
                        icon:"[ISO_DOCS_SKIN]/images/silkicons/sc_insertformula.png",
                        ref:"filterBuilderBracket",
                        title:"Advanced Filter"
                    },
                    {
                        icon:"[ISO_DOCS_SKIN]/images/silkicons/application_view_detail.png",
                        ref:"dynamicFreeze",
                        title:"Dynamic Frozen Columns"
                    },
                    {
                        icon:"[ISO_DOCS_SKIN]/images/silkicons/application_side_tree.png",
                        ref:"userDefinedHilites",
                        title:"User-Defined Hilites"
                    },
                    {
                        icon:"[ISO_DOCS_SKIN]/images/silkicons/application_side_tree.png",
                        ref:"dynamicGrouping",
                        title:"Dynamic Grouping"
                    },
                    {
                        icon:"[ISO_DOCS_SKIN]/images/silkicons/application_side_tree.png",
                        ref:"summaryGrid",
                        title:"Grid Summaries"
                    },
                    {
                        icon:"[ISO_DOCS_SKIN]/images/silkicons/arrow_out.png",
                        ref:"massUpdate",
                        title:"Mass Update"
                    },
                    {
                        icon:"[ISO_DOCS_SKIN]/images/silkicons/application_split.png",
                        ref:"expansionRelatedRecords",
                        title:"Expanding Rows"
                    },
                    {
                        icon:"[ISO_DOCS_SKIN]/images/silkicons/application_view_columns.png",
                        ref:"databoundDependentSelects",
                        title:"Dependent Selects (Grid)"
                    },
                    {
                        icon:"[ISO_DOCS_SKIN]/images/silkicons/application_view_columns.png",
                        ref:"formDependentSelects",
                        title:"Dependent Selects (Form)"
                    },
                    {
                        icon:"[ISO_DOCS_SKIN]/images/silkicons/application_view_columns.png",
                        ref:"filterRelated",
                        title:"Filter Related Records"
                    },
                    {
                        icon:"[ISO_DOCS_SKIN]/images/silkicons/database_link.png",
                        ref:"databoundDragCopy",
                        title:"Databound Dragging"
                    },
                    {
                        dataSource:"supplyCategory",
                        fullScreen:"true",
                        icon:"[ISO_DOCS_SKIN]/images/silkicons/application_osx.png",
                        id:"showcaseApp",
                        jsURL:"demoApp/demoAppJS.js",
                        needServer:"true",
                        screenshot:"demoApp/demoApp.png",
                        screenshotHeight:"337",
                        screenshotWidth:"480",
                        title:"Complete Application",
                        xmlURL:"demoApp/demoAppXML.xml",
                        tabs:[
                            {
                                title:"supplyItem",
                                url:"supplyItem.ds.xml"
                            }
                        ],
                        description:"Demonstrates a range of SmartClient GUI components, data binding operations,\n        and layout managers in a single-page application.\n        "
                    },
                    {
                        isOpen:true,
                        ref:"serverExamples",
                        title:"Server Examples"
                    },
                    {
                        icon:"[ISO_DOCS_SKIN]/images/silkicons/application_view_tile.png",
                        ref:"tilingFilter",
                        title:"Tile Sort & Filter"
                    },
                    {
                        icon:"[ISO_DOCS_SKIN]/images/silkicons/cube_simple.png",
                        ref:"basicCube",
                        title:"Simple Cube"
                    },
                    {
                        icon:"[ISO_DOCS_SKIN]/images/silkicons/cube_simple.png",
                        ref:"analytics",
                        title:"Advanced Cube"
                    },
                    {
                        external:true,
                        externalWindowConfig:"height=675,width=800,toolbar=no,resizable=no",
                        id:"DrawingPreview",
                        requiresModules:"Drawing",
                        screenshot:"drawing/drawing_screenshot.png",
                        screenshotHeight:"336",
                        screenshotWidth:"386",
                        title:"Drawing Preview",
                        url:"drawing/Drawing.html",
                        description:"Click on the buttons at the top to create (or erase) shapes using\n       the SmartClient drawing module. Drawn objects may be manipulated at runtime - drag the\n       control points, or change the control form values to update them.\n       <P>\n       This is a <span style=\"color:red\">preview</span> of unreleased SmartClient functionality.  \n       <a href=\"http://www.smartclient.com/company/contact.jsp\">Contact\n       Isomorphic</a> to get\n       early access to this technology.\n       "
                    },
                    {
                        icon:"[ISO_DOCS_SKIN]/images/silkicons/world.png",
                        ref:"portal",
                        title:"Portal Preview"
                    },
                    {
                        icon:"[ISO_DOCS_SKIN]/images/silkicons/printer.png",
                        ref:"printing",
                        title:"Printing"
                    },
                    {
                        descriptionHeight:"150",
                        id:"offlineSupport",
                        jsURL:"offline/offlineSupport.js",
                        title:"Offline support",
                        tabs:[
                            {
                                dataSource:"supplyItem",
                                name:"supplyItem"
                            },
                            {
                                dataSource:"supplyCategory",
                                name:"supplyCategory"
                            }
                        ],
                        description:"\n        SmartClient has support for caching server responses in browser storage, allowing\n        these cached responses to be returned to an application at some future point when \n        the application is offline.  Offline support is automatic once enabled - if the user\n        switches the application or browser into offline mode, or the browser detects that it\n        is offline, the framework automatically and transparently starts returning cached \n        responses whenever it can (application code can determine that responses have come \n        from offline cache if necessary)<p>\n        Use the tree to navigate categories; click a category to load the grid with\n        matching items.  Now reload the page and click \"Go offline\" (or switch your\n        browser into offline mode).  If you click a category that you selected before\n        the reload, you will see that the grid is still populated from the Offline cache; if \n        you click a category you did not previously select, or attempt to open a node\n        in the tree that you did not previously open, you will get the \"Data not available\n        while offline\" message.\n        "
                    },
                    {
                        descriptionHeight:"150",
                        id:"offlinePrefs",
                        jsURL:"offline/offlinePrefs.js",
                        title:"Offline preferences",
                        tabs:[
                            {
                                canEdit:"false",
                                title:"countryDS",
                                url:"grids/ds/countrySQLDS.ds.xml"
                            }
                        ],
                        description:"\n        SmartClient provides a unified Offline browser storage API that your programs can use \n        for any client-side persistence task.  In this example, we store the ListGrid's \n        viewState to browser-local storage. Try resizing or reordering some columns in the\n        grid, click \"Persist State\", then press F5 to reload, or close and re-open the browser;\n        your changes have been remembered.  Try adding a formula field to the grid and reload \n        again. Persisting a user's preferences like this is a compelling addition to any application, \n        and in this case we don't even need a server.<p>\n        Offline support is provided in modern HTML5 browsers, and also in older versions of \n        Internet Explorer (6 and greater); the underlying technologies used are very different,\n        but the SmartClient API you use is the same regardless of browser.\n        "
                    },
                    {
                        icon:"[ISO_DOCS_SKIN]/images/silkicons/database_table.png",
                        ref:"patternReuse",
                        title:"Pattern Reuse"
                    },
                    {
                        icon:"[ISO_DOCS_SKIN]/images/silkicons/arrow_in.png",
                        ref:"xmlSchemaImport",
                        title:"Metadata Import"
                    },
                    {
                        icon:"[ISO_DOCS_SKIN]/images/silkicons/bug.png",
                        jsURL:"devConsole/devConsole.js",
                        showSkinSwitcher:false,
                        showSource:false,
                        title:"Developer Console",
                        description:"\nThe Developer Console is a suite of development tools implemented in SmartClient itself.  The\nConsole runs in its own browser window, parallel to your running application, so it is always\navailable: in every browser, and in every deployment environment.<BR> \nClick on the name of a screenshot below to see more information about developer\nconsole features.\n        "
                    },
                    {
                        jsURL:"docs/docs.js",
                        showSkinSwitcher:false,
                        showSource:false,
                        title:"SmartClient Docs",
                        description:"\n        SmartClient contains over 100 documented components with more than 2000 documented,\n        supported APIs.  All of SmartClient's documentation is integrated into a\n        SmartClient-based, searchable documentation browser, including API reference, concepts,\n        tutorials, live examples, architectural blueprints and deployment instructions.\n        "
                    }
                ]
            },
            {
                icon:"[ISO_DOCS_SKIN]/images/silkicons/application_view_columns.png",
                isOpen:false,
                showSkinSwitcher:"true",
                title:"Grids",
                description:"\n    High-performance interactive data grids.\n",
                children:[
                    {
                        isOpen:false,
                        title:"Appearance",
                        description:"\n    Styling, sizing and formatting options for grids, as well as built-in end user controls.\n",
                        children:[
                            {
                                id:"columnOrder",
                                jsURL:"grids/layout/columnOrder.js",
                                title:"Column order",
                                tabs:[
                                    {
                                        title:"countryData",
                                        url:"grids/data/countryData.js"
                                    }
                                ],
                                description:"\n        Drag and drop the column headers to rearrange columns in the grid.\n        Right-click the column headers to hide or show columns.\n        Click the buttons to hide or show the \"Capital\" column.\n        "
                            },
                            {
                                id:"columnSize",
                                jsURL:"grids/layout/columnSize.js",
                                title:"Column size",
                                tabs:[
                                    {
                                        title:"countryData",
                                        url:"grids/data/countryData.js"
                                    }
                                ],
                                description:"\n        Click and drag between the column headers to resize columns in the grid.\n        "
                            },
                            {
                                jsURL:"grids/layout/columnAlign.js",
                                title:"Column align",
                                visibility:"sdk",
                                tabs:[
                                    {
                                        title:"countryData",
                                        url:"grids/data/countryData.js"
                                    }
                                ],
                                description:"\n        Click the radio buttons to change the alignment of the \"Flag\" column.\n        "
                            },
                            {
                                jsURL:"grids/layout/columnHeaders.js",
                                title:"Column headers",
                                visibility:"sdk",
                                tabs:[
                                    {
                                        title:"countryData",
                                        url:"grids/data/countryData.js"
                                    }
                                ],
                                description:"\n        Click the buttons to show or hide the column headers.\n        "
                            },
                            {
                                jsURL:"grids/layout/columnTitles.js",
                                title:"Column titles",
                                visibility:"sdk",
                                tabs:[
                                    {
                                        title:"countryData",
                                        url:"grids/data/countryData.js"
                                    }
                                ],
                                description:"\n        Click the buttons to change the title of the \"Country\" column.\n        "
                            },
                            {
                                id:"multilineValues",
                                jsURL:"grids/layout/multiLineValues.js",
                                title:"Multiline values",
                                tabs:[
                                    {
                                        title:"countryData",
                                        url:"grids/data/countryDataDetail.js"
                                    }
                                ],
                                description:"\n        Click and drag between the \"Background\" and \"Flag\" column headers, or resize your browser\n        window to change the size of the entire grid. The \"Background\" values are\n        confined to a fixed row height.\n        "
                            },
                            {
                                ref:"headerSpans",
                                title:"Header Spans"
                            },
                            {
                                jsURL:"grids/formatting/cellStyles.js",
                                title:"Cell styles",
                                tabs:[
                                    {
                                        title:"CSS",
                                        url:"grids/formatting/cellStyles.css"
                                    },
                                    {
                                        title:"countryData",
                                        url:"grids/data/countryData.js"
                                    }
                                ],
                                description:"\n        Mouse over the rows and click-drag to select rows, to see the effects of different\n        base styles on these two grids.\n        "
                            },
                            {
                                id:"addStyle",
                                jsURL:"grids/formatting/addStyle.js",
                                title:"Hilite cells (add style)",
                                tabs:[
                                    {
                                        title:"countryData",
                                        url:"grids/data/countryData.js"
                                    }
                                ],
                                description:"\n        This grid hilites \"Population\" values greater than 1 billion or less than 50 million\n        using additive style attributes (text color and weight).\n        "
                            },
                            {
                                id:"replaceStyle",
                                jsURL:"grids/formatting/replaceStyle.js",
                                title:"Hilite cells (replace style)",
                                tabs:[
                                    {
                                        title:"CSS",
                                        url:"grids/formatting/replaceStyle.css"
                                    },
                                    {
                                        title:"countryData",
                                        url:"grids/data/countryData.js"
                                    }
                                ],
                                description:"\n        This grid hilites \"Population\" values greater than 1 billion or less than 50 million\n        using a full set of compound styles (with customized background colors). Mouse over or\n        click-drag rows to see how these styles apply to different row states.\n        "
                            },
                            {
                                jsURL:"grids/formatting/roundedSelection.js",
                                title:"Rounded Selection",
                                tabs:[
                                    {
                                        title:"CSS",
                                        url:"grids/formatting/simpleCellStyles.css"
                                    },
                                    {
                                        title:"countryData",
                                        url:"grids/data/countryData.js"
                                    }
                                ],
                                description:"\n        Select the rows to see a custom selection effect with rounded edges achieved via the\n        selectionCanvas subsystem.\n        ",
                                badSkins:"BlackOps",
                                bestSkin:"TreeFrog"
                            },
                            {
                                jsURL:"grids/formatting/animatedSelection_RollOver.js",
                                title:"Animated Selection and RollOvers",
                                tabs:[
                                    {
                                        title:"CSS",
                                        url:"grids/formatting/simpleCellStyles.css"
                                    },
                                    {
                                        title:"countryData",
                                        url:"grids/data/countryData.js"
                                    }
                                ],
                                description:"\n        RollOver, and select the rows in the grid to see rollover and selection indicators fade\n        into view. This is achieved via the rollOverCanvas and selectionCanvas subsystem. \n        Note that the opacity setting on the rollUnderCanvas allows true color layering.\n        "
                            },
                            {
                                jsURL:"grids/formatting/rollOverRecticle.js",
                                title:"RollOver Reticle Effect",
                                tabs:[
                                    {
                                        title:"countryData",
                                        url:"grids/data/countryData.js"
                                    }
                                ],
                                description:"\n        RollOver the rows in the grid to see a custom roll over recticle effect, \n        achieved via the rollOverCanvas subsystem.\n        ",
                                badSkins:"BlackOps",
                                bestSkin:"TreeFrog"
                            },
                            {
                                jsURL:"grids/formatting/rollOverControls.js",
                                title:"RollOver Controls",
                                tabs:[
                                    {
                                        title:"countryData",
                                        url:"grids/data/countryData.js"
                                    }
                                ],
                                description:"\n        RollOver the rows in the grid to row-level controls buttons appear embedded in the row.\n        This example utilizes the rollOverCanvas subsystem to achieve this effect. \n        "
                            },
                            {
                                id:"formatValues",
                                jsURL:"grids/formatting/formatValues.js",
                                title:"Format values",
                                tabs:[
                                    {
                                        title:"countryData",
                                        url:"grids/data/countryData.js"
                                    }
                                ],
                                description:"\n        This grid applies custom formatters to the \"Nationhood\" and \"Area\" columns.\n        Click on the \"Nationhood\" or \"Area\" column headers to sort the underlying data values.\n        "
                            },
                            {
                                id:"emptyValues",
                                jsURL:"grids/formatting/emptyValues.js",
                                title:"Empty values",
                                tabs:[
                                    {
                                        title:"countryData",
                                        url:"grids/data/countryData.js"
                                    }
                                ],
                                description:"\n        Double-click any cell, delete its value, and press Enter or click outside the cell to\n        save and display the empty value. This grid shows \"--\" for empty date values, and\n        \"unknown\" for other empty values.\n        "
                            },
                            {
                                id:"emptyGrid",
                                jsURL:"grids/layout/emptyGrid.js",
                                title:"Empty grid",
                                tabs:[
                                    {
                                        title:"countryData",
                                        url:"grids/data/countryData.js"
                                    }
                                ],
                                description:"\n        Click the buttons to add or remove all data in the grid.\n        "
                            }
                        ]
                    },
                    {
                        isOpen:false,
                        title:"Interaction",
                        description:"\n    Selection and drag and drop of data, hovers, and grid events.\n",
                        children:[
                            {
                                jsURL:"grids/interaction/rollover.js",
                                title:"Rollover",
                                visibility:"sdk",
                                tabs:[
                                    {
                                        title:"countryData",
                                        url:"grids/data/countryData.js"
                                    }
                                ],
                                description:"\n        Move the mouse over rows in the grid to see rollover highlights.\n        Click the buttons to enable or disable this behavior.\n        "
                            },
                            {
                                jsURL:"grids/selection/singleSelect.js",
                                title:"Single select",
                                visibility:"sdk",
                                tabs:[
                                    {
                                        title:"countryData",
                                        url:"grids/data/countryData.js"
                                    }
                                ],
                                description:"\n        Click to select any single row in the grid.\n        "
                            },
                            {
                                id:"multipleSelect",
                                jsURL:"grids/selection/multipleSelect.js",
                                title:"Multiple select",
                                tabs:[
                                    {
                                        title:"countryData",
                                        url:"grids/data/countryData.js"
                                    }
                                ],
                                description:"\n        Click to select a single row in the grid. Shift-click to select a continuous range of rows.\n        Ctrl-click to add or remove individual rows from the selection.\n        "
                            },
                            {
                                jsURL:"grids/selection/simpleSelect.js",
                                title:"Simple select",
                                tabs:[
                                    {
                                        title:"countryData",
                                        url:"grids/data/countryData.js"
                                    }
                                ],
                                description:"\n        Click to select or deselect any row in the grid.\n        "
                            },
                            {
                                jsURL:"grids/selection/checkboxSelect.js",
                                title:"Checkbox Select",
                                tabs:[
                                    {
                                        title:"countryData",
                                        url:"grids/data/countryData.js"
                                    }
                                ],
                                description:"\n        By setting selectionAppearance to \"checkbox\", the ListGrid can use checkboxes \n        to indicate the selected state of records. Only by clicking on a checkbox will the \n        corresponding record be selected or unselected.\n        "
                            },
                            {
                                jsURL:"grids/selection/dragSelect.js",
                                title:"Drag select",
                                tabs:[
                                    {
                                        title:"countryData",
                                        url:"grids/data/countryData.js"
                                    }
                                ],
                                description:"\n        Click and drag to select a continuous range of rows in the grid.\n        "
                            },
                            {
                                id:"valueHoverTips",
                                jsURL:"grids/interaction/valueHover.js",
                                title:"Value hover tips",
                                tabs:[
                                    {
                                        title:"countryData",
                                        url:"grids/data/countryData.js"
                                    }
                                ],
                                description:"\n        Move the mouse over a value in the \"Government\" column and pause (hover) for a\n        longer description of that value.\n        "
                            },
                            {
                                jsURL:"grids/interaction/headerHover.js",
                                title:"Header hover tips",
                                tabs:[
                                    {
                                        title:"countryData",
                                        url:"grids/data/countryData.js"
                                    }
                                ],
                                description:"\n        Move the mouse over a column header and pause (hover) for a longer description\n        of that column.\n        "
                            },
                            {
                                id:"gridsDragReorder",
                                jsURL:"grids/interaction/dragOrder.js",
                                title:"Drag reorder",
                                tabs:[
                                    {
                                        title:"countryData",
                                        url:"grids/data/countryData.js"
                                    }
                                ],
                                description:"\n        Drag and drop to change the order of countries in this list.\n        "
                            },
                            {
                                id:"gridsDragMove",
                                jsURL:"grids/interaction/dragMove.js",
                                title:"Drag move",
                                tabs:[
                                    {
                                        title:"countryData",
                                        url:"grids/data/countryData.js"
                                    }
                                ],
                                description:"\n        Drag and drop to move rows between the two lists. \n        "
                            },
                            {
                                id:"gridsDragCopy",
                                jsURL:"grids/interaction/dragCopy.js",
                                title:"Drag copy",
                                tabs:[
                                    {
                                        title:"countryData",
                                        url:"grids/data/countryData.js"
                                    }
                                ],
                                description:"\n        Drag and drop to copy rows from the first list to the second list.\n        "
                            },
                            {
                                id:"disabledRows",
                                jsURL:"grids/interaction/disabled.js",
                                title:"Disabled rows",
                                tabs:[
                                    {
                                        title:"countryData",
                                        url:"grids/data/countryData.js"
                                    }
                                ],
                                description:"\n        Mouse over, drag, or click on any values in this grid.\n        All \"Europe\" country records in this grid are disabled.\n        "
                            },
                            {
                                id:"recordClicks",
                                jsURL:"grids/interaction/recordClicks.js",
                                title:"Record clicks",
                                tabs:[
                                    {
                                        title:"countryData",
                                        url:"grids/data/countryData.js"
                                    }
                                ],
                                description:"\n        Click, double-click, or right-click any row in the grid.\n        "
                            },
                            {
                                id:"cellClicks",
                                jsURL:"grids/interaction/cellClicks.js",
                                title:"Cell clicks",
                                tabs:[
                                    {
                                        title:"countryData",
                                        url:"grids/data/countryData.js"
                                    }
                                ],
                                description:"\n        Click, double-click, or right-click any value in the grid.\n        "
                            }
                        ]
                    },
                    {
                        isOpen:false,
                        title:"Autofit",
                        description:"\n    Various auto-fit behaviors built-in to every Grid\n",
                        children:[
                            {
                                id:"autoFitFreeSpace",
                                jsURL:"grids/autofit/autoFitFreeSpace.js",
                                title:"Free Space",
                                tabs:[
                                    {
                                        title:"supplyItem",
                                        url:"supplyItem.ds.xml"
                                    }
                                ],
                                description:"\n        By default, grids used in a Layout will fill available space, allowing users to show \n        and hide other components on the screen in order to view and interact with more data \n        at once in the grid.  Imagine that the blue outline represents all the space that is\n        available for this interface.  Click on the \"Details\" header to hide the tabs and\n        reveal more rows.  Click on the resizebar next to the Navigation tree to hide it,\n        allowing more space for columns.\n        "
                            },
                            {
                                id:"autofitValues",
                                jsURL:"grids/autofit/autoFitValues.js",
                                title:"Cell Values",
                                tabs:[
                                    {
                                        title:"countryData",
                                        url:"grids/data/countryDataDetail.js"
                                    }
                                ],
                                description:"\n        Click and drag between the \"Background\" and \"Flag\" column headers, or resize your browser\n        window to change the size of the entire grid. The rows resize to fit\n        the \"Background\" values.\n        "
                            },
                            {
                                id:"autofitRows",
                                jsURL:"grids/autofit/autoFitRows.js",
                                title:"Rows",
                                tabs:[
                                    {
                                        title:"countryData",
                                        url:"grids/data/countryData.js"
                                    }
                                ],
                                description:"\n        Click the buttons to show different numbers of records. The grid resizes to fit\n        all rows without scrolling.\n        "
                            },
                            {
                                id:"autofitColumns",
                                jsURL:"grids/autofit/autoFitColumns.js",
                                title:"Columns",
                                tabs:[
                                    {
                                        title:"countryData",
                                        url:"grids/data/countryData.js"
                                    }
                                ],
                                description:"\n        Click and drag between the column headers to resize the columns. The grid resizes to\n        fit the new column widths.  The width setting on the grid as a whole acts as a minimum.\n        "
                            },
                            {
                                id:"autofitColumnWidths",
                                jsURL:"grids/autofit/autoFitColumnWidths.js",
                                title:"Column Widths",
                                tabs:[
                                    {
                                        title:"countryData",
                                        url:"grids/data/countryData.js"
                                    }
                                ],
                                description:"\n        ListGrid fields can be set to auto-fit to their titles and/or field values. In this\n        example the first two columns are set to auto-fit when the grid is drawn. The first\n        field's title exceeds the space used by its values, and the second field the reverse\n        is true. In both cases the column is correctly sized to fit its content.\n        Note that the user can also perform one time auto-fit of columns at runtime by\n        double-clicking on any header or using\n        the context-menu option.\n        "
                            },
                            {
                                id:"autofitNewRecords",
                                jsURL:"grids/autofit/autoFitNewRecords.js",
                                title:"New Records",
                                description:"\n        Autofit to rows can be made subject to a maximum. Add new rows to the grid, and note that the\n        grid expands to show the new rows. This grid is configured to stop expanding once you have more\n        than 5 rows, and begin scrolling instead.\n        "
                            },
                            {
                                id:"autofitFilter",
                                jsURL:"grids/autofit/autoFitFilter.js",
                                title:"Filter",
                                tabs:[
                                    {
                                        canEdit:"false",
                                        title:"worldDS",
                                        url:"grids/ds/worldSQLDS.ds.xml"
                                    }
                                ],
                                description:"\n        Change the filter to show the grid resizing within the constraint of its maximum \n        autofit rows. Enter a country filter of \"cook island\" to see the grid shrink down to minimum\n        size. Change the country filter to \"island\" to show the grid at almost maximum size, but\n        not scrolling.  Change the country filter to \"land\" to show the grid scrolling because\n        its maximum autofit size (10) isn't large enough to display all rows.\n        "
                            }
                        ]
                    },
                    {
                        isOpen:false,
                        title:"Filtering",
                        description:"\n    SmartClient grids provide interactive filtering of standard and custom data types,\n    with automatic client/server coordination.\n",
                        children:[
                            {
                                id:"filter",
                                jsURL:"grids/filtering/filter.js",
                                title:"Filter",
                                tabs:[
                                    {
                                        canEdit:"false",
                                        title:"worldDS",
                                        url:"grids/ds/worldSQLDS.ds.xml"
                                    }
                                ],
                                description:"\n        Type \"island\" above the Country column, then press Enter or click the filter button\n        (top-right corner of the grid) to show only countries with \"island\" in their name.\n        Select \"North America\" above the Continent column to filter countries by that continent.\n        "
                            },
                            {
                                id:"liveFilter",
                                jsURL:"grids/filtering/liveFilter.js",
                                title:"Live Filter",
                                tabs:[
                                    {
                                        canEdit:"false",
                                        title:"worldDS",
                                        url:"grids/ds/worldSQLDS.ds.xml"
                                    }
                                ],
                                description:"\n        Begin typing a country name into in the filter box for the Country column.  Grids can \n        be configured to filter as you type.        \n        "
                            },
                            {
                                descriptionHeight:"165",
                                id:"adaptiveFilter",
                                jsURL:"grids/filtering/adaptiveFilter.js",
                                title:"Adaptive Filter",
                                tabs:[
                                    {
                                        title:"supplyItem",
                                        url:"supplyItem.ds.xml"
                                    }
                                ],
                                description:"\n        SmartClient combines large dataset handling with <b>adaptive</b> use of\n        client-side filtering.  Begin typing an Item name in the filter box above the \"Item\"\n        column (for example, enter \"add\").  When the dataset becomes small enough, SmartClient\n        switches to client-side filtering automatically - enter more letters, or criteria on\n        other columns, to see this.  The label underneath the grid flashes briefly\n        every time SmartClient needs to visit the server.\n        <P>\n        Delete part of the item name to see SmartClient automatically switch back to\n        server-side filtering when necessary.  \n        <P>\n        Adaptive filtering eliminates up to 90% of the most costly types of server contact\n        (searching through large datasets), <b>dramatically improving responsiveness and\n        scalability</b>.\n        "
                            },
                            {
                                id:"advancedFilter",
                                jsURL:"grids/filtering/advancedFilter.js",
                                title:"Advanced Filter",
                                tabs:[
                                    {
                                        canEdit:"false",
                                        title:"worldDS",
                                        url:"grids/ds/worldSQLDS.ds.xml"
                                    }
                                ],
                                description:"\n        SmartClient's Advanced Filter feature allows you to create complex, multi-condition \n        filters.  Ordinary DynamicForms can be used to generate AdvancedCriteria objects, as we\n        are doing here, simply by specifying a valid \"operator\" on one or more of the form fields.  \n        "
                            },
                            {
                                id:"filterBuilder",
                                jsURL:"grids/filtering/filterBuilder.js",
                                title:"Custom Filter",
                                tabs:[
                                    {
                                        canEdit:"false",
                                        title:"worldDS",
                                        url:"grids/ds/worldSQLDS.ds.xml"
                                    }
                                ],
                                description:"\n        Use the FilterBuilder to construct custom queries that combine multiple criteria across\n        any field in your DataSource.  Note that the operator\n        select list only shows operators suitable for the field selected, and the comparison\n        field changes to suit the type of the selected field (for example, select field \n        \"independence\" and note that the comparison field changes to a date). Add clauses to\n        your query with the \"+\" icon. Click \"Filter\" to see the result in the ListGrid.\n        "
                            },
                            {
                                id:"filterBuilderBracket",
                                jsURL:"grids/filtering/filterBuilderBracket.js",
                                title:"Nested Filter",
                                tabs:[
                                    {
                                        canEdit:"false",
                                        title:"worldDS",
                                        url:"grids/ds/worldSQLDS.ds.xml"
                                    }
                                ],
                                descriptionHeight:"120",
                                description:"\n        Use the FilterBuilder to construct queries of arbitrary complexity.  The FilterBuilder,\n        and the underlying AdvancedCriteria system, support building queries with subclauses\n        nested to any depth.  The initial criteria in this example are set up to display only\n        European countries where either the name ends with \"land\", or the population is less \n        than 3 million - an unlikely query, perhaps, but one that shows the barest example of\n        the FilterBuilder's flexibility.\n        <p>\n        Add clauses to the query with the \"+\" icon; add nested subclauses with the \"+()\" button.\n        Click \"Filter\" to see the result in the ListGrid.\n        "
                            },
                            {
                                id:"bigFilter",
                                jsURL:"grids/filtering/bigFilter.js",
                                title:"Big Filter",
                                tabs:[
                                ],
                                descriptionHeight:"120",
                                description:"\n        When a FilterBuilder must work with a very large number of fields, you can set \n        FilterBuilder.fieldDataSource to a DataSource containing records that represent the\n        fields to display in the FieldPickers in each clause.  The FilterBuilder below is \n        created without a normal DataSource, but specifies a fieldDataSource and the \n        FieldPicker items in each clause are populated dynamically with it's records.  In this \n        mode, the FieldPickers are represented by ComboBoxItems, rather than SelectItems, and \n        have default settings that provide type-ahead auto-completion.\n        <P>Note also that, when fieldDataSource is specified and the operator for a clause\n        is of a type that uses a field-lookup, the valueField is also populated dynamically by \n        the fieldDataSource.\n        "
                            },
                            {
                                id:"headerSpans",
                                jsURL:"grids/sorting/headerSpans.js",
                                title:"Header Spans",
                                tabs:[
                                    {
                                        canEdit:"false",
                                        title:"countryDS",
                                        url:"grids/ds/countrySQLDS.ds.xml"
                                    }
                                ],
                                description:"\n        HeaderSpans are a second level of headers that appears above the normal ListGrid \n        headers, providing a visual cue for grouping. Resize columns and note that the \n        HeaderSpans change accordingly. Right-click in the header and note that you can hide\n        and display spanned columns as a group, as well as individually.\n        "
                            },
                            {
                                id:"disableFilter",
                                jsURL:"grids/filtering/disable.js",
                                title:"Disable filter",
                                tabs:[
                                    {
                                        canEdit:"false",
                                        title:"worldDS",
                                        url:"grids/ds/worldSQLDS.ds.xml"
                                    }
                                ],
                                description:"\n        Type \"island\" above the Country column, then press Enter or click the filter button\n        (top-right corner of the grid) to show only countries with \"island\" in their name.\n        Select \"North America\" above the Continent column to filter countries by that continent.\n        Filtering is disabled on the \"Flag\" and \"Capital\" columns.\n        "
                            },
                            {
                                ref:"autofitFilter",
                                title:"Autofit filter"
                            },
                            {
                                id:"dateRangeFilter",
                                jsURL:"grids/filtering/dateRangeFilter.js",
                                title:"Date Range",
                                tabs:[
                                    {
                                        canEdit:"false",
                                        title:"worldDS",
                                        url:"grids/ds/worldSQLDS.ds.xml"
                                    }
                                ],
                                descriptionHeight:"180",
                                description:"\n        Smartclient provides special widgets for filtering date values, including recognised \n        RelativeDate strings that cause filtering relative to some other base-date. \n        <P>The RelativeDateRangeItem allows selection of dates in three ways: you can select a\n        preset date-string, like \"Today\" or \"Tomorrow\", or a \"ranged\" date-string, such as \n        \"N days from now\" and enter a quantity to associate with it, or you can directly enter\n        a date-string in a recognized format.  You can also select a date from the DateChooser\n        by click the icon to the right of the widget.\n        <P>The first example below demonstrates using a DateRangeItem in a seperate DynamicForm\n        to filter a ListGrid.  Select start and end values for the range using one of the \n        methods described above and click the \"Search\" button to see the data filtered \n        according to the values in the \"Nationhood\" field.\n        <P>The second example below demonstrates filtering grid data using a MiniDateRangeItem\n        to filter data when a ListGrid is showing it's FilterEditor.  In\n        this example, click the Date icon in the header for the Nationhood field to open a \n        popup DateRangeItemDialog.  In the dialog, select start and end values for the range, \n        as described above, and click Ok to close the Window.  Then click the Filter button in\n        the top right of the grid to see the data filtered.  You can hover the mouse over the \n        Nationhood field-header to see the full date-range string.\n        "
                            },
                            {
                                id:"expressionFilter",
                                jsURL:"grids/filtering/expressionFilter.js",
                                title:"Expression filter",
                                tabs:[
                                    {
                                        canEdit:"false",
                                        title:"worldDS",
                                        url:"grids/ds/worldSQLDS.ds.xml"
                                    }
                                ],
                                descriptionHeight:"140",
                                description:"\n        DynamicForms and FormItems are capable of parsing simple expressions entered as part of\n        their values, when allowExpressions is true on either entity.  ListGrids use this\n        facility, when showFilterEditor and allowFilterExpressions are true, to allow \n        expressions to be entered directly into the FormItems displayed in the filterEditor.\n        <P>Below is a ListGrid with a FilterEditor and allowFilterExpressions: true.  Some \n        expression-based filter-criteria have been applied via initialCriteria: the list displays \n        countries with no 'i's in the country name, with a Capital that starts with a letter \"A\"\n        through \"F\" and with a population less than 1 million or more than 100 million.\n        <P>See the table on the right for the supported expression-symbols.  Note that \n        logical \"and\" and \"or\" expressions are treated as text in text-based fields and ignored.\n        "
                            }
                        ]
                    },
                    {
                        isOpen:false,
                        title:"Sorting",
                        description:"\n    SmartClient grids provide interactive sorting of standard and custom data types,\n    with automatic client/server coordination.\n",
                        children:[
                            {
                                id:"sort",
                                jsURL:"grids/sorting/sort.js",
                                title:"Sort",
                                tabs:[
                                    {
                                        title:"countryData",
                                        url:"grids/data/countryData.js"
                                    }
                                ],
                                description:"\n        Click on any column header to sort by that column. To reverse the sort direction,\n        click on the same column header, or the top-right corner of the grid.\n        "
                            },
                            {
                                jsURL:"grids/sorting/disableSort.js",
                                title:"Disable sort",
                                visibility:"sdk",
                                tabs:[
                                    {
                                        title:"countryData",
                                        url:"grids/data/countryData.js"
                                    }
                                ],
                                description:"\n        Sorting is disabled on the \"Flag\" column. Click on any other column header to sort\n        on the corresponding column.\n        "
                            },
                            {
                                jsURL:"grids/sorting/sortArrow.js",
                                title:"Sort arrows",
                                visibility:"sdk",
                                tabs:[
                                    {
                                        title:"countryData",
                                        url:"grids/data/countryData.js"
                                    }
                                ],
                                description:"\n        Click on any column header to sort or reverse-sort by that column.\n        This grid shows the sort-direction arrow in the top-right corner only.\n        "
                            },
                            {
                                id:"dataTypes",
                                jsURL:"grids/sorting/dataTypes.js",
                                title:"Data-Aware Sort",
                                tabs:[
                                    {
                                        title:"countryData",
                                        url:"grids/data/countryData.js"
                                    }
                                ],
                                description:"\n        Click on any column header to sort by that column.\n        The \"Nationhood\", \"Area\", and \"GDP (per capita)\" columns are sorted as date, number, and\n        calculated number values, respectively.\n        "
                            },
                            {
                                id:"multiLevelSortLG",
                                jsURL:"grids/sorting/multiLevelSortLG.js",
                                title:"Multilevel Sort (UI)",
                                tabs:[
                                    {
                                        canEdit:"false",
                                        title:"supplyItem",
                                        url:"supplyItem.ds.xml"
                                    }
                                ],
                                descriptionHeight:"150",
                                description:"\n        This grid is displayed pre-sorted on two fields: first by Category ascending and then\n        by Item Name descending.  As well as the field's title, the header of each field included \n        in the sort configuration displays a sort-arrow indicating the direction of sort on that \n        field and, when multiple fields are sorted, a small numeral indicating this field's \n        position in the list of fields being sorted.  You can hold down SHIFT and \n        click an already sorted column-header to reverse it's direction, or an unsorted column-\n        header to add that field to the list of fields being sorted.  Clicking a column header\n        without holding down SHIFT clears the current sort configuration and initializes a new\n        sort on the selected field.\n        <P>SmartClient's SQL and Hibernate adapters support server-side multi-sorting and this\n        is in evidence in this example.\n        "
                            },
                            {
                                id:"multiLevelSortDialog",
                                jsURL:"grids/sorting/multiLevelSort.js",
                                title:"Multilevel Sort (Dialog)",
                                tabs:[
                                    {
                                        canEdit:"false",
                                        title:"supplyItem",
                                        url:"supplyItem.ds.xml"
                                    }
                                ],
                                description:"\n        Click the \"Multilevel Sort\" button to show a MultiSortDialog.  Select a set of sort\n        properties and directions and click \"Save\" to see the grid resorted by those properties.\n        "
                            },
                            {
                                id:"adaptiveSort",
                                jsURL:"grids/sorting/adaptiveSort.js",
                                title:"Adaptive Sort",
                                tabs:[
                                    {
                                        title:"supplyItem",
                                        url:"supplyItem.ds.xml"
                                    }
                                ],
                                description:"\n        SmartClient combines large dataset handling with <b>adaptive</b> use of\n        client-side sort.  Click any header now and server-side sort will be used for this\n        large dataset.  Check \"Limit to Electronics\" to limit the dataset and sort again:\n        when the dataset becomes small enough, SmartClient switches to client-side\n        sorting automatically.  The label underneath the grid flashes briefly \n        every time SmartClient needs to visit the server.\n        "
                            }
                        ]
                    },
                    {
                        isOpen:false,
                        title:"Editing",
                        description:"\n    SmartClient grids provide inline editing of all data types, with automatic validation and\n    client/server updates.<br><br>\n    These examples are all bound to the same remote DataSource, so your\n    changes are saved on SmartClient.com and will appear in all Grid Editing examples during this\n    session. To end your SmartClient.com session and reset the example data on the server, simply\n    close all instances of your web browser.\n",
                        children:[
                            {
                                id:"editByRow",
                                jsURL:"grids/editing/editRows.js",
                                title:"Edit by row",
                                tabs:[
                                    {
                                        canEdit:"false",
                                        title:"countryDS",
                                        url:"grids/ds/countrySQLDS.ds.xml"
                                    }
                                ],
                                description:"\n        <b>Click</b> on any cell to start editing. Use <b>Tab</b>, <b>Shift-Tab</b>,\n        <b>Up Arrow</b>, and <b>Down Arrow</b> to move between cells. Changes are saved\n        automatically when you move to another row. Press <b>Enter</b> to save the current row\n        and dismiss the editors, or <b>Esc</b> to discard changes for the current row and dismiss\n        the editors.\n        "
                            },
                            {
                                id:"editByCell",
                                jsURL:"grids/editing/editCells.js",
                                title:"Edit by cell",
                                tabs:[
                                    {
                                        canEdit:"false",
                                        title:"countryDS",
                                        url:"grids/ds/countrySQLDS.ds.xml"
                                    }
                                ],
                                description:"\n        <b>Click</b> on any cell to start editing. Use <b>Tab</b>, <b>Shift-Tab</b>,\n        <b>Up Arrow</b>, and <b>Down Arrow</b> to move between cells. Press <b>Enter</b> to save\n        the current row and dismiss the editors, or <b>Esc</b> to discard changes for the current\n        cell and dismiss the editors.\n        "
                            },
                            {
                                id:"enterNewRows",
                                jsURL:"grids/editing/enterRows.js",
                                title:"Enter new rows",
                                tabs:[
                                    {
                                        canEdit:"false",
                                        title:"countryDS",
                                        url:"grids/ds/countrySQLDS.ds.xml"
                                    }
                                ],
                                description:"\n        <b>Click</b> on any cell to start editing, then <b>Tab</b> or <b>Down Arrow</b> past the\n        last row in the grid to create a new row. Alternatively, click the <b>Edit New</b> button\n        to create a new data-entry row at the end of the grid.\n        "
                            },
                            {
                                ref:"autofitNewRecords",
                                title:"Autofit new rows"
                            },
                            {
                                id:"massUpdate",
                                jsURL:"grids/editing/massUpdate.js",
                                title:"Mass Update",
                                tabs:[
                                    {
                                        canEdit:"false",
                                        title:"countryDS",
                                        url:"grids/ds/countrySQLDS.ds.xml"
                                    }
                                ],
                                description:"\n        <b>Click</b> on any cell to start editing, then <b>Tab</b> or <b>Down Arrow</b> past the\n        last row in the grid to create a new row. Alternatively, click the <b>Edit New</b> button\n        to create a new data-entry row at the end of the grid. Unlike the other editing examples, \n        none of your changes are being automatically saved to the server.  Note how SmartClient\n        highlights changed values, and new rows. Click the \"Save\" button to save all your \n        changes at once, or click the \"Discard\" button to discard all your changes (including \n        any new rows) and revert to the data as it was before you started editing.\n        "
                            },
                            {
                                id:"modalEditing",
                                jsURL:"grids/editing/modalEditing.js",
                                title:"Modal editing",
                                tabs:[
                                    {
                                        canEdit:"false",
                                        title:"countryDS",
                                        url:"grids/ds/countrySQLDS.ds.xml"
                                    }
                                ],
                                description:"\n        <b>Double-click</b> on any cell to start editing. Click anywhere outside of the cell\n        editors to save changes, or press the <b>Esc</b> key to discard changes.\n        "
                            },
                            {
                                id:"disableEditing",
                                jsURL:"grids/editing/disableEditing.js",
                                title:"Disable editing",
                                tabs:[
                                    {
                                        canEdit:"false",
                                        title:"countryDS",
                                        url:"grids/ds/countrySQLDS.ds.xml"
                                    }
                                ],
                                description:"\n        <b>Click</b> on any cell to start editing. Use Tab/Arrow keys to move between cells,\n        Enter/Esc keys to save or cancel. Editing is disabled for the \"Country\" and \"G8\" columns.\n        "
                            },
                            {
                                id:"customEditors",
                                jsURL:"grids/editing/customEditors.js",
                                title:"Custom editors",
                                tabs:[
                                    {
                                        canEdit:"false",
                                        title:"countryDS",
                                        url:"grids/ds/countrySQLDS.ds.xml"
                                    }
                                ],
                                description:"\n        <b>Click</b> on any cell to start editing. The \"Government\", \"Population\", and \"Nationhood\"\n        columns specify custom editors: a multiple-line textarea, a numeric spinner, and a compound\n        date control.\n        "
                            },
                            {
                                id:"dataValidation",
                                jsURL:"grids/editing/validation.js",
                                title:"Data validation",
                                tabs:[
                                    {
                                        canEdit:"false",
                                        title:"countryDS",
                                        url:"grids/ds/countrySQLDS.ds.xml"
                                    }
                                ],
                                description:"\n        <b>Click</b> on any cell to start editing. Delete the value in a \"Country\" cell, or type a\n        non-numeric value in a \"Population\" cell, to see validation errors.\n        "
                            },
                            {
                                id:"databoundDependentSelects",
                                jsURL:"grids/editing/dependentSelects.js",
                                title:"Dependent Selects",
                                tabs:[
                                    {
                                        dataSource:"supplyItem",
                                        name:"supplyItem"
                                    },
                                    {
                                        dataSource:"supplyCategory",
                                        name:"supplyCategory"
                                    }
                                ],
                                description:"\n                \n                <p />In the first example, <b>Double Click</b> on any row to start editing. Select a value \n                in the \"Division\" column to change the set of options available in the \"Department\" \n                column.\n                <p />\n                <p />In the second example, click the <b>Order New Item</b> button to add an editable row \n                to the grid.  Select a Category in the second column to change the set of options \n                available in the \"Item\" column.\n            "
                            }
                        ]
                    },
                    {
                        isOpen:false,
                        title:"Frozen Columns",
                        description:"\n    SmartClient supports rendering out grids with frozen fields.<br><br>\n    Frozen fields are fields that do not scroll horizontally with the other fields, remaining\n    visible on the screen while other fields may be scrolled out of view.\n",
                        children:[
                            {
                                id:"simpleFreeze",
                                jsURL:"grids/freezeFields/simpleFreeze.js",
                                title:"Simple Freeze",
                                tabs:[
                                    {
                                        dataSource:"supplyItem",
                                        name:"supplyItem"
                                    }
                                ],
                                description:"\n        Setting <code>frozen:true</code> on a column definition establishes a\n        frozen column.  Column resize and reorder work normally.\n        "
                            },
                            {
                                id:"dynamicFreeze",
                                jsURL:"grids/freezeFields/dynamicFreeze.js",
                                title:"Dynamic Freeze",
                                tabs:[
                                    {
                                        dataSource:"supplyItem",
                                        name:"supplyItem"
                                    }
                                ],
                                description:"\n        Right click on any column header to show a menu that allows you to freeze\n        that column.<br>\n        Multiple columns may be frozen, and frozen columns may be\n        reordered.<br>\n        Right click on a frozen column to unfreeze it.\n        "
                            },
                            {
                                id:"canEditFreeze",
                                jsURL:"grids/freezeFields/freezeEditing.js",
                                title:"Editing",
                                tabs:[
                                    {
                                        dataSource:"supplyItem",
                                        name:"supplyItem"
                                    }
                                ],
                                description:"\n        SmartClient's inline editing support works normally with frozen columns\n        with no further configuration.\n        "
                            },
                            {
                                jsURL:"grids/freezeFields/freezeDragDrop.js",
                                title:"Drag and Drop",
                                tabs:[
                                    {
                                        title:"countryData",
                                        url:"grids/data/splitCountryData.js"
                                    }
                                ],
                                description:"\n        SmartClient's drag and drop support works normally with frozen columns\n        with no further configuration.  Drag countries within grids to reorder them, or between\n        grids to move countries back and forth.\n        "
                            }
                        ]
                    },
                    {
                        isOpen:false,
                        title:"Cell Widgets",
                        description:"\n    Examples of SmartClient's ability to embed arbitrary widgets in ListGrid cells.\n    ",
                        children:[
                            {
                                id:"gridCellWidgets",
                                jsURL:"grids/cellWidgets/gridCellWidgets.js",
                                title:"Grid Cell Widgets",
                                tabs:[
                                    {
                                        title:"countryData",
                                        url:"grids/data/countryDataDetail.js"
                                    }
                                ],
                                description:"\n        This example illustrates embedding arbitrary widgets in ListGrid cells. Notice how \n        reordering the column with widgets works as any other column. SmartClient uses widget \n        pooling to maximize efficiency - however, for better performance consider using one or \n        more fields of type \"icon\".\n        "
                            }
                        ]
                    },
                    {
                        isOpen:false,
                        title:"Expanding Rows",
                        description:"\n    SmartClient grids support a special <code>expansionField</code>.<br><br>\n    When grid.canExpandRecords is true, the expansionField is rendered out at the beginning of\n    the field list.  When this field is clicked for a record, the record is expanded and a\n    builtin component is embedded into the record's row, beneath it's field values.\n    <br><br>\n    A variety of components are supported by default, according to the value grid.expansionMode,\n    and you can also override grid.getExpansionComponent() to add your own expansion behaviors.\n",
                        children:[
                            {
                                id:"expansionDetailField",
                                jsURL:"grids/expansion/expansionDetailField.js",
                                title:"Detail Field",
                                tabs:[
                                    {
                                        title:"countryData",
                                        url:"grids/data/countryDataDetail.js"
                                    }
                                ],
                                description:"\n        This grid displays some fields from the Countries dataSource.  You can expand a \n        row by clicking the special <code>expansionField</code> to see the details of\n        the selected country's background in the expanded section.\n        "
                            },
                            {
                                id:"expansionDetails",
                                jsURL:"grids/expansion/expansionDetails.js",
                                title:"Details",
                                tabs:[
                                    {
                                        canEdit:"false",
                                        dataSource:"supplyItem",
                                        name:"supplyItem"
                                    }
                                ],
                                description:" \n        This grid displays a limited number of fields from the supplyItem dataSource.  You can\n        expand a row by clicking the special <code>expansionField</code> to see a DetailViewer\n        embedded in the expanded portion of the record which displays the rest of the\n        data from the dataSource that isn't already visible in the grid.\n        "
                            },
                            {
                                id:"expansionRelatedRecords",
                                jsURL:"grids/expansion/expansionRelatedRecords.js",
                                title:"Related Records",
                                tabs:[
                                    {
                                        canEdit:"false",
                                        dataSource:"supplyCategory",
                                        name:"supplyCategory"
                                    },
                                    {
                                        canEdit:"false",
                                        dataSource:"supplyItem",
                                        name:"supplyItem"
                                    }
                                ],
                                description:" \n        In this grid of Supply Categories, you can expand a row by clicking the special \n        <code>expansionField</code> to see a sub-grid containing the list of Supply Items \n        applicable to the selected Category.\n        "
                            },
                            {
                                id:"expansionLimitedWithDetails",
                                jsURL:"grids/expansion/expansionLimitedWithDetails.js",
                                title:"Limited Data",
                                tabs:[
                                    {
                                        canEdit:"false",
                                        dataSource:"supplyItemWithOps",
                                        name:"supplyItem"
                                    }
                                ],
                                description:" \n        This grid displays a limited number of fields from the supplyItem dataSource.  Only the\n        data values you can see have been returned from the server, via the \n        <i>operationBinding.outputs</i> feature.  When you click the special expansion field for\n        a row, the system goes to the server to retrieve the entire record, creates a \n        DetailViewer to display that data and expands the row to show the DetailViewer.  See \n        the code in the overridden getExpansionComponent() method.\n        <P>\n        Note also the use of the <i>maxExpandedRecords</i> attribute to limit the total number \n        of simultaneously expanded records.\n        "
                            }
                        ]
                    },
                    {
                        isOpen:false,
                        title:"Hover Components",
                        description:"\n    Any SmartClient Canvas can display a <i>hover</i> label when a user pauses momentarily\n    above it.  This is a builtin behavior for showing arbitrary HTML text, configured by\n    returning a value from <i>Canvas.hoverHTML</i>.  In addition to this, the builtin Hover\n    Label can be replaced with any other Canvas-based component by overriding and returning\n    a component from <i>getHoverComponent()</i>.\n    <P>\n    This section covers some examples of this feature.\n    <P>\n    When <b>showHoverComponents</b> is true and the mouse hovers over a field, a builtin \n    component is created and used in place of the standard hover Label.\n    <br><br>\n    A variety of components are supported by default, according to the value grid.hoverMode,\n    and you can also override grid.getHoverComponent() to add your own hover behaviors.\n",
                        children:[
                            {
                                id:"hoverRelatedRecords",
                                jsURL:"grids/hover/hoverRelatedRecords.js",
                                title:"Related Records",
                                tabs:[
                                    {
                                        canEdit:"false",
                                        doEval:"true",
                                        title:"customerOrders",
                                        url:"grids/data/customerOrders.js"
                                    },
                                    {
                                        canEdit:"false",
                                        doEval:"true",
                                        title:"customerOrderMessages",
                                        url:"grids/data/customerOrderMessages.js"
                                    },
                                    {
                                        canEdit:"false",
                                        doEval:"true",
                                        title:"orderDS",
                                        url:"grids/ds/orderDS.ds.js"
                                    },
                                    {
                                        canEdit:"false",
                                        doEval:"true",
                                        title:"orderMessagesDS",
                                        url:"grids/ds/orderMessagesDS.ds.js"
                                    }
                                ],
                                description:" \n        In this grid of Customer Orders, you can hover over a row to see a list of messages\n        attached to the order.  This gives you a quick preview of discussion about the order,\n        without the need to leave the list of orders.\n        <P>\n        In a complete application, clicking the order would lead to a detail screen showing the\n        full order details and the ability to add to the discussion.\n        "
                            },
                            {
                                id:"hoverDetails",
                                jsURL:"grids/hover/hoverDetails.js",
                                title:"Details",
                                tabs:[
                                    {
                                        canEdit:"false",
                                        dataSource:"supplyItemWithOps",
                                        name:"supplyItem"
                                    }
                                ],
                                description:" \n        This grid displays a limited number of fields from the supplyItem dataSource.  Only the\n        data values you can see have been returned from the server, via the \n        <i>operationBinding.outputs</i> feature.  When you hover over a row, the system goes to \n        the server to retrieve the entire record, creates a DetailViewer to display that data \n        and shows it as the hoverComponent.  See \n        the code in the overridden getCellHoverComponent() method.\n        "
                            }
                        ]
                    },
                    {
                        isopen:"false",
                        title:"Grouping",
                        description:"\n    List entries can be grouped according to field value.\n    ",
                        children:[
                            {
                                id:"dynamicGrouping",
                                jsURL:"grids/grouping/dynamicGrouping.js",
                                title:"Dynamic Grouping",
                                tabs:[
                                    {
                                        canEdit:"false",
                                        title:"countryDS",
                                        url:"grids/ds/countrySQLDS.ds.xml"
                                    }
                                ],
                                description:"\n        Right click on any column header to show a menu that allows you to group by that \n        column. Right click and pick \"ungroup\" to go back to a flat listing.\n        "
                            },
                            {
                                id:"groupedEditing",
                                jsURL:"grids/grouping/groupedEditing.js",
                                title:"Grouped Editing",
                                tabs:[
                                    {
                                        canEdit:"false",
                                        title:"countryDS",
                                        url:"grids/ds/countrySQLDS.ds.xml"
                                    }
                                ],
                                description:"\n        Inline editing works normally with grouped data. Try editing the field that records \n        are grouped by and notice that the record moves to its new group automatically.\n        "
                            },
                            {
                                id:"customGrouping",
                                jsURL:"grids/grouping/customGrouping.js",
                                title:"Custom Grouping",
                                tabs:[
                                    {
                                        canEdit:"false",
                                        title:"countryDS",
                                        url:"grids/ds/countrySQLDS.ds.xml"
                                    }
                                ],
                                description:"\n        You can specify custom grouping behavior for a field. Group by the Nationhood and \n        Population fields to see examples of custom grouping.\n        "
                            }
                        ]
                    },
                    {
                        id:"summaryGrid",
                        jsURL:"grids/summaries/gridSummary.js",
                        title:"Grid Summaries",
                        tabs:[
                            {
                                canEdit:"false",
                                title:"OrderItem",
                                url:"grids/ds/orderItemLocalDS.ds.js"
                            }
                        ],
                        descriptionHeight:"210",
                        description:"\n        ListGrids support displaying summaries of the current data set in various ways:\n        <P>\n        Fields from individual records can be summarized into a single field value. In this\n        example the <code>\"Total\"</code> field displays a summary value calculated by\n        multiplying the <code>\"Quantity\"</code> and <code>\"Price\"</code> fields.\n        <P>\n        Summaries can also be displayed for multiple records. This example shows a summary row\n        at the end of each group in the grid as well as an overall summary row with information\n        about every record in the grid. Note that in addition to standard summary functions\n        such as <code>\"sum\"</code> to generate a total, or <code>\"count\"</code> to generate\n        a count of records, completely custom functions may be used. This is demonstrated in\n        the <code>\"Category\"</code> field where a custom function determines how many\n        categories exist in this dataset. \n        <P>\n        Click to edit and summaries are dynamically recalculated to reflect your changes.\n        "
                    },
                    {
                        id:"customColumns",
                        jsURL:"grids/customColumns.js",
                        title:"Custom Columns",
                        tabs:[
                            {
                                canEdit:"false",
                                title:"countryDS",
                                url:"grids/ds/countrySQLDS.ds.xml"
                            }
                        ],
                        descriptionHeight:"220",
                        description:"\n        <b>Formula and Summary fields</b> provide built-in wizards for end users to define \n        formula fields that can compute values using other fields, or summary fields that can \n        combine other fields with intervening / surrounding text. Available in all \n        DataBoundComponents and easy to persist as preferences.\n        <P>\n        The Formula and Summary Builders are accessible from the grid header context menu. They \n        can also be invoked programmatically as demonstrated by clicking the buttons in this \n        sample.\n        <P>\n        Launch the Formula Builder and enter the title of the new field, and the desired \n        formula. For example, enter <b>Population Density</b> for the field title and in the \n        formula field enter <b>A / B</b>. As indicated in the dialog, <b>A</b> represents the \n        Population field, and <b>B</b> represents the Area field. Notice that you can now sort on this \n        newly added <b>Population Density</b> field just like any other field. Click the help \n        icon to view the various supported inbuilt functions.\n        <P>\n        Next launch the Summary Builder and enter the title of the new field, and the Summary \n        formulation. For example, enter <b>County (Flag)</b> for the field name and enter \n        <b>#B (#A)</b> in the summary field.\n        <P>\n        Once some additional user-fields have been added, all you need to persist the column-layout\n        for later restoration to another grid, is the ability to save a string.  Click the \n        button below to store the grid's state by calling <i>getFieldState()</i>, destroy the \n        grid and restore it's state to another grid using <i>setFieldState()</i>. \n        \n    "
                    },
                    {
                        isOpen:false,
                        title:"Hiliting",
                        description:"\n		The 'hiliting' system allows end users to visually define data highlighting rules, such \n		as using colors to pick out high values, or using multiple colors to indicate\n		ranges of values.\n		<P>\n		Because 'hilites' can be easily stored and re-applied, it's easy to build an interface \n        that allows users to store their own private data highlighting rules, or even build a\n		highlighted report to share with other users.		\n	",
                        children:[
                            {
                                id:"userDefinedHilites",
                                jsURL:"grids/hiliting/userDefinedHilites.js",
                                title:"User Defined",
                                tabs:[
                                    {
                                        canEdit:"false",
                                        title:"countryDS",
                                        url:"grids/ds/countrySQLDS.ds.xml"
                                    }
                                ],
                                descriptionHeight:"240",
                                description:"\n		DataBoundComponents allow end-users to create 'hilites' with rules based on the values \n        of data.  There are two sorts of hilites: simple hilites, which allow a single \n        criterion based on a single field to affect a single target field (the same one), and \n        Advanced hilites, which allow very complex criteria, based on multiple fields, to \n        affect multiple target fields.\n		<P>\n		Click the \"Edit Hilites\" button below to show the HiliteEditor interface.  To set up a \n        simple hilite, click on the \"Area (km<sup>2</sup>)\" record in the list to the left.  \n        When the simple hilite rule appears on the right, select the \"greater than\" operation \n        from the drop-down box, type \"5000000\" into the value textBox, select a color from the \n        'Color' picker widget and click 'Save'.  You'll see that all \"Area (km<sup>2</sup>)\" \n        values in the grid that exceed 5000000 are now hilighted in your chosen color.\n        <P>\n		Now, add an Advanced criteria.  Again, click the \"Edit Hilites\" button and then click \n        the \"Add Advanced Rule\" button in the top left of the HiliteEditor - you'll now see the \n		AdvancedHiliteEditor window.  Add a new criterion that specifies <i>GDP ($M) greater \n        than 1000000</i>.  Click the green plus icon beneath the criterion and add a second one, \n        this time specifying <i>Area (km<sup>2</sup>) less than 500000</i>.  In the list below, \n        select both \"GDP ($M)\" and \"Area (km<sup>2</sup>)\" and select a background color.  \n        Clicking 'Save' now will update the grid, showing both GDP and Area data in your \n        selected background color, when GDP is higher than 1 million and Area is less than\n		500,000.\n		<P>\n        You can easily provide users with the ability to save and restore their hilite \n        information - all you need is the ability to save a string.  Click the \n        button below to see the grid's hilite state retrieved and serialized, by calling \n		<i>getHiliteState()</i>, the grid destroyed and it's hilite-state restored to another \n        grid via <i>setHiliteState()</i>. \n    "
                            },
                            {
                                id:"preDefinedHilites",
                                jsURL:"grids/hiliting/preDefinedHilites.js",
                                title:"Pre-Defined",
                                tabs:[
                                    {
                                        canEdit:"false",
                                        title:"countryDS",
                                        url:"grids/ds/countrySQLDS.ds.xml"
                                    }
                                ],
                                descriptionHeight:"130",
                                description:"\n		This example demonstrates producing hilites in code.  The hilites applied to the grid \n        below match those suggested in the <i>User Defined</i> hilites example.  Additionaly, \n        the Advanced Hilite in this example also demonstrates using <i>Canvas.imgHTML()</i> \n        and the <i>htmlAfter</i> attribute of hilite-objects to append a warning icon to the \n        end of each field value, as part of the hilite.\n		<P>\n		Hilite-objects also support an <i>htmlBefore</i> attribute - you can use these before \n        and after properties to entend color-based hilites to format values, for instance, as \n        <b>bold</b> or <i>italic</i> text using HTML tags.\n    "
                            },
                            {
                                id:"dataDrivenHilites",
                                jsURL:"grids/hiliting/dataDrivenHilites.js",
                                title:"Data-Driven",
                                tabs:[
                                    {
                                        canEdit:"false",
                                        doEval:"true",
                                        title:"countryDataHilites",
                                        url:"grids/data/countryDataHilites.js"
                                    },
                                    {
                                        canEdit:"true",
                                        title:"countryHilitesDS",
                                        url:"grids/ds/countryHilitesDS.ds.js"
                                    }
                                ],
                                descriptionHeight:"80",
                                description:"\n		This example demonstrates hiliting in a data-driven fashion, where hilites contain no\n        criteria, and instead the data itself is flagged by setting the \n        <i>DataBoundComponent.hiliteProperty</i> attribute on each record.  This method is \n        useful when some complex server-based calculation is used to decide which records to \n        hilite, and the client only needs to handle displaying them.\n    "
                            },
                            {
                                id:"formulaHilites",
                                jsURL:"grids/hiliting/formulaHilites.js",
                                title:"Formula Hilites",
                                tabs:[
                                    {
                                        canEdit:"false",
                                        title:"countryDS",
                                        url:"grids/ds/countrySQLDS.ds.xml"
                                    }
                                ],
                                descriptionHeight:"170",
                                description:"\n        Hiliting can be applied to any field in a DataBoundComponent, including custom formula\n        and summary fields.\n        <P>\n        Launch the Formula Builder and enter the title of the new field, and the desired \n        formula. In this case, enter <b>Population Density</b> for the field title and in the \n        formula field enter <b>A / B</b>. As indicated in the dialog, <b>A</b> represents the \n        Population field, and <b>B</b> represents the Area field. Notice that you can now sort on this \n        newly added <b>Population Density</b> field just like any other field. Click the help \n        icon to view the various supported inbuilt functions.\n        <P>\n		Now, click the \"Edit Hilites\" button to show the HiliteEditor interface.  To set up a \n        simple hilite on the custom <b>Population Density</b> field, select it in the list to \n        the left.  When the simple hilite rule appears on the right, select the \"greater than\" \n        operation from the drop-down box, type \"300\" into the value textBox, select a color \n        from the 'Color' picker widget and click 'Save'.  You'll see that all the grid-values \n        in the <b>Population Density</b> field that exceed 300 are now hilighted in your \n        chosen color.\n    "
                            }
                        ]
                    },
                    {
                        isOpen:false,
                        title:"Tiling",
                        description:"\n    Using the TileGrid to display data in a tiled format.\n",
                        children:[
                            {
                                id:"tilingBasic",
                                jsURL:"grids/tiling/basic.js",
                                title:"Basic",
                                tabs:[
                                    {
                                        title:"animalData",
                                        url:"grids/data/animalData.js"
                                    }
                                ],
                                description:"\n       SmartClient can display data in a \"tiled\" view.  Mouse over widgets to see rollovers, click to\n       select (shift- and ctrl-click for multi-select).\n        "
                            },
                            {
                                cssURL:"grids/tiling/tileStyle.css",
                                id:"tilingFilter",
                                jsURL:"grids/tiling/filter.js",
                                title:"Filter & Sort",
                                tabs:[
                                    {
                                        canEdit:"false",
                                        title:"animalsDS",
                                        url:"grids/ds/animalsSQLDS.ds.xml"
                                    }
                                ],
                                description:"\n        Tiled views can be filtered and sorted just like ListGrids.  Use the \"Search\" form to eliminate\n        some tiles and watch remaining tiles animate to new positions.  Use the \"Sort\" form to change\n        the sort direction.\n        "
                            },
                            {
                                id:"tilingLoadOnDemand",
                                jsURL:"grids/tiling/loadOnDemand.js",
                                title:"Load on Demand",
                                description:"\n       Tiled views can load data from services via DataSources, just like other data-aware components.\n       Enter a search term to search Yahoo Images and display available images as tiles.  \n        "
                            },
                            {
                                cssURL:"grids/tiling/tileStyle.css",
                                id:"tilingEditing",
                                jsURL:"grids/tiling/editing.js",
                                title:"Editing",
                                tabs:[
                                    {
                                        canEdit:"false",
                                        title:"animalsDS",
                                        url:"grids/ds/animalsSQLDS.ds.xml"
                                    }
                                ],
                                description:"\n       Tiled views can be connected to editors.  The TiledView automatically reacts to changes to the\n       underlying dataset.  Change the life span of Gazelle to 2 to see it\n       animate to the beginning of the list.\n        "
                            },
                            {
                                cssURL:"grids/tiling/tileStyle.css",
                                id:"tilingCustomTiles",
                                jsURL:"grids/tiling/customTiles.js",
                                title:"Custom Tiles",
                                tabs:[
                                    {
                                        canEdit:"false",
                                        title:"animalsDS",
                                        url:"grids/ds/animalsSQLDS.ds.xml"
                                    }
                                ],
                                description:"\n        You can customize the tiles in a TileGrid.  This example demonstrates adding a \"Remove\"\n        button to each tile which, when clicked, executes a dataSource operation to remove the\n        selected tile.\n        "
                            }
                        ]
                    },
                    {
                        id:"export",
                        jsURL:"grids/export.js",
                        requiresModules:"SCServer",
                        title:"Export",
                        tabs:[
                            {
                                canEdit:"false",
                                title:"worldDSExport",
                                url:"grids/ds/worldSQLDSExport.ds.xml"
                            }
                        ],
                        description:"\n            It's now easy to export data from a DataSource or from DataboundComponents, \n            such as <i>ListGrid</i>, <i>TreeGrid</i> and <i>TileGrid</i>.  In the example \n            below, choose an export format from the select-list, decide whether to \n            download the results or view them in a window using the checkbox and \n            click the Export button.  Because exporting to JSON is allowed only via \n            server-side custom code or via an OperationBinding (for security reasons), choosing\n            <i>JSON</i> from the select-item issues the export using the operationId set up in\n            the DataSource but still respects the \"Show in Window\" checkbox.  See the \n            <i>JS</i> and <i>worldDSExport</i> tabs below.\n            <p>Try changing the filters and sort-order on the grid to see that the exported data \n            is filtered and sorted according to criteria applied to the grid.\n        "
                    },
                    {
                        id:"gridsDataTypes",
                        isOpen:false,
                        title:"Data types",
                        description:"\n    Built-in display and editing behaviors for common data types, and how to customize them.\n",
                        children:[
                            {
                                jsURL:"grids/dataTypes/text.js",
                                title:"Text",
                                tabs:[
                                    {
                                        title:"countryData",
                                        url:"grids/data/countryData.js"
                                    }
                                ],
                                description:"\n        Click on column headers to sort, or data values to edit.\n        All fields in this grid are text fields.\n        "
                            },
                            {
                                id:"imageType",
                                jsURL:"grids/dataTypes/image.js",
                                title:"Image",
                                tabs:[
                                    {
                                        title:"countryData",
                                        url:"grids/data/countryData.js"
                                    }
                                ],
                                description:"\n        \"Flag\" is an image field.\n        "
                            },
                            {
                                id:"longText",
                                jsURL:"grids/dataTypes/longtext.js",
                                title:"Long Text",
                                tabs:[
                                    {
                                        title:"countryData",
                                        url:"grids/data/countryData.js"
                                    }
                                ],
                                description:"\n        Click on data values to edit.\n        \"Government\" is a long text field with a popup editor.\n        "
                            },
                            {
                                jsURL:"grids/dataTypes/date.js",
                                title:"Date",
                                tabs:[
                                    {
                                        title:"countryData",
                                        url:"grids/data/countryData.js"
                                    }
                                ],
                                description:"\n        Click on column headers to sort, or data values to edit.\n        \"Nationhood\" is a date field.\n        "
                            },
                            {
                                jsURL:"grids/dataTypes/integer.js",
                                title:"Integer",
                                tabs:[
                                    {
                                        title:"countryData",
                                        url:"grids/data/countryData.js"
                                    }
                                ],
                                description:"\n        Click on column headers to sort, or data values to edit.\n        \"Population\" is an integer field.\n        "
                            },
                            {
                                jsURL:"grids/dataTypes/decimal.js",
                                title:"Decimal",
                                tabs:[
                                    {
                                        title:"countryData",
                                        url:"grids/data/countryData.js"
                                    }
                                ],
                                description:"\n        Click on column headers to sort, or data values to edit.\n        \"GDP\" is a decimal (aka float) field.\n        "
                            },
                            {
                                jsURL:"grids/dataTypes/boolean.js",
                                title:"Boolean",
                                tabs:[
                                    {
                                        title:"countryData",
                                        url:"grids/data/countryData.js"
                                    }
                                ],
                                description:"\n        Click on column headers to sort, or data values to edit.\n        \"G8\" is a boolean (true/false) field.\n        "
                            },
                            {
                                jsURL:"grids/dataTypes/linkText.js",
                                title:"Link (text)",
                                tabs:[
                                    {
                                        title:"countryData",
                                        url:"grids/data/countryData.js"
                                    }
                                ],
                                description:"\n        Click on the values in the \"Info\" column to open external links.\n        "
                            },
                            {
                                id:"linkImage",
                                jsURL:"grids/dataTypes/linkImage.js",
                                title:"Link (image)",
                                tabs:[
                                    {
                                        title:"countryData",
                                        url:"grids/data/countryData.js"
                                    }
                                ],
                                description:"\n        Click on the book images in the \"Info\" column to open external links.\n        "
                            },
                            {
                                id:"listType",
                                jsURL:"grids/dataTypes/list.js",
                                title:"List",
                                tabs:[
                                    {
                                        title:"countryData",
                                        url:"grids/data/countryData.js"
                                    }
                                ],
                                description:"\n        Click on column headers to sort, or data values to edit.\n        \"Continent\" is a list (aka valueMapped) field.\n        "
                            },
                            {
                                id:"calculatedCellValue",
                                jsURL:"grids/dataTypes/calculated.js",
                                title:"Calculated",
                                tabs:[
                                    {
                                        title:"countryData",
                                        url:"grids/data/countryData.js"
                                    }
                                ],
                                description:"\n        Click on column headers to sort, or data values to edit.\n        \"GDP (per capita)\" is calculated from the \"GDP\" and \"Population\" fields.\n        "
                            }
                        ]
                    },
                    {
                        id:"gridsDataBinding",
                        isOpen:false,
                        title:"Data binding",
                        description:"\n    How to bind grids to DataSources to share field (column) definitions with other components,\n    and how to load data from local and remote data sources and services.    \n",
                        children:[
                            {
                                id:"listGridFields",
                                jsURL:"grids/dataBinding/fieldsGrid.js",
                                title:"ListGrid fields",
                                tabs:[
                                    {
                                        title:"countryData",
                                        url:"grids/data/countryData.js"
                                    }
                                ],
                                description:"\n        This <code>ListGrid</code> takes its field (column) settings from the \"fields\"\n        property of the component definition only. This technique is appropriate for\n        presentation-only grids that do not require data binding.\n        "
                            },
                            {
                                id:"dataSourceFields",
                                jsURL:"grids/dataBinding/fieldsDS.js",
                                title:"DataSource fields",
                                tabs:[
                                    {
                                        title:"countryData",
                                        url:"grids/data/countryData.js"
                                    },
                                    {
                                        title:"countryDS",
                                        url:"grids/ds/countryMergeDS.ds.js"
                                    }
                                ],
                                description:"\n        This <code>ListGrid</code> takes its field (column) settings from the\n        <code>countryDS</code> DataSource specified in the \"dataSource\" property of the\n        component definition. This technique is appropriate for easy display of a shared\n        data model with the default UI appearance and behaviors.\n        "
                            },
                            {
                                id:"mergedFields",
                                jsURL:"grids/dataBinding/fieldsMerged.js",
                                title:"Merged fields",
                                tabs:[
                                    {
                                        title:"countryData",
                                        url:"grids/data/countryData.js"
                                    },
                                    {
                                        title:"countryDS",
                                        url:"grids/ds/countryMergeDS.ds.js"
                                    }
                                ],
                                description:"\n        This <code>ListGrid</code> merges field settings from both the component \"fields\"\n        (for presentation attributes) and the <code>countryDS</code> DataSource (for\n        data model attributes). This is the usual approach to customize the look and feel of a\n        data-bound component.\n        "
                            },
                            {
                                id:"inlineData",
                                jsURL:"grids/dataProviders/inlineData.js",
                                title:"Inline data",
                                description:"\n        This <code>ListGrid</code> uses an inline data array in the component definition. This\n        technique is appropriate for very small read-only data sets, typically with static data\n        values.\n        "
                            },
                            {
                                id:"localData",
                                jsURL:"grids/dataProviders/localData.js",
                                title:"Local data",
                                tabs:[
                                    {
                                        title:"countryData",
                                        url:"grids/data/countryData.js"
                                    }
                                ],
                                description:"\n        This <code>ListGrid</code> loads data from a local data array (included in a separate\n        JavaScript data file). This technique is appropriate for read-only data sets, typically\n        with less than 500 records.\n        "
                            },
                            {
                                id:"localDataSource",
                                jsURL:"grids/dataProviders/databound.js",
                                title:"Local DataSource",
                                tabs:[
                                    {
                                        title:"countryData",
                                        url:"grids/data/countryData.js"
                                    },
                                    {
                                        title:"countryDS",
                                        url:"grids/ds/countryLocalDS.ds.js"
                                    }
                                ],
                                description:"\n        This <code>ListGrid</code> binds to a client-only <code>DataSource</code> that loads data\n        from a local data array. This technique is appropriate for client-only rapid prototyping\n        when the production application will support add or update (write operations), switchable\n        data providers (JSON, XML, WSDL, Java), arbitrarily large data sets (1000+ records), or\n        a data model that is shared by multiple components.\n        "
                            },
                            {
                                id:"jsonDataSource",
                                jsURL:"grids/dataProviders/databound.js",
                                title:"JSON DataSource",
                                tabs:[
                                    {
                                        title:"countryDS",
                                        url:"grids/ds/countryJSONDS.ds.xml"
                                    },
                                    {
                                        canEdit:"false",
                                        doEval:"false",
                                        title:"countryData.json",
                                        url:"grids/data/countryData.json"
                                    }
                                ],
                                description:"\n        This <code>ListGrid</code> binds to a <code>DataSource</code> that loads data from a\n        remote JSON data provider.  This approach of loading simple JSON data over HTTP can be\n        used with PHP and other server technologies.\n        "
                            },
                            {
                                id:"xmlDataSource",
                                jsURL:"grids/dataProviders/databound.js",
                                needXML:"true",
                                title:"XML DataSource",
                                tabs:[
                                    {
                                        title:"countryDS",
                                        url:"grids/ds/countryXMLDS.ds.xml"
                                    },
                                    {
                                        canEdit:"false",
                                        doEval:"false",
                                        title:"countryData.xml",
                                        url:"grids/data/countryData.xml"
                                    }
                                ],
                                description:"\n        This <code>ListGrid</code> binds to a <code>DataSource</code> that loads data from a\n        remote XML data provider.  This approach of loading simple XML data over HTTP can be\n        used with PHP and other server technologies.\n        "
                            },
                            {
                                id:"WSDLDataSource",
                                jsURL:"grids/dataProviders/WSDLBound.js",
                                needXML:"true",
                                title:"WSDL DataSource",
                                tabs:[
                                    {
                                        title:"countryDS",
                                        url:"grids/ds/countryWSDLDS.ds.xml"
                                    },
                                    {
                                        canEdit:"false",
                                        doEval:"false",
                                        title:"soapRequest.xml",
                                        url:"grids/data/countrySoapRequest.xml"
                                    },
                                    {
                                        canEdit:"false",
                                        doEval:"false",
                                        title:"soapResponse.xml",
                                        url:"grids/data/countrySoapResponse.xml"
                                    },
                                    {
                                        canEdit:"false",
                                        doEval:"false",
                                        title:"WSDL",
                                        url:"grids/ds/SmartClientOperations.wsdl"
                                    }
                                ],
                                description:"\n        This <code>ListGrid</code> binds to a <code>DataSource</code> that loads data via a\n        WSDL service.  This example WSDL service supports all 4 basic operation types (fetch,\n        add, update, remove) and can be implemented with any server technology.  Sample\n        request/response SOAP messages for a \"fetch\" operation are shown.\n        "
                            }
                        ]
                    },
                    {
                        isOpen:false,
                        title:"Data operations",
                        description:"\n    Basic operations on datasets, both local and remote.\n",
                        children:[
                            {
                                jsURL:"grids/dataOperations/localSet.js",
                                title:"Local set",
                                visibility:"sdk",
                                tabs:[
                                    {
                                        title:"countryData",
                                        url:"grids/data/countryData.js"
                                    }
                                ],
                                description:"\n        Click the buttons to populate the grid with records from a local data set.\n        "
                            },
                            {
                                jsURL:"grids/dataOperations/localAdd.js",
                                title:"Local add",
                                visibility:"sdk",
                                tabs:[
                                    {
                                        title:"countryData",
                                        url:"grids/data/countryData.js"
                                    }
                                ],
                                description:"\n        Click the buttons to add records to the top and bottom of the list.\n        "
                            },
                            {
                                jsURL:"grids/dataOperations/localRemove.js",
                                title:"Local remove",
                                visibility:"sdk",
                                tabs:[
                                    {
                                        title:"countryData",
                                        url:"grids/data/countryData.js"
                                    }
                                ],
                                description:"\n        Click \"Remove first\" to remove the first record in the list. Click the other buttons to\n        remove records based on your selection (click, Ctrl-click, or\n        Shift-click in the list to select records).\n        "
                            },
                            {
                                jsURL:"grids/dataOperations/localUpdate.js",
                                title:"Local update",
                                visibility:"sdk",
                                tabs:[
                                    {
                                        title:"countryData",
                                        url:"grids/data/countryData.js"
                                    }
                                ],
                                description:"\n        Click to select any record in the list, then click one of the buttons to change\n        the \"Continent\" value for that record. Also see the <b>Grids > Editing</b> examples\n        for automatic update behavior.\n        "
                            },
                            {
                                id:"databoundFetch",
                                jsURL:"grids/dataOperations/databoundFetch.js",
                                title:"Databound fetch",
                                tabs:[
                                    {
                                        canEdit:"false",
                                        title:"worldDS",
                                        url:"grids/ds/worldSQLDS.ds.xml"
                                    }
                                ],
                                description:"\n        Click the buttons to fetch (exact match) country records from the server.\n        Click the \"Fetch All\" button to fetch the first \"page\" of 50 records, then scroll\n        the grid to fetch new pages of data on demand.\n        "
                            },
                            {
                                id:"databoundFilter",
                                jsURL:"grids/dataOperations/databoundFilter.js",
                                title:"Databound filter",
                                tabs:[
                                    {
                                        canEdit:"false",
                                        title:"worldDS",
                                        url:"grids/ds/worldSQLDS.ds.xml"
                                    }
                                ],
                                description:"\n        Click the buttons to filter (partial match) records from the server. Also see the\n        <b>Grids &gt; Sort &amp; filter &gt; Filter</b> example for automatic databound Filter\n        operations triggered by user input.\n        "
                            },
                            {
                                id:"databoundAdd",
                                jsURL:"grids/dataOperations/databoundAdd.js",
                                title:"Databound add",
                                tabs:[
                                    {
                                        canEdit:"false",
                                        title:"worldDS",
                                        url:"grids/ds/worldSQLDS.ds.xml"
                                    }
                                ],
                                description:"\n        Click the \"Add new country\" button to create a new country record on the server.\n        Also see the <b>Grids &gt; Editing &gt; Enter new rows</b> example for automatic databound\n        Add operations triggered by user input.\n        "
                            },
                            {
                                id:"databoundRemove",
                                jsURL:"grids/dataOperations/databoundRemove.js",
                                title:"Databound remove",
                                tabs:[
                                    {
                                        canEdit:"false",
                                        title:"worldDS",
                                        url:"grids/ds/worldSQLDS.ds.xml"
                                    }
                                ],
                                description:"\n        Click \"Remove first\" to remove (delete) the first record in the list from the server.\n        Click the other buttons to remove records based on your selection (click, Ctrl-click, or\n        Shift-click in the list to select records).\n        "
                            },
                            {
                                id:"databoundUpdate",
                                jsURL:"grids/dataOperations/databoundUpdate.js",
                                title:"Databound update",
                                tabs:[
                                    {
                                        canEdit:"false",
                                        title:"worldDS",
                                        url:"grids/ds/worldSQLDS.ds.xml"
                                    }
                                ],
                                description:"\n        Click to select any record in the list, then click one of the buttons to change\n        the \"Continent\" value for that record on the server. Also see the <b>Grids &gt; Editing</b>\n        examples for automatic databound Update operations triggered by user input.\n        "
                            }
                        ]
                    },
                    {
                        ref:"dataIntegration",
                        title:"Service Integration",
                        visibility:"none"
                    }
                ]
            },
            {
                icon:"[ISO_DOCS_SKIN]/images/silkicons/chart_organisation.png",
                isOpen:false,
                showSkinSwitcher:"true",
                title:"Trees",
                description:"\n    High-performance interactive tree views\n    <BR>\n    <BR>\n    Trees are based on grid views, and so share all of the appearance, interactivity and\n    databinding features of grids, in addition to tree-specific features.\n",
                children:[
                    {
                        isOpen:false,
                        title:"Appearance",
                        description:"\n        Trees can have dynamic titles, display multiple columns and show connector\n        lines.\n    ",
                        children:[
                            {
                                dataSource:"employees",
                                id:"nodeTitles",
                                jsURL:"trees/appearance/nodeTitles.js",
                                title:"Node Titles",
                                description:"\n            Formatter interfaces allow you to add custom tree titles.\n            "
                            },
                            {
                                dataSource:"employees",
                                jsURL:"trees/appearance/multipleColumns.js",
                                title:"Multiple Columns",
                                description:"\n            Trees can show multiple columns of data for each node.  Each column has the\n            styling, formatting, and data type awareness features of columns in a normal\n            grid.\n\n            Try drag reordering columns, or sorting by the Salary field.\n            "
                            },
                            {
                                cssURL:"trees/appearance/connectors.css",
                                dataSource:"employees",
                                id:"connectors",
                                jsURL:"trees/appearance/connectors.js",
                                title:"Connectors",
                                description:"\n            Trees can show skinnable connector lines. Toggle the checkbox to show or hide \"full\"\n            connector lines.\n        ",
                                badSkins:[
                                    "BlackOps",
                                    "SilverWave"
                                ],
                                bestSkin:"TreeFrog"
                            }
                        ]
                    },
                    {
                        isOpen:false,
                        title:"Dragging",
                        description:"\n        Trees have built-in drag and drop behaviors and tree-specific events.\n    ",
                        children:[
                            {
                                id:"treeDragReparent",
                                jsURL:"trees/interaction/dragReparent.js",
                                title:"Drag reparent",
                                tabs:[
                                    {
                                        name:"employeeData",
                                        url:"trees/employeeData.js"
                                    }
                                ],
                                description:"\n            Try dragging employees under new managers.  Note that a position indicator line\n            appears during drag, allowing employees to be placed in a particular order.\n            "
                            },
                            {
                                ref:"dragTree",
                                title:"Drag nodes"
                            },
                            {
                                ref:"treeDragReparent",
                                title:"Springloaded Folders",
                                description:"\n            Try dragging employees under new managers.  Note that closed folders automatically\n            open if you hover over them momentarily.\n            "
                            },
                            {
                                id:"treeDropEvents",
                                jsURL:"trees/interaction/dropEvents.js",
                                title:"Drop Events",
                                tabs:[
                                    {
                                        dataSource:"supplyCategory",
                                        name:"supplyCategory"
                                    },
                                    {
                                        dataSource:"supplyItem",
                                        name:"supplyItem"
                                    }
                                ],
                                description:"\n            Click on any category on the left to show items from that category on the right.  \n            Drag and drop items from the list on the right into new categories in the tree on\n            the left.\n            "
                            }
                        ]
                    },
                    {
                        id:"cascadingSelection",
                        jsURL:"trees/cascadingSelection.js",
                        title:"Cascading Selection",
                        description:"\n        Tree selection can be automatically propagated up and down the tree. Select a\n        parent or child node to see how other nodes are affected.\n        "
                    },
                    {
                        id:"treesDataBinding",
                        isOpen:false,
                        title:"Data binding",
                        description:"\n        Trees can bind to DataSources and handle all the data formats that grids can, using\n        additional properties to control tree structure, open state, and folderness.\n    ",
                        children:[
                            {
                                id:"parentLinking",
                                jsURL:"trees/dataBinding/parentLinking.js",
                                title:"Parent Linking",
                                description:"\n            Tree data can be specified as a flat list of nodes that refer to each other by\n            ID.  This format is also used for load on demand.\n            "
                            },
                            {
                                id:"childrenArrays",
                                jsURL:"trees/dataBinding/childrenArrays.js",
                                title:"Children Arrays",
                                description:"\n            Tree data can be specified as a tree of nodes where each node lists its children.\n            "
                            },
                            {
                                jsURL:"trees/dataBinding/loadXMLParentLinked.js",
                                title:"Load XML (Parent Linked)",
                                tabs:[
                                    {
                                        canEdit:"false",
                                        doEval:"false",
                                        title:"employeesXMLData",
                                        url:"trees/dataBinding/employeesDataParentLinked.xml"
                                    }
                                ],
                                description:"\n            Tree data can be loaded in XML or JSON format.  For a \"parent-linked\" Tree, the\n            <code>primaryKey</code> and <code>foreignKey</code> declarations in the DataSource\n            control how nodes are linked together to form the tree structure.\n            \n            "
                            },
                            {
                                id:"treeLoadXML",
                                jsURL:"trees/dataBinding/loadXMLChildrenArrays.js",
                                needXML:"true",
                                title:"Load XML (Child Arrays)",
                                tabs:[
                                    {
                                        canEdit:"false",
                                        doEval:"false",
                                        title:"employeesXMLData",
                                        url:"trees/dataBinding/employeesDataChildrenArrays.xml"
                                    }
                                ],
                                description:"\n            Tree data can be loaded in XML or JSON format.  For a \"children arrays\" Tree, one\n            DataSource field is marked <code>childrenProperty:true</code>, and the children of\n            a node are expected to appear under the same-named XML element or JSON property.\n            \n            "
                            },
                            {
                                dataSource:"employees",
                                jsURL:"trees/dataBinding/loadOnDemand.js",
                                title:"Load on Demand",
                                description:"\n            Begin opening folders and note the prompt which briefly appears during server\n            fetches.  Trees can load data one folder at a time.  When a folder is opened for the first\n            time, the tree asks the server for the children of the node just opened by passing\n            the unique id of the parent as search criteria.\n            "
                            },
                            {
                                dataSource:"employees",
                                id:"initialData",
                                jsURL:"trees/dataBinding/initialDataLOD.js",
                                title:"Initial Data & Load on Demand",
                                description:"\n            Begin opening folders and note the load on demand behavior.\n            \n            Trees that use load on demand can optional specify an initial dataset set up front.  \n            "
                            }
                        ]
                    },
                    {
                        dataSource:"employees",
                        jsURL:"trees/sorting.js",
                        title:"Sorting",
                        description:"\n        Trees sort per folder.  Click on the \"Name\" column header to sort alphabetically by\n        folder name, or on the \"Salary\" column header to sort by Salary.\n    "
                    },
                    {
                        dataSource:"employees",
                        id:"treesEditing",
                        jsURL:"trees/editing.js",
                        title:"Editing",
                        description:"\n        Click on employees in the tree to edit them, and drag and drop employees to rearrange them.\n        Choose an employee via the menu to see that employee's direct reports in the ListGrid.  Changes\n        made in the tree or ListGrid are automatically saved to the server and reflected in the other\n        components.\n    "
                    },
                    {
                        dataSource:"employees",
                        id:"freezeTree",
                        jsURL:"trees/freezeTree.js",
                        title:"Frozen Columns",
                        description:"\n     Setting <code>frozen:true</code> enables frozen columns for Trees.  Columns\n     can be frozen and unfrozen by right-clicking on column headers.<br>\n     Column resize, column reorder, drag and drop and load on demand all function normally.\n     "
                    },
                    {
                        dataSource:"employees",
                        id:"millerColumns",
                        jsURL:"trees/millerColumns.js",
                        title:"Miller Columns",
                        description:"\n        The <code>ColumnTree</code> provides an alternate navigation paradigm for Tree data,\n        sometimes called \"Miller Columns\" and seen in Apple&trade; iTunes&trade;.\n        The <code>ColumnTree</code> provides identical data binding and load on demand facilities to\n        normal <code>TreeGrids</code>.\n        "
                    }
                ]
            },
            {
                icon:"[ISO_DOCS_SKIN]/images/cube_blue.png",
                id:"cubeGrid",
                isOpen:false,
                title:"Cubes",
                description:"\n    Multidimensional \"cube\" data sets as used in BI, Analytics and OLAP applications.\n    Load-on-demand, drill-down, roll-up, in-browser dataset pivoting, multiple frozen panes, \n    resizing and reorder of fields, tree dimensions, chart generation, editing and other\n    features.\n",
                children:[
                    {
                        id:"basicCube",
                        jsURL:"cubes/basicCube.js",
                        requiresModules:"Analytics",
                        title:"Basic Cube",
                        tabs:[
                            {
                                title:"productData",
                                url:"cubes/productData.js"
                            }
                        ],
                        description:"\n        In this multi-dimensional dataset, each cell value has a series of attributes,\n        called \"facets\", that appear as stacked headers labelling the cell value. \n        "
                    },
                    {
                        ref:"analytics",
                        requiresModules:"Analytics",
                        title:"Analytics"
                    }
                ]
            },
            {
                icon:"[ISO_DOCS_SKIN]/images/cube_blue.png",
                id:"comboBoxAndFamily",
                isOpen:false,
                title:"ComboBox & Family",
                description:"\n",
                children:[
                    {
                        ref:"listComboBox",
                        title:"Data Binding"
                    },
                    {
                        ref:"relatedRecords",
                        title:"Related Records"
                    },
                    {
                        dataSource:"employees",
                        id:"formatRelatedValue",
                        jsURL:"combobox/formatRelatedValue.js",
                        title:"Format Related Value",
                        description:"\n        When using an optionDataSource to allow a user to select a record from\n        a related DataSource, you can show a formatted value based on multiple \n        fields in the related record.\n        "
                    },
                    {
                        ref:"formDependentSelects",
                        title:"Dependent Selects"
                    },
                    {
                        id:"dropdownGrid",
                        jsURL:"combobox/dropDownGrid.js",
                        title:"Dropdown Grid",
                        tabs:[
                            {
                                title:"supplyItem",
                                url:"supplyItem.ds.xml"
                            }
                        ],
                        description:"\n        The SelectItem displays multiple fields in a ListGrid.\n        You can scroll to dynamically load more records.\n        This pattern works with any DataSource.\n        "
                    },
                    {
                        dataSource:"employees",
                        id:"formatDropdown",
                        jsURL:"combobox/formatDropDown.js",
                        title:"Format Dropdown",
                        description:"\n        The dropdown list supports formatting APIs that can use multiple fields\n        from related records.\n        "
                    },
                    {
                        ref:"filterRelated",
                        title:"Multi-Field Search"
                    },
                    {
                        id:"multiSelect",
                        jsURL:"combobox/multiSelect.js",
                        title:"Multi-Select",
                        description:"Demonstration of SelectItems with multiple selections."
                    },
                    {
                        ref:"comboBoxStyled",
                        title:"Styled ComboBox"
                    }
                ]
            },
            {
                icon:"[ISO_DOCS_SKIN]/images/silkicons/vcard_edit.png",
                isOpen:false,
                title:"Forms",
                description:"\n    Form managers and input controls.\n",
                children:[
                    {
                        id:"formsLayout",
                        isOpen:false,
                        title:"Layout",
                        description:"\n        A specialized form layout manager allows your forms to grow into available space,\n        hide sections, and span across tabs.\n    ",
                        children:[
                            {
                                id:"formLayoutTitles",
                                jsURL:"forms/layout/titles.js",
                                title:"Titles",
                                description:"\n            Click on \"Swap Titles\" to change title orientation.\n            \n            Form layout automatically places titles next to fields.  Left-oriented titles take\n            up a column so that labels line up.  Top oriented titles don't.\n            "
                            },
                            {
                                id:"columnSpanning",
                                jsURL:"forms/layout/spanning.js",
                                title:"Spanning",
                                description:"\n            Drag resize the form from the right edge to see the effect of spanning.\n            \n            Specifying column widths and column spanning items allows for larger and smaller\n            input areas.\n            "
                            },
                            {
                                id:"formLayoutFilling",
                                jsURL:"forms/layout/filling.js",
                                title:"Filling",
                                description:"\n            Click on the \"Short Message\" and \"Long Message\" buttons to change the amount of\n            space available to the form.\n            \n            SmartClient form layouts allow you to fill available space, even when\n            available space cannot be known in advance because it is data-dependant.\n            "
                            },
                            {
                                id:"formSplitting",
                                jsURL:"forms/layout/valuesManager.js",
                                showSkinSwitcher:true,
                                title:"Splitting",
                                xmlURL:"ValuesManager.xml",
                                description:"\n            Click \"Submit\" to jump to a validation error in the \"Stock\" tab.\n            \n            Forms which are split for layout purposes can behave like a single logical form for\n            validation and saves.\n            <BR><BR>JS and XML tabs show two alternative versions of source, only one is\n                required.\n            "
                            },
                            {
                                id:"formSections",
                                jsURL:"forms/layout/sectionItem.js",
                                showSkinSwitcher:true,
                                title:"Sections",
                                xmlURL:"SectionItem.xml",
                                description:"\n            Click on \"Stock\" to reveal fields relating to stock on hand.\n            <BR><BR>JS and XML tabs show two alternative versions of source, only one is\n                required.\n            "
                            },
                            {
                                ref:"validationFieldBinding",
                                title:"Data Binding"
                            }
                        ]
                    },
                    {
                        isOpen:false,
                        title:"Field Dependencies",
                        description:"\n        Common field dependencies within a form, such as fields that are only applicable to\n        some users, can be handled by specifying simple expressions.\n    ",
                        children:[
                            {
                                id:"formShowAndHide",
                                jsURL:"forms/fieldDependencies/showAndHide.js",
                                title:"Show & Hide",
                                description:"\n            Select \"On order\" to reveal the \"Shipping Date\" field.\n            "
                            },
                            {
                                id:"fieldEnableDisable",
                                jsURL:"forms/fieldDependencies/enableAndDisable.js",
                                title:"Enable & Disable",
                                description:"\n            Check \"I accept the agreement\" to enable the \"Proceed\" button.\n            "
                            },
                            {
                                id:"conditionallyRequired",
                                jsURL:"forms/fieldDependencies/conditionallyRequired.js",
                                title:"Conditionally Required",
                                description:"\n            Select \"No\" and click the \"Validate\" button - the reason field becomes required.\n            "
                            },
                            {
                                id:"matchValue",
                                jsURL:"forms/fieldDependencies/matchValue.js",
                                title:"Match Value",
                                description:"\n            Try entering mismatched values for \"Password\" and \"Password Again\", then click\n            \"Create Account\" to see a validation error.\n            "
                            },
                            {
                                id:"formDependentSelects",
                                jsURL:"forms/fieldDependencies/dependentSelects.js",
                                title:"Dependent Selects",
                                tabs:[
                                    {
                                        dataSource:"supplyItem",
                                        name:"supplyItem"
                                    },
                                    {
                                        dataSource:"supplyCategory",
                                        name:"supplyCategory"
                                    }
                                ],
                                description:"\n            <p />\n            In the first example, select a \"Division\" to cause the \"Department\" select to be \n            populated with departments from that division.\n            <p />\n            The second example demonstrates two select items, both of which load data on the fly from\n            a DataSource, where the \"Category\" drop-down controls the list of available items\n            in the \"Item\" drop-down.  Try selecting a value in the \"Category\" drop-down list to change the set of options \n            available in the \"Item\" drop-down.\n            "
                            }
                        ]
                    },
                    {
                        isOpen:false,
                        title:"Form Controls",
                        description:"\n        The form has built-in editors and pickers for common types such as numbers and dates,\n        as well as the ability to use the databinding framework to pick from lists and trees of\n        related records.\n    ",
                        children:[
                            {
                                id:"textItem",
                                jsURL:"forms/dataTypes/text.js",
                                title:"Text",
                                xmlURL:"TextItem.xml",
                                description:"\n                JS and XML tabs show two alternative versions of source, only one is\n                required.\n            "
                            },
                            {
                                descriptionHeight:"350",
                                id:"maskedTextItem",
                                jsURL:"forms/dataTypes/maskedTextItem.js",
                                title:"Text - Masked",
                                description:"\n            <p>TextItems support a masked entry to restrict and format entry.</p>\n            <p>Overview of available mask characters</p>\n            <p><table class=\"normal\">\n            <tr>\n                <th>Character</th>\n                <th>Description</th>\n            </tr>\n            <tr>\n                <td>0</td>\n                <td>Digit (0 through 9) or plus [+] or minus [-] signs</td>\n            </tr>\n            <tr>\n                <td>9</td>\n                <td>Digit or space</td>\n            </tr>\n            <tr>\n                <td>#</td>\n                <td>Digit</td>\n            </tr>\n            <tr>\n                <td>L</td>\n                <td>Letter (A through Z)</td>\n            </tr>\n            <tr>\n                <td>?</td>\n                <td>Letter (A through Z) or space</td>\n            </tr>\n            <tr>\n                <td>A</td>\n                <td>Letter or digit</td>\n            </tr>\n            <tr>\n                <td>a</td>\n                <td>Letter or digit</td>\n            </tr>\n            <tr>\n                <td>C</td>\n                <td>Any character or space</td>\n            </tr>\n            <tr>\n                <td>&nbsp;</td>\n            </tr>\n            <tr>\n                <td>&lt;</td>\n                <td>Causes all characters that follow to be converted to lowercase</td>\n            </tr>\n            <tr>\n                <td>&gt;</td>\n                <td>Causes all characters that follow to be converted to uppercase</td>\n            </tr>\n            </table></p>\n            <p>Any character not matching one of the above mask characters or that is\n            escaped with a backslash (\\) is considered to be a literal.</p>\n            <p>Custom mask characters can be defined by standard regular expression\n            character set or range. For example, a hexadecimal color code mask could be:\n            <UL>\n                <LI>Color: \\#>[0-9A-F][0-9A-F][0-9A-F][0-9A-F][0-9A-F][0-9A-F]</LI>\n            </UL></p>\n            "
                            },
                            {
                                id:"textAreaItem",
                                jsURL:"forms/dataTypes/textAreaItem.js",
                                title:"TextArea",
                                xmlURL:"TextAreaItem.xml",
                                description:"\n                JS and XML tabs show two alternative versions of source, only one is\n                required.\n            "
                            },
                            {
                                id:"dateItem",
                                jsURL:"forms/dataTypes/dateItem.js",
                                title:"Date",
                                xmlURL:"DateItem.xml",
                                description:"\n            DateItems support direct or pickList-based input of dates, and have a built-in\n            pop-up day picker.\n            <BR><BR>JS and XML tabs show two alternative versions of source, only one is\n             required.\n            "
                            },
                            {
                                id:"timeItem",
                                jsURL:"forms/dataTypes/timeItem.js",
                                title:"Time",
                                xmlURL:"TimeItem.xml",
                                description:"\n            TimeItem supports text-based input of Times.\n            <BR><BR>JS and XML tabs show two alternative versions of source, only one is\n             required.\n            "
                            },
                            {
                                id:"spinnerItem",
                                jsURL:"forms/dataTypes/numberSpinner.js",
                                title:"Number - Spinner",
                                description:"\n            Click the up and down buttons to change shoe size.  Click and hold to change shoe\n            size quickly.  Note spinner stops at a maximum and minimum value.\n            "
                            },
                            {
                                id:"sliderItem",
                                jsURL:"forms/dataTypes/numberSlider.js",
                                title:"Number - Slider",
                                description:"\n            Change the value by clicking and dragging the thumb, clicking on the track, or\n            using the keyboard. \n            "
                            },
                            {
                                id:"checkboxItem",
                                jsURL:"forms/dataTypes/checkboxItem.js",
                                title:"Boolean - Checkbox",
                                xmlURL:"CheckboxItem.xml",
                                description:"\n            JS and XML tabs show two alternative versions of source, only one is\n             required.     \n            "
                            },
                            {
                                id:"selectItem",
                                jsURL:"forms/dataTypes/listSelect.js",
                                title:"List - Select",
                                description:"\n            Note the icons and customized text styling.  Click to reveal the options and note\n            the drop shadow.  \n            \n            The SmartClient SelectItem offers more powerful and consistent control over\n            appearance and behavior than the HTML &lt;SELECT&gt; element.\n            "
                            },
                            {
                                dataSource:"supplyItem",
                                id:"listComboBox",
                                jsURL:"forms/dataTypes/listComboBox.js",
                                title:"List - Combo Box",
                                description:"\n            Start typing in either field to see a list of matching options.  The field\n            labelled \"Item Name\" retrieves options dynamically from the SupplyItem\n            DataSource\n            "
                            },
                            {
                                dataSource:"supplyItem",
                                id:"comboBoxStyled",
                                jsURL:"forms/dataTypes/comboBoxStyled.js",
                                title:"Combo Box - Styled",
                                description:"\n           Combo box rows can be styled via html to display data in almost any \n           way imaginable. Row hovers are also customized in this example.\n        "
                            },
                            {
                                dataSource:"supplyItem",
                                id:"relatedRecords",
                                jsURL:"forms/dataTypes/relatedRecords.js",
                                showSkinSwitcher:true,
                                title:"List - Related Records",
                                description:"\n            Open the picker in either form to select the item you want to order from the\n            \"supplyItem\" DataSource.  The picker on the left stores the \"itemId\" from the\n            related \"supplyItem\" records.  The picker on the right stores the \"SKU\" while\n            displaying multiple fields.  You can scroll to dynamically load more records.  \n            This pattern works with any DataSource.  \n            "
                            },
                            {
                                dataSource:"supplyItem",
                                descriptionHeight:"150",
                                id:"filterRelated",
                                jsURL:"forms/dataTypes/filterPickList.js",
                                showSkinSwitcher:true,
                                title:"List - Multi-Field Search",
                                description:"\n            Click on the SelectItem on the left to see the full set of data. You can enter filter\n            criteria directly on the drop-down list in either field to filter the set of\n            options down to a managable size.<P>\n            Now move focus to the ComboBoxItem and start typing. The set of options displayed are\n            automatically filtered against both fields as you type. Tab or Enter keypress will complete selection.\n            "
                            },
                            {
                                id:"colorItem",
                                jsURL:"forms/dataTypes/colorItem.js",
                                title:"Color",
                                xmlURL:"ColorItem.xml",
                                description:"\n            ColorItems support direct input of HTML color values (in the form #RRGGBB), or \n            selection of colors via a ColorPicker widget.\n            <BR><BR>JS and XML tabs show two alternative versions of source, only one is\n             required.\n            "
                            },
                            {
                                dataSource:"supplyCategory",
                                id:"pickTree",
                                jsURL:"forms/dataTypes/pickTree.js",
                                showSkinSwitcher:true,
                                title:"Tree",
                                xmlURL:"PickTree.xml",
                                description:"\n            Click on \"Department\" or \"Category\" below to show hierarchical menus.  The\n            \"Category\" menu loads options dynamically from the SupplyCategory DataSource.\n            <BR><BR>JS and XML tabs show two alternative versions of source, only one is\n             required.\n            "
                            },
                            {
                                title:"List - Select Other",
                                visibility:"sdk",
                                xmlURL:"SelectOtherItem.xml",
                                description:"\n            Select \"Other..\" from the drop down to enter a custom value.\n            <BR><BR>This example source is written in XML. \n            SmartClient supports code written directly in JavaScript, or in this declaritive XML\n            format.\n            "
                            },
                            {
                                ref:"RichTextEditor",
                                title:"HTML"
                            },
                            {
                                dataSource:"countryDS",
                                id:"canvasItem",
                                jsURL:"forms/dataTypes/canvasItem.js",
                                title:"CanvasItem",
                                description:"\n                A special type of form control called a CanvasItem allows any kind of SmartClient widget to\n                participate in form layout and values management.\n                <P>\n                Drag resize the form - notice how the embedded ListGrid fills the available space.\n                <P>\n                The embedded ListGrid starts out showing the initial value provided to the form (\"Germany\").\n                Click the button titled \"Set Value: France\" to provide a new value to the form, causing the\n                CanvasItem to display this value.\n                <P>\n                Click on any country in the list - the form picks up the value and fires standard change\n                events, causing new values to be displayed in a Label.\n                <P>\n                This CanvasItem provides functionality similar to an HTML <select>, however, because it's\n                based on a ListGrid, any ListGrid behavior could be added: data paging, drag and drop, hovers,\n                inline search, inline editing, grouping, etc.\n	        "
                            },
                            {
                                id:"nestedEditor",
                                jsURL:"forms/dataTypes/nestedEditing.js",
                                title:"CanvasItem - Nested Editor",
                                description:"\n                This example shows a reusable CanvasItem that edits nested data structures.\n                <P>\n                Here, a Hibernate entity representing an Order contains OrderItems - in the\n                Record for\n                an Order, value of the field \"items\" in an Array of Records representing OrderItems.\n                <P>\n                The CanvasItem embeds an editable ListGrid to provide an editing interaction for the\n                OrderItems right in the midst of the form.  It can be used with any DataSource that\n                has nested records.\n	        ",
                                tabs:[
                                    {
                                        canEdit:"false",
                                        dataSource:"masterDetail_orderHB",
                                        title:"masterDetail_orderHB"
                                    },
                                    {
                                        canEdit:"false",
                                        dataSource:"masterDetail_orderItemHB",
                                        name:"masterDetail_orderItem"
                                    }
                                ]
                            }
                        ]
                    },
                    {
                        isOpen:false,
                        ref:"validation",
                        title:"Validation"
                    },
                    {
                        isOpen:false,
                        title:"Details",
                        description:"\n        Hovers and hints explain the form to the user.  Icons provide an easy extension point\n        for help, custom pickers and other extensions.  KeyPress filtering allows character\n        casing to be forced on entry or invalid keystrokes to be ignored.\n    ",
                        children:[
                            {
                                id:"formIcons",
                                jsURL:"forms/layout/icons.js",
                                title:"Icons",
                                description:"\n            Click on the help icon below to see a description for severity levels.  Form items\n            can show an arbitrary number of icons to do whatever you need.\n            "
                            },
                            {
                                id:"itemHoverHTML",
                                jsURL:"forms/details/hovers.js",
                                title:"Hovers",
                                description:"\n            Hover anywhere over the field to see what the current value means.  Change the\n            value or disable the field to see different hovers.  Note that the hovers contain\n            HTML formatting.  \n            "
                            },
                            {
                                id:"formHints",
                                jsURL:"forms/layout/hints.js",
                                title:"Hints",
                                description:"\n            Hints provide guidance to the user filling out the form.  In this case, the MM/YYYY\n            hint tells the user the expected format for the free-form date field. Note both\n            trailing and in-field styles are shown.\n            "
                            },
                            {
                                id:"formFilters",
                                jsURL:"forms/details/filters.js",
                                title:"KeyPress Filters",
                                description:"\n            KeyPress filters help prevent the user from entering invalid characters.\n            Additionally, character casing can be forced to either upper or lowercase.\n            "
                            }
                        ]
                    }
                ]
            },
            {
                icon:"[ISO_DOCS_SKIN]/images/silkicons/cal.png",
                isOpen:false,
                showSkinSwitcher:"true",
                title:"Calendars",
                description:"Customizable calendars that display events in day, week, and month views.",
                children:[
                    {
                        cssURL:"calendar/calendar.css",
                        id:"simpleCalendar",
                        jsURL:"calendar/simpleCalendar.js",
                        requiresModules:"Calendar",
                        title:"Simple Calendar",
                        tabs:[
                            {
                                title:"eventData",
                                url:"calendar/calendarData.js"
                            }
                        ],
                        description:"\n            This calendar is bound to an array of event data. Drag events to alter their start times, and\n            resize events to alter their durations. Click and drag in an empty cell to create new events,\n            or click on an existing event to edit it. Notice how the red event can't be edited; This was \n            specified within the event data itself (see the 'eventData' tab below).\n        "
                    },
                    {
                        cssURL:"calendar/calendar.css",
                        id:"databoundCalendar",
                        jsURL:"calendar/databoundCalendar.js",
                        requiresModules:"Calendar",
                        title:"Databound Calendar",
                        tabs:[
                            {
                                title:"eventData",
                                url:"calendar/calendarData.js"
                            }
                        ],
                        description:"\n            This calendar is bound to a dataSource. Drag events to alter their start times, and\n            resize events to alter their durations. Click and drag in an empty cell to create new events,\n            or click on an existing event to edit it. Notice how the red event can't be edited; This was \n            specified within the event data itself (see the 'eventData' tab below).\n        "
                    },
                    {
                        id:"compactCalendar",
                        jsURL:"calendar/compactCalendar.js",
                        requiresModules:"Calendar",
                        title:"Compact Calendar",
                        tabs:[
                            {
                                title:"eventData",
                                url:"calendar/calendarData.js"
                            }
                        ],
                        description:"\n            Hover over the days with the check icon in them to see the events for those days.\n            Use the 'next' and 'previous' arrows to change months.\n        "
                    },
                    {
                        cssURL:"calendar/calendar.css",
                        id:"workdayCalendar",
                        jsURL:"calendar/workdayCalendar.js",
                        requiresModules:"Calendar",
                        title:"Workday Calendar",
                        tabs:[
                            {
                                title:"eventData",
                                url:"calendar/calendarData.js"
                            }
                        ],
                        description:"\n            The calendar can focus in on workday hours, giving a clearer view of events that occur\n            durring the work day. The boundaries of the workday itself can also be customized.\n        "
                    },
                    {
                        cssURL:"calendar/calendar.css",
                        id:"customCalendar",
                        jsURL:"calendar/customEventEditing.js",
                        requiresModules:"Calendar",
                        title:"Custom Event Editing",
                        tabs:[
                            {
                                title:"eventData",
                                url:"calendar/calendarData.js"
                            }
                        ],
                        description:"\n            Click in an empty cell or in an event to see custom fields in the quick event editor and in \n            the full event editor. Notice how the red event can't be edited; This was \n            specified within the event data itself (see the 'eventData' tab below).\n        "
                    },
                    {
                        cssURL:"calendar/calendar.css",
                        id:"eventAutoArrange",
                        jsURL:"calendar/eventAutoArrange.js",
                        requiresModules:"Calendar",
                        title:"Event Auto-Arranging",
                        tabs:[
                            {
                                title:"eventOverlapData",
                                url:"calendar/calendarOverlapData.js"
                            }
                        ],
                        description:"\n            The calendar can automatically arrange events that share time so that each is always\n            fully visible at its proper location.  Drag one event onto or away from another to \n            see the effect.\n        "
                    },
                    {
                        cssURL:"calendar/calendar.css",
                        id:"eventOverlapping",
                        jsURL:"calendar/eventOverlapping.js",
                        requiresModules:"Calendar",
                        title:"Event Overlapping",
                        tabs:[
                            {
                                title:"eventOverlapData",
                                url:"calendar/calendarOverlapData.js"
                            }
                        ],
                        description:"\n            When eventAutoArrange is true, you can have the Calendar overlap concurrent \n            events slightly.  The zorder is from left to right and the overlap-size is a \n            percentage of event-width (see the \"JS\" tab).  If two events start at exactly the \n            same time, the default behavior is to reject the overlap to avoid the first event's \n            close button from being hidden by the second event (see the \"JS\" tab).  You can see\n            this by dropping one event onto the start-time of another below.\n        "
                    }
                ]
            },
            {
                icon:"[ISO_DOCS_SKIN]/images/silkicons/application_side_list.png",
                isOpen:false,
                title:"Layout",
                description:"\n    Liquid layout managers and user interface containers.\n",
                children:[
                    {
                        jsURL:"forms/layout/filling.js",
                        title:"Filling",
                        description:"\n        Click on the \"Short Message\" and \"Long Message\" buttons to change the amount of\n        space available to the form.\n        \n        Layouts automatically react to resizes and re-apply the layout policy.\n        "
                    },
                    {
                        id:"layoutNesting",
                        jsURL:"layout/nesting.js",
                        showSkinSwitcher:true,
                        title:"Nesting",
                        description:"\n        Use the resize bars to reallocate space between the 3 panes.\n        \n        Layouts can be nested to create standard application views.  Resize bars are built-in.\n        "
                    },
                    {
                        id:"userSizing",
                        jsURL:"layout/userSizing.js",
                        title:"User Sizing",
                        description:"\n        Resize the outer frame to watch \"Member 1\" and \"Member 2\" split the space.  Now resize\n        either member and resize the outer frame again.\n        \n        Layouts track sizes which have been set by user action and respect the user's settings.\n        "
                    },
                    {
                        id:"layoutCenterAlign",
                        jsURL:"layout/centerAlign.js",
                        title:"Center Align",
                        descriptionHeight:"160",
                        description:"\n          \n            <p>\n              To center components within layouts, set <code>layout.align</code> to center along the\n              <i>length</i> axis (vertical axis for a <code>VLayout</code>, horizontal axis for an \n              <code>HLayout</code>).\n            </p><p>\n              To center along the <i>breadth</i> axis (horizontal axis for an <code>VLayout</code>, \n              vertical axis for an <code>HLayout</code>), set <code>member.layoutAlign</code> on each \n              member that should be centered, or set <code>layout.defaultLayoutAlign</code> to center \n              all members.\n            </p><p>\n              Combine both settings to center along both axes.\n            </p><p>\n              You can also use LayoutSpacers to center components.  This is particularly useful if you\n              have a layout where you want to center something in the <i>remaining space</i> after other\n              components have taken the space they require.\n            </p>\n          \n        "
                    },
                    {
                        id:"snapTo",
                        jsURL:"layout/snapto.js",
                        title:"Snap To",
                        description:"\n		\n        <p>\n        Snap-to positioning can be used to place components along a specific edge or corners of a \n		container, or centered in the container. <code>snapEdge</code> allows you to attach a \n		corner or edge of a component to a corner or edge of another component, and \n		<code>snapOffsetLeft/Top</code> allows you to place components at a specific pixel or \n		percentage offset relative to a snap position. \n		</p><p>\n		Drag resize the containers below to see a variety of snap-to positioning behaviors. \n		</p>\n		\n        "
                    },
                    {
                        id:"dragSnapTo",
                        jsURL:"dragdrop/dragSnapTo.js",
                        showSkinSwitcher:true,
                        title:"Snap-to-grid Dragging",
                        description:"\n        Drag the box around the grid. It will snap into alignment according to the values you \n        set in the radio buttons below. Snap-to-grid dragging can be enabled separately for \n        moving and resizing; toggle the checkboxes to see this working.\n        "
                    },
                    {
                        icon:"[ISO_DOCS_SKIN]/images/silkicons/vcard_edit.png",
                        ref:"formsLayout",
                        title:"Form Layout"
                    },
                    {
                        icon:"[ISO_DOCS_SKIN]/images/silkicons/application_cascade.png",
                        isOpen:false,
                        showSkinSwitcher:"true",
                        title:"Windows",
                        description:"\n        Windows for dialogs, wizards, tools and free-form application layouts.\n    ",
                        children:[
                            {
                                id:"windowAutosize",
                                jsURL:"layout/window/autoSize.js",
                                title:"Auto Size",
                                description:"\n            Windows can autoSize to content or can dictate the content's size.\n            "
                            },
                            {
                                ref:"modality",
                                title:"Modality"
                            },
                            {
                                jsURL:"layout/window/dragging.js",
                                title:"Dragging",
                                description:"\n            Grab the window by its title bar to move it around.  Resize it by the right or\n            bottom edge.\n            "
                            },
                            {
                                ref:"windowMinimize",
                                title:"Minimize"
                            },
                            {
                                id:"windowHeaderControls",
                                jsURL:"layout/window/controls.js",
                                title:"Header Controls",
                                description:"\n            Header controls can be reordered and custom controls added.\n            "
                            },
                            {
                                id:"windowFooter",
                                jsURL:"layout/window/footer.js",
                                title:"Footer",
                                description:"\n            Windows support a footer with a visible resizer and updateable status bar.\n            "
                            }
                        ]
                    },
                    {
                        icon:"[ISO_DOCS_SKIN]/images/silkicons/tab.png",
                        isOpen:false,
                        showSkinSwitcher:"true",
                        title:"Tabs",
                        description:"\n        Tabs for sectioning applications and forms.\n    ",
                        children:[
                            {
                                id:"tabsOrientation",
                                jsURL:"layout/tabs/orientation.js",
                                title:"Orientation",
                                description:"\n            Tabs can be horizontally or vertically oriented.  To select tabs, click on them, or\n            on click the \"Select Blue\" and \"Select Green\" buttons.\n            "
                            },
                            {
                                id:"tabsAlign",
                                jsURL:"layout/tabs/align.js",
                                title:"Align",
                                description:"\n            Tabs can be left or right aligned (for horizontal tabs) or top or bottom aligned\n            (for vertical tabs)\n            "
                            },
                            {
                                id:"tabsAddAndRemove",
                                jsURL:"layout/tabs/addAndRemove.js",
                                title:"Add and Remove",
                                description:"\n            Click on \"Add Tab\" and \"Remove Tab\" to add and remove tabs.   When you add too many\n            tabs to display at once, a tab scrolling interface will appear.\n            "
                            },
                            {
                                id:"closeableTabs",
                                jsURL:"layout/tabs/closeableTabs.js",
                                title:"Closeable Tabs",
                                description:"\n            Click on the red close icons to close tabs.  Tabbed views can have any mixture of\n            closeable and permanent tabs.\n            "
                            },
                            {
                                id:"titleChange",
                                jsURL:"layout/tabs/titleChange.js",
                                title:"Title Change",
                                description:"\n            Titles can be changed on the fly.  Type in your name to see the \"Preferences\" tab\n            change its title to include your name.  Note that the tab automatically sizes to\n            accommodate the longer title - automatic sizing also happens at initialization.\n            "
                            },
                            {
                                id:"userEditableTitles",
                                jsURL:"layout/tabs/userEditableTitles.js",
                                title:"User-Editable Titles",
                                description:"\n            Optionally, titles can be directly edited in place by your application's end users.\n            This TabSet specifies <code>canEditTabTitles</code> - double-click a tab title to \n            edit it.  Individual tabs can override the TabSet behavior; in this example, the \n            \"Can't change me\" tab has <code>canEditTitle</code> set to false.  Your code can\n            cancel the user changes - try editing the \"123-Yellow\" tab to a title that doesn't \n            begin with \"123-\"\n            "
                            },
                            {
                                id:"selectionEvents",
                                jsURL:"layout/tabs/selectionEvents.js",
                                title:"Selection and Deselection Handling",
                                description:"\n            Developers can apply custom event handler logic to fire when the user selects tabs.\n            The preferences pane in this example has a tabSelected handler which will create\n            its pane lazily the first time the tab is selected, and a tabDeselected handler\n            which returns false to stop the user changing tabs if the form item is unchecked.\n            "
                            },
                            {
                                id:"viewLoading",
                                jsURL:"advanced/viewLoading.js",
                                needXHR:"true",
                                title:"View Loading",
                                description:"\n            Click on \"Tab2\" to load a grid view on the fly.\n            \n            Declarative view loading allows extremely large applications to be split into\n            separately loadable chunks, and creates an easy integration path for applications\n            with server-driven application flow.\n            ",
                                tabs:[
                                    {
                                        canEdit:"false",
                                        doEval:"false",
                                        title:"loadedView",
                                        url:"advanced/loadedView.js"
                                    }
                                ]
                            }
                        ]
                    },
                    {
                        icon:"[ISO_DOCS_SKIN]/images/silkicons/application_tile_vertical.png",
                        isOpen:false,
                        showSkinSwitcher:"true",
                        title:"Sections",
                        description:"\n        Sections (also called Accordions) label sections of the application\n        and allow users to hide or resize sections.\n    ",
                        children:[
                            {
                                id:"sectionsExpandCollapse",
                                jsURL:"layout/sections/expandCollapse.js",
                                title:"Expand / Collapse",
                                description:"\n            Click on any section header showing an arrow to expand and collapse it (the \"Green \n            Cube\" section is marked not collapsible).  Click on the \"Expand Blue\" and \n            \"Collapse Blue\" buttons to expand and collapse sections externally.\n            "
                            },
                            {
                                id:"resizeSections",
                                jsURL:"layout/sections/resizeSections.js",
                                title:"Resize Sections",
                                description:"\n            Drag the \"Help 2\" header to resize sections, or press \"Resize Help 1\" to resize to\n            fixed height.  The \"Blue Pawn\" section is marked not resizeable.\n            "
                            },
                            {
                                id:"sectionControls",
                                jsURL:"layout/sections/sectionControls.js",
                                title:"Custom Controls",
                                description:"\n            Custom controls may appear on section headers.\n            "
                            },
                            {
                                id:"sectionsAddAndRemove",
                                jsURL:"layout/sections/addAndRemove.js",
                                title:"Add and Remove",
                                description:"\n            Press the \"Add Section\" and \"Remove Section\" buttons to add or remove sections.\n            "
                            },
                            {
                                id:"sectionsShowAndHide",
                                jsURL:"layout/sections/showAndHide.js",
                                title:"Show and Hide",
                                description:"\n            Press the \"Show Section\" and \"Hide Section\" buttons to reveal or hide the Yellow\n            Section.  Showing and hiding sections lets you reuse a SectionStack for slightly\n            different purposes, hiding or revealing relevant sections.\n            "
                            }
                        ]
                    }
                ]
            },
            {
                icon:"[ISO_DOCS_SKIN]/images/silkicons/disconnect.png",
                isOpen:false,
                title:"Data Binding",
                description:"\n    Data binding allows multiple components to share a central definition of an object (called\n    a DataSource), so that all components can consistently retrieve, display, edit, validate\n    and save objects of that type.\n",
                children:[
                    {
                        id:"lists",
                        isOpen:false,
                        ref:"gridsDataBinding",
                        showSkinSwitcher:"true",
                        title:"Lists"
                    },
                    {
                        id:"trees",
                        isOpen:false,
                        ref:"treesDataBinding",
                        showSkinSwitcher:"true",
                        title:"Trees"
                    },
                    {
                        id:"operations",
                        isOpen:false,
                        showSkinSwitcher:"true",
                        title:"Operations",
                        description:"\n    DataBound Components understand a core set of operations called \"Fetch\", \"Add\", \"Update\"\n    and \"Remove\" (also known as CRUD operations).  These operations can be programmatically\n    initiated or automatically initiated in response to user action.\n    In either case the integration model and APIs are the same.\n    ",
                        children:[
                            {
                                dataSource:"supplyItem",
                                id:"fetchOperation",
                                jsURL:"databind/operations/fetch.js",
                                title:"Fetch",
                                xmlURL:"databind/operations/fetch.xml",
                                description:"\n            Rows are fetched automatically as the user drags the scrollbar.  Drag the scrollbar\n            quickly to the bottom to fetch a range near the end (a prompt will appear during\n            server fetch).  Scroll slowly back up to fill in the middle.\n            "
                            },
                            {
                                dataSource:"supplyItem",
                                id:"addOperation",
                                title:"Add",
                                xmlURL:"databind/operations/add.xml",
                                description:"\n            Use the form to create a new stock item.  Create an item in the currently shown\n            category to see it appear in the filtered listing automatically.  Create an item in\n            any other category and note that it is filtered out.\n            "
                            },
                            {
                                dataSource:"supplyItem",
                                id:"updateOperation",
                                title:"Update",
                                xmlURL:"databind/operations/update.xml",
                                description:"\n            Select an item and use the form to change its price.  The list updates\n            automatically.  Now change the item's category and note that it is removed\n            automatically from the list.\n            "
                            },
                            {
                                dataSource:"supplyItem",
                                id:"removeOperation",
                                title:"Remove",
                                xmlURL:"databind/operations/remove.xml",
                                description:"\n            Click the \"Remove\" button to remove the selected item.\n            "
                            }
                        ]
                    },
                    {
                        id:"validation",
                        isOpen:false,
                        title:"Validation",
                        description:"\n        Typical validation needs are covered by validators built-in to the SmartClient\n        framework.  Validators can be combined into custom type definitions which are reusable\n        across all components.\n    ",
                        children:[
                            {
                                icon:"[ISO_DOCS_SKIN]/images/silkicons/server_lightning.png",
                                id:"serverValidationCopy",
                                isOpen:false,
                                title:"Server-based",
                                description:"\n            The SmartClient Server provides powerful support for server-based validation.\n        ",
                                children:[
                                    {
                                        dataSource:"supplyItem",
                                        id:"singleSourceValidation",
                                        jsURL:"dataIntegration/java/serverValidation.js",
                                        requiresModules:"SCServer",
                                        title:"Single Source",
                                        description:"\n            Validation rules are automatically enforced on both the client- and server-side based on\n            a single, shared declaration.  Press \"Save\" to see errors from client-side\n            validation.  Press \"Clear Errors\" then \"Disable Validation\" then \"Save\" again to see the\n            same errors caught by the SmartClient server.\n            "
                                    },
                                    {
                                        dataSource:"queuing_userHB",
                                        id:"uniqueCheckValidation",
                                        jsURL:"dataIntegration/java/uniqueCheckValidation.js",
                                        requiresModules:"SCServer",
                                        title:"Unique Check",
                                        description:"\n            Enter the email address \"kamirov@server.com\" in the email field and press Tab. Do so with\n            any other email address as well.\n            <P/>\n            The resulting validation error is based upon the server-side isUnique validator that\n            checks to see if there is already a record in the DataSource and if so fails validation. \n            "
                                    },
                                    {
                                        dataSource:"velocity_orderForm",
                                        id:"velocityValidation",
                                        jsURL:"dataIntegration/java/velocityValidation.js",
                                        requiresModules:"SCServer",
                                        title:"Velocity Expression",
                                        tabs:[
                                            {
                                                canEdit:"false",
                                                dataSource:"StockItem",
                                                title:"StockItem"
                                            }
                                        ],
                                        description:"\n            Use the \"Item Id\" ComboBox to select an item,  enter a very large quantity (999999)\n            and press the \"Submit Order\" button.\n            <P/>\n            The resulting validation error is based upon server-side condition specified in\n            the validator using a Velocity expression. It checks a related DataSource (StockItem)\n            to see if there is sufficient quantity in stock to fulfill the order.\n            "
                                    },
                                    {
                                        dataSource:"validationDMI_orderForm",
                                        id:"dmiValidation",
                                        jsURL:"dataIntegration/java/validationDMI.js",
                                        requiresModules:"SCServer",
                                        title:"DMI Validation",
                                        tabs:[
                                            {
                                                canEdit:"false",
                                                dataSource:"StockItem",
                                                title:"StockItem"
                                            },
                                            {
                                                canEdit:"false",
                                                doEval:"false",
                                                title:"ValidatorDMI.java",
                                                url:"serverExamples/validation/ValidatorDMI.java"
                                            }
                                        ],
                                        descriptionHeight:"150",
                                        description:"\n            Use the \"Item Id\" ComboBox to select an item,  enter a very large quantity (999999)\n            and press the \"Submit Order\" button.\n            <P/>\n            The resulting validation error is based upon server-side logic in ValidatorDMI.java\n            that checks a related DataSource (StockItem) to see if there is sufficient quantity in\n            stock to fulfill the order.  Hover over the error icon to see the error message and\n            note that it includes an indication of the stock level: error messages are Velocity \n            templates, and DMI validators can easily populate variable values, as \n            ValidatorDMI.java shows\n            <P/>\n            Validators can use SmartClient DMI to call any server-side method to check the validity\n            of data, including methods on Java beans looked up via Spring.\n            "
                                    },
                                    {
                                        dataSource:"complaint",
                                        id:"hasRelatedValidation",
                                        jsURL:"dataIntegration/java/hasRelatedValidation.js",
                                        requiresModules:"SCServer",
                                        title:"Related Records",
                                        tabs:[
                                            {
                                                canEdit:"false",
                                                dataSource:"masterDetail_orderHB",
                                                title:"masterDetail_orderHB"
                                            }
                                        ],
                                        description:"\n            Enter a complaint for a received shipment using its tracking number. The tracking\n            number must reference an existing tracking number so try with an existing number\n            (4110884 or 9631143) and with a random number (like 1234).\n            <P/>\n            The relatedRecord validator can be used to validate that an ID entered by\n            a user actually exists.  This is useful in situations where using a comboBox for record\n            lookup is inappropriate (the user should not be able to select among all valid tracking\n            numbers, or among other types of IDs, such as license keys or driver's license numbers)\n            or in situations such as batch upload of many records.\n            <P/>\n            The relatedRecord validator can also be used with a ComboBox as the UI in order to\n            enforce that related records are checked <b>before</b> a request reaches business logic\n            where it would be convenient to assume the ID is already validated, or as a means of\n            enforcing referential integrity in systems that don't have built-in enforcement.\n            "
                                    },
                                    {
                                        dataSource:"complaint",
                                        id:"blockingErrors",
                                        jsURL:"dataIntegration/java/blockingErrors.js",
                                        requiresModules:"SCServer",
                                        title:"Blocking Errors",
                                        tabs:[
                                            {
                                                canEdit:"false",
                                                dataSource:"masterDetail_orderHB",
                                                title:"masterDetail_orderHB"
                                            }
                                        ],
                                        description:"\n            Enter a complaint for a received shipment using its tracking number. The tracking\n            number must reference an existing tracking number so try with an existing number\n            (4110884 or 9631143) and with a random number (like 1234). Note that when a\n            non-existing value is entered, focus is not allowed to move forward.\n            <P/>\n            The relatedRecord validator can be used to validate that an ID entered by\n            a user actually exists.  This is useful in situations where using a comboBox for record\n            lookup is inappropriate (the user should not be able to select among all valid tracking\n            numbers, or among other types of IDs, such as license keys or driver's license numbers)\n            or in situations such as batch upload of many records.\n            <P/>\n            The relatedRecord validator can also be used with a ComboBox as the UI in order to\n            enforce that related records are checked <b>before</b> a request reaches business logic\n            where it would be convenient to assume the ID is already validated, or as a means of\n            enforcing referential integrity in systems that don't have built-in enforcement.\n            "
                                    }
                                ]
                            },
                            {
                                dataSource:"databind/validation/type.ds.xml",
                                id:"validationType",
                                jsURL:"databind/validation/type.js",
                                title:"Type",
                                description:"\n            Type a non-numeric value into the field and press \"Validate\" to receive a\n            validation error.\n            \n            Declaring field type implies automatic validation anywhere a value is edited.\n            "
                            },
                            {
                                dataSource:"databind/validation/builtins.ds.xml",
                                id:"validationBuiltins",
                                jsURL:"databind/validation/builtins.js",
                                title:"Built-ins",
                                description:"\n            Type a number greater than 20 or less than 1 and press \"Validate\" to receive a\n            validation error.\n            \n            SmartClient implements the XML Schema set of validators on both client and server\n            "
                            },
                            {
                                dataSource:"databind/validation/regularExpression.ds.xml",
                                id:"regularExpression",
                                jsURL:"databind/validation/regularExpression.js",
                                title:"Regular Expression",
                                description:"\n            Enter a bad email address (eg just \"mike\") and press \"Validate\" to receive a\n            validation error.\n            \n            The regular expression validator allows simple custom field types, with automatic\n            enforcement on client on server.\n            "
                            },
                            {
                                dataSource:"databind/validation/valueTransform.ds.xml",
                                id:"valueTransform",
                                jsURL:"databind/validation/valueTransform.js",
                                title:"Value Transform",
                                description:"\n            Enter a 10 digit US phone number with any typical punctuation press \"Validate\" to see it\n            transformed to a canonical format.\n            "
                            },
                            {
                                dataSource:"databind/validation/customTypes.ds.xml",
                                id:"customSimpleType",
                                jsURL:"databind/validation/customTypes.js",
                                title:"Custom Types",
                                description:"\n            Enter a bad zip code (eg just \"123\") and press \"Validate\" to receive a\n            validation error.\n            \n            Custom types can be declared based on built-in validators and re-used in multiple\n            DataSources\n            "
                            },
                            {
                                dataSource:"databind/forms/users.ds.xml",
                                id:"validationFieldBinding",
                                jsURL:"databind/forms/customBinding.js",
                                title:"Customized Binding",
                                description:"\n            Click \"Validate\" to see validation errors triggered by rules both in this form and\n            in the DataSource.\n            \n            Screen-specific fields and validation logic, such as the duplicate password entry\n            box, can be added to a particular form while still sharing schema information that\n            applies to all views.\n            "
                            }
                        ]
                    },
                    {
                        id:"dataDragging",
                        isOpen:false,
                        title:"Dragging",
                        description:"\n        Databound components have built-in dragging behaviors that operate on persistent\n        datasets.\n    ",
                        children:[
                            {
                                dataSource:"employees",
                                jsURL:"databind/drag/treeReparent.js",
                                ref:"treeReparent",
                                title:"Tree Reparent",
                                description:"\n            Dragging employees between managers in this tree automatically saves the new\n            relationship to a DataSource, without writing any code.  Make changes, then \n            reload the page: your changes persist.\n            "
                            },
                            {
                                dataSource:"supplyCategory",
                                jsURL:"databind/drag/treeRecategorize.js",
                                ref:"treeRecategorize",
                                title:"Recategorize (Tree)",
                                tabs:[
                                    {
                                        title:"supplyItem",
                                        url:"supplyItem.ds.xml"
                                    }
                                ],
                                description:"\n            Dragging items from the list and dropping them on categories in the tree automatically\n            re-categorizes the item, without any code needed.  Make changes, then \n            reload the page: your changes persist.  This behavior is (optionally) automatic where\n            SmartClient can establish a relationship via foreign key between the DataSources\n            two components are bound to.\n            "
                            },
                            {
                                dataSource:"supplyItem",
                                jsURL:"databind/drag/listRecategorize.js",
                                ref:"listRecategorize",
                                title:"Recategorize (List)",
                                description:"\n            The two lists are showing items in different categories.  Drag items from one list to\n            another to automatically recategorize the items without writing any code.  Make\n            changes, then reload the page; your changes persist.\n            "
                            },
                            {
                                ref:"recategorizeTiles",
                                title:"Recategorize (Tile)"
                            },
                            {
                                dataSource:"employees",
                                jsURL:"databind/drag/listCopy.js",
                                ref:"databoundDragCopy",
                                showSkinSwitcher:true,
                                title:"Copy",
                                tabs:[
                                    {
                                        title:"teamMembers",
                                        url:"teamMembers.ds.xml"
                                    }
                                ],
                                description:"\n            Drag employee records into the Project Team Members list.  SmartClient recognizes that the \n            two dataSources are linked by a foreign key relationship, and automatically uses that \n            relationship to populate values in the record that is added when you drop. SmartClient\n            also populates fields based on current criteria and maps explicit titleFields as \n            necessary.<p>\n            In this example, note that SmartClient is automatically populating all three\n            of the fields in the teamMembers dataSource, even though none of those fields is present \n            in the employees dataSource we are dragging from.  Change the \"Team for Project\" select \n            box, then try dragging employees across; note that the Project Code column is being \n            correctly populated for the dropped records.\n            "
                            }
                        ]
                    },
                    {
                        ref:"adaptiveFilter",
                        title:"Adaptive Filter"
                    },
                    {
                        ref:"adaptiveSort",
                        title:"Adaptive Sort"
                    },
                    {
                        ref:"relatedRecords",
                        title:"Related Records",
                        description:"\n        Open the picker in either form to select the item you want to order from the\n        \"supplyItem\" DataSource.  The picker on the left stores the \"itemId\" from the\n        related \"supplyItem\" records.  The picker on the right stores the \"SKU\" while\n        displaying multiple fields.  You can scroll to dynamically load more records.  \n        This pattern works with any DataSource.  \n    "
                    }
                ]
            },
            {
                icon:"[ISO_DOCS_SKIN]/images/silkicons/database_table.png",
                id:"dataIntegration",
                isOpen:false,
                title:"Data Integration",
                description:"\n    SmartClient supports declarative, XPath-based binding of visual components to web services\n    that return XML or JSON responses.  SmartClient understands XML Schema and can bind\n    components directly to WSDL web services.  \n",
                children:[
                    {
                        id:"xmlDataIntegration",
                        isOpen:false,
                        title:"XML",
                        description:"\n        SmartClient can declaratively bind to standard formats like WSDL or RSS, homebrew\n        formats, or simple flat files.  \n    ",
                        children:[
                            {
                                id:"rssFeed",
                                jsURL:"dataIntegration/xml/rssFeed.js",
                                needXML:"true",
                                showSkinSwitcher:true,
                                title:"RSS Feed",
                                description:"\n            DataSources can bind directly to simple XML documents where field values appear as\n            attributes or subelements.\n            "
                            },
                            {
                                id:"xpathBinding",
                                jsURL:"dataIntegration/xml/xpathBinding.js",
                                needXML:"true",
                                showSkinSwitcher:true,
                                title:"XPath Binding",
                                tabs:[
                                    {
                                        canEdit:"false",
                                        doEval:"false",
                                        title:"contactsData.xml",
                                        url:"dataIntegration/xml/contactsData.xml"
                                    }
                                ],
                                description:"\n            DataSources can extract field values from complex XML documents via XPath\n            expressions.  Note how the address fields, which are represented in the contacts\n            data as a subelement, appear as columns in the grid. This approach of loading\n            simple XML data over HTTP can be used with PHP and other server technologies.\n            "
                            },
                            {
                                id:"wsdlOperation",
                                jsURL:"dataIntegration/xml/wsdlWebServiceOperations.js",
                                needXML:"true",
                                showSkinSwitcher:false,
                                title:"WSDL Web Services",
                                description:"\n            SmartClient can load WSDL service definitions and call web service operations\n            with automatic JSON<->XML translation.\n            \n            SOAP encoding rules, namespacing, and element ordering are handled automatically\n            for your inputs and outputs. \n            "
                            },
                            {
                                id:"wsdlBinding",
                                jsURL:"dataIntegration/xml/weatherForecastSearch.js",
                                needXML:"true",
                                showSkinSwitcher:true,
                                title:"Weather SOAP Search",
                                description:"\n            Enter a zip code  in the \"Zip\" field to retrieve a weather forecast. \n            \n            DataSources can bind directly to the structure of WSDL messages.\n            "
                            },
                            {
                                id:"xmlEditSave",
                                jsURL:"dataIntegration/xml/operationBinding_dataURL.js",
                                title:"Edit and Save",
                                tabs:[
                                    {
                                        canEdit:"false",
                                        doEval:"false",
                                        title:"country_fetch.xml",
                                        url:"dataIntegration/xml/responses/country_fetch.xml"
                                    },
                                    {
                                        canEdit:"false",
                                        doEval:"false",
                                        title:"country_add.xml",
                                        url:"dataIntegration/xml/responses/country_add.xml"
                                    },
                                    {
                                        canEdit:"false",
                                        doEval:"false",
                                        title:"country_update.xml",
                                        url:"dataIntegration/xml/responses/country_update.xml"
                                    },
                                    {
                                        canEdit:"false",
                                        doEval:"false",
                                        title:"country_remove.xml",
                                        url:"dataIntegration/xml/responses/country_remove.xml"
                                    }
                                ],
                                description:"\n        Demonstrates <b>Add</b>, <b>Update</b> and <b>Remove</b> operations with a server that\n        returns simple XML responses, an integration strategy popular with PHP, Ruby and Perl\n        backends.\n        <br>\n        Each operation is directed to a different XML file containing a sample response for\n        that operationType.  The server returns the data-as-saved to allow the grid to update\n        its cache.\n        "
                            },
                            {
                                id:"restEditSave",
                                jsURL:"dataIntegration/xml/restDS_operationBinding.js",
                                title:"RestDataSource - Edit and Save",
                                tabs:[
                                    {
                                        canEdit:"false",
                                        doEval:"false",
                                        title:"country_fetch.xml",
                                        url:"dataIntegration/xml/responses/country_fetch_rest.xml"
                                    },
                                    {
                                        canEdit:"false",
                                        doEval:"false",
                                        title:"country_add.xml",
                                        url:"dataIntegration/xml/responses/country_add_rest.xml"
                                    },
                                    {
                                        canEdit:"false",
                                        doEval:"false",
                                        title:"country_update.xml",
                                        url:"dataIntegration/xml/responses/country_update_rest.xml"
                                    },
                                    {
                                        canEdit:"false",
                                        doEval:"false",
                                        title:"country_remove.xml",
                                        url:"dataIntegration/xml/responses/country_remove_rest.xml"
                                    }
                                ],
                                description:"\n        The RestDataSource provides a simple protocol based on XML or JSON over HTTP.  This\n        protocol can be implemented with any server technology (PHP, Ruby, ..) and \n        includes all the features of SmartClient's databinding layer (data paging, server\n        validation errors, cache sync, etc).<br>\n        In this example, each DataSource operation is directed to a different XML file\n        containing a sample response for that operationType.  The server returns the\n        data-as-saved to allow the grid to update its cache.\n        "
                            },
                            {
                                id:"xmlServerValidationErrors",
                                jsURL:"dataIntegration/xml/serverValidationErrors/serverValidationErrors.js",
                                needXML:"true",
                                showSkinSwitcher:false,
                                title:"Server Validation Errors",
                                tabs:[
                                    {
                                        canEdit:"false",
                                        doEval:"false",
                                        title:"serverResponse.xml",
                                        url:"dataIntegration/xml/serverValidationErrors/serverResponse.xml"
                                    }
                                ],
                                description:"\n            Click \"Save\" to see validation errors derived from an XML response.\n            \n            Validation errors expressed in application-specific XML formats can be \n            communicated to visual components by implementing\n            <code>DataSource.transformResponse()</code>.  The resulting validation\n            errors will be displayed and tracked by forms and editabled grids.\n            "
                            },
                            {
                                id:"xmlSchemaImport",
                                needXML:"true",
                                showSkinSwitcher:true,
                                title:"XML Schema Import",
                                url:"dataIntegration/xml/xmlSchemaImport.js",
                                tabs:[
                                    {
                                        canEdit:"false",
                                        doEval:"false",
                                        title:"supplyItem.xsd",
                                        url:"dataIntegration/xml/supplyItem.xsd"
                                    }
                                ],
                                description:"\n            Click \"Load Schema\" to load a version of the <code>supplyItem</code>\n            DataSource expressed in XML Schema format, and bind the Grid and Form to it.  Note\n            that the form and grid choose appropriate editors according to declared XML Schema\n            types.  Click \"Validate\" to see validation errors from automatically imported\n            validators.\n            "
                            },
                            {
                                id:"schemaChaining",
                                needXML:"true",
                                showSkinSwitcher:true,
                                title:"Schema Chaining",
                                url:"dataIntegration/xml/schemaChaining.js",
                                tabs:[
                                    {
                                        canEdit:"false",
                                        doEval:"false",
                                        title:"supplyItem.xsd",
                                        url:"dataIntegration/xml/supplyItem.xsd"
                                    }
                                ],
                                description:"\n            Click \"Load Schema\" to load a <code>supplyItem</code> DataSource from\n            XML Schema format, then extend that schema with SmartClient-specific presentation\n            attributes, and bind the Grid and Form to it.  Note that the internal \"itemId\"\n            field has been hidden from the user, some fields have been retitled, and default\n            editors overridden.\n            "
                            },
                            {
                                needXML:"true",
                                ref:"WSDLDataSource",
                                showSkinSwitcher:true,
                                title:"SmartClient WSDL"
                            }
                        ]
                    },
                    {
                        isOpen:false,
                        title:"JSON",
                        description:"\n        SmartClient brings declarative XPath binding and typed schema (even XML Schema) to the\n        simple and convenient JSON format.\n    ",
                        children:[
                            {
                                id:"simpleJSON",
                                jsURL:"dataIntegration/json/simpleJSON.js",
                                showSkinSwitcher:true,
                                title:"Simple JSON",
                                tabs:[
                                    {
                                        canEdit:"false",
                                        doEval:"false",
                                        title:"countries_small.js",
                                        url:"dataIntegration/json/countries_small.js"
                                    }
                                ],
                                description:"\n            DataSources can bind directly to JSON data where records appear as an Array of\n            JavaScript Objects with field values as properties.  This approach of loading\n            simple JSON data over HTTP can be used with PHP and other server technologies.\n            "
                            },
                            {
                                id:"jsonXPath",
                                jsURL:"dataIntegration/json/xpathBinding.js",
                                showSkinSwitcher:true,
                                title:"JSON XPath Binding",
                                tabs:[
                                    {
                                        canEdit:"false",
                                        doEval:"false",
                                        title:"contactsData.js",
                                        url:"dataIntegration/json/contactsData.js"
                                    }
                                ],
                                description:"\n            DataSources can extract field values from complex JSON structures via XPath\n            expressions.  Note how the address fields, which are represented in the contacts\n            data as a subobject, appear as columns in the grid.\n            "
                            },
                            {
                                id:"jsonServerValidationErrors",
                                jsURL:"dataIntegration/json/serverValidationErrors/serverValidationErrors.js",
                                showSkinSwitcher:false,
                                title:"Server Validation Errors",
                                tabs:[
                                    {
                                        canEdit:"false",
                                        doEval:"false",
                                        title:"serverResponse.js",
                                        url:"dataIntegration/json/serverValidationErrors/serverResponse.js"
                                    }
                                ],
                                description:"\n            Click \"Save\" to see validation errors derived from a JSON response.<BR>\n            \n            Validation errors expressed in application-specific JSON formats can be \n            communicated to the SmartClient system by implementing\n            <code>DataSource.transformResponse()</code>.  The resulting validation\n            errors will be displayed and tracked by forms and editabled grids.\n            "
                            }
                        ]
                    }
                ]
            },
            {
                icon:"[ISO_DOCS_SKIN]/images/silkicons/server_lightning.png",
                id:"serverExamples",
                isOpen:false,
                title:"Server examples",
                description:"\n    The SmartClient Server framework is a collection of .jar files and optional servlets that works with\n    any J2EE or J2SE container and is easily integrated into existing applications.  Its major\n    features include:<ul>\n    <li><b>Simplified server integration:</b> A pre-built network protocol for browser-server\n        communication, which handles data paging, transactions/batch operations, server-side\n        sort, automatic cache updates, validation and other error handling, optimistic\n        concurrency (aka long transactions) and binary file uploads.<P></li>\n    <li><b>SQL, JPA & Hibernate Connectors:</b> Secure, flexible, transactional support for all\n        CRUD operations, either directly via JDBC or via Hibernate or JPA beans.<P></li>\n    <li><b>Rapid integration with Java Beans:</b> Robust, complete, bi-directional translation\n        between Java and Javascript objects for rapid integration with any Java beans-based\n        persistence system, such as Spring services or custom ORM implementations.  Send and\n        receive complex structures including Java Enums and Java Generics without the need to\n        write mapping or validation code.  Declaratively trim and rearrange data so that only\n        selected data is sent to the client <b>without</b> the need to create and populate\n        redundant DTOs (data transfer objects).<P></li>\n    <li><b>Server enforcement of Validators:</b> A single file specificies validation rules\n        which are enforced both client and server side<P></li>\n    <li><b>Declarative Security:</b> Easily attach role- or capability-based security rules to\n        data operations, with server-side enforcement plus automatic client-side effects such as\n        hiding fields or showing fields as read-only based on the user role.<P></li>\n    <li><b>Export:</b> Export any dataset to CSV or true Excel spreadsheets, including data\n        highlights and formatting rules<br></li>\n    <li><b>High speed data delivery / data compression:</b> automatically uses the fastest \n        possible mechanism for delivering data to the browser<br></li>\n    </ul>\n    The SmartClient Server framework is an optional, commercially-licensed package.  See the \n    <a href=http://www.smartclient.com/product/index.jsp>products page</a> for details.\n    \n",
                children:[
                    {
                        icon:"[ISO_DOCS_SKIN]/images/silkicons/server_lightning.png",
                        id:"serverValidation",
                        isOpen:false,
                        title:"Validation",
                        description:"\n        The SmartClient Server provides powerful support for server-based validation.\n    ",
                        children:[
                            {
                                dataSource:"supplyItem",
                                id:"singleSourceValidation",
                                jsURL:"dataIntegration/java/serverValidation.js",
                                requiresModules:"SCServer",
                                title:"Single Source",
                                description:"\n        Validation rules are automatically enforced on both the client- and server-side based on\n        a single, shared declaration.  Press \"Save\" to see errors from client-side\n        validation.  Press \"Clear Errors\" then \"Disable Validation\" then \"Save\" again to see the\n        same errors caught by the SmartClient server.\n        "
                            },
                            {
                                dataSource:"validationDMI_orderForm",
                                id:"dmiValidation",
                                jsURL:"dataIntegration/java/validationDMI.js",
                                requiresModules:"SCServer",
                                title:"DMI Validation",
                                tabs:[
                                    {
                                        canEdit:"false",
                                        dataSource:"StockItem",
                                        title:"StockItem"
                                    },
                                    {
                                        canEdit:"false",
                                        doEval:"false",
                                        title:"ValidatorDMI.java",
                                        url:"serverExamples/validation/ValidatorDMI.java"
                                    }
                                ],
                                description:"\n        Use the \"Item Id\" ComboBox to select an item,  enter a very large quantity (999999)\n        and press the \"Submit Order\" button.\n        <P/>\n        The resulting validation error is based upon server-side logic in ValidatorDMI.java\n        that checks a related DataSource (StockItem) to see if there is sufficient quantity in\n        stock to fulfill the order.\n        <P/>\n        Validators can use SmartClient DMI to call any server-side method to check the validity\n        of data, including methods on Java beans looked up via Spring.\n        "
                            },
                            {
                                dataSource:"velocity_orderForm",
                                id:"velocityValidation",
                                jsURL:"dataIntegration/java/velocityValidation.js",
                                requiresModules:"SCServer",
                                title:"Velocity Expression",
                                tabs:[
                                    {
                                        canEdit:"false",
                                        dataSource:"StockItem",
                                        title:"StockItem"
                                    }
                                ],
                                description:"\n        Use the \"Item Id\" ComboBox to select an item,  enter a very large quantity (999999)\n        and press the \"Submit Order\" button.\n        <P/>\n        The resulting validation error is based upon server-side condition specified in\n        the validator using a Velocity expression. It checks a related DataSource (StockItem)\n        to see if there is sufficient quantity in stock to fulfill the order.\n        "
                            },
                            {
                                dataSource:"queuing_userHB",
                                id:"uniqueCheckValidation",
                                jsURL:"dataIntegration/java/uniqueCheckValidation.js",
                                requiresModules:"SCServer",
                                title:"Unique Check",
                                description:"\n        Enter the email address \"kamirov@server.com\" in the email field and press Tab. Do so with\n        any other email address as well.\n        <P/>\n        The resulting validation error is based upon the server-side isUnique validator that\n        checks to see if there is already a record in the DataSource and if so fails validation. \n        "
                            },
                            {
                                dataSource:"complaint",
                                id:"hasRelatedValidation",
                                jsURL:"dataIntegration/java/hasRelatedValidation.js",
                                requiresModules:"SCServer",
                                title:"Related Records",
                                tabs:[
                                    {
                                        canEdit:"false",
                                        dataSource:"masterDetail_orderHB",
                                        title:"masterDetail_orderHB"
                                    }
                                ],
                                description:"\n        Enter a complaint for a received shipment using its tracking number. The tracking\n        number must reference an existing tracking number so try with an existing number\n        (4110884 or 9631143) and with a random number (like 1234).\n        <P/>\n        The relatedRecord validator can be used to validate that an ID entered by\n        a user actually exists.  This is useful in situations where using a comboBox for record\n        lookup is inappropriate (the user should not be able to select among all valid tracking\n        numbers, or among other types of IDs, such as license keys or driver's license numbers)\n        or in situations such as batch upload of many records.\n        <P/>\n        The relatedRecord validator can also be used with a ComboBox as the UI in order to\n        enforce that related records are checked <b>before</b> a request reaches business logic\n        where it would be convenient to assume the ID is already validated, or as a means of\n        enforcing referential integrity in systems that don't have built-in enforcement.\n        "
                            },
                            {
                                dataSource:"complaint",
                                id:"blockingErrors",
                                jsURL:"dataIntegration/java/blockingErrors.js",
                                requiresModules:"SCServer",
                                title:"Blocking Errors",
                                tabs:[
                                    {
                                        canEdit:"false",
                                        dataSource:"masterDetail_orderHB",
                                        title:"masterDetail_orderHB"
                                    }
                                ],
                                description:"\n            Enter a complaint for a received shipment using its tracking number. The tracking\n            number must reference an existing tracking number so try with an existing number\n            (4110884 or 9631143) and with a random number (like 1234). Note that when a\n            non-existing value is entered, focus is not allowed to move forward.\n            <P/>\n            The relatedRecord validator can be used to validate that an ID entered by\n            a user actually exists.  This is useful in situations where using a comboBox for record\n            lookup is inappropriate (the user should not be able to select among all valid tracking\n            numbers, or among other types of IDs, such as license keys or driver's license numbers)\n            or in situations such as batch upload of many records.\n            <P/>\n            The relatedRecord validator can also be used with a ComboBox as the UI in order to\n            enforce that related records are checked <b>before</b> a request reaches business logic\n            where it would be convenient to assume the ID is already validated, or as a means of\n            enforcing referential integrity in systems that don't have built-in enforcement.\n            "
                            }
                        ]
                    },
                    {
                        icon:"[ISO_DOCS_SKIN]/images/silkicons/database_lightning.png",
                        isOpen:false,
                        title:"SQL",
                        description:"\n        The SmartClient Server provides powerful built-in support for codeless connection to\n        mainstream SQL databases.\n    ",
                        children:[
                            {
                                icon:"[ISO_DOCS_SKIN]/images/silkicons/database_gear.png",
                                id:"sqlWizard",
                                jsURL:"serverExamples/sql/vb_Database.js",
                                requiresModules:"SCServer",
                                showSkinSwitcher:false,
                                showSource:false,
                                title:"Database Wizard",
                                description:"\n            SmartClient's Visual Builder tool provides an extremely easy and completely codeless \n            way to create DataSources for instantly connecting to existing database tables.  Just\n            click the \"New\" button, select \"Existing SQL Table\", and the Database Browser will\n            show you your tables, column details and the actual data.  Select a table, and \n            Visual Builder will create a fully-functioning DataSource that can perform\n            all four CRUD operations on that table, including - if you have the Power Edition or \n            Enterprise Edition - complex searches enabled by SmartClient Advanced Criteria system.\n            "
                            },
                            {
                                icon:"[ISO_DOCS_SKIN]/images/silkicons/database_gear.png",
                                id:"sqlConnector",
                                jsURL:"serverExamples/sql/basicConnector.js",
                                requiresModules:"SCServer",
                                title:"Basic Connector",
                                tabs:[
                                    {
                                        canEdit:"false",
                                        title:"worldDS",
                                        url:"grids/ds/worldSQLDS.ds.xml"
                                    }
                                ],
                                descriptionHeight:"220",
                                description:"\n            The basic SQL Connector gives you the ability to immediately connect SmartClient components to\n            SQL databases without writing any code.  \n            <P>\n            You can either use the SQL Wizard in Visual Builder to generate a DataSource descriptor\n            (.ds.xml file) from an existing SQL table, or use the Admin Console to generate a SQL table\n            from a DataSource descriptor you write.  Either way, you get the immediate ability to perform\n            all 4 basic SQL operations (select, insert, update, delete) from any of SmartClient's\n            data-aware components.\n            <P>\n            The grid below is connected to a SQL DataSource and has settings enabled to allow this grid to\n            perform all 4 operations.  Type in the input boxes above each column to do query by example.\n            Note that data paging is automatically enabled - just scroll to load data on demand.  Click on\n            a red X to delete a record.  Click on a record to edit it and click \"Add New\" to add a new record.\n            <P>\n            It's easy to add business logic that takes place before and after SQL operations to enforce\n            security or add additional data validation rules.\n            <P>\n            Even if your primary data storage approach is non-SQL or if you choose to use JPA or other ORM\n            systems for most objects, the SQL connector is still valuable for initial prototypes and for\n            lightweight storage when a full ORM approach would be overkill.\n            "
                            },
                            {
                                icon:"[ISO_DOCS_SKIN]/images/iconexperience/funnel.png",
                                ref:"filterBuilderBracket",
                                requiresModules:"SCServer,serverCriteria",
                                title:"Server Advanced Filtering (SQL)",
                                descriptionHeight:"150",
                                description:"\n            Use the FilterBuilder to construct queries of arbitrary complexity.  The FilterBuilder,\n            and the underlying AdvancedCriteria system, support building queries with subclauses\n            nested to any depth. Add clauses to your query with the \"+\" icon; add nested subclauses \n            with the \"+()\" button. Click \"Filter\" to see the result in the ListGrid.\n            <P>\n            Note that this example is backed by a \"sql\" dataSource; the SmartClient Server is \n            automatically generating the SQL queries required to implement the filters that the \n            FilterBuilder can assemble.  This works adaptively and seamlessly with client-side \n            Advanced Filtering: the generated SQL query will yield exactly the same resultset \n            as the client-side filtering.  This means SmartClient is able to switch to client-side\n            filtering when its cache is full, giving a more responsive, more scalable application.\n            "
                            },
                            {
                                icon:"[ISO_DOCS_SKIN]/images/silkicons/table_relationship.png",
                                id:"largeValueMapSQL",
                                jsURL:"serverExamples/sql/largeValueMap/largeValueMap.js",
                                requiresModules:"SCServer,customSQL",
                                title:"Large Value Map",
                                tabs:[
                                    {
                                        canEdit:"false",
                                        dataSource:"largeValueMap_orderItem",
                                        name:"orderItem"
                                    },
                                    {
                                        canEdit:"false",
                                        dataSource:"supplyItem"
                                    }
                                ],
                                descriptionHeight:"180",
                                description:"\n            This example shows the simple use of custom SQL clauses to provide a DataSource that\n            joins multiple tables while retaining SmartClient's automatic paging and filtering\n            behavior.  When trying this example, remember that this is <b>automatic</b> \n            dataset-handling behavior that works without any coding, even though the data is being \n            provided by a custom SQL query.<p>\n            \n            The list contains order items; each order item holds an itemID, which is being used\n            to join to the supplyItem table and obtain the itemName.  Note that you can filter on\n            the itemName - either select a full item name or just enter a partial value in the \n            combo box.  Pagination is also active - try quickly dragging the scrollbar down, and\n            you'll see SmartClient contacting the server for more records.<p>\n           \n             Editing is also enabled in this example.  Try filtering to a small sample of items,\n             then edit one of them by double-clicking it and choose a different item.  Note how \n             that order item is immediately filtered out of the list: SmartClient's intelligent \n             cache sync also automatically handles custom SQL statements.\n            "
                            },
                            {
                                icon:"[ISO_DOCS_SKIN]/images/silkicons/user_orange.png",
                                id:"userSpecificData",
                                jsURL:"serverExamples/sql/userSpecificData/userSpecificData.js",
                                requiresModules:"SCServer,customSQL",
                                title:"User-Specific Data",
                                tabs:[
                                    {
                                        canEdit:"false",
                                        dataSource:"cartItem",
                                        name:"cartItem"
                                    },
                                    {
                                        canEdit:"false",
                                        title:"supplyItem",
                                        url:"supplyItem.ds.xml"
                                    },
                                    {
                                        canEdit:"false",
                                        doEval:"false",
                                        title:"CartDMI.java",
                                        url:"serverExamples/sql/userSpecificData/CartDMI.java"
                                    }
                                ],
                                descriptionHeight:"200",
                                description:"\n            This example shows the use of some simple user-written server code in conjunction with\n            SmartClient databound dragging features and the SmartClient SQL DataSource to implement\n            a simple, but secure, shopping cart example.\n            <p>\n            Via DMI (Direct Method Invocation), the <code>cartItem</code> DataSource declares\n            that all DataSource operations should go through a custom Java method\n            <code>CartDMI.enforceUserAccess()</code> <b>before</b> proceeding to read or write\n            the database.  &nbsp;&nbsp<code>CartDMI.enforceUserAccess()</code> adds the current sessionId to the\n            DSRequest, so that the user can only read and write his own shopping cart.\n            <P>\n            Drag items from the left-hand grid to the right-hand grid.  You can edit the quantity\n            in the right-hand grid, and you can delete records.  You can verify that the example\n            is protecting each user's data from others by running the example in two different\n            browsers (eg one Firefox and one IE) - this creates distinct sessions with separate\n            carts.\n            "
                            },
                            {
                                icon:"[ISO_DOCS_SKIN]/images/silkicons/table_multiple.png",
                                id:"dynamicReporting",
                                jsURL:"serverExamples/sql/dynamicReporting/dynamicReporting.js",
                                requiresModules:"SCServer,customSQL",
                                title:"Dynamic Reporting",
                                tabs:[
                                    {
                                        canEdit:"false",
                                        dataSource:"dynamicReporting_orderItem",
                                        name:"orderItem"
                                    }
                                ],
                                descriptionHeight:"210",
                                description:"\n            This example shows the use of custom SQL clauses to build a fairly complex query, including\n            both standard and bespoke WHERE conditions and the use of aggregate functions and a \n            GROUP BY.  It is important to note that we can do this whilst still keeping the normal \n            benefits of SmartClient DataSources, such as automatic dataset paging and arbitrary\n            filtering and sorting.  Also note that this example, though it makes heavy use of custom\n            SQL clauses, doesn't make use of any database-specific syntax or functions, so it is \n            portable across different database products.<p>\n            \n            The list contains a summary of orders in a given date range, summarized by item - each\n            item appears just once in the list, alongside the total quantity of that item ordered \n            in the given date range.  Change the date range to be more restrictive (all the rows\n            in the sample database have dates in February 2009) and click \"Filter\", and you will see\n            the quantities change, and items disappear from the list.  You can also use the \n            filter editor at the top of the grid to arbitrarily filter the records, or click\n            the column headings to sort.<p>\n            \n            Scroll the grid quickly to the bottom, and you will see a brief notification as \n            SmartClient contacts the server - pagination is still working, despite the unusual\n            and complex query.\n            "
                            },
                            {
                                ref:"autoTransactions",
                                title:"Transactions"
                            }
                        ]
                    },
                    {
                        icon:"[ISO_DOCS_SKIN]/images/silkicons/server_lightning.png",
                        isOpen:false,
                        title:"Hibernate / Beans",
                        description:"\n        The SmartClient Server's built-in support for Hibernate\n    ",
                        children:[
                            {
                                dataSource:"supplyItemHBAutoDerive",
                                icon:"[ISO_DOCS_SKIN]/images/iconexperience/coffeebean.png",
                                id:"hibernateAutoDerivation",
                                jsURL:"serverExamples/hibernate/autoDerivation/hibernateAutoDerivation.js",
                                requiresModules:"SCServer",
                                showSkinSwitcher:false,
                                title:"Auto Derivation",
                                tabs:[
                                    {
                                        canEdit:"false",
                                        doEval:"false",
                                        title:"SupplyItemHB.java",
                                        url:"serverExamples/hibernate/autoDerivation/SupplyItemHB.java"
                                    }
                                ],
                                description:"\n            If you have pre-existing Hibernate beans, SmartClient can automatically derive fully functional\n            DataSources given just the Java classname of the Hibernate Bean.  The grid below is connected\n            to a Hibernate-managed bean via the simple declarations in supplyItemHBAutoDerive.ds.xml - no other\n            configuration or Java code is required beyond the bean itself and Hibernate mapping, which are\n            samples intended to represent a pre-existing Hibernate bean.\n            <p/>\n            To search, use the controls above the grid's header. Note that data paging is automatically\n            enabled - just scroll down to load data on demand. Click on the red icon next to each record to\n            delete it. Click on a record to edit it and click \"Add New\" to add a new record.  Note that the\n            editing controls are type sensitive: a date picker appears for the \"Next Shipment\" field, and\n            the \"Units\" field shows a picklist because its Java type is an Enum.\n            <p/>\n            You can use DMI to add business logic that takes place before and after Hibernate operations to\n            enforce security or add additional data validation rules.\n            "
                            },
                            {
                                icon:"[ISO_DOCS_SKIN]/images/iconexperience/coffeebean.png",
                                id:"hibernateConnector",
                                jsURL:"serverExamples/hibernate/hibernateConnector.js",
                                requiresModules:"SCServer",
                                title:"Beanless Mode",
                                tabs:[
                                    {
                                        canEdit:"false",
                                        dataSource:"worldHB",
                                        title:"worldHB"
                                    }
                                ],
                                descriptionHeight:"220",
                                description:"\n            Beanless mode allows you to use Hibernate for persistence without writing any Java code at all.\n            Declare the properties of your object in your DataSource descriptor (.ds.xml file), and\n            SmartClient will generate the Hibernate configuration automatically.  You can use the Admin\n            Console to generate the underlying SQL table as well, so the only file you create is the\n            .ds.xml file.\n            <P>\n            As with the previous example, the grid below provides the ability to search, edit, and delete\n            records.\n            <P>\n            Beanless mode helps you avoid writing boilerplate Java code (several classes full of getter\n            and setter methods that do nothing) for simple entities.  Even in beanless mode, you can still\n            use DMI to add Java business logic that takes place before and after Hibernate operations; the\n            Hibernate data is represented as a Java Map. \n            <P>\n            You can also use a mixture of beanless mode and normal Hibernate beans, even in the same\n            transaction.\n            "
                            },
                            {
                                icon:"[ISO_DOCS_SKIN]/images/iconexperience/funnel.png",
                                id:"advancedFilterHibernate",
                                jsURL:"serverExamples/hibernate/advancedFilter/advancedFilterHibernate.js",
                                requiresModules:"SCServer,serverCriteria",
                                title:"Advanced Filtering",
                                tabs:[
                                    {
                                        canEdit:"false",
                                        dataSource:"supplyItemHB",
                                        name:"supplyItemHB"
                                    }
                                ],
                                descriptionHeight:"150",
                                description:"\n            Use the FilterBuilder to construct queries of arbitrary complexity.  The FilterBuilder,\n            and the underlying AdvancedCriteria system, support building queries with subclauses\n            nested to any depth. Add clauses to your query with the \"+\" icon; add nested subclauses \n            with the \"+()\" button. Click \"Filter\" to see the result in the ListGrid.\n            <p>\n            Note that this example is backed by a \"hibernate\" dataSource; the SmartClient Server is \n            automatically generating the Hibernate Criteria Queries (including database-specific SQL\n            where necessary) to implement the filters that the FilterBuilder \n            can assemble.    This works adaptively and seamlessly with client-side \n            Advanced Filtering: the generated Criteria query will yield exactly the same resultset \n            as the client-side filtering.  This means SmartClient is able to switch to client-side\n            filtering when its cache is full, giving a more responsive, more scalable application.\n            "
                            },
                            {
                                id:"hbRelationManyToOneSimple",
                                jsURL:"serverExamples/hibernate/relations/hbRelationManyToOneSimple.js",
                                requiresModules:"SCServer",
                                title:"Many-to-One Relation",
                                tabs:[
                                    {
                                        dataSource:"cityManyToOneSimpleHB",
                                        name:"cityManyToOneSimpleHB"
                                    },
                                    {
                                        dataSource:"countryManyToOneSimpleHB",
                                        name:"countryManyToOneSimpleHB"
                                    },
                                    {
                                        canEdit:"false",
                                        doEval:"false",
                                        title:"CityManyToOneSimple.java",
                                        url:"serverExamples/hibernate/relations/CityManyToOneSimple.java"
                                    },
                                    {
                                        canEdit:"false",
                                        doEval:"false",
                                        title:"CountryManyToOneSimple.java",
                                        url:"serverExamples/hibernate/relations/CountryManyToOneSimple.java"
                                    }
                                ],
                                descriptionHeight:"200",
                                description:"\n                SmartClient handles Hibernate Many-to-One relations transparently, such as Cities which belong\n                to Countries.  Just declare a foreignKey field on the City DataSource to indicate you\n                want to use the related Hibernate bean Country.\n                <P>\n                The grid below shows Cities, but the Country name is automatically shown even though the\n                countryName is stored in the related Hibernate bean Country.  Any fields from any number of\n                related beans can be automatically loaded this way.\n                <P>\n                Click to edit and change the Country of a City.  The list of Countries is automatically\n                loaded from the related Hibernate bean, along with their IDs (not shown).  \n                <P>\n                Changing the Country of a City sends the ID of the new Country back to the server, and\n                SmartClient automatically makes all the required Hibernate calls to persist the change - no\n                server-side code needs to be written beyond the Hibernate beans themselves and their\n                annotations.\n            "
                            },
                            {
                                id:"hbRelationOneToMany",
                                jsURL:"serverExamples/hibernate/relations/hbRelationOneToMany.js",
                                requiresModules:"SCServer",
                                title:"One-to-Many Relation",
                                tabs:[
                                    {
                                        dataSource:"cityOneToManyHB",
                                        name:"cityOneToManyHB"
                                    },
                                    {
                                        dataSource:"countryOneToManyHB",
                                        name:"countryOneToManyHB"
                                    },
                                    {
                                        canEdit:"false",
                                        doEval:"false",
                                        title:"CityOneToMany.java",
                                        url:"serverExamples/hibernate/relations/CityOneToMany.java"
                                    },
                                    {
                                        canEdit:"false",
                                        doEval:"false",
                                        title:"CountryOneToMany.java",
                                        url:"serverExamples/hibernate/relations/CountryOneToMany.java"
                                    }
                                ],
                                descriptionHeight:"150",
                                description:"\n                SmartClient handles Hibernate One-to-Many relations transparently, such as a Country which has\n                multiple Cities.  Just declare a collection field (multiple:true) on the Country\n                DataSource to indicate you want to load its list of Cities.\n                <P>\n                Click on a Country below - its list of Cities is revealed without a new trip to the\n                server.  Cities can be now edited in the lower grid.  \n                <P>\n                When data is saved, all changes to the Country and its Cities are sent in one save\n                request, and SmartClient automatically makes all the required Hibernate calls to persist the\n                changes - no server-side code needs to be written beyond the Hibernate beans themselves and\n                their annotations.\n            "
                            },
                            {
                                icon:"[ISO_DOCS_SKIN]/images/silkicons/table_multiple.png",
                                id:"masterDetail",
                                jsURL:"serverExamples/hibernate/masterDetail/masterDetail.js",
                                requiresModules:"SCServer",
                                title:"Master-Detail (Batch Load and Save)",
                                tabs:[
                                    {
                                        canEdit:"false",
                                        dataSource:"masterDetail_orderHB",
                                        name:"masterDetail_order"
                                    },
                                    {
                                        canEdit:"false",
                                        dataSource:"masterDetail_orderItemHB",
                                        name:"masterDetail_orderItem"
                                    },
                                    {
                                        canEdit:"false",
                                        doEval:"false",
                                        title:"Order.java",
                                        url:"serverExamples/hibernate/masterDetail/Order.java"
                                    },
                                    {
                                        canEdit:"false",
                                        doEval:"false",
                                        title:"Order.hbm.xml",
                                        url:"serverExamples/hibernate/masterDetail/Order.hbm.xml"
                                    },
                                    {
                                        canEdit:"false",
                                        doEval:"false",
                                        title:"OrderItem.java",
                                        url:"serverExamples/hibernate/masterDetail/OrderItem.java"
                                    },
                                    {
                                        canEdit:"false",
                                        doEval:"false",
                                        title:"OrderItem.hbm.xml",
                                        url:"serverExamples/hibernate/masterDetail/OrderItem.hbm.xml"
                                    }
                                ],
                                descriptionHeight:"150",
                                description:"\n            This example shows a simple way to implement an updatable parent-child relationship\n            with SmartClient, the SmartClient Server and Hibernate.  As you can see from from the \n            various source tabs, <code>Order</code> and <code>OrderItem</code> are related via \n            a unidirectional Set collection in Hibernate.\n            The order dataSource also declares its <code>items</code> field as being\n            of type <code>masterDetail_orderItemHB</code>, which tells SmartClient to use that \n            dataSource as schema when processing the detail lines.  With this configuration in\n            place, creating a UI capable of updating across this parent-child association becomes\n            extremely easy - only two lines of SmartClient code, beyond the creation and layout \n            of the visual components themselves, is required.\n            <p>\n            Click a record in the top grid to see the order's details and the associated detail \n            lines in the form and grid below.\n            You can edit the order information using this screen (both header and detail - \n            double-click the grid to edit the details); when you click Save, SmartClient will \n            submit the master and detail information together, and Hibernate will save all \n            changes as a single operation.\n            "
                            },
                            {
                                dataSource:"flattenedBeans_flatUserHB",
                                icon:"[ISO_DOCS_SKIN]/images/iconexperience/branch.png",
                                id:"flattenedBeans",
                                jsURL:"serverExamples/hibernate/flattenedBeans/flattenedBeans.js",
                                requiresModules:"SCServer",
                                title:"Data Selection",
                                tabs:[
                                    {
                                        canEdit:"false",
                                        doEval:"false",
                                        title:"FlatUser.java",
                                        url:"serverExamples/hibernate/flattenedBeans/FlatUser.java"
                                    },
                                    {
                                        canEdit:"false",
                                        doEval:"false",
                                        title:"FlatUser.hbm.xml",
                                        url:"serverExamples/hibernate/flattenedBeans/FlatUser.hbm.xml"
                                    },
                                    {
                                        canEdit:"false",
                                        doEval:"false",
                                        title:"Address.java",
                                        url:"serverExamples/hibernate/flattenedBeans/Address.java"
                                    },
                                    {
                                        canEdit:"false",
                                        doEval:"false",
                                        title:"Address.hbm.xml",
                                        url:"serverExamples/hibernate/flattenedBeans/Address.hbm.xml"
                                    }
                                ],
                                descriptionHeight:"150",
                                description:"\n            This example shows the SmartClient Server's support for flattening and reconstructing\n            hierarchical data, by use of XPaths.  The ListGrid below shows each user's address,\n            city and state as if those fields were part of the user's data.  In fact, this address\n            information is held in a separate Address bean; this information is extracted from \n            the separate bean at fetch time by the SmartClient Server, based purely on the XPath\n            declarations of those fields in the dataSource.\n            <p>\n            More interestingly, the SmartClient Server is also able to reconstruct the hierarchical\n            data from the flattened version, again transparently by use of the XPath.  This means\n            that you can update the flattened fields in this example - for example, changing a\n            user's city - and your changes will be correctly persisted.\n            <p>\n            Note also that the User bean has a <code>password</code> attribute which is being \n            completely excluded from this example.  When you specify <code>dropExtraFields</code>\n            on a DataSource, as we are doing here, SmartClient Server returns just those fields \n            defined in the DataSource.  So, as in this example, you can use existing schema\n            whilst easily retaining tight control over what gets delivered to the client.  This \n            includes related entities as well as simple attributes.\n            <p>\n            Click a record in the grid to see the order's details in the form.  Edit the user\n            details and click \"Save Changes\".  Using the declared XPaths, the SmartClient Server \n            will populate any changed flattened field back into its correct place in the hierarchy,\n            allowing the data provider (Hibernate, in this case) to persist the change.\n            "
                            },
                            {
                                dataSource:"supplyItemSpringDMI",
                                id:"hibernateProduction",
                                jsURL:"dataIntegration/java/hibernateProduction.js",
                                requiresModules:"SCServer",
                                title:"Spring with Beans",
                                tabs:[
                                    {
                                        canEdit:"false",
                                        doEval:"false",
                                        title:"Spring applicationContext.xml",
                                        url:"dataIntegration/java/applicationContext.xml"
                                    },
                                    {
                                        canEdit:"false",
                                        doEval:"false",
                                        title:"SupplyItemDao.java",
                                        url:"dataIntegration/java/SupplyItemDao.java"
                                    },
                                    {
                                        canEdit:"false",
                                        doEval:"false",
                                        title:"SupplyItem.hbml.xml",
                                        url:"dataIntegration/java/SupplyItem.hbm.xml"
                                    },
                                    {
                                        canEdit:"false",
                                        doEval:"false",
                                        title:"SupplyItem.java",
                                        url:"dataIntegration/java/SupplyItem.java"
                                    }
                                ],
                                description:"\n            This example demonstrates how SmartClient can be used to call pre-existing Spring\n            business logic, and provides a general sample of integrating with beans-based\n            persistence systems.  <b>NOTE: if you want to use Hibernate in a new application,\n            use the built-in HibernateDataSource connector, not this code.</b>  The sample code\n            shown here has <b>less features</b> than the built-in connector (which supports\n            advanced search, multi-level sort, automatic transactions, and other features).\n            <P>\n            In this sample, Hibernate's <code>Criteria</code> object can be created\n            from SmartClient's <code>DSRequest</code> in order to fulfill the\n            \"fetch\" operation, with data paging enabled.  Hibernate-managed beans can be\n            populated with inbound, validated data with a single method call.\n            "
                            },
                            {
                                dataSource:"supplyItemDMI",
                                icon:"[ISO_DOCS_SKIN]/images/iconexperience/coffeebean.png",
                                id:"javaBeans",
                                jsURL:"dataIntegration/java/javaBeans.js",
                                requiresModules:"SCServer",
                                showDataSource:"false",
                                title:"Java Beans",
                                tabs:[
                                    {
                                        canEdit:"false",
                                        doEval:"false",
                                        title:"SupplyItemFetch.java",
                                        url:"dataIntegration/java/SupplyItemFetch.java"
                                    },
                                    {
                                        canEdit:"false",
                                        doEval:"false",
                                        title:"SupplyItem.java",
                                        url:"dataIntegration/java/SupplyItem.java"
                                    }
                                ],
                                description:"\n            SmartClient DataSource operations can be fulfilled by returning Java Beans (aka EJBs \n            or POJOs) from your existing business logic.  When you call SmartClient's \n            <code>DSResponse.setData()</code> API, your Java objects are automatically translated \n            to JavaScript, transmitted to the browser, and provided to the requesting component.\n            See the sample implementation of the \"fetch\" operation in SupplyItemFetch.java\n            "
                            },
                            {
                                dataSource:"supplyItemDMI",
                                icon:"[ISO_DOCS_SKIN]/images/iconexperience/code_java.png",
                                id:"DMI",
                                jsURL:"dataIntegration/java/dmi.js",
                                requiresModules:"SCServer",
                                title:"DMI",
                                tabs:[
                                    {
                                        canEdit:"false",
                                        doEval:"false",
                                        title:"SupplyItemDMI.java",
                                        url:"dataIntegration/java/SupplyItemDMI.java"
                                    },
                                    {
                                        canEdit:"false",
                                        doEval:"false",
                                        title:"SupplyItem.java",
                                        url:"dataIntegration/java/SupplyItem.java"
                                    }
                                ],
                                description:"\n            Direct Method Invocation (DMI) allows you to map DataSource operations directly \n            to Java methods via XML configuration in a DataSource descriptor (.ds.xml file).\n            The arguments of your Java methods are automatically populated from the inbound \n            request.  See the sample implementation in SupplyItemDMI.java\n            "
                            },
                            {
                                dataSource:"supplyItemHB",
                                icon:"[ISO_DOCS_SKIN]/images/silkicons/database_gear.png",
                                id:"autoTransactionsHB",
                                jsURL:"serverExamples/hibernate/autoTransactions/autoTransactions.js",
                                requiresModules:"SCServer, transactions",
                                showSkinSwitcher:false,
                                showSource:true,
                                title:"Auto Transactions",
                                description:"\n            SmartClient Hibernate DataSources participate fully in automatic transaction \n            management (Power and Enterprise Editions only).<p>\n            Drag multiple records from the left-hand grid to the right.  SmartClient will \n            send the updates to the server in a single queue; SmartClient Server will \n            automatically treat that queue as a single database transaction.  This is the\n            default behavior, and requires no code or config to enable it; if you require\n            it, however, very flexible, fine-grained control over transactions is possible,\n            through configuration, code or a combination of the two.\n            "
                            },
                            {
                                id:"uploadHB",
                                jsURL:"serverExamples/hibernate/upload/upload.js",
                                requiresModules:"SCServer",
                                title:"Upload",
                                tabs:[
                                    {
                                        canEdit:"false",
                                        dataSource:"mediaLibraryHB",
                                        name:"mediaLibraryHB"
                                    }
                                ],
                                descriptionHeight:"160",
                                description:"\n		This example uses a DynamicForm bound to a DataSource with a field of type \"imageFile\" to\n		enable files to be uploaded and both a <b>ListGrid</b> and <b>TileGrid</b> to display \n		the existing records, via a shared ResultSet.\n		<P>\n		Enter a Title and select a local image-file to upload and click 'Save' to upload the file.\n		Note that the file-size is limited to 50k via the DataSourceField property \n		<i>maxFileSize</i> (see the mediaLibrary tab below).\n		<P>\n		\"imageFile\" fields can either display a download/save icon-pair and title, or can render\n		the image directly inline.  Use the buttons below to switch between the TileGrid and \n		ListGrid views to see each of these behaviors.  Note that both components can render\n		either UI for \"imageFile\" fields and will do so automatically, according to the value of \n		field.<i>showFileInline</i>.\n		\n		"
                            }
                        ]
                    },
                    {
                        icon:"[ISO_DOCS_SKIN]/images/silkicons/server_lightning.png",
                        isOpen:false,
                        title:"JPA",
                        description:"\n        The SmartClient Server's built-in support for JPA/JPA2 allows you to easily use your JPA annotated entities\n        in SmartClient's client-side widgets.<p/>\n        In server.properties file you can specify entity manager factory acquisition and transaction management mode\n        by setting \"jpa.emfProvider\" property to one of:<ul>\n            <li>com.isomorphic.jpa.EMFProviderLMT - for Locally Managed Transactions (LMT)</li>\n            <li>com.isomorphic.jpa.EMFProviderNoTransactions - no transactions support</li>\n            <li>com.isomorphic.jpa.EMFProviderCMT - for Container Managed Transactions (CMT)</li>\n            <li>your own implementation of com.isomorphic.jpa.EMFProviderInterface</li></ul>\n        For LMT provider you have to specify \"jpa.persistenceUnitName\" property specifying PU name.<br/>\n        For CMT provider you have to specify \"jpa.cmt.entityManager\" and \"jpa.cmt.transaction\" properties\n        specifying appropriate resource reference names declared in /WEB-INF/web.xml.<p/>\n        When creating DataSource descriptors specify properties:<ul>\n            <li>\"serverConstructor\" to:<ul>\n                <li>com.isomorphic.jpa.JPADataSource - for JPA 1.0 implementation</li>\n                <li>com.isomorphic.jpa.GAEJPADataSource - for JPA 1.0 implementation for Google Application Engine</li>\n                <li>com.isomorphic.jpa.JPA2DataSource - for JPA 2.0 implementation which uses Criteria API</li></ul></li>\n            <li>\"beanClassName\" to fully qualified entity class name</li></ul>\n    ",
                        children:[
                            {
                                dataSource:"supplyItemJPAAutoDerive",
                                id:"jpaConnector",
                                jsURL:"serverExamples/jpa/jpaConnector.js",
                                requiresModules:"SCServer",
                                title:"Auto Derivation",
                                tabs:[
                                    {
                                        canEdit:"false",
                                        doEval:"false",
                                        title:"SupplyItemHB.java",
                                        url:"serverExamples/jpa/SupplyItemHB.java"
                                    },
                                    {
                                        canEdit:"false",
                                        doEval:"false",
                                        title:"persistence.xml",
                                        url:"serverExamples/jpa/persistence1.xml"
                                    }
                                ],
                                descriptionHeight:"150",
                                description:"\n                If you have pre-existing JPA entities, SmartClient can automatically derive fully functional\n                DataSources given just the Java classname of the mapped JPA entity.  The grid below is connected\n                to a JPA-managed entity via the simple declarations in supplyItemJPAAutoDerive.ds.xml - no other\n                configuration or Java code is required beyond the entity itself with JPA mapping, which are\n                samples intended to represent a pre-existing JPA entity.\n                <p/>\n                To search, use the controls above the grid's header. Note that data paging is automatically\n                enabled - just scroll down to load data on demand. Click on the red icon next to each record to\n                delete it. Click on a record to edit it and click \"Add New\" to add a new record.  Note that the\n                editing controls are type sensitive: a date picker appears for the \"Next Shipment\" field, and\n                the \"Units\" field shows a picklist because its Java type is an Enum.\n                <p/>\n                You can use DMI to add business logic that takes place before and after JPA operations to\n                enforce security or add additional data validation rules.\n            "
                            },
                            {
                                dataSource:"worldJPA2",
                                id:"jpa2Connector",
                                jsURL:"serverExamples/jpa/jpa2Connector.js",
                                requiresModules:"SCServer,serverCriteria",
                                title:"Advanced Filtering",
                                tabs:[
                                    {
                                        canEdit:"false",
                                        doEval:"false",
                                        title:"World.java",
                                        url:"serverExamples/jpa/World.java"
                                    },
                                    {
                                        canEdit:"false",
                                        doEval:"false",
                                        title:"persistence.xml",
                                        url:"serverExamples/jpa/persistence2.xml"
                                    }
                                ],
                                descriptionHeight:"150",
                                description:"\n                Use the FilterBuilder to construct queries of arbitrary complexity.  The FilterBuilder,\n                and the underlying AdvancedCriteria system, support building queries with subclauses\n                nested to any depth. Add clauses to your query with the \"+\" icon; add nested subclauses\n                with the \"+()\" button. Click \"Filter\" to see the result in the ListGrid.\n                <p/>\n                Note that this example is backed by a JPA 2.0 dataSource; the SmartClient Server is \n                automatically generating the JPA Criteria Queries to implement the filters that\n                the FilterBuilder can assemble. This works adaptively and seamlessly with client-side \n                Advanced Filtering: the generated Criteria query will yield exactly the same resultset \n                as the client-side filtering.  This means SmartClient is able to switch to client-side\n                filtering when its cache is full, giving a more responsive, more scalable application.\n                <p/>\n                When using JPA 2 make sure you have correctly specified its version in persistence.xml.\n            "
                            },
                            {
                                id:"jpaRelationManyToOneSimple",
                                jsURL:"serverExamples/jpa/relations/jpaRelationManyToOneSimple.js",
                                requiresModules:"SCServer",
                                title:"Many-to-One Relation",
                                tabs:[
                                    {
                                        dataSource:"cityManyToOneSimpleJPA",
                                        name:"cityManyToOneSimpleJPA"
                                    },
                                    {
                                        dataSource:"countryManyToOneSimpleJPA",
                                        name:"countryManyToOneSimpleJPA"
                                    },
                                    {
                                        canEdit:"false",
                                        doEval:"false",
                                        title:"CityManyToOneSimple.java",
                                        url:"serverExamples/jpa/relations/CityManyToOneSimple.java"
                                    },
                                    {
                                        canEdit:"false",
                                        doEval:"false",
                                        title:"CountryManyToOneSimple.java",
                                        url:"serverExamples/jpa/relations/CountryManyToOneSimple.java"
                                    },
                                    {
                                        canEdit:"false",
                                        doEval:"false",
                                        title:"persistence.xml",
                                        url:"serverExamples/jpa/relations/persistenceManyToOneSimple.xml"
                                    }
                                ],
                                descriptionHeight:"200",
                                description:"\n                SmartClient handles JPA Many-to-One relations transparently, such as Cities which belong\n                to Countries.  Just declare a foreignKey field on the City DataSource to indicate you\n                want to use the related JPA entity Country.\n                <P>\n                The grid below shows Cities, but the Country name is automatically shown even though the\n                countryName is stored in the related JPA entity Country.  Any fields from any number of\n                related entities can be automatically loaded this way.\n                <P>\n                Click to edit and change the Country of a City.  The list of Countries is automatically\n                loaded from the related JPA entity, along with their IDs (not shown).  \n                <P>\n                Changing the Country of a City sends the ID of the new Country back to the server, and\n                SmartClient automatically makes all the required JPA calls to persist the change - no\n                server-side code needs to be written beyond the JPA beans themselves and their\n                annotations.\n            "
                            },
                            {
                                id:"jpaRelationOneToMany",
                                jsURL:"serverExamples/jpa/relations/jpaRelationOneToMany.js",
                                requiresModules:"SCServer",
                                title:"One-to-Many Relation",
                                tabs:[
                                    {
                                        dataSource:"cityOneToManyJPA",
                                        name:"cityOneToManyJPA"
                                    },
                                    {
                                        dataSource:"countryOneToManyJPA",
                                        name:"countryOneToManyJPA"
                                    },
                                    {
                                        canEdit:"false",
                                        doEval:"false",
                                        title:"CityOneToMany.java",
                                        url:"serverExamples/jpa/relations/CityOneToMany.java"
                                    },
                                    {
                                        canEdit:"false",
                                        doEval:"false",
                                        title:"CountryOneToMany.java",
                                        url:"serverExamples/jpa/relations/CountryOneToMany.java"
                                    },
                                    {
                                        canEdit:"false",
                                        doEval:"false",
                                        title:"persistence.xml",
                                        url:"serverExamples/jpa/relations/persistenceOneToMany.xml"
                                    }
                                ],
                                descriptionHeight:"150",
                                description:"\n                SmartClient handles JPA One-to-Many relations transparently, such as a Country which has\n                multiple Cities.  Just declare a collection field (multiple:true) on the Country\n                DataSource to indicate you want to load its list of Cities.\n                <P>\n                Click on a Country below - its list of Cities is revealed without a new trip to the\n                server.  Cities can be now edited in the lower grid.  \n                <P>\n                When data is saved, all changes to the Country and its Cities are sent in one save\n                request, and SmartClient automatically makes all the required JPA calls to persist the\n                changes - no server-side code needs to be written beyond the JPA beans themselves and\n                their annotations.\n            "
                            }
                        ]
                    },
                    {
                        icon:"[ISO_DOCS_SKIN]/images/silkicons/server_lightning.png",
                        id:"transactionsFolder",
                        isOpen:false,
                        title:"Transactions",
                        description:"\n            SmartClient provides robust support for transactional applications.\n            <P>\n            <b>Queuing</b> allows you to easily combine operations together into a single\n            transaction, for more efficient data loading and transactional saves.\n            <P>\n            <b>Automatic Transaction Management</b> support in the SmartClient Server, with \n            specific implementations for the built-in SQL and Hibernate DataSources, allows \n            for queued requests to be committed or rolled back as a single database transaction.\n            This feature is only available in Power and Enterprise editions.\n            <P>\n            <b>Transaction Chaining</b> allows you to declaratively handle data dependencies\n            between operations submitted together in a queue.  This feature is only available\n            in Power and Enterprise editions.\n     ",
                        children:[
                            {
                                id:"queuing",
                                jsURL:"serverExamples/hibernate/queuing/queuing.js",
                                requiresModules:"SCServer",
                                title:"Simple Queuing",
                                tabs:[
                                    {
                                        canEdit:"false",
                                        dataSource:"queuing_userHB",
                                        name:"queuing_user"
                                    },
                                    {
                                        canEdit:"false",
                                        dataSource:"queuing_order",
                                        name:"queuing_order"
                                    },
                                    {
                                        canEdit:"false",
                                        doEval:"false",
                                        title:"User.java",
                                        url:"serverExamples/hibernate/queuing/User.java"
                                    },
                                    {
                                        canEdit:"false",
                                        doEval:"false",
                                        title:"User.hbm.xml",
                                        url:"serverExamples/hibernate/queuing/User.hbm.xml"
                                    }
                                ],
                                descriptionHeight:"150",
                                description:"\n            Queuing allows any set of DataSource operations to be combined into a single HTTP\n            request, without requiring any special code to be written to transport the combined\n            inputs and outputs.\n            <p>\n            Click the \"Find Orders\" button and the example will load both the selected user's\n            details and all the orders associated with that user, as a single request.\n            Queuing works transparently to the components involved, so for example, scrolling down\n            in the orders grid causes data paging to be activated, exactly as though the grid had\n            done a fetch that was not combined into a queue.  \n            <P>\n            Since queuing is transparent to components, a screen full of various components\n            which need to load data from different sources can participate in a queue without\n            any special component-specific code, and with no need to rework how data is\n            transferred if new components are added - each component can be treated as though\n            it were standalone.\n            <P>\n            Server-side, queuing allows you to focus on simple, secure, reusable data\n            operations and other services, which can then be accessed in arbitrary combinations\n            according to the data loading and saving requirements of particular screens, with\n            no need to write brittle, screen-specific server code.\n            <P>\n            Queuing works even when the operations are on different data providers (as in this \n            case, where the user details are coming from Hibernate and the order details are coming\n            from the SmartClient Server SQL provider).\n            "
                            },
                            {
                                dataSource:"supplyItem",
                                icon:"[ISO_DOCS_SKIN]/images/silkicons/database_gear.png",
                                id:"autoTransactions",
                                jsURL:"serverExamples/transactions/autoTransactions/autoTransactions.js",
                                requiresModules:"SCServer, transactions",
                                showSkinSwitcher:false,
                                showSource:true,
                                title:"Automatic Transaction Management",
                                description:"\n            Drag multiple records from the left-hand grid to the right.  SmartClient will \n            send the updates to the server in a single queue; SmartClient Server will \n            automatically treat that queue as a single database transaction.  This is the\n            default behavior, and requires no code or config to enable it; if you require\n            it, however, very flexible, fine-grained control over transactions is possible,\n            through configuration, code or a combination of the two.\n            "
                            },
                            {
                                icon:"[ISO_DOCS_SKIN]/images/silkicons/table_row_insert.png",
                                id:"queuedAdd",
                                jsURL:"serverExamples/sql/queuedAdd/queuedMasterDetailAdd.js",
                                requiresModules:"SCServer, chaining",
                                title:"Master/Detail Add",
                                tabs:[
                                    {
                                        canEdit:"false",
                                        dataSource:"queuedAdd_order",
                                        name:"order"
                                    },
                                    {
                                        canEdit:"false",
                                        dataSource:"queuedAdd_orderItem",
                                        name:"orderItem"
                                    },
                                    {
                                        canEdit:"false",
                                        dataSource:"supplyItem",
                                        name:"supplyItem"
                                    },
                                    {
                                        canEdit:"false",
                                        dataSource:"supplyCategory",
                                        name:"supplyCategory"
                                    }
                                ],
                                descriptionHeight:"150",
                                description:"\n            This example makes use of the SmartClient server's support for setting DSRequest \n            properties dynamically at runtime, based on responses to requests earlier in the \n            same queue.<p>\n            Edit the order header details, then add one or more lines.  When you click \"Save \n            Order\", SmartClient will send multiple DataSource requests to the server - one to\n            save the order header, and one each for however many lines you enter - but it will combine\n            them into a single HTTP request, so that a transactional commit is possible. Since\n            this sample is backed by SmartClient SQLDataSources, the queue is \n            automatically assembled into a single transaction (in Power Edition and above).<p>\n            New orders are given an automatically generated sequence value as a primary\n            key, and the orderItems need this value in order to establish a relationship with\n            their order.<P>\n            As a result of the <code>&lt;values&gt;</code> tag in the <code>queuedAdd_orderItem</code>\n            DataSource definition, the server will set the \"orderID\" property on each order\n            item to the unique sequence value assigned to the order header when it was\n            saved.<P>\n            This entire interaction is accomplished by simply re-using the capability of the\n            DataSource to add new records, without the need to write any server-side\n            code.  SQL DataSources are shown, but this interaction works with any DataSource\n            that can support CRUD operations, including custom DataSources and even a mix of\n            DataSources that use different storage systems.<P>\n            The <code>&lt;values&gt;</code> tag (and the similar \n            property <code>&lt;criteria&gt;</code> tag) are specified using the Velocity\n            Template Language, so the support is very flexible.<P>\n            "
                            },
                            {
                                icon:"[ISO_DOCS_SKIN]/images/silkicons/arrow_out.png",
                                ref:"massUpdate",
                                title:"Mass Update",
                                descriptionHeight:"120",
                                description:"\n        <b>Click</b> on any cell to start editing, then <b>Tab</b> or <b>Down Arrow</b> past the\n        last row in the grid to create a new row. Alternatively, click the <b>Edit New</b> button\n        to create a new data-entry row at the end of the grid.  When you click the \"Save\" button\n        all your changes - changed rows and new ones - are sent to the server in a queue, as a \n        single HTTP request.<p>\n        Because all your changes arrive on the server at once, committing them as a single \n        transaction becomes possible; if you are using the built-in SQL or Hibernate dataSources,\n        and have Power edition or above, automatic transactional commit is the default.  And \n        because SmartClient's queuing support is completely inobtrusive and requires no extra \n        code on either client or server, as soon as you have an operation that can update a \n        single record, you automatically have an operation that can participate in SmartClient\n        queued updates and automatic transactional commits.\n        "
                            },
                            {
                                icon:"[ISO_DOCS_SKIN]/images/silkicons/table_go.png",
                                ref:"databoundDragCopy",
                                title:"Multi-Row Drag & Save",
                                descriptionHeight:"160",
                                description:"\n        Drag employee records into the Project Team Members list.  SmartClient recognizes that the \n        two dataSources are linked by a foreign key relationship, and automatically uses that \n        relationship to populate values in the records that are added when you drop. SmartClient \n        also populates fields based on current criteria and maps explicit titleFields as necessary.<p>\n        Multi-row selection is enabled on the Employees grid, so you can select multiple employees \n        and drag them to the Teams grid in one go.  Because the grids are databound, this drag and \n        drop action will send data operations to the server automatically, using SmartClient \n        queuing to ensure all the updates arrive on the server together and, since this example\n        is backed by a SmartClient SQLDataSource, are committed together in a single database \n        transaction (in Power edition and above).<p>\n        All of this just works, there is no coding needed to enable it.\n         "
                            },
                            {
                                descriptionHeight:"140",
                                icon:"[ISO_DOCS_SKIN]/images/silkicons/arrow_undo.png",
                                id:"rollback",
                                jsURL:"serverExamples/transactions/rollback/rollback.js",
                                requiresModules:"SCServer, transactions",
                                showSkinSwitcher:false,
                                showSource:true,
                                title:"Rollback",
                                tabs:[
                                    {
                                        canEdit:"false",
                                        dataSource:"rbCountryTransactions",
                                        name:"rbCountryTransactions"
                                    },
                                    {
                                        canEdit:"false",
                                        dataSource:"worldDS",
                                        name:"worldDS"
                                    }
                                ],
                                description:"\n            SmartClient Server detects when a DSRequest that is part of a transaction fails,\n            and automatically rolls the transaction back.<p>\n            Change several records in the grid, then click \"Save\".  The underlying DataSource \n            specifies a \"hasRelatedRecord\" validation on the country name, looking up against \n            all the countries of the world; if you change a country's name to something \n            non-existent, that validation will fail and the entire transaction will be rolled\n            back.  All of your changes will remain pending (the changed values will still be \n            shown in blue), and if you refresh the page you can verify that the data is \n            unchanged on the server.<p>\n            If you correct the validation error and click \"Save\" again, the transaction will \n            be committed and your changes will be persisted.\n            "
                            },
                            {
                                descriptionHeight:"150",
                                icon:"[ISO_DOCS_SKIN]/images/silkicons/user_go.png",
                                id:"jdbcOperations",
                                jsURL:"serverExamples/transactions/jdbcOperations/jdbcOperations.js",
                                requiresModules:"SCServer, transactions",
                                showSkinSwitcher:false,
                                showSource:true,
                                title:"Transactional User Operations",
                                tabs:[
                                    {
                                        canEdit:"false",
                                        dataSource:"countryTransactions",
                                        name:"countryTransactions"
                                    },
                                    {
                                        canEdit:"false",
                                        dataSource:"lastUpdated",
                                        name:"lastUpdated"
                                    },
                                    {
                                        canEdit:"false",
                                        doEval:"false",
                                        name:"JDBCOperations.java",
                                        url:"serverExamples/transactions/jdbcOperations/JDBCOperations.java"
                                    }
                                ],
                                description:"\n            User-written operations - in this example, hand-crafted JDBC updates - can be \n            included in SmartClient automatic transactions, and will be committed or rolled \n            back alongside the normal SmartClient operations.<p>\n            Edit rows in the grid, then click \"Good Save\".  Your changes will be \n            persisted to the database as part of a queue that also includes a user-written \n            JDBC update to a \"lastChanged\" table; the DMI method has been written to use \n            the SmartClient transaction (see the <code>JS</code> and \n            <code>JDBCOperations.java</code> tabs).  The example will then fetch the current \n            value from the lastUpdated table and display it in the blue label; you will see \n            that it has been updated.<p>\n            Now make further changes and click \"Bad Save\".  This causes a deliberately \n            broken version of the user-written JDBC update to be run, resulting in a SQL error\n            and a rolled-back transaction (and an error dialog referring to an unknown column).  \n            Note that your changes have not been saved (they\n            are still presented in blue, to show that they are pending) and the \"last updated\"\n            label has not changed; the entire transaction, both SmartClient requests and \n            user-written query, has been rolled back.  If you now click \"Good Save\", your pending\n            changes will be persisted and the \"last updated\" label will change to reflect this.\n            "
                            }
                        ]
                    },
                    {
                        icon:"[ISO_DOCS_SKIN]/images/silkicons/server_lightning.png",
                        isOpen:false,
                        title:"Custom DataSources",
                        description:"\n        Examples showing how to leverage the SmartClient Server to create partially or completely\n        customized DataSource implementations.\n    ",
                        children:[
                            {
                                icon:"[ISO_DOCS_SKIN]/images/iconexperience/coffeebean.png",
                                id:"javabeanWizard",
                                jsURL:"serverExamples/other/vb_Javabean.js",
                                requiresModules:"SCServer",
                                showSkinSwitcher:false,
                                showSource:false,
                                title:"Javabean Wizard",
                                description:"\n            SmartClient's Visual Builder tool provides an extremely easy and completely codeless \n            way to create DataSources based on your existing Javabeans and POJOs.  Click\n            the \"New\" button, select \"JavaBean\", and enter the name of an existing Javabean \n            class.  Visual Builder will create a DataSource descriptor that is almost complete -\n            just connect it up to your custom DataSource implementation with the \n            <code>serverConstructor</code> property and it's ready to go.\n            "
                            },
                            {
                                icon:"[ISO_DOCS_SKIN]/images/silkicons/table_row_insert.png",
                                id:"customDataSource",
                                jsURL:"serverExamples/other/customDataSource/customDataSource.js",
                                requiresModules:"SCServer",
                                title:"Simple (Hardcoded)",
                                tabs:[
                                    {
                                        canEdit:"false",
                                        dataSource:"customDataSource_user",
                                        name:"user"
                                    },
                                    {
                                        canEdit:"false",
                                        doEval:"false",
                                        title:"UserDataSource.java",
                                        url:"serverExamples/other/customDataSource/UserDataSource.java"
                                    }
                                ],
                                descriptionHeight:"150",
                                description:"\n            This example shows an entirely custom DataSource.  It is created by extending \n            <code>BasicDataSource</code> and implementing the four core CRUD methods.  In this \n            case, we maintain a static List of Maps that is initialized with hard-coded data\n            every time the server starts; but of course, this code could do <i>anything</i>. This \n            approach allows completely custom data operations to be simply plugged in to the\n            SmartClient Server framework.<p>\n            Note also that this code deals directly with Java <code>Map</code>s and \n            <code>List</code>s, without worrying about format conversions - even custom code \n            leverages the SmartClient Server's automatic and transparent translation of request\n            data, from JSON to Java and back to JSON.\n            "
                            },
                            {
                                icon:"[ISO_DOCS_SKIN]/images/iconexperience/objects_exchange.png",
                                id:"ormDataSource",
                                jsURL:"serverExamples/other/ormDataSource/ormDataSource.js",
                                requiresModules:"SCServer",
                                title:"ORM DataSource",
                                tabs:[
                                    {
                                        canEdit:"false",
                                        dataSource:"ormDataSource_country",
                                        name:"ormDataSource_country"
                                    },
                                    {
                                        canEdit:"false",
                                        doEval:"false",
                                        title:"ORMDataSource.java",
                                        url:"serverExamples/other/ormDataSource/ORMDataSource.java"
                                    },
                                    {
                                        canEdit:"false",
                                        doEval:"false",
                                        title:"Country.java",
                                        url:"serverExamples/other/ormDataSource/Country.java"
                                    },
                                    {
                                        canEdit:"false",
                                        doEval:"false",
                                        title:"Country.hbm.xml",
                                        url:"serverExamples/other/ormDataSource/Country.hbm.xml"
                                    }
                                ],
                                descriptionHeight:"220",
                                description:"\n            This example shows an entirely custom DataSource that connects SmartClient Server to\n            Hibernate. It is very simple implementation created by extending <code>BasicDataSource</code>\n            and implementing the four core CRUD methods. In this case, single DataSource\n            implementation handles single Hibernate entity. Features like data pagination, server-side sorting\n            and filtering are not implemented here.<p>\n            Creating an equivalent adapter for Toplink or Ibatis or some other ORM solution would\n            be a fairly simple matter of replacing the Hibernate-specific code in this example\n            with the equivalent specifics from the other ORM system.\n            <p>\n            As with the other custom DataSource examples, note how the <code>ORMDataSource.java</code> \n            code deals entirely in native Java objects - even entirely custom DataSources benefit\n            from SmartClient Server's robust and comprehensive Javascript<->Java translation.\n            "
                            },
                            {
                                icon:"[ISO_DOCS_SKIN]/images/iconexperience/objects_exchange.png",
                                id:"reusableORMDataSource",
                                jsURL:"serverExamples/other/reusableORMDataSource/reusableORMDataSource.js",
                                requiresModules:"SCServer",
                                title:"Reusable ORM DataSource",
                                tabs:[
                                    {
                                        canEdit:"false",
                                        dataSource:"reusableORMDataSource_supplyItem",
                                        name:"reusableORMDataSource_supplyItem"
                                    },
                                    {
                                        canEdit:"false",
                                        dataSource:"reusableORMDataSource_country",
                                        name:"reusableORMDataSource_country"
                                    },
                                    {
                                        canEdit:"false",
                                        doEval:"false",
                                        title:"ReusableORMDataSource.java",
                                        url:"serverExamples/other/reusableORMDataSource/ReusableORMDataSource.java"
                                    },
                                    {
                                        canEdit:"false",
                                        doEval:"false",
                                        title:"SupplyItemHB.java",
                                        url:"serverExamples/other/reusableORMDataSource/SupplyItemHB.java"
                                    },
                                    {
                                        canEdit:"false",
                                        doEval:"false",
                                        title:"Country.java",
                                        url:"serverExamples/other/reusableORMDataSource/Country.java"
                                    },
                                    {
                                        canEdit:"false",
                                        doEval:"false",
                                        title:"Country.hbm.xml",
                                        url:"serverExamples/other/reusableORMDataSource/Country.hbm.xml"
                                    }
                                ],
                                descriptionHeight:"220",
                                description:"\n            This example shows an entirely custom DataSource that connects SmartClient Server to\n            Hibernate (note that this is just an example of the principles involved - SmartClient\n            Server's built-in Hibernate support is considerably more sophisticated than the\n            simple adapter shown here).  It is created by extending <code>BasicDataSource</code>\n            and implementing the four core CRUD methods.  In this case, we connect DataSource\n            requests to Hibernate <code>Criteria</code> queries and the <code>saveOrUpdate</code>\n            method.<p>\n            This implementation, though simple, is fully functional and could be used unchanged\n            in a real application.  It supports all four CRUD operations, plus data pagination,\n            server-side sorting and filtering, client cache synchronization, and of course it\n            is actually persisting the data to a real database. In this case, single DataSource\n            implementation handles two different entities using reflrection.\n            Note that it is simplified version of built-in connector which handles AdvancedCriteria\n            filtering.<p>\n            As with the other custom DataSource examples, note how the <code>ORMDataSource.java</code>\n            code deals entirely in native Java objects - even entirely custom DataSources benefit\n            from SmartClient Server's robust and comprehensive Javascript<->Java translation.\n            "
                            },
                            {
                                dataSource:"dynamicDSFields",
                                id:"editableServerSideDataSource",
                                jsURL:"serverExamples/other/editableServerSideDataSource/editableServerSideDataSource.js",
                                requiresModules:"SCServer",
                                title:"Editable Server-Side DataSource",
                                tabs:[
                                    {
                                        canEdit:"false",
                                        doEval:"false",
                                        title:"DynamicDSFields.data.xml",
                                        url:"serverExamples/other/editableServerSideDataSource/dynamicDSFields.data.xml"
                                    },
                                    {
                                        canEdit:"false",
                                        doEval:"false",
                                        title:"GeneratorSetup.java",
                                        url:"serverExamples/other/editableServerSideDataSource/GeneratorSetup.java"
                                    }
                                ],
                                descriptionHeight:"100",
                                description:"\n           	This example demonstrates a DataSource whose definition is stored in a SQL database rather\n		    than in a static .ds.xml file.  You can edit the fields of the DataSource in the grid\n		    below, then press \"Reload\" to see a DynamicForm bound to the modified DataSource.\n		    <P>\n		    You can use this pattern to allow your end users to dynamically change the definition of\n		    DataSources in your application - for example, add new fields, or add additional validators\n		    to existing fields.\n            "
                            }
                        ]
                    },
                    {
                        icon:"[ISO_DOCS_SKIN]/images/silkicons/arrow_out.png",
                        isOpen:false,
                        showSkinSwitcher:"true",
                        title:"Export",
                        description:"\n    Exporting Data from DataSources and DataBoundComponents.\n    ",
                        children:[
                            {
                                icon:"[ISO_DOCS_SKIN]/images/silkicons/arrow_out.png",
                                ref:"export",
                                title:"Excel Export"
                            },
                            {
                                descriptionHeight:"100",
                                icon:"[ISO_DOCS_SKIN]/images/silkicons/arrow_out.png",
                                id:"formattedExport",
                                jsURL:"grids/formattedExport.js",
                                requiresModules:"SCServer",
                                title:"Formatted Export",
                                tabs:[
                                    {
                                        canEdit:"false",
                                        title:"worldDSExport",
                                        url:"grids/ds/worldSQLDSExport.ds.xml"
                                    }
                                ],
                                description:"\n            You can export the client-side data from a DataBoundComponent.  That is, the data \n            as seen in a component, including the effects of client-side formatters.\n            <p>In the example below, choose an export format from the select-list, decide \n            whether to download the results or view them in a window using the checkbox and \n            click the Export button.  \n            <p>Data is exported according to the filters and sort-order on the grid and includes\n            the formatted values and field-titles as seen in the grid.\n        "
                            },
                            {
                                icon:"[ISO_DOCS_SKIN]/images/silkicons/arrow_out.png",
                                id:"customExport",
                                jsURL:"serverExamples/sql/customExport/customExport.js",
                                requiresModules:"SCServer",
                                title:"Custom Export",
                                tabs:[
                                    {
                                        canEdit:"false",
                                        doEval:"false",
                                        title:"CustomExportDMI.java",
                                        url:"serverExamples/sql/customExport/CustomExportDMI.java"
                                    },
                                    {
                                        canEdit:"false",
                                        title:"worldDSExportCustom",
                                        url:"serverExamples/sql/customExport/worldDSExportCustom.ds.xml"
                                    }
                                ],
                                description:"\n            You can produce a <i>formatted</i> export using DMI and affecting data server-side.\n            This example shows a normal export via a DMI in an operationBinding, where the DMI\n            enhances the exported data, formatting the <i>Independence</i> date field and \n            adding a calculated field <i>gdppercapita</i> at the server-side.\n            <p>Choose an Export-Format from the select-list, decide \n            whether to download the results or view them in a window using the checkbox and \n            click the Export button.  In this case, exporting to all formats is achieved via\n            operationBindings that specify the server DMI and, in the case of exports to JSON,\n            also the <i>exportAs</i> flag.  See the <i>JS</i> and <i>worldDSExportCustom</i> \n            tabs below.\n        "
                            },
                            {
                                icon:"[ISO_DOCS_SKIN]/images/silkicons/arrow_out.png",
                                id:"customExportCustomResponse",
                                jsURL:"serverExamples/sql/customExport/customExportCustomResponse.js",
                                requiresModules:"SCServer",
                                title:"Custom Export (Custom Response)",
                                tabs:[
                                    {
                                        canEdit:"false",
                                        doEval:"false",
                                        title:"CustomExportCustomResponseDMI.java",
                                        url:"serverExamples/sql/customExport/CustomExportCustomResponseDMI.java"
                                    },
                                    {
                                        canEdit:"false",
                                        title:"supplyItemExport.ds.xml",
                                        url:"serverExamples/sql/customExport/supplyItemExport.ds.xml"
                                    }
                                ],
                                description:"\n            You can export entirely custom data via a DMI.  Click the button to issue a call\n            to dataSource.exportData() with an operationId that specifies a server DMI.  In\n            this example, the DMI method ignores all the regular export parameters, calls\n            doCustomResponse() and writes directly into the response output stream.\n        "
                            }
                        ]
                    },
                    {
                        isOpen:false,
                        showSkinSwitcher:"true",
                        title:"Real-Time Messaging",
                        description:"\n	     RTM module provides low-latency, high data volume streaming\n         capabilities for latency-sensitive applications such as trading desks and operations\n         centers.\n	    ",
                        children:[
                            {
                                dataSource:"stockQuotes",
                                id:"portfolioGrid",
                                jsURL:"serverExamples/other/rtm/stockQuotes.js",
                                requiresModules:"RealtimeMessaging",
                                showDataSource:"true",
                                title:"Portfolio Grid",
                                tabs:[
                                    {
                                        canEdit:"false",
                                        doEval:"false",
                                        title:"stockQuotesData",
                                        url:"serverExamples/other/rtm/stockQuotes.data.xml"
                                    }
                                ],
                                descriptionHeight:"100",
                                description:"\n		        The grid below is receiving simulated, real-time updates of stock data via the Real Time Messaging\n		        (RTM) module.  The RTM module provides low-latency, high data volume streaming\n		        capabilities for latency-sensitive applications such as trading desks and operations\n		        centers.\n		        <P>\n		        Randomly generated updates will stream from the server for 90 seconds - click 'Generate\n		        Data' to restart streaming.  \n		        <P>\n		        The RTM module can connect to Java Message Service (JMS) channels without writing any\n		        code, or can be connected to custom messaging solutions with a simple adapter.\n		        \n		        "
                            },
                            {
                                dataSource:"stockQuotes",
                                id:"stockQuotesChart",
                                jsURL:"serverExamples/other/rtm/stockQuotesChart.js",
                                requiresModules:"Drawing,Analytics,RealtimeMessaging",
                                showDataSource:"true",
                                title:"Stock Chart",
                                tabs:[
                                    {
                                        canEdit:"false",
                                        doEval:"false",
                                        title:"stockQuotesData",
                                        url:"serverExamples/other/rtm/stockQuotes.data.xml"
                                    }
                                ],
                                descriptionHeight:"100",
                                description:"\n		        The chart below is receiving simulated, real-time updates to stock values via the Real\n		        Time Messaging (RTM) module.  The RTM module provides low-latency, high data\n		        volume streaming capabilities for latency-sensitive applications such as\n		        trading desks and operations centers.\n		        <P>\n		        Randomly generated updates will stream from the server for 90 seconds - click 'Generate\n		        More Updates' to restart streaming.  \n                <P>\n                Right click on the chart to switch the type of visualization.\n		        \n		        "
                            }
                        ]
                    },
                    {
                        id:"upload",
                        jsURL:"serverExamples/sql/upload/upload.js",
                        requiresModules:"SCServer",
                        title:"Upload",
                        tabs:[
                            {
                                canEdit:"false",
                                dataSource:"mediaLibrary",
                                name:"mediaLibrary"
                            }
                        ],
                        descriptionHeight:"160",
                        description:"\n        This example uses a DynamicForm bound to a DataSource with a field of type \"imageFile\" to\n        enable files to be uploaded and both a <b>ListGrid</b> and <b>TileGrid</b> to display \n        the existing records, via a shared ResultSet.\n        <P>\n        Enter a Title and select a local image-file to upload and click 'Save' to upload the file.\n        Note that the file-size is limited to 50k via the DataSourceField property \n        <i>maxFileSize</i> (see the mediaLibrary tab below).\n        <P>\n        \"imageFile\" fields can either display a download/save icon-pair and title, or can render\n        the image directly inline.  Use the buttons below to switch between the TileGrid and \n        ListGrid views to see each of these behaviors.  Note that both components can render\n        either UI for \"imageFile\" fields and will do so automatically, according to the value of \n        field.<i>showFileInline</i>.\n        \n        "
                    },
                    {
                        dataSource:"supplyItemHB",
                        icon:"[ISO_DOCS_SKIN]/images/iconexperience/server_from_client.png",
                        id:"batchUpload",
                        jsURL:"serverExamples/other/batchUpload/batchUploadExample.js",
                        requiresModules:"SCServer, batchUploader",
                        title:"Batch Upload",
                        descriptionHeight:"100",
                        description:"\n        This example shows the BatchUploader in action.  The BatchUploader encapsulates the \n        end-to-end process of importing flat data into a DataSource, including validation of\n        the import data, all without any client- or server-side code required.\n        <P>\n        Follow the instructions in the example.  Note that the download link is provided to \n        give you some suitable example data to try with the BatchUploader - you would not\n        normally download this, of course.\n        "
                    },
                    {
                        ref:"rssFeed",
                        requiresModules:"SCServer",
                        title:"HTTP Proxy",
                        descriptionHeight:"150",
                        description:"\n            The SmartClient Server includes an HTTP Proxy servlet which allows you to contact REST and\n            WSDL web services as though they were hosted by your web server, avoiding the \"same origin\n            policy\" restriction which normally prevents web applications from accessing remote\n            services.\n            <P>\n            The proxy is used automatically whenever you attempt to contact a URL on another host - no\n            special code is needed.  In this example, a DataSource is configured to download the\n            Slashdot RSS feed, with no server-side code or proxy configuration required.\n            <P>\n            Configuration files allow you to restrict proxying to specific\n            services you wish to allow users to contact through your application.\n        "
                    }
                ]
            },
            {
                icon:"[ISO_DOCS_SKIN]/images/silkicons/chart_bar.png",
                isOpen:false,
                requiresModules:"Drawing,PluginBridges,Analytics",
                title:"Charting",
                description:"\n    SmartClient supports advanced charting components that work in all supported browsers,\n    including mobile browsers, without requiring plugins and without writing browser-specific\n    code.\n    <P>\n    SmartClient charting components are data-aware, and allow end users to switch both the type\n    of chart and the placement of data on the fly.\n",
                children:[
                    {
                        id:"simpleChart",
                        jsURL:"charts/simpleChart.js",
                        requiresModules:"Drawing,PluginBridges,Analytics",
                        title:"Simple Chart",
                        description:"\n            <p>Charts can be created with inline Javascript data.</p>\n\n            <p>Use the \"Chart Type\" selector below to see same data rendered by multiple different chart types.\n            You can also right-click on the chart to change the way data is visualized.</p>\n        "
                    },
                    {
                        id:"multiSeriesChart",
                        jsURL:"charts/multiSeriesChart.js",
                        requiresModules:"Drawing,PluginBridges,Analytics",
                        title:"Multi-Series Chart",
                        tabs:[
                            {
                                title:"Data",
                                url:"charts/multiSeriesData.js"
                            }
                        ],
                        description:"\n            <p>Multi-series charts can be viewed with \"stacked\" data (to show totals) or \"unstacked\" to compare\n            values from each series. The \"Area\" chart type defaults to using stacked data, while the \"Line\" chart\n            type defaults to unstacked. You can use the default setting, or explicitly specify whether to stack data.</p>\n            <p>Use the \"Chart Type\" selector to see same data rendered by multiple different chart types.\n            You can also right-click on the chart to change the way data is visualized.</p>\n        "
                    },
                    {
                        id:"gridCharting",
                        jsURL:"charts/gridChart.js",
                        requiresModules:"Drawing,PluginBridges,Analytics",
                        title:"Grid Charting",
                        description:"\n        Data loaded into a ListGrid can be charted with a single API call.\n        <P>\n        Use the \"Chart Type\" selector below to see same data rendered by multiple different\n        chart types.  You can also right-click on the chart to change the way data is\n        visualized.\n        <P>\n        Edit the data in the grid to have the chart regenerated automatically.\n        "
                    },
                    {
                        dataSource:"productRevenue",
                        id:"dynamicDataCharting",
                        jsURL:"charts/dynamicData.js",
                        requiresModules:"Drawing,PluginBridges,Analytics",
                        title:"Dynamic Data",
                        descriptionHeight:"140",
                        description:"\n            <p>Charts can be created directly from a DataSource without a ListGrid.</p>\n            <p>Use the \"Time Period\" menu to change the criteria passed to the DataSource.</p>\n            <p>Use the \"Chart Type\" selector below to see same data rendered by multiple different chart types.\n            You can also right-click on the chart to change the way data is visualized.</p>\n        "
                    },
                    {
                        ref:"analytics",
                        requiresModules:"Drawing,PluginBridges,Analytics",
                        title:"CubeGrid Charting",
                        description:"\n       This example shows binding to a multi-dimensional dataset, where each cell value has a\n       series of attributes, called \"facets\", that appear as headers labelling the cell value.\n       Drag facets onto the grid to expand the cube model.<BR>\n       Right click on any cell and pick \"Chart\" to chart values by any two facets.\n       "
                    },
                    {
                        jsURL:"charts/logScaling.js",
                        requiresModules:"Drawing,PluginBridges,Analytics",
                        title:"Log Scaling",
                        tabs:[
                            {
                                title:"Data",
                                url:"charts/sp500.js"
                            }
                        ],
                        description:"\n            Charts can use logarithmic scaling, which shows equal percentage changes as the same\n            difference in height.  This is useful for data that spans a very large range.\n        "
                    },
                    {
                        jsURL:"charts/dataPoints.js",
                        requiresModules:"Drawing,PluginBridges,Analytics",
                        title:"Interactive Data Points",
                        tabs:[
                            {
                                title:"Data",
                                url:"charts/animalData.js"
                            }
                        ],
                        description:"\n            <p>The data points in a chart can be interactive. Hover over a data point to see additional information,\n            and click to edit.</p>\n        "
                    }
                ]
            },
            {
                isOpen:false,
                ref:"dragDropExamples",
                title:"Drag & Drop"
            },
            {
                icon:"[ISO_DOCS_SKIN]/images/silkicons/brick.png",
                isOpen:false,
                title:"Control",
                description:"\n    Navigation and action controls.\n",
                children:[
                    {
                        icon:"[ISO_DOCS_SKIN]/images/silkicons/brick.png",
                        isOpen:false,
                        title:"Buttons",
                        description:"\n    SmartClient buttons are visually appealing, easily skinned, and easy to use.\n    ",
                        children:[
                            {
                                id:"buttonAppearance",
                                jsURL:"actions/buttons/appearance.js",
                                showSkinSwitcher:true,
                                title:"Appearance",
                                description:"\n            Buttons come in three basic types: CSS buttons, single-image buttons, and \n            multiple-image stretch buttons.  All share a basic set of capabilities.\n        "
                            },
                            {
                                css:"actions/buttons/states.css",
                                id:"buttonStates",
                                jsURL:"actions/buttons/states.js",
                                title:"States",
                                description:"\n            Move the mouse over the buttons, and click and hold to see buttons in different\n            states.  Click \"Disable All\" to put all buttons in the disabled state.\n            \n            Edit the CSS style definitions to change the appearance of various states.\n        "
                            },
                            {
                                id:"buttonIcons",
                                jsURL:"actions/buttons/icons.js",
                                title:"Icons",
                                description:"\n            Click and hold on the \"Save\" button to see the icon change as the button goes\n            down.  Note that the binoculars icon does not change when the button goes down.\n            Click \"Disable Save\" to see the icon change to reflect disabled state.\n            \n            Button icons can be left or right oriented, and can optionally react to any the\n            state of the button.\n        "
                            },
                            {
                                id:"buttonAutoFit",
                                jsURL:"actions/buttons/autoFit.js",
                                title:"Auto Fit",
                                description:"\n            Buttons can automatically size to accommodate the title and icon, and resize\n            automatically when the title is changed, notifying components around them they have\n            changed size.\n        "
                            },
                            {
                                id:"buttonRadioToggle",
                                jsURL:"actions/buttons/radioCheckbox.js",
                                title:"Radio / Toggle Behavior",
                                description:"\n            Click on the buttons for Bold, Italic, and Underline and note that they stick in a\n            down state.  Click on the buttons for left, center and right justify and note that\n            they are mutually exclusive.\n        "
                            }
                        ]
                    },
                    {
                        icon:"[ISO_DOCS_SKIN]/images/silkicons/application_osx.png",
                        isOpen:false,
                        showSkinSwitcher:"true",
                        title:"Menus",
                        description:"\n    Dynamic, appealing menus that can bind directly to data.\n    ",
                        children:[
                            {
                                id:"fullMenu",
                                jsURL:"actions/menus/appearance.js",
                                title:"Appearance",
                                description:"\n            Click \"File\" to see a typical File menu with icons, submenus, checks,\n            separators, disabled items, and keyboard shortcut hints.  Note the beveled edge and\n            drop shadow.\n            "
                            },
                            {
                                id:"menuDynamicItems",
                                jsURL:"actions/menus/dynamicItems.js",
                                title:"Dynamic Items",
                                description:"\n            Open the \"File\" menu to see the \"New file in..\" item initially disabled.  Select a\n            project and note that the menu item has become enabled, changed title and changed\n            icon.  Pick \"Project Listing\" to show and hide the project list, and note the item\n            checks and unchecks itself.\n            "
                            },
                            {
                                ref:"fullMenu",
                                title:"Submenus",
                                description:"\n            Click \"File\" and navigate over \"Recent Documents\" or \"Export as...\" to see\n            submenus.\n            "
                            },
                            {
                                id:"menuColumns",
                                jsURL:"actions/menus/columns.js",
                                title:"Custom Columns",
                                description:"\n            Open the menu to see a standard column showing item titles, and an additional\n            column showing an option to close menu items. Clicking in the second column will\n            remove the item from the menu.\n            "
                            },
                            {
                                dataSource:"supplyCategory",
                                id:"treeBinding",
                                jsURL:"actions/menus/treeBinding.js",
                                title:"Tree Binding",
                                description:"\n            Click on \"Department\" or \"Category\" below to show hierarchical menus.  The\n            \"Category\" menu loads options dynamically from the SupplyCategory DataSource.\n            "
                            }
                        ]
                    },
                    {
                        id:"toolstrip",
                        jsURL:"actions/toolStrips.js",
                        title:"ToolStrips",
                        description:"\n        Click the icons at left to see \"radio\"-style selection.  Click the drop-down to see\n        font options.\n        ",
                        bestSkin:"Enterprise",
                        badSkins:[
                            "BlackOps",
                            "SilverWave"
                        ]
                    },
                    {
                        id:"toolstripVertical",
                        jsURL:"actions/toolStripVertical.js",
                        title:"ToolStrips (Vertical)",
                        description:"\n        Toolstrips can also be vertically aligned.\n        ",
                        bestSkin:"Enterprise",
                        badSkins:[
                            "BlackOps",
                            "SilverWave"
                        ]
                    },
                    {
                        id:"dialogs",
                        jsURL:"actions/dialogs.js",
                        showSkinSwitcher:true,
                        title:"Dialogs",
                        description:"\n        Click \"Confirm\", \"Ask\" or \"Ask For Value\" to show three of the pre-built, skinnable \n        SmartClient Dialogs for common interactions.  \n        "
                    },
                    {
                        id:"loginDialog",
                        jsURL:"actions/loginDialog.js",
                        showSkinSwitcher:true,
                        title:"Login Dialog",
                        description:"\n        Click \"Login\" to show SmartClient's built-in user login dialog.  Try entering both good\n        and bad credentials - user \"barney\", password \"rubble\" is a valid user.\n        "
                    },
                    {
                        id:"slider",
                        title:"Slider",
                        xmlURL:"actions/slider.js",
                        description:"\n        Move either Slider to update the other.  You can change the value by clicking and\n        dragging the thumb, clicking on the track, or using the keyboard (once you've focused\n        on one of the sliders)\n        "
                    },
                    {
                        id:"colorPicker",
                        jsURL:"actions/colorPicker.js",
                        showSkinSwitcher:true,
                        title:"ColorPicker",
                        description:"\n        Use the radio buttons to set which mode the ColorPicker initially appears in, and the \n        window position policy.  Click \"Pick a Color\" and select a color from either the simple\n        or complex picker - the \"Selected color\" label changes to reflect your selection.  The \n        ColorPicker also supports selecting semi-transparent colors - this is more easily seen\n        in a skin that shows a background image (eg BlackOps).\n        "
                    }
                ]
            },
            {
                icon:"[ISO_DOCS_SKIN]/images/silkicons/overlays.png",
                isOpen:false,
                title:"Basics",
                description:"\n    Basic capabilities shared by all SmartClient visual components.\n",
                children:[
                    {
                        isOpen:false,
                        title:"Components",
                        description:"\n    Basic capabilities shared by all SmartClient visual components.\n",
                        children:[
                            {
                                id:"create",
                                jsURL:"basics/create.js",
                                title:"Create",
                                description:"\n        Click the button to create new cube objects.\n        "
                            },
                            {
                                id:"autodraw",
                                jsURL:"basics/draw.js",
                                title:"Draw",
                                description:"\n        Click the button to draw another Label component. The first Label is configured\n        to draw automatically.\n        "
                            },
                            {
                                id:"showAndHide",
                                jsURL:"basics/show.js",
                                title:"Show & Hide",
                                description:"\n        Click the buttons to show or hide the message.\n        "
                            },
                            {
                                id:"move",
                                jsURL:"basics/move.js",
                                title:"Move",
                                description:"\n        Click and hold the arrow to move the van. Click on the solid circle to return to\n        the starting position.\n        "
                            },
                            {
                                id:"resize",
                                jsURL:"basics/resize.js",
                                title:"Resize",
                                description:"\n        Click the buttons to expand or collapse the text box.\n        "
                            },
                            {
                                id:"layer",
                                jsURL:"basics/layer.js",
                                title:"Layer",
                                description:"\n        Click the buttons to move the draggable box above or below the other boxes.\n        "
                            },
                            {
                                jsURL:"basics/stack.js",
                                title:"Stack",
                                description:"\n        <code>HStack</code> and <code>VStack</code> containers manage the stacked positions\n        of multiple member components.\n        "
                            },
                            {
                                jsURL:"basics/layout.js",
                                title:"Layout",
                                description:"\n        <code>HLayout</code> and <code>VLayout</code> containers manage the stacked positions and\n        sizes of multiple member components. Resize the browser window to reflow these layouts.\n        "
                            },
                            {
                                doEval:"false",
                                id:"inlineComponents",
                                iframe:"true",
                                title:"Inline components",
                                url:"inlineComponents/inlineComponents.html",
                                tabs:[
                                    {
                                        title:"cssLayout.css",
                                        url:"inlineComponents/cssLayout.css"
                                    }
                                ],
                                description:"\n        SmartClient GUI components are assembled from the same standard HTML and CSS as\n        plain old web pages. So you can add SmartClient controls above, below, inline,\n        and inside your existing web page elements.\n        "
                            }
                        ]
                    },
                    {
                        isOpen:false,
                        title:"HTML",
                        description:"\n    Mixing SmartClient components with HTML pages, chunks, and elements.\n",
                        children:[
                            {
                                ref:"inlineComponents",
                                title:"Inline Components"
                            },
                            {
                                title:"Back Button",
                                description:"\n        SmartClient supports browser history management.  Click your browser's Back button to go\n        to a previous example, and click forward to return to this example.  You can even\n        navigate off the SmartClient site and navigate back.  SmartClient's History module\n        allows you to pick which application events create history entries.\n        "
                            },
                            {
                                id:"htmlFlow",
                                jsURL:"html/htmlFlow.js",
                                title:"HTMLFlow",
                                xmlURL:"html/htmlFlow.xml",
                                description:"\n        The <code>HTMLFlow</code> component displays a chunk of standard HTML in a free-form,\n        flowable region.\n        "
                            },
                            {
                                id:"htmlPane",
                                jsURL:"html/htmlPane.js",
                                title:"HTMLPane",
                                xmlURL:"html/htmlPane.xml",
                                description:"\n        The <code>HTMLPane</code> component displays a chunk or page of standard HTML in a\n        sizeable, scrollable pane.\n        "
                            },
                            {
                                id:"label",
                                jsURL:"html/htmlLabel.js",
                                title:"Label",
                                xmlURL:"html/htmlLabel.xml",
                                description:"\n        The <code>Label</code> component adds alignment, text wrapping, and icon support for\n        small chunks of standard HTML.\n        "
                            },
                            {
                                id:"RichTextEditor",
                                jsURL:"html/richTextEditor.js",
                                requiresModules:"RichTextEditor",
                                title:"Editing HTML",
                                xmlURL:"html/richTextEditor.xml",
                                description:"RichTextEditor supports editing of HTML with a configurable set of\n       styling controls"
                            },
                            {
                                id:"img",
                                jsURL:"html/htmlImg.js",
                                title:"Img",
                                description:"\n        The <code>Img</code> component displays images in the standard web formats\n        (png, gif, jpg) and other image formats supported by the web browser.\n        "
                            },
                            {
                                id:"dynamicContents",
                                jsURL:"html/htmlDynamic1.js",
                                title:"Dynamic HTML (inline)",
                                description:"\n        Embed JavaScript expressions inside chunks of HTML to create simple dynamic elements.\n        "
                            },
                            {
                                id:"setContents",
                                jsURL:"html/htmlDynamic2.js",
                                title:"Dynamic HTML (set)",
                                description:"\n        Click the buttons to display different chunks of HTML.\n        "
                            },
                            {
                                id:"loadImages",
                                jsURL:"html/htmlLoadImg.js",
                                title:"Load images",
                                description:"\n        Click the buttons to load different images.\n        "
                            },
                            {
                                jsURL:"html/htmlLoadChunks.js",
                                title:"Load HTML chunks",
                                description:"\n        Click the buttons to load different chunks of HTML.\n        "
                            },
                            {
                                id:"loadHtmlPages",
                                jsURL:"html/htmlLoadPages.js",
                                title:"Load HTML pages",
                                description:"\n        Click the buttons to display different websites.\n        "
                            }
                        ]
                    },
                    {
                        isOpen:false,
                        title:"Interaction",
                        description:"\n    Basic interactive component capabilities.\n    <BR>\n    <BR>\n    SmartClient components provide hundreds of hooks for event handlers, including\n    all the standard mouse, keyboard, and communication events.\n",
                        children:[
                            {
                                id:"customMouseEvents",
                                jsURL:"interact/mouseEvents.js",
                                title:"Mouse events",
                                description:"\n        Mouse over the blue square to see the color respond to your position.  Click and hold\n        to see a fade.  If you have a mousewheel, roll up and down to change size.\n        SmartClient components support the standard mouse events in addition to custom events\n        like \"mouseStillDown\".\n        "
                            },
                            {
                                id:"customDrag",
                                jsURL:"interact/dragEvents.js",
                                title:"Drag events",
                                description:"\n        Click and drag the pawn over \"Show Drop Reticle\" to see a simple custom drag and drop\n        interaction.\n        "
                            },
                            {
                                css:"interact/hover.css",
                                id:"customHovers",
                                jsURL:"interact/hover.js",
                                showSkinSwitcher:true,
                                title:"Hovers / Tooltips",
                                description:"\n        Hover over the button, the image, the \"Interesting Facts\" field of the grid, and the\n        \"Severity\" form label to see various hovers.\n        "
                            },
                            {
                                id:"contextMenus",
                                jsURL:"interact/contextmenu.js",
                                showSkinSwitcher:true,
                                title:"Context menus",
                                description:"\n        Right click (or option-click on Macs) on the Yin Yang image to access a context menu.\n        You can also click on the \"Widget\" button to access the identical menu.\n        "
                            },
                            {
                                ref:"fieldEnableDisable",
                                title:"Enable / Disable"
                            },
                            {
                                id:"focus",
                                jsURL:"interact/focus.js",
                                title:"Focus & Tabbing",
                                description:"\n        Press the Tab key to cycle through through the tab order starting from the blue\n        piece.  Then drag reorder either piece, click on the leftmost piece and use Tab to\n        cycle through again. Tab order is automatically updated to reflect the visual order.\n        "
                            },
                            {
                                id:"cursors",
                                jsURL:"interact/cursor.js",
                                title:"Cursors",
                                description:"\n        Mouse over the draggable labels for a 4-way move cursor.  Move over drag resizeable\n        edges to see resize cursors.  Mouse over the \"Save\" button to see the hand cursor,\n        which is not shown if the \"Save\" button is disabled.\n        "
                            },
                            {
                                id:"keyboardEvents",
                                jsURL:"interact/keyboard.js",
                                title:"Keyboard events",
                                description:"\n        Click the \"Move Me\" label, then use the arrow keys to move it around.  Hold down keys to see the\n        component respond to key repetition. SmartClient unifies keyboard event handling across browsers.\n        "
                            },
                            {
                                id:"modality",
                                jsURL:"interact/modality.js",
                                showSkinSwitcher:true,
                                title:"Modality",
                                description:"\n        Click on \"Show Window\" to show a modal window.  Note that the \"Touch This\" button no\n        longer shows rollovers or an interactive cursor, nothing outside the window can be\n        clicked, clicks outside the window cause the window to flash, and tabbing remains in a\n        closed loop cycling through only the contents of the window.\n        "
                            }
                        ]
                    },
                    {
                        id:"printing",
                        jsURL:"basics/printing.js",
                        showSkinSwitcher:true,
                        title:"Printing",
                        tabs:[
                            {
                                canEdit:"false",
                                title:"worldDS",
                                url:"grids/ds/worldSQLDS.ds.xml"
                            }
                        ],
                        description:"\n        SmartClient provides comprehensive support for rendering your UI in a print-friendly\n        fashion.  Click the \"Print Preview\" button and note the following things:\n        <ul>\n        <li>All components have simplified appearance (eg gradients omitted) to be legible in \n        black and white\n        <li>The ListGrid had a scrollbar because it wasn't big enough to show all records, \n        but the printable view shows all data\n        <li>Buttons and other interactive controls that are not meaningful in print view are omitted\n        \n        "
                    }
                ]
            },
            {
                icon:"[ISO_DOCS_SKIN]/images/silkicons/layers.png",
                isOpen:false,
                title:"Effects",
                description:"\n    Effects for creating a polished, branded, appealing application.\n    <BR>\n    <BR>\n    SmartClient supports rich skinning and styling capabilities, drag and drop interactions,\n    and built-in animations.\n",
                children:[
                    {
                        id:"dragDropExamples",
                        isOpen:false,
                        title:"Drag & Drop",
                        description:"\n    Drag & drop services and built-in drag & drop interactions.\n",
                        children:[
                            {
                                jsURL:"dragdrop/dragListCopy.js",
                                showSkinSwitcher:true,
                                title:"Drag list (copy)",
                                tabs:[
                                    {
                                        title:"exampleData",
                                        url:"dragdrop/dragList_data.js"
                                    }
                                ],
                                description:"\n        Drag and drop to copy items from the first list to the second list.\n        You can drag over the top or bottom edge of a scrolling list to scroll\n        in that direction before dropping.\n        "
                            },
                            {
                                id:"dragListMove",
                                jsURL:"dragdrop/dragListMove.js",
                                showSkinSwitcher:true,
                                title:"Drag list (move)",
                                tabs:[
                                    {
                                        title:"exampleData",
                                        url:"dragdrop/dragList_data.js"
                                    }
                                ],
                                description:"\n        Drag and drop to move items within or between the lists.\n        You can drag over the top or bottom edge of a scrolling list to scroll\n        in that direction before dropping.\n        "
                            },
                            {
                                id:"dragListSelect",
                                jsURL:"dragdrop/dragListSelect.js",
                                showSkinSwitcher:true,
                                title:"Drag list (select)",
                                tabs:[
                                    {
                                        title:"exampleData",
                                        url:"dragdrop/dragList_data.js"
                                    }
                                ],
                                description:"\n        Drag to select items in the first list. The second list will\n        mirror your selection.\n        "
                            },
                            {
                                id:"dragTree",
                                jsURL:"dragdrop/dragTreeMove.js",
                                showSkinSwitcher:true,
                                title:"Drag tree (move)",
                                tabs:[
                                    {
                                        title:"exampleData",
                                        url:"dragdrop/dragTree_data.js"
                                    }
                                ],
                                description:"\n        Drag and drop to move parts and folders within and between the trees.\n        You can open a closed folder by pausing over it during a drag interaction\n        (aka \"spring loaded folders\").\n        "
                            },
                            {
                                id:"dragTiles",
                                jsURL:"dragdrop/dragTilesMove.js",
                                showSkinSwitcher:true,
                                title:"Drag tiles (move)",
                                tabs:[
                                    {
                                        title:"animalData",
                                        url:"grids/data/animalData2.js"
                                    }
                                ],
                                description:"\n        Drag and drop animals from the ListGrid on the left to the TileGrid on the right. \n        Animals can also be dragged from the TileGrid back to the ListGrid.\n        "
                            },
                            {
                                id:"dataDraggingCopy",
                                isOpen:false,
                                title:"Data Binding",
                                description:"\n        Databound components have built-in dragging behaviors that operate on persistent\n        datasets.\n    ",
                                children:[
                                    {
                                        dataSource:"employees",
                                        id:"treeReparent",
                                        jsURL:"databind/drag/treeReparent.js",
                                        title:"Tree Reparent",
                                        description:"\n            Dragging employees between managers in this tree automatically saves the new\n            relationship to a DataSource, without writing any code.  Make changes, then \n            reload the page: your changes persist.\n            "
                                    },
                                    {
                                        dataSource:"supplyCategory",
                                        id:"treeRecategorize",
                                        jsURL:"databind/drag/treeRecategorize.js",
                                        title:"Recategorize (Tree)",
                                        tabs:[
                                            {
                                                title:"supplyItem",
                                                url:"supplyItem.ds.xml"
                                            }
                                        ],
                                        description:"\n            Dragging items from the list and dropping them on categories in the tree automatically\n            re-categorizes the item, without any code needed.  Make changes, then \n            reload the page: your changes persist.  This behavior is (optionally) automatic where\n            SmartClient can establish a relationship via foreign key between the DataSources\n            two components are bound to.\n            "
                                    },
                                    {
                                        dataSource:"supplyItem",
                                        id:"listRecategorize",
                                        jsURL:"databind/drag/listRecategorize.js",
                                        title:"Recategorize (List)",
                                        description:"\n            The two lists are showing items in different categories.  Drag items from one list to\n            another to automatically recategorize the items without writing any code.  Make\n            changes, then reload the page; your changes persist.\n            "
                                    },
                                    {
                                        id:"recategorizeTiles",
                                        jsURL:"dragdrop/recategorizeTile.js",
                                        showSkinSwitcher:true,
                                        title:"Recategorize (Tile)",
                                        tabs:[
                                            {
                                                canEdit:"false",
                                                title:"animalsDS",
                                                url:"grids/ds/animalsSQLDS.ds.xml"
                                            }
                                        ],
                                        description:"\n            Drag and drop animals between the grids in either direction, and the status of the dropped tile will change to \n            match the filtered status of the TileGrid in which it was dropped. Select different values\n            in the drop down lists above each TileGrid to change the animals that will appear in each grid.\n            "
                                    },
                                    {
                                        dataSource:"employees",
                                        id:"databoundDragCopy",
                                        jsURL:"databind/drag/listCopy.js",
                                        showSkinSwitcher:true,
                                        title:"Copy",
                                        tabs:[
                                            {
                                                title:"teamMembers",
                                                url:"teamMembers.ds.xml"
                                            }
                                        ],
                                        description:"\n            Drag employee records into the Project Team Members list.  SmartClient recognizes that the \n            two dataSources are linked by a foreign key relationship, and automatically uses that \n            relationship to populate values in the record that is added when you drop. SmartClient\n            also populates fields based on current criteria and maps explicit titleFields as \n            necessary.<p>\n            In this example, note that SmartClient is automatically populating all three\n            of the fields in the teamMembers dataSource, even though none of those fields is present \n            in the employees dataSource we are dragging from.  Change the \"Team for Project\" select \n            box, then try dragging employees across; note that the Project Code column is being \n            correctly populated for the dropped records.\n            "
                                    }
                                ]
                            },
                            {
                                id:"dragMenu",
                                jsURL:"dragdrop/dragFromMenu.js",
                                showSkinSwitcher:true,
                                title:"Drag from Menu",
                                tabs:[
                                    {
                                        title:"exampleData",
                                        url:"dragdrop/dragList_data.js"
                                    }
                                ],
                                description:"\n        Open the parts menu and drag parts from the menu onto the grid.\n        Menus support all the drag and drop behaviors supported by grids.\n        "
                            },
                            {
                                id:"dragMove",
                                jsURL:"dragdrop/dragMove.js",
                                title:"Drag move",
                                description:"\n        Drag and drop to move pieces between the boxes. The green box sets a thicker green\n        \"drop line\" indicator to match its border. The blue box shows a \"drag placeholder\"\n        outline at the original location of the dragged object while dragging.\n        "
                            },
                            {
                                jsURL:"dragdrop/dragReorder.js",
                                title:"Drag reorder",
                                description:"\n        Drag and drop to rearrange the order of the pieces.\n        "
                            },
                            {
                                jsURL:"dragdrop/dragTypes.js",
                                title:"Drag types",
                                description:"\n        Drag and drop to move pieces between the three boxes.\n        The gray box accepts any piece.\n        The blue and green boxes accept pieces of the same color only.\n        "
                            },
                            {
                                id:"dragCreate",
                                jsURL:"dragdrop/dragCreate.js",
                                title:"Drag create",
                                description:"\n        Drag the large cubes into the boxes to create new small cubes.\n        The blue, yellow, and green boxes accept cubes with the same color only.\n        The gray box accepts any color.\n        Right-click on the small cubes to remove them from the boxes.\n        "
                            },
                            {
                                id:"dragEffects",
                                jsURL:"dragdrop/dragEffects.js",
                                title:"Drag effects",
                                description:"\n        Click and drag to move the labels.\n        "
                            },
                            {
                                jsURL:"dragdrop/dragReposition.js",
                                title:"Drag reposition",
                                visibility:"sdk",
                                description:"\n        Click and drag to move the piece.\n        "
                            },
                            {
                                id:"dragResize",
                                jsURL:"dragdrop/dragResize.js",
                                title:"Drag resize",
                                description:"\n        Click and drag on the edges of the labels to resize.\n        "
                            },
                            {
                                id:"dragTracker",
                                jsURL:"dragdrop/dragTracker.js",
                                title:"Drag tracker",
                                description:"\n        Drag and drop the pieces onto the box.\n        "
                            },
                            {
                                id:"dragPan",
                                jsURL:"dragdrop/dragPan.js",
                                title:"Drag pan",
                                description:"\n        Click and drag to pan the image inside its frame.\n        "
                            }
                        ]
                    },
                    {
                        isOpen:false,
                        title:"Animation",
                        description:"\n    Animation services and built-in animation effects.\n",
                        children:[
                            {
                                id:"animateTree",
                                jsURL:"animate/animateTree.js",
                                showSkinSwitcher:true,
                                title:"Tree Folders",
                                description:"\n        Click the open/close icon for any folder.\n        ",
                                tabs:[
                                    {
                                        title:"exampleData",
                                        url:"animate/animateTreeData.js"
                                    }
                                ]
                            },
                            {
                                id:"windowMinimize",
                                jsURL:"animate/animateMinimize.js",
                                showSkinSwitcher:true,
                                title:"Window Minimize",
                                description:"\n        Click on the minimize button (round button in header with flat line).\n        "
                            },
                            {
                                id:"animateSections",
                                jsURL:"animate/animateSections.xml",
                                showSkinSwitcher:true,
                                title:"Section Reveal",
                                description:"\n        Click on any section header to expand/collapse sections.\n        "
                            },
                            {
                                id:"animateLayout",
                                jsURL:"animate/animateLayout.js",
                                title:"Layout Add & Remove",
                                description:"\n        Click on the buttons to hide and show the green star.\n        "
                            },
                            {
                                id:"animateMove",
                                jsURL:"animate/animateMove.js",
                                title:"Fly Onscreen",
                                description:"\n        Click the buttons to move the Label into view or out of view.\n        "
                            },
                            {
                                id:"animateResize",
                                jsURL:"animate/animateResize.js",
                                title:"Resize",
                                description:"\n        Click the buttons to expand or collapse the text box.\n        "
                            },
                            {
                                id:"animateWipe",
                                jsURL:"animate/animateWipe.js",
                                title:"Wipe Show & Hide",
                                description:"\n        Click the buttons to show or hide the Label with a \"wipe\" effect.\n        "
                            },
                            {
                                jsURL:"animate/animateSlide.js",
                                title:"Slide Show & Hide",
                                description:"\n        Click the buttons to show or hide the Label with a \"slide\" effect.\n        "
                            },
                            {
                                id:"animateFade",
                                jsURL:"animate/animateFade.js",
                                title:"Fade Show & Hide",
                                description:"\n        Click the buttons to fade the image.\n        "
                            },
                            {
                                id:"animateZoom",
                                jsURL:"animate/animateZoom.js",
                                title:"Zoom & Shrink",
                                description:"\n        Click the buttons to zoom or shrink the image.\n        "
                            },
                            {
                                jsURL:"animate/animateSeqSimple.js",
                                title:"Sequence (simple)",
                                description:"\n        Click the buttons for a 2-stage expand or collapse effect.\n        "
                            },
                            {
                                jsURL:"animate/animateSeqComplex.js",
                                title:"Sequence (complex)",
                                description:"\n        Click to select and zoom each piece.\n        "
                            },
                            {
                                id:"customAnimation",
                                jsURL:"animate/animateCustom.js",
                                title:"Custom Animation",
                                description:"\n        Click on the globe for a custom \"orbit\" animation.\n        "
                            },
                            {
                                ref:"tilingFilter",
                                title:"Tile Filter & Sort"
                            },
                            {
                                fullScreen:"true",
                                id:"portalAnimation",
                                jsURL:"animate/portal.js",
                                needServer:"true",
                                screenshot:"animate/portal.png",
                                screenshotHeight:"337",
                                screenshotWidth:"480",
                                showSkinSwitcher:true,
                                title:"Simple Portal",
                                description:"Animations built into SmartClient layouts can be used to create a drag and drop portal\n      experience.  Click on the portlet list to the left to create portlets and see them\n      animate into place.  Drag portlets around to new locations and they animate into place.\n        "
                            }
                        ]
                    },
                    {
                        isOpen:false,
                        title:"Look & Feel",
                        description:"\n    Apply rich visual styles to SmartClient components.\n",
                        children:[
                            {
                                id:"edges",
                                jsURL:"lookfeel/edges.js",
                                title:"Edges",
                                description:"\n        Drag the text boxes. These boxes show customized frame and glow effects\n        using edge images.\n        "
                            },
                            {
                                id:"corners",
                                jsURL:"lookfeel/corners.js",
                                title:"Corners",
                                description:"\n        Drag the text boxes. These boxes show customized rounded-corner effects\n        using edge images.        \n        "
                            },
                            {
                                id:"shadows",
                                jsURL:"lookfeel/shadows.js",
                                title:"Shadows",
                                description:"\n        Drag the slider to change the shadow depth for the text box.\n        ",
                                badSkins:"BlackOps",
                                bestSkin:"TreeFrog"
                            },
                            {
                                jsURL:"lookfeel/bgColor.js",
                                title:"Background color",
                                visibility:"sdk",
                                description:"\n        Click on the color picker to select a background color for the box.\n        "
                            },
                            {
                                jsURL:"lookfeel/bgImage.js",
                                title:"Background texture",
                                visibility:"sdk",
                                description:"\n        Click any button to change the background texture for the box.\n        "
                            },
                            {
                                id:"translucency",
                                jsURL:"lookfeel/opacity.js",
                                title:"Translucency",
                                description:"\n        Drag the slider to change opacity.\n        "
                            },
                            {
                                jsURL:"lookfeel/boxAttrs.js",
                                title:"Box attributes",
                                visibility:"sdk",
                                description:"\n        Drag the sliders to change the CSS box attributes.\n        <P>\n        Containers in SmartClient automatically react to changes in CSS styling on contained elements\n        \n        "
                            },
                            {
                                id:"styles",
                                jsURL:"lookfeel/styles.js",
                                title:"CSS styles",
                                tabs:[
                                    {
                                        title:"CSS",
                                        url:"lookfeel/styles.css"
                                    }
                                ],
                                description:"\n        Click the radio buttons to apply different CSS styles to the text. Click the CSS tab for\n        CSS class definitions.<BR>\n        This container auto-sizes to the styled text.\n        ",
                                badSkins:"BlackOps",
                                bestSkin:"TreeFrog"
                            },
                            {
                                css:"lookfeel/consistentSizing.css",
                                id:"consistentSizing",
                                jsURL:"lookfeel/consistentSizing.js",
                                title:"Consistent sizing",
                                description:"\n      Drag the slider to resize all three text boxes. The box sizes match despite different\n      edge styling specified in CSS, enabling CSS-based skinning without affecting\n      application layout.\n    "
                            },
                            {
                                id:"gridCells",
                                jsURL:"grids/formatting/cellStyles.js",
                                showSkinSwitcher:true,
                                title:"Grid cells",
                                tabs:[
                                    {
                                        title:"CSS",
                                        url:"grids/formatting/cellStyles.css"
                                    },
                                    {
                                        title:"countryData",
                                        url:"grids/data/countryData.js"
                                    }
                                ],
                                description:"\n        Mouse over the rows and click-drag to select rows, to see the effects of different\n        base styles on these two grids.\n        "
                            }
                        ]
                    }
                ]
            },
            {
                icon:"[ISO_DOCS_SKIN]/images/cube_blue.png",
                isOpen:false,
                requiresModules:"Drawing",
                title:"Drawing",
                visibility:"none",
                description:"\n    SmartClient leverages the native browser drawing capabilities to provide a consistent cross\n    browser drawing interface via the optional <b>Drawing</b> module.\n    <P>\n    This is a <span style=\"color:red\">preview</span> of unreleased SmartClient functionality.  \n    <a href=\"http://www.smartclient.com/company/contact.jsp\">Contact\n    Isomorphic</a> to get\n    early access to this technology.\n",
                children:[
                    {
                        id:"ShapeGallery",
                        requiresModules:"Drawing",
                        title:"Shape Gallery",
                        url:"drawing/shapeGallery.js",
                        description:"Below is a sampling of the shapes available in the SmartClient\n        drawing package.\n        "
                    },
                    {
                        id:"Rotation",
                        requiresModules:"Drawing",
                        title:"Rotation",
                        url:"drawing/rotation.js",
                        description:"Sample of Rotation feature of Drawing module.\n        "
                    },
                    {
                        id:"ZoomAndPan",
                        requiresModules:"Drawing",
                        title:"Zoom and Pan",
                        url:"drawing/zoomAndPan.js",
                        description:"Sample of zooming and panning features of Drawing module. Use\n        slider for zoom and drag image by mouse.\n        "
                    },
                    {
                        id:"LinesAndArrowheads",
                        requiresModules:"Drawing",
                        title:"Lines and Arrowheads",
                        url:"drawing/linesAndArrowheads.js",
                        description:"Sample of using lines and curves with selects for line width, style, \n        and arrowhead style, generated at random coordinates.\n        "
                    },
                    {
                        isOpen:true,
                        requiresModules:"Drawing",
                        title:"Gradients",
                        description:"\n            Different types of gradients can be used with shapes.\n        ",
                        children:[
                            {
                                id:"SimpleGradient",
                                requiresModules:"Drawing",
                                title:"Simple",
                                url:"drawing/gradients/simpleGradient.js",
                                description:"Sample of using simple type of gradient.\n                "
                            },
                            {
                                id:"LinearGradient",
                                requiresModules:"Drawing",
                                title:"Linear",
                                url:"drawing/gradients/linearGradient.js",
                                description:"Sample of using linear type of gradient.\n                "
                            },
                            {
                                id:"RadialGradient",
                                requiresModules:"Drawing",
                                title:"Radial",
                                url:"drawing/gradients/radialGradient.js",
                                description:"Sample of using radial type of gradient.\n                "
                            }
                        ]
                    }
                ]
            },
            {
                isOpen:false,
                requiresModules:"Tools",
                showSkinSwitcher:"true",
                title:"Portals & Tools Framework",
                visibility:"none",
                description:" \n        <p>You can use the Portal &amp; Tools Framework to allow users to construct their own\n        customized user interface, and persist and recreate those customizations. It is\n        the underpinning of the SmartClient Visual Builder tool.</p>\n\n        <p>Palettes present the components available for use, and specify their\n        type and default properties. Users can drag components from the Palettes, or\n        double-click on them.</p>\n\n        <p>Edit Contexts organize and present the components that users have\n        chosen and customized. Several strategies are available for persisting and recreating\n        Edit Contexts.</p>\n    ",
                children:[
                    {
                        isOpen:false,
                        requiresModules:"Tools",
                        title:"Palettes",
                        description:"\n                Palettes organize and present the components available for the user\n                to select and customize. Users choose items from palettes by clicking and/or dragging\n                (depending on the palette type).\n            ",
                        children:[
                            {
                                jsURL:"portal/palettes/treePalette.js",
                                requiresModules:"Tools",
                                title:"Tree Palette",
                                description:"\n                      Tree Palettes organize available components in a tree structure.\n                      The user can double-click or drag to create a component.\n                   "
                            },
                            {
                                jsURL:"portal/palettes/listPalette.js",
                                requiresModules:"Tools",
                                title:"List Palette",
                                description:"\n                      List Palettes organize available components in a list grid structure.\n                      The user can double-click or drag to create a component.\n                   "
                            },
                            {
                                id:"tilePalette",
                                jsURL:"portal/palettes/tilePalette.js",
                                requiresModules:"Tools",
                                title:"Tile Palette",
                                description:"\n                      Tile Palettes organize available components in a tile grid structure.\n                      The user can double-click or drag to create a component.\n                   "
                            },
                            {
                                jsURL:"portal/palettes/menuPalette.js",
                                requiresModules:"Tools",
                                title:"Menu Palette",
                                description:"\n                      Menu Palettes present available components as a menu.\n                      The user can click or drag to create a component.\n                   "
                            }
                        ]
                    },
                    {
                        isOpen:false,
                        requiresModules:"Tools",
                        title:"Edit Contexts",
                        description:"\n                Edit Contexts are containers for the components that a user has selected.\n            ",
                        children:[
                            {
                                ref:"tilePalette",
                                requiresModules:"Tools",
                                title:"Edit Pane",
                                description:"\n                        An Edit Pane is a container that allows drag and drop instantiation\n                        of visual components from a Palette, and direct manipulation of the \n                        position and size of those components.\n                    "
                            },
                            {
                                isOpen:"false",
                                ref:"automaticPersistence",
                                requiresModules:"Tools",
                                title:"Coordinate Persistence",
                                description:"\n                        <p>By default, an Edit Context will automatically persist the position and size of\n                        components.</p>\n                        <p>Try dragging some components from the Tile Palette to the Edit Pane, and then move\n                        and resize them. Clicking on the \"Destroy and Recreate\" button will recreate the\n                        Edit Pane from saved state. Note how the position and size of components has been\n                        preserved.</p>\n                    "
                            }
                        ]
                    },
                    {
                        isOpen:false,
                        requiresModules:"Tools",
                        title:"Persistence",
                        description:"\n                <p>You can persist and recreate Edit Contexts using several persistence strategies.</p>\n                <ul>\n                    <li>Use a variable to store state, and recreate or duplicate an Edit Context from that variable.</li>\n                    <li>Persist state to a DataSource.</li>\n                    <li>Use Offline storage to persist state.</li>\n                </ul>\n            ",
                        children:[
                            {
                                id:"automaticPersistence",
                                isOpen:"false",
                                jsURL:"portal/persistence/automaticPersistence.js",
                                requiresModules:"Tools",
                                title:"Automatic",
                                tabs:[
                                    {
                                        title:"Tile Palette",
                                        url:"portal/palettes/tilePalette.js"
                                    }
                                ],
                                description:"\n                        <p>The state of an Edit Context can be saved to a variable. You can then use the variable to\n                        duplicate or recreate the Edit Context.</p>\n                        <p>Try dragging some components from the Tile Palette to the Edit Pane. Click the\n                        \"Destroy and Recreate\" button to save the Edit Pane's state, destroy it, and then\n                        recreate it. The process is animated, to illustrate the process\n                        (which would otherwise occur instantly).</p>\n                    "
                            },
                            {
                                dataSource:"editNodes",
                                isOpen:"false",
                                jsURL:"portal/persistence/datasource.js",
                                requiresModules:"Tools",
                                title:"DataSource",
                                tabs:[
                                    {
                                        title:"Tile Palette",
                                        url:"portal/palettes/tilePalette.js"
                                    }
                                ],
                                description:"\n                        The state of an Edit Context can be connected to a DataSource. Try dragging some\n                        components from the Tile Palette to the Edit Pane. Click on \"Save\" to save the state\n                        of the Edit Pane to a DataSource. Make some changes to the Edit Pane, and then click\n                        \"Restore\". Note how the state of the Edit Pane is restored to its saved state.\n                    "
                            },
                            {
                                isOpen:"false",
                                jsURL:"portal/persistence/offline.js",
                                requiresModules:"Tools",
                                title:"Offline",
                                tabs:[
                                    {
                                        title:"Tile Palette",
                                        url:"portal/palettes/tilePalette.js"
                                    }
                                ],
                                description:"\n                        <p>The state of an Edit Context can be connected to Offline storage. Try dragging some\n                        components from the Tile Palette to the Edit Pane. Click on \"Save\" to save the state\n                        of the Edit Pane to a DataSource. Make some changes to the Edit Pane, and then click\n                        \"Restore\". Note how the state of the Edit Pane is restored to its saved state.</p>\n                        <p>Try reloading the page to see saved state automatically restored. (Note that the\n                        example does not automatically save state).</p>\n                    "
                            }
                        ]
                    }
                ]
            },
            {
                icon:"[ISO_DOCS_SKIN]/images/silkicons/application_osx.png",
                isOpen:false,
                title:"Applications",
                description:"\n    Demos of complete applications based on SmartClient.\n",
                children:[
                    {
                        ref:"showcaseApp",
                        title:"Office Supply Catalog"
                    },
                    {
                        dataSource:"productRevenue",
                        fullScreen:"true",
                        id:"analytics",
                        jsURL:"advanced/cubegrid/databound_cubegrid.js",
                        requiresModules:"PluginBridges,Drawing,Analytics",
                        screenshot:"advanced/cubegrid/databound_cubegrid.png",
                        screenshotHeight:"327",
                        screenshotWidth:"468",
                        showSkinSwitcher:true,
                        title:"Interactive Analytics",
                        tabs:[
                            {
                                loadAtEnd:"true",
                                title:"facet controls",
                                url:"advanced/cubegrid/facet_controls.js"
                            }
                        ],
                        description:"\n        This example shows binding to a multi-dimensional dataset, where each\n        cell value has a series of attributes, called \"facets\", that appear as headers\n        labelling data values.  Facets can be added to the view, exposing more detail, by\n        dragging the menu buttons onto the grid, or into the \"Row Facets\" and \"Column Facets\"\n        listings.\n        <P>\n        Facets can be removed from the view by using the menus to set a facet to a fixed\n        value.  For example, use the \"Time\" menu to show just data from 2002: \"Time -> Fix Time\n        Value -> All Years -> 2002\".\n        <P>\n        Click the turndown controls on facet values to expand tree facets.  Note that data\n        loads as it is revealed by expanding and collapsing tree facets, by adding facets, or\n        by scrolling in either direction.  This allows users to navigate extremely large data sets.\n        <P>\n        Right click on any data value or facet value to generate a chart showing how that\n        particular value varies along up to two facets.  For example, click on any data\n        value for \"Office Paper Products\" and choose \"Chart -> Sales by Time and Region\" to see\n        how this category of products is selling in different regions and time periods.  You\n        can also switch to different chart types (eg Radar) on the fly.\n        <i><b>(Note: Chart support requires the optional Drawing Module. \n        If not installed, the Analytics Module, including the CubeGrid and\n        the remainder of this sample, will continue to function normally.)</b></i>\n        <P>\n        Because the CubeGrid uses a DataSource to loaded data, it can be connected to any kind\n        of server or data provider.  This sample loads data from a SQL database.\n    "
                    },
                    {
                        fullScreen:"true",
                        id:"portal",
                        jsURL:"portal/smartclientPortal.js",
                        requiresModules:"PluginBridges,Analytics",
                        screenshot:"portal/salesPortal.png",
                        screenshotHeight:"327",
                        screenshotWidth:"468",
                        showSkinSwitcher:true,
                        title:"Portal (Preview)",
                        description:"\n       Going beyond basic portal layout, SmartClient's portalling framework allows users to\n       build a customized layout from a palette of available components, and automatically\n       persist both the layout and customizations made to components.\n       <P>\n       This is a <span style=\"color:red\">preview</span> of unreleased SmartClient functionality.  \n       <a href=\"http://www.smartclient.com/company/contact.jsp\">Contact\n       Isomorphic</a> to get\n       early access to this technology.\n    "
                    }
                ]
            },
            {
                icon:"[ISO_DOCS_SKIN]/images/silkicons/arrow_branch.png",
                isOpen:false,
                title:"Extending",
                description:"\n    Examples of extending SmartClient functionality\n",
                children:[
                    {
                        css:"extending/portlet.css",
                        jsURL:"extending/componentReuse.js",
                        title:"Component Reuse",
                        description:"\n        The portlets below are a custom component created with less than one page of code\n        (see the \"JS\" tab).  The portlets support drag repositioning, drag resizing, a close\n        button, can contain any HTML content, and are skinnable.\n    "
                    },
                    {
                        dataSource:"supplyItem",
                        id:"patternReuse",
                        jsURL:"extending/patternReuse.js",
                        title:"Pattern Reuse",
                        tabs:[
                            {
                                canEdit:"false",
                                title:"countryDS",
                                url:"grids/ds/countrySQLDS.ds.xml"
                            }
                        ],
                        description:"\n        Click to select a DataSource, click on records to edit them in the adjacent form, then\n        click the \"Save\" button to save changes.<br>\n        This custom component combines a databound form and grid into a reusable application\n        pattern of side-by-side editing, that can be used with any DataSource.\n    "
                    },
                    {
                        ref:"schemaChaining",
                        title:"Schema Reuse"
                    },
                    {
                        ref:"customSimpleType",
                        title:"Type Reuse"
                    },
                    {
                        id:"changeLocales",
                        jsURL:"extending/changeLocales.js",
                        title:"Localization",
                        tabs:[
                            {
                                canEdit:"false",
                                title:"worldDS",
                                url:"grids/ds/worldSQLDS.ds.xml"
                            }
                        ],
                        description:"\n         Select a different language from the Locale drop down list, and click the Change\n        Locale button to change the default language. The following UI elements will change \n        the language in which they are displayed: the month chooser of the date picker, the operator\n        chooser of the custom filter, and the header context menus of the ListGrid. \n        SmartClient supports localization via configurable property files. See the \n        documentation under 'Internationalization and Localization' for more information about using existing locale files or\n        creating custom locales.\n        "
                    },
                    {
                        id:"dateFormat",
                        jsURL:"extending/dateFormat_local.js",
                        showSource:false,
                        title:"Standard Date Format",
                        tabs:[
                            {
                                doEval:"false",
                                title:"JS",
                                url:"extending/dateFormat.js"
                            },
                            {
                                title:"employees",
                                url:"extending/employees.js"
                            }
                        ],
                        description:"\n         Dates displayed in the <b>\"Hire Date\"</b> field in this example are formatted using the\n        standard <code>\"toJapanShortDate\"</code> formatter. Click on a record to edit it in the\n        Form, or double click to edit inline in the ListGrid.\n        "
                    },
                    {
                        id:"customDateFormat",
                        jsURL:"extending/customDateFormat_local.js",
                        showSource:false,
                        title:"Custom Date Format",
                        tabs:[
                            {
                                doEval:"false",
                                title:"JS",
                                url:"extending/customDateFormat.js"
                            },
                            {
                                title:"employees",
                                url:"extending/employees.js"
                            }
                        ],
                        description:"\n         Dates displayed in the <b>\"Hire Date\"</b> field in this example are formatted\n        using a custom formatting function. Click on a record to edit it in the\n        Form, or double click to edit inline in the ListGrid.\n        "
                    },
                    {
                        dataSource:"supplyItemCurrency",
                        id:"customDataType",
                        jsURL:"extending/customDataType.js",
                        title:"Custom Data Type",
                        description:"\n         This example demonstrates using a custom SimpleType to provide standard\n        type based validation, formatting and parsing logic across components. The \"unitCost\"\n        field is of type <code>\"currency\"</code> which is explicitly defined in the source\n        as a SimpleType inheriting from float. Both the (editable) ListGrid and the DynamicForm\n        respect the settings defined in this type definition.\n        "
                    },
                    {
                        dataSource:"supplyItem",
                        jsURL:"extending/customizeFields.js",
                        title:"Customize Fields",
                        description:"\n        Edit field definitions in the grid below to override how this form binds to the \n        <code>supplyItem</code> DataSource.  This is a simplified example of how\n        you can deliver an application that can be customized with organization-specific fields\n        and rules.  Dynamic schema binding makes building WYSIWYG editing interfaces very\n        simple.  \n        "
                    },
                    {
                        ref:"customDrag",
                        title:"Drag and Drop"
                    },
                    {
                        ref:"customHovers",
                        title:"Hovers"
                    },
                    {
                        ref:"customMouseEvents",
                        title:"Mouse Handling"
                    },
                    {
                        ref:"customAnimation",
                        title:"Animation"
                    },
                    {
                        ref:"portalAnimation",
                        title:"Simple Portal"
                    }
                ]
            }
        ]
    }
})

