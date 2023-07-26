function storePrevision(currentStock, orderedProducts) {
    let everyProduct = [...currentStock, ...orderedProducts];
    let store = {};
    
    for (let index = 0; index < everyProduct.length; index++) {
        let prop = everyProduct[index];
        if(index % 2 === 0) {
            if(!store.hasOwnProperty(prop)) {
                store[prop] = 0;
            }
        } else {
            price = Number(prop);
            let nameOfProduct = everyProduct[index - 1];
            store[nameOfProduct] += price;
        }
    }

    for (const key in store) {
        console.log(`${key} -> ${store[key]}`)
    }
}


storePrevision(
    [
        'Chips', '5', 'CocaCola', '9', 'Bananas', 
        '14', 'Pasta', '4', 'Beer', '2'
        ],
        [
        'Flour', '44', 'Oil', '12', 'Pasta', '7', 
        'Tomatoes', '70', 'Bananas', '30'
        ]
)