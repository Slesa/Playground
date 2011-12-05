isc.DynamicForm.create({
    width: 200,
    fields: [
        { name: "shoeSize", title: "Shoe Size", editorType: "spinner", defaultValue: 8.5,
          min: 6, max: 13, step: 0.5, width: 70}
    ]
});
