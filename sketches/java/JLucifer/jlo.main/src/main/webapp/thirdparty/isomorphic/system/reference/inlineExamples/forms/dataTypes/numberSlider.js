isc.DynamicForm.create({
    width: 200,
    fields: [
        { name: "rating", title: "Rating", editorType: "slider",
          minValue: 1, maxValue: 5, numValues: 5, width: 200, titleOrientation: "top"}
    ],
    values: { rating: 4 }
});
