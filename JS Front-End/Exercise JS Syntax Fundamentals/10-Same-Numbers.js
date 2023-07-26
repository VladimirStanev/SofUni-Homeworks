function solve(numbers) {
    const digits = numbers.toString();
    let sum = 0;
    let resultat = 'true';

    if(digits.length <= 1) {
        console.log(true)
        console.log(digits[0])
    } else {
        sum += Number(digits[0]);
        for (let index = 1; index < digits.length; index++) {
            if(digits[0] != digits[index]) {
                resultat = 'false';
            }
            sum += Number(digits[index]);
        }
    }

    console.log(resultat);
    console.log(sum);
}

solve(2222222);
solve(1234);