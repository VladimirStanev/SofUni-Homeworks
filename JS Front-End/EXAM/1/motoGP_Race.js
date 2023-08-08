function solve(input) {
    const n = Number(input.shift());
    const riders = {};
    for (let index = 0; index < n; index++) {
        const [ rider, fuelCapacity, position ] = input.shift().split('|');

        if(!riders.hasOwnProperty(rider)) {
            riders[rider] = [];
        }

        riders[rider] = { fuelCapacity, position };
    }

    let commandFunction = {
        'StopForFuel': stopForFuelTask,
        'Overtaking': overtakingTask,
        'EngineFail': engineFailTask,
    };

    for (const inputLine of input) {
        if(inputLine === 'Finish') {
            break;
        }

        let fullCommandInfo = inputLine.split(' - ');
        let commandType = fullCommandInfo[0];

        commandFunction[commandType](...fullCommandInfo.slice(1));
    }

    for (const rider in riders) {
        const { position } = riders[rider];
        console.log(`${rider}
        Final position: ${position}`);
    }

    function stopForFuelTask(rider, minimumFuel, changedPosition) {
        const { fuelCapacity } = riders[rider];
        if(Number(fuelCapacity) >= minimumFuel) {
            console.log(`${rider} does not need to stop for fuel!`);
        } else {
            riders[rider].position = changedPosition;
            console.log(`${rider} stopped to refuel but lost his position, now he is ${changedPosition}.`);
        }
    }

    function overtakingTask(overtakingRider, rider) {
        const positionOf1 = riders[overtakingRider].position;
        const positionOf2 = riders[rider].position;

        riders[overtakingRider].position = positionOf2;
        riders[rider].position = positionOf1;

        console.log(`${overtakingRider} overtook ${rider}!`);
    }

    function engineFailTask(rider, lapsLeft) {
        delete riders[rider];
        console.log(`${rider} is out of the race because of a technical issue, ${lapsLeft} laps before the finish.`)
    }
}

solve(
[
    "4",
    "Valentino Rossi|100|1",
    "Marc Marquez|90|3",
    "Jorge Lorenzo|80|4",
    "Johann Zarco|80|2",
    "StopForFuel - Johann Zarco - 90 - 5",
    "Overtaking - Marc Marquez - Jorge Lorenzo",
    "EngineFail - Marc Marquez - 10",
    "Finish"
]
)

