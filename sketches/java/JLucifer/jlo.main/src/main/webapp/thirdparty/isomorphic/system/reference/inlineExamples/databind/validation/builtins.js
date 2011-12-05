isc.DynamicForm.create({
    ID: "boundForm",
    dataSource: "builtins"
});

isc.Button.create({
    left: 220,
    title: "Validate",
    click: "boundForm.validate()"
});
