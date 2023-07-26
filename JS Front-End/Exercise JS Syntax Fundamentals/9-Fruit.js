function solve(type, weight, priceperkilo) {
    const weightintoKG = weight / 1000;
    const priceForAmount = weightintoKG * priceperkilo;

    console.log(`I need $${priceForAmount.toFixed(2)} to buy ${weightintoKG.toFixed(2)} kilograms ${type}.`)
}

solve('orange', 2500, 1.80);