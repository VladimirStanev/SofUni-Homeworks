function solve(input) {
    const groceriesInput = input.shift();
    const groceriesList = groceriesInput.split('!');


    for (const inputLine of input) {
        if(inputLine === 'Go Shopping!') {
            break;
        }

        const inputLineElements = inputLine.split(' ');
        const commandType = inputLineElements.shift();
        const item = inputLineElements.join(" ");
        
        if(commandType === 'Urgent') {
            const itemInArray = groceriesList.filter(element => element === item);

            if(itemInArray.length === 0) {
                groceriesList.unshift(item);
            }
        } else if(commandType === 'Unnecessary') {
            const itemInArray = groceriesList.filter(element => element === item);

            if(itemInArray.length > 0) {
                const index = groceriesList.indexOf(item);
                groceriesList.splice(index, 1);
            }
        } else if(commandType === 'Correct') {
            const oldAndNew = item.split(" ");
            const oldItem = oldAndNew[0];

            const itemInArray = groceriesList.filter(element => element === oldItem);

            if(itemInArray.length > 0) {
                const newItem = oldAndNew[1];
                const oldPlace = groceriesList.indexOf(oldItem);
                groceriesList[oldPlace] = newItem;
            }
        } else if(commandType === 'Rearrange') {
            const itemInArray = groceriesList.filter(element => element === item);

            if(itemInArray.length > 0) {
                const index = groceriesList.indexOf(item);
                groceriesList.splice(index, 1);
                groceriesList.push(item);
            }
        }
    }
    console.log(groceriesList.join(", "));
}

solve(
    ["Tomatoes!Potatoes!Bread",
    "Unnecessary Milk",
    "Urgent Tomatoes",
    "Go Shopping!"]
)

solve(
    ["Milk!Pepper!Salt!Water!Banana",
    "Urgent Salt",
    "Unnecessary Grapes",
    "Correct Pepper Onion",
    "Rearrange Grapes",
    "Correct Tomatoes Potatoes",
    "Go Shopping!"]
)

