function parkingLot(input) {
    let carNumbers = new Set();
    
    for(const line of input) {
        let info = line.split(', ');
        let command = info[0];
        let carNum = info[1];

        if(command == 'IN') {
            carNumbers.add(carNum);
        }
        else if(command == 'OUT') {
            carNumbers.delete(carNum);
        }
    }

    if(carNumbers.size === 0) {
        console.log(`Parking Lot is Empty`);
    } else {
        let sortedNumbers = [...carNumbers.keys()]
            .sort((carNumA, carNumB) => carNumA.localeCompare(carNumB));
        sortedNumbers
            .forEach((carNum) => {
              console.log(carNum);
            })
    }
}

parkingLot(
    ['IN, CA2844AA',
    'IN, CA1234TA',
    'OUT, CA2844AA',
    'IN, CA9999TT',
    'IN, CA2866HI',
    'OUT, CA1234TA',
    'IN, CA2844AA',
    'OUT, CA2866HI',
    'IN, CA9876HH',
    'IN, CA2822UU']
)