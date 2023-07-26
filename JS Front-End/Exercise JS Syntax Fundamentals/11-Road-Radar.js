function solve(speed, place) {
    let motorwaySpeed = 130;
    let interstateSpeed = 90;
    let citySpeed = 50;
    let residentialSpeed = 20;

    if(place == 'motorway') {
        if(speed <= motorwaySpeed) {
            console.log(`Driving ${speed} km/h in a ${motorwaySpeed} zone`);
        } else {
            if(speed - motorwaySpeed <= 20) {
                console.log(`The speed is ${speed - motorwaySpeed} km/h faster than the allowed speed of ${motorwaySpeed} - speeding`);
            } else if (speed - motorwaySpeed > 20 && speed - motorwaySpeed <= 40) {
                console.log(`The speed is ${speed - motorwaySpeed} km/h faster than the allowed speed of ${motorwaySpeed} - excessive speeding`);
            } else {
                console.log(`The speed is ${speed - motorwaySpeed} km/h faster than the allowed speed of ${motorwaySpeed} - reckless driving`);
            }
        }
    } else if (place == 'interstate') {
        if(speed <= interstateSpeed) {
            console.log(`Driving ${speed} km/h in a ${interstateSpeed} zone`);
        } else {
            if(speed - interstateSpeed <= 20) {
                console.log(`The speed is ${speed - interstateSpeed} km/h faster than the allowed speed of ${interstateSpeed} - speeding`);
            } else if (speed - interstateSpeed > 20 && speed - interstateSpeed <= 40) {
                console.log(`The speed is ${speed - interstateSpeed} km/h faster than the allowed speed of ${interstateSpeed} - excessive speeding`);
            } else {
                console.log(`The speed is ${speed - interstateSpeed} km/h faster than the allowed speed of ${interstateSpeed} - reckless driving`)
            }
        }
    } else if (place == 'city') {
        if(speed <= citySpeed) {
            console.log(`Driving ${speed} km/h in a ${citySpeed} zone`);
        } else {
            if(speed - citySpeed <= 20) {
                console.log(`The speed is ${speed - citySpeed} km/h faster than the allowed speed of ${citySpeed} - speeding`);
            } else if (speed - citySpeed > 20 && speed - citySpeed <= 40) {
                console.log(`The speed is ${speed - citySpeed} km/h faster than the allowed speed of ${citySpeed} - excessive speeding`);
            } else {
                console.log(`The speed is ${speed - citySpeed} km/h faster than the allowed speed of ${citySpeed} - reckless driving`);
            }
        }
    } else if (place == 'residential') {
        if(speed <= residentialSpeed) {
            console.log(`Driving ${speed} km/h in a ${residentialSpeed} zone`);
        } else {
            if(speed - residentialSpeed <= 20) {
                console.log(`The speed is ${speed - residentialSpeed} km/h faster than the allowed speed of ${residentialSpeed} - speeding`);
            } else if (speed - residentialSpeed > 20 && speed - residentialSpeed <= 40) {
                console.log(`The speed is ${speed - residentialSpeed} km/h faster than the allowed speed of ${residentialSpeed} - excessive speeding`);
            } else {
                console.log(`The speed is ${speed - residentialSpeed} km/h faster than the allowed speed of ${residentialSpeed} - reckless driving`);
            }
        }
    }
}

solve(40, 'city');
solve(21, 'residential' );
solve(120, 'interstate');