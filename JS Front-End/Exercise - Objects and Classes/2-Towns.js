function town(input) {
    for (const line of input) {
        let info = line.split(' | ');
        let townName = info[0];
        let latitudeOne = Number(info[1]).toFixed(2);
        let latitudeTwo = Number(info[2]).toFixed(2);

        console.log(`{ town: '${townName}', latitude: '${latitudeOne}', longitude: '${latitudeTwo}' }`);
    }
}

town(
    ['Sofia | 42.696552 | 23.32601',
    'Beijing | 39.913818 | 116.363625']
)

town(
    ['Plovdiv | 136.45 | 812.575']
)