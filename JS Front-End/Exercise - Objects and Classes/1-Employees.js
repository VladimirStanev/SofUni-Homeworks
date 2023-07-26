function employeesPersonalNumbers(input) {
    let employees = {};
    for (const employee of input) {
        employees[employee] = employee.length;
    }

    for (const key in employees) {
        console.log(`Name: ${key} -- Personal Number: ${employees[key]}`)
    }
}

employeesPersonalNumbers(
    [
        'Silas Butler',
        'Adnaan Buckley',
        'Juan Peterson',
        'Brendan Villarreal'
    ]
);