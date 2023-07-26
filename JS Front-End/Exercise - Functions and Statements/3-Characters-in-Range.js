function charactersInBetween(firstChar, secondChar) {
    const getAsciiCode = (char) => char.charCodeAt(0);

    let firstCharAscii = getAsciiCode(firstChar);
    let secondCharAscii = getAsciiCode(secondChar);

    let minAscii = Math.min(firstCharAscii, secondCharAscii);
    let maxAscii = Math.max(firstCharAscii, secondCharAscii);
    let chars = [];

    for (let asciiCode = minAscii + 1; asciiCode < maxAscii; asciiCode++) {
        chars.push(String.fromCharCode(asciiCode));
    }

    return chars.join(' ');
}

console.log(
    charactersInBetween(
        'a',
        'd'
    )
)