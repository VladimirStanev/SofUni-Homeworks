window.addEventListener('load', solve);

function solve() {
    let totalLikes = 0;
    const inputDOMSelectors = {
      genre: document.querySelector('#genre'),
      name: document.querySelector('#name') ,
      author: document.querySelector('#author'),
      date: document.querySelector('#date'),
    };

    const otherDomSelectors = {
      addBtn: document.getElementById('add-btn'),
      allHitsContainer: document.querySelector('.all-hits-container'),
      savedContainer: document.querySelector('.saved-container'),
      totalLikesContainer: document.querySelector('.likes > p'),
    };

    otherDomSelectors.addBtn.addEventListener('click', addEvent);
    otherDomSelectors.addBtn.addEventListener('mouseenter', () => {
        console.log('Mouse Enter');
    })

    function addEvent(event) {
        event.preventDefault();
        let allInputsAreNonEmpty = Object.values(inputDOMSelectors)
            .every((input) => input.value !== '');

        if(!allInputsAreNonEmpty) {
            console.log("HAS INVALID");
            return;
        }

        const { genre, name, author, date } = inputDOMSelectors;
        const songContainer = createElement("div", otherDomSelectors.allHitsContainer, null, ['hits-info']);
        createElement('img', songContainer, null, null, null, { src: './static/img/img.png' });
        createElement('h2', songContainer, `Genre: ${genre.value}`);
        createElement('h2', songContainer, `Name: ${name.value}`);
        createElement('h2', songContainer, `Author: ${author.value}`);
        createElement('h3', songContainer, `Date: ${date.value}`);
        const saveBtn = createElement('button', songContainer, 'Save song', ['save-btn']);
        const likeBtn = createElement('button', songContainer, 'Like song', ['like-btn']);
        const deleteBtn = createElement('button', songContainer, 'Delete', ['delete-btn']);

        saveBtn.addEventListener('click', saveSongTask);
        likeBtn.addEventListener('click', likeSongTask);
        deleteBtn.addEventListener('click', deleteSongTask);


        document.querySelector('form').reset();
    }

    function saveSongTask() {
        const songRef = this.parentNode;
        const saveBtn = songRef.querySelector('.save-btn');
        const likeBtn = songRef.querySelector('.like-btn');
        
        otherDomSelectors.savedContainer.appendChild(songRef);
        saveBtn.remove();
        likeBtn.remove();
    }

    function likeSongTask() {
        this.setAttribute('disabled', true);
        totalLikes ++;
        otherDomSelectors.totalLikesContainer.textContent = `Total Likes: ${totalLikes}`;
    }

    function deleteSongTask() {
        this.parentNode.remove();
    }

    function createElement(type, parentNode, content, classes, id, attributes, useInnerHtml) {
        const htmlElement = document.createElement(type);
      
        if (content && useInnerHtml) {
          htmlElement.innerHTML = content;
        } else {
          if (content && type !== 'input') {
            htmlElement.textContent = content;
          }
      
          if (content && type === 'input') {
            htmlElement.value = content;
          }
        }
      
        if (classes && classes.length > 0) {
          htmlElement.classList.add(...classes);
        }
      
        if (id) {
          htmlElement.id = id;
        }
      
        if (attributes) {
          for(const key in attributes) {
            htmlElement[key] = attributes[key];
          }
        }
        
        if (parentNode) {
          parentNode.appendChild(htmlElement);
        }
        
        return htmlElement;
      }
}