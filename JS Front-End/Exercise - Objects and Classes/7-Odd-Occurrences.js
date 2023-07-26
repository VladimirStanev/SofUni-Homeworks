function solve(input) {
    let wordsToCHeck = input.split(' ');
    let wordsWithCount = {};

    for(const word of wordsToCHeck) {
        let wordToCheck = word.toLowerCase();
        if(wordsWithCount.hasOwnProperty(wordToCheck)) {
            wordsWithCount[wordToCheck] += 1;
        } else {
            wordsWithCount[wordToCheck] = 1;
        }
    }

    let validElements = [];
    let sortedWordsWithCount = Object.entries(wordsWithCount).sort((wordA, wordB) => {
        let [ _nameA, countA ] = wordA;
        let [ _nameB, countB ] = wordB;

        return countB - countA;
    })

    for(const element of sortedWordsWithCount) {
        if(element[1] % 2 != 0 || element[1] == 1) {
            validElements.push(element[0]);
        }
    }

    console.log(validElements.join(' '));
}

solve(
    'Java C# Php PHP Java PhP 3 C# 3 1 5 C#'
);
solve(
    'Cake IS SWEET is Soft CAKE sweet Food'
);