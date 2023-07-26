function create(words) {
   const boxes = document.querySelector('#content');

   words.forEach((word) => {
      const div = document.createElement("div");
      const p = document.createElement("p");
      p.textContent = word;
      p.style.display = 'none';

      div.appendChild(p);
      div.addEventListener("click", (e) => {
         p.style.display = "block";
      })

      boxes.appendChild(div);
   });
}