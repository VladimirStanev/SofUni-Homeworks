function sumThenSubstract(x, y, z) {
    function sum(x, y) {
        return x + y;
    }

    const sumOfNumbers = sum(x,y);

    function substract(sumOfNumbers, z) {
        return sumOfNumbers - z;
    }

    const finalresult = substract(sumOfNumbers, z);
    return finalresult;
}

console.log(
    sumThenSubstract(23, 6, 10)
);