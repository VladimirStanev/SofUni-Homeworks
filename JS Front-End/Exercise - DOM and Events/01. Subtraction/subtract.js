function subtract() {
    let num1 = Number(document.getElementById('firstNumber').value);
    let num2 = Number(document.getElementById('secondNumber').value);

    const substraction = num1 - num2;
    const result = document.querySelector("#result");
    result.textContent = substraction;
}