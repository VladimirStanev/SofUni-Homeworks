window.addEventListener("load", solve);

function solve() {

  const studentState = {
    name: null,
    university: null,
    score: null,
  };

  const inputDOMSelectors = {
    name: document.getElementById('student'),
    university: document.getElementById('university'),
    score: document.getElementById('score'),
  };

  const otherDomSelectors = {
    nextBtn: document.getElementById('next-btn'),
    previewContainer: document.getElementById('preview-list'),
    candidatesContainer: document.getElementById('candidates-list'),
  };

  otherDomSelectors.nextBtn.addEventListener('click', addToPreviewTask);

  function addToPreviewTask(event) {
    event.preventDefault();
    let allInputsAreNonEmpty = Object.values(inputDOMSelectors)
      .every((input) => input.value !== '');

    if(!allInputsAreNonEmpty) {
      return;
    }

    for (const key in inputDOMSelectors) {
      studentState[key] = inputDOMSelectors[key].value;
    }

    otherDomSelectors.previewContainer.innerHTML = '';
    const { name, university, score} = inputDOMSelectors;
    const applicationLi = createElement("li", otherDomSelectors.previewContainer, null, ['application']);
    const currentArticle = createElement("article", applicationLi);
    createElement('h4', currentArticle, name.value);
    createElement('p', currentArticle, `University: ${university.value}`);
    createElement('p', currentArticle, `Score: ${score.value}`);

    const editBtn = createElement('button', applicationLi, 'edit', ['action-btn', 'edit']);
    const applyBtn = createElement('button', applicationLi, 'apply', ['action-btn', 'apply']);

    editBtn.addEventListener('click', editInfoTask);
    applyBtn.addEventListener('click', applyForScolarshipTask);

    otherDomSelectors.nextBtn.setAttribute('disabled', true);
    document.querySelector('form').reset();
  }

  function editInfoTask() {
    for(const key in inputDOMSelectors) {
      inputDOMSelectors[key].value = studentState[key];
    }

    otherDomSelectors.previewContainer.innerHTML = '';
    otherDomSelectors.nextBtn.removeAttribute('disabled');
  }

  function applyForScolarshipTask() {
    const studentRef = this.parentNode;
    const editBtn = studentRef.querySelector('.edit');
    const applyBtn = studentRef.querySelector('.apply');

    otherDomSelectors.candidatesContainer.appendChild(studentRef);
    editBtn.remove();
    applyBtn.remove();

    otherDomSelectors.nextBtn.removeAttribute('disabled');
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
  