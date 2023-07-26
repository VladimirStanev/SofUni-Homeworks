function wordTracker(input) {
    let wordsWIthCount = {};
    let containerForComparing = [];
    let wordsTosearch = input[0].split(' ');

    for(const word of wordsTosearch) {
        if(!wordsWIthCount.hasOwnProperty(word)) {
            wordsWIthCount[word] = 0;
        }
        if(!containerForComparing.hasOwnProperty(word)) {
            containerForComparing.push(word);
        }
    }

    for (let i = 1; i < input.length; i++) {
        let currentWord = input[i];
        for (const word of containerForComparing) {
            if(currentWord === word) {
                wordsWIthCount[word] += 1;
            }
        }
    }

    let sortedWordsWithCount = Object.entries(wordsWIthCount).sort((wordA, wordB) => {
        let [ _nameA, countA ] = wordA;
        let [ _nameB, countB ] = wordB;

        return countB - countA;
    })

    for (const word of sortedWordsWithCount) {
        console.log(`${word[0]} - ${word[1]}`);
    }
}

wordTracker(
    [
        'this sentence',
        'In', 'this', 'sentence', 'you', 'have',
        'to', 'count', 'the', 'occurrences', 'of',
        'the', 'words', 'this', 'and', 'sentence',
        'because', 'this', 'is', 'your', 'task'
    ]
)

wordTracker(
    [
        'is the',
        'first', 'sentence', 'Here', 'is', 
        'another', 'the', 'And', 'finally', 'the', 
        'the', 'sentence'
    ]    
)