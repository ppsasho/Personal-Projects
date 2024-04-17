let personBtn = document.getElementById("person");
let starShipBtn = document.getElementById("star-ship");
let moonBtn = document.getElementById("moon");
let nextBtn = document.getElementById("next");
let prevBtn = document.getElementById("previous");
let table = document.getElementsByTagName("table")[0];
let spinner = document.getElementById("spinner");
let pageDisplay = document.getElementById("page");
let search = document.getElementById('search-bar');
let searchBtn = document.getElementById('searchBtn');
let searchField = document.getElementById('searchField');

searchField.value = '';
search.style.visibility = 'hidden';
pageDisplay.style.visibility = "hidden";
spinner.style.visibility = "hidden";
nextBtn.style.visibility = "hidden";
prevBtn.style.visibility = "hidden";

let allPeople = []; 
let planets = [];
let people = [];
let starships = [];
let currentObject = null;
let currentRender = null;
let sortSwitch = null;

 let checkCurrentRender = () => {
  switch(currentRender) {
    case 'person':
      return renderPerson(createPerson(currentObject));
    case 'starship':
      return renderStarShip(createStarShip(currentObject));
    case 'planets':
      return renderPlanet(createPlanet(currentObject));
  }
 }

let getDataFromApi = (url, key) => {
  spinner.style.visibility = "visible";
  fetch(url)
    .then((response) => response.json())
    .then((response) => {
      spinner.style.visibility = "hidden";
      currentObject = JSON.parse(JSON.stringify(response));
      console.log(currentObject);

      switch (key) {
        case "person":
          currentRender = "person";
          return renderPerson(createPerson(currentObject));

        case "starship":
          currentRender = "starship";
          return renderStarShip(createStarShip(currentObject));

        case "planets":
          currentRender = "planets";
          return renderPlanet(createPlanet(currentObject));

        case "next":
        case "prev":
          return checkCurrentRender();

        default:
          console.log("Invalid key");
      }
    })
    .catch((error) => {
      console.log('Request limit was probably hit ]:', error);
    });
}

(async function() {
  for(let i = 1; i < 10; i++) {
    console.log(`Fetch page #${i}`);
    await fetch(`https://swapi.dev/api/people/?page=${i}`)
    .then((response) => response.json())
    .then((data) => allPeople.push(data.results))
  }
  allPeople = allPeople.flat();
  console.log(allPeople);
})();

let drawFirstRow = (attributes, tableRow) => {
  for (let i = 0; i < attributes.length; i++) {
    let attrCell = tableRow.insertCell(i);
    attrCell.innerText = attributes[i];
    attrCell.id = `header${i}`;
    attrCell.addEventListener("click", () => {
      sortTable(array, attrCell.innerText.toLowerCase());
    });
  }
}

let drawOtherRows = (array) => {
  for (let i = 0; i < array.length; i++) {
    let tableRow = table.insertRow(i + 1);
    let object = array[i];
    let values = Object.values(object);

    for (let b = 0; b < values.length; b++) {
      let tableCells = tableRow.insertCell(b);
      tableCells.innerText += `${values[b]}`;
    }
  }
}

let hideNextPrevAndClean = () => {
  table.innerHTML = "";
  nextBtn.style.visibility = "hidden";
  prevBtn.style.visibility = "hidden";
}
let checkNextPrev = () => {
  if (currentObject.previous) {
    prevBtn.style.visibility = "visible";
  }
  if (currentObject.next) {
    nextBtn.style.visibility = "visible";
  }
  page.style.visibility = "visible";
  search.style.visibility = 'visible';
}

let renderPerson = (array) => {
  getPageNumber();
  hideNextPrevAndClean();

  let attributes = ["Name", "Height", "Mass", "Gender", "Birth_Year", "Films"];

  let tableRow = table.insertRow(0);
  drawFirstRow(attributes, tableRow);
  drawOtherRows(array);

  checkNextPrev();
};

let renderStarShip = (array) => {
  getPageNumber();
  hideNextPrevAndClean();

  let attributes = [
    "Name",
    "Model",
    "Manufacturer",
    "Cost",
    "Capacity",
    "Class",
  ];
  let tableRow = table.insertRow(0);
  drawFirstRow(attributes, tableRow);
  drawOtherRows(array);

  checkNextPrev();
};

let renderPlanet = (array) => {
  getPageNumber();
  hideNextPrevAndClean();

  let attributes = [
    "Name",
    "Climate",
    "Diameter",
    "Population",
    "Gravity",
    "Terrain",
  ];

  let tableRow = table.insertRow(0);
  drawFirstRow(attributes, tableRow);
  drawOtherRows(array);

  checkNextPrev();
};

class Person {
  constructor({name, height, mass, gender, birth_year, films} = object) {
    this.name = name,
    this.height = height,
    this.mass = mass,
    this.gender = gender,
    this. birthYear = birth_year,
    this.appearances = films.length
  }
}

class StarShip {
  constructor({name, model, manufacturer, cost_in_credits, passengers, starship_class} = object) {
    this.name = name,
    this.model = model,
    this.manufacturer = manufacturer,
    this.cost = cost_in_credits,
    this.capacity = passengers,
    this.class = starship_class
  }
}

class Planet {
  constructor({name, climate, diameter, population, gravity, terrain} = object) {
    this.name = name,
    this. climate = climate,
    this.diameter = diameter,
    this.population = population,
    this.gravity = gravity,
    this.terrain = terrain
  }
}


let createStarShip = (object) => {
  starships = [];
  for (let i = 0; i < object.results.length; i++) {
    let starShip = new StarShip(object.results[i]);
    starships.push(starShip);
  }
  return starships;
};

let createPerson = (object) => {
  people = [];
  for (let i = 0; i < object.results.length; i++) {
    let person = new Person(object.results[i]);
    people.push(person);
  }
  return people;
};

let createPlanet = (object) => {
  planets = [];
  for (let i = 0; i < object.results.length; i++) {
    let planet = new Planet(object.results[i]);
    planets.push(planet);
  }
  return planets;
};


let sendPersonRequest = () =>
  getDataFromApi("https://swapi.dev/api/people/?page=1", "person");

let sendStarShipRequest = () =>
  getDataFromApi("https://swapi.dev/api/starships/?page=1", "starship");

let sendPlanetsRequest = () =>
  getDataFromApi("https://swapi.dev/api/planets/", "planets");

let sendNextRequest = () => {
  if (currentObject) {
    console.log("Sending next request...");
    getDataFromApi(currentObject.next, "next");
  }
};

let sendPrevRequest = () => {
  if (currentObject) {
    console.log("Sending previous request...");
    getDataFromApi(currentObject.previous, "prev");
  }
};

let nextPage = () => {
  let pageInfo = currentObject.next;
  let page = pageInfo.indexOf("=");
  let pageNumber = pageInfo[page + 1];
  pageDisplay.innerText = `Page ${--pageNumber}`;
  console.log(`Current page: ${pageNumber}`);
};
let previousPage = () => {
  let pageInfo = currentObject.previous;
  let page = pageInfo.indexOf("=");
  let pageNumber = pageInfo[page + 1];
  pageDisplay.innerText = `Page ${++pageNumber}`;
  console.log(`Current page: ${pageNumber}`);
};

let getPageNumber = () => {
  if (!currentObject.next) {
    previousPage();
    return;
  }
  if (!currentObject.previous) {
    nextPage();
    return;
  }
  nextPage();
};

let sortRender = array => {
  switch (currentRender) {
    case "person":
      renderPerson(array);
      break;

    case "starship":
      renderStarShip(array);
      break;

    case "planets":
      renderPlanet(array);
      break;
  }
}

let sortTable = (input, attr) => {
  let array = [...input];
  console.clear();
  console.log(`Attribute clicked: ${attr}`);
  console.log(`Sorted array:`);
  console.log(array);

  if (sortSwitch) {
    array.sort((attr1, attr2) => attr2.name.length - attr1.name.length);
    sortRender(array);
    sortSwitch = false;
    return;
  }
  array.sort((attr1, attr2) => attr1.name.length - attr2.name.length);
  sortRender(array);
  sortSwitch = true;
};

let searchAllPeople = (request, array)=> {
  let copyArray = [...array];
  if (searchField.value) {
    let personArray = [];
    console.log(`The requested name is: ${request}`);
    let foundPerson = copyArray.filter(person => person.name === `${request}`);
    if(foundPerson) {
      console.log('The requested person was found!');
      console.log(foundPerson);
      let newPerson = new Person(foundPerson[0]);
      personArray.push(newPerson);
      console.log(`Extracted the required info from ${request}`);
      renderPerson(personArray);
      return;
    }
  }
  alert('Nothing to search!');
}

let sendSearchRequest = () => {
  console.clear();
  console.log('Sending search request...');
  searchAllPeople(searchField.value, allPeople);
  searchField.value = '';
}


searchBtn.addEventListener('click', sendSearchRequest)
personBtn.addEventListener("click", sendPersonRequest);
starShipBtn.addEventListener("click", sendStarShipRequest);
moonBtn.addEventListener("click", sendPlanetsRequest);

nextBtn.addEventListener("click", () => {
  console.clear();
  console.log("Next button clicked");
  sendNextRequest();
});

prevBtn.addEventListener("click", () => {
  console.clear();
  console.log("Previous button clicked");
  sendPrevRequest();
});
