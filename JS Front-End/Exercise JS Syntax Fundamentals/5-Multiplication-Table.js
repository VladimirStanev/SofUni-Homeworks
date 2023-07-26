function solve(number) {
    for(let index = 1; index <= 10; index++) {
        let currentSum = index * number;
        console.log(`${number} X ${index} = ${currentSum}`)
    }
}

solve(5);