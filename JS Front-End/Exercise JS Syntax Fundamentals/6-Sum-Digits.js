function solve(number) {
    const digitsToString = number.toString();
    let sum = 0;
    
    for(let index = 0; index < digitsToString.length; index++) {
        let currentDigit = Number(digitsToString[index]);
        sum += currentDigit;
    }

    console.log(sum);
}

solve(245678)