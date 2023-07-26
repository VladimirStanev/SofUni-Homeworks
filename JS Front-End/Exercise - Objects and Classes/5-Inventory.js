function heroes(input) {
    class Hero {
        constructor(heroName, heroLevel, heroItems) {
            this.heroName = heroName;
            this.heroLevel = heroLevel;
            this.heroItems = heroItems;
        }
    }

    let allHeroes = [];

    for (const line of input) {
        let splitInformation = line.split(' / ');

        let heroName = splitInformation[0];
        let heroLevel = splitInformation[1];
        let heroItems = splitInformation[2];

        let hero = new Hero(heroName, heroLevel, heroItems);

        if(!allHeroes.hasOwnProperty(hero)) {
            allHeroes.push(hero);
        }
    }

    let orderedListOfHeroes = allHeroes.sort((heroA, heroB) => heroA.heroLevel - heroB.heroLevel);

    for (const heroes of orderedListOfHeroes) {
        console.log(`Hero: ${heroes.heroName}`);
        console.log(`level => ${heroes.heroLevel}`);
        console.log(`items => ${heroes.heroItems}`);
    }
}

heroes(
    [
        'Isacc / 25 / Apple, GravityGun',
        'Derek / 12 / BarrelVest, DestructionSword',
        'Hes / 1 / Desolator, Sentinel, Antara'
    ]
)

heroes(
    [
        'Batman / 2 / Banana, Gun',
        'Superman / 18 / Sword',
        'Poppy / 28 / Sentinel, Antara'
    ]
)