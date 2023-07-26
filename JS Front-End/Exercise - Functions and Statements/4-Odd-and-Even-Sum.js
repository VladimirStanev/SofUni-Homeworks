function recievingNumber(number) {
    const digits = number.toString();

    let evenDigits = [];
    let oddDigits = [];

    let oddSum = 0;
    let evenSum = 0;

    for (let i = 0; i < digits.length; i++) {
        let currentDigit = Number(digits[i]);

        if(currentDigit % 2 == 0) {
            evenDigits.push(currentDigit);
        } else {
            oddDigits.push(currentDigit);
        }
    }

    for (let o = 0; o < oddDigits.length; o++) {
        let oddDigit = oddDigits[o];

        oddSum += oddDigit;
    }

    for (let e = 0; e < evenDigits.length; e++) {
        let evenDigit = evenDigits[e];

        evenSum += evenDigit;
    }

    return `Odd sum = ${oddSum}, Even sum = ${evenSum}`;
}

console.log(
    recievingNumber(1000435)
);