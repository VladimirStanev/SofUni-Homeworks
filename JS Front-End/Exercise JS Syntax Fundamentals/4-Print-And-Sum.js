function solve(startingNum, endingNum) {
    const numbers = [];
    let sum = 0;

    for (let index = startingNum; index <= endingNum; index++) {
        numbers.push(index);
        sum += index;
    }

    console.log(numbers.join(" "));
    console.log(`Sum: ${sum}`);
}

solve(5, 10);