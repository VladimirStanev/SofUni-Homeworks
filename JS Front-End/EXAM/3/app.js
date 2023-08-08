function attachEvents() {
    const BASE_URL = 'http://localhost:3030/jsonstore/tasks/';
    const inputDOMSelectors = {
        name: document.getElementById('name'),
        days: document.getElementById('num-days'),
        date: document.getElementById('from-date'),
    };

    const otherDOMSelectors = {
        loadVacationsBtn: document.getElementById('load-vacations'),
        confirmedVacationsContainer: document.getElementById('list'),
        addBtn: document.getElementById('add-vacation'),
        editBtn: document.getElementById('edit-vacation'),
        form: document.querySelector('form'),
    };

    let currentVacations = [];
    let vacationToEdit = {};

    otherDOMSelectors.loadVacationsBtn.addEventListener('click', loadVacationsTask);
    otherDOMSelectors.addBtn.addEventListener('click', addVacationTask);
    otherDOMSelectors.editBtn.addEventListener('click', editVacationTask);

    function loadVacationsTask(event) {
        if(event) {
            event.preventDefault();
        }

        otherDOMSelectors.confirmedVacationsContainer.innerHTML = '';
        fetch(BASE_URL)
            .then((res) => res.json())
            .then((allConfirmedVacs) => {
                currentVacations = Object.values(allConfirmedVacs);
                for(const { name, days, date, _id } of currentVacations) {
                    const div = createElement('div', otherDOMSelectors.confirmedVacationsContainer, null, ['container']);
                    div.id = _id;
                    createElement('h2', div, name);
                    createElement('h3', div, date);
                    createElement('h3', div, days);
                    const changeBtn = createElement('button', div, 'Change', ['change-btn']);
                    const doneBtn = createElement('button', div, 'Done', ['done-btn']);

                    changeBtn.addEventListener('click', changeBtnTask);
                    doneBtn.addEventListener('click', doneTask)
                }
            })
    }

    function doneTask() {
        const id = this.parentNode.id;
        const httpHeaders = {
            method: 'DELETE',
        };

        fetch(`${BASE_URL}${id}`, httpHeaders)
            .then(() => loadAllProductsTask())
            .catch((err) => {
                console.error(err);
            })
    }

    function editVacationTask(event) {
        event.preventDefault();

        let allInputsAreNonEmpty = Object.values(inputDOMSelectors)
            .every((input) => input.value !== '');

        if(!allInputsAreNonEmpty) {
            console.log("HAS INVALID");
            return;
        }

        event.preventDefault();
        const { name, days, date } = inputDOMSelectors;
        const payload = JSON.stringify({
            name: name.value,
            days: days.value,
            date: date.value
        });
        const httpHeaders = {
            method: 'PUT',
            body: payload
        }

        fetch(`${BASE_URL}${vacationToEdit._id}`, httpHeaders)
            .then(() => {
                loadVacationsTask();
                otherDOMSelectors.editBtn.setAttribute('disabled', true);
                otherDOMSelectors.addBtn.removeAttribute('disabled');
                otherDOMSelectors.form.reset();
            })
            .catch((err) => {
                console.error(err);
            })
    }

    function changeBtnTask() {
        const id = this.parentNode.id;
        vacationToEdit = currentVacations.find((p) => p._id === id);

        for (const key in inputDOMSelectors) {
            inputDOMSelectors[key].value = vacationToEdit[key];
        }

        otherDOMSelectors.addBtn.setAttribute('disabled', true);
        otherDOMSelectors.editBtn.removeAttribute('disabled');
    }

    function addVacationTask(event) {
        event.preventDefault();

        let allInputsAreNonEmpty = Object.values(inputDOMSelectors)
            .every((input) => input.value !== '');

        if(!allInputsAreNonEmpty) {
            console.log("HAS INVALID");
            return;
        }

        const { name, days, date } = inputDOMSelectors;
        const payload = JSON.stringify({
            name: name.value,
            days: days.value,
            date: date.value
        });
        const httpHeaders = {
            method: 'POST',
            body: payload
        };

        fetch(BASE_URL, httpHeaders)
            .then(() => {
                loadVacationsTask();
                otherDOMSelectors.form.reset();
            })
            .catch((err) => {
                console.error(err);
            })         
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

attachEvents();