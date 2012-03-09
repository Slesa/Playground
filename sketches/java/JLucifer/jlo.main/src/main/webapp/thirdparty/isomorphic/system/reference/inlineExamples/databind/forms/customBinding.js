isc.DynamicForm.create({
    ID: "boundForm",
    dataSource: "users",
    useAllDataSourceFields: true,
    fields: [
        {type:"header", defaultValue:"Registration Form"},
        {name: "password"},
        {name: "password2", title: "Password Again", type: "password", required: true, 
         length: 20, validators: [{
             type: "matchesField",
             otherField: "password",
             errorMessage: "Passwords do not match"
         }]
        },
        {name: "acceptTerms", title: "I accept the terms of use.", type: "checkbox", required: true, width: "150"},
        {name: "validateBtn", title: "Validate", type: "button", click: "form.validate()"}
    ],
    values : {
        firstName: "Bob",
        email: "bob@.com",
        password: "sekrit",
        password2: "fatfinger"
    }
});
