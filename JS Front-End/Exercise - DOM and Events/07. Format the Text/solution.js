function solve() {
  const textArea = document.querySelector("#input");
  const output = document.querySelector("#output");

  let sentences = textArea.value.split('.');
  sentences.pop();

  while (sentences.length > 0) {
    let paragraphSentes = sentences.splice(0, 3)
      .map((p) => p.trimStart());
    const newPharagraph = document.createElement("p");
    newPharagraph.textContent = paragraphSentes.join(".") + ".";
    output.appendChild(newPharagraph);
  }
}