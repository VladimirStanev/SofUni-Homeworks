function toggle() {
   const content = document.querySelector("#extra");

    if(content.style.display == "block") {
        content.style.display = "none";
        const button = document.querySelector("#button");
        button.documentElement.textContent = "More";
    } else {
        content.style.display = "block";
        const button = document.querySelector("#button");
        button.documentElement.textContent = "Less";
    }
}

//const content = document.querySelector("#extra");
   //const button = document.querySelector("span.button");

   //content.style.display = content.style.display === "block" ? "none" : "block";
   //button.textContent = button.textContent === "More" ? "Less" : "More";