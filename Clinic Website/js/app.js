const btnDoctor = document.querySelector(".doctor");
const btnPatient = document.querySelector(".patient");
const btnClinic = document.querySelector(".clinic");
const divContent = document.querySelector(".my-content");

const fr = document.createDocumentFragment();

document.body.onload = loadDoctors;

// view doctors
btnDoctor.addEventListener("click", loadDoctors);

// view patients
btnPatient.addEventListener("click", loadPatients);

// view clinics
btnClinic.addEventListener("click", loadClinics);

function loadDoctors() {
  divContent.textContent = "";

  const divCardGrid = document.createElement("div");
  divCardGrid.classList.add("card-grid");

  for (let i = 0; i < 50; i++) {
    // create doctor card
    const divCard = document.createElement("div");
    divCard.classList.add(
      "card",
      "m-auto",
      "animate__animated",
      "animate__zoomIn"
    );
    divCard.style = "max-width: 540px;";

    const divRow = document.createElement("div");
    divRow.classList.add("row", "no-gutters");

    const divImgCol = document.createElement("div");
    divImgCol.classList.add("col-md-4");

    const img = document.createElement("img");
    img.setAttribute("src", "imgs/doctor2.jpg");
    img.classList.add("card-img", "h-100");

    const divBodyCol = document.createElement("div");
    divBodyCol.classList.add("col-md-8");

    const divBody = document.createElement("div");
    divBody.classList.add("card-body");

    const h5Body = document.createElement("h5");
    h5Body.classList.add("card-title");
    h5Body.textContent = "Card title";

    const p1Body = document.createElement("p");
    p1Body.classList.add("card-text");
    p1Body.textContent =
      "This is a wider card with supporting text below as a natural lead-in to additional content. This content is a little bit longer.";

    const p2Body = document.createElement("p");
    p2Body.classList.add("card-text");

    const aDetails = document.createElement("a");
    aDetails.classList.add("btn", "btn-primary");
    aDetails.textContent = "Details";
    aDetails.setAttribute("href", "#");

    // append element to create card tree
    divCard.appendChild(divRow);

    divRow.appendChild(divImgCol);
    divRow.appendChild(divBodyCol);

    divImgCol.appendChild(img);

    divBodyCol.appendChild(divBody);

    divBody.appendChild(h5Body);
    divBody.appendChild(p1Body);
    divBody.appendChild(p2Body);

    p2Body.appendChild(aDetails);

    // append card to grid div
    divCardGrid.appendChild(divCard);
  }
  divContent.appendChild(divCardGrid);
  //   fr.textContent = "";
}

function loadPatients() {
  divContent.textContent = "";
  const divTableGrid = document.createElement("div");
  divTableGrid.classList.add("table-grid");

  // create table
  const table = document.createElement("table");
  table.classList.add(
    "table",
    "table-hover",
    "animate__animated",
    "animate__slideInUp"
  );

  // create table head
  const thead = document.createElement("thead");
  thead.classList.add("thead-dark");

  const trHead = document.createElement("tr");
  thead.appendChild(trHead);

  const thList = ["#", "name", "gender", "phone", "Date", "address", "queue"];

  for (let i = 0; i < thList.length; i++) {
    const element = thList[i];
    const th = document.createElement("th");
    th.setAttribute("scope", "col");
    th.textContent = element;
    trHead.appendChild(th);
  }

  // create table body
  const tbody = document.createElement("tbody");

  for (let i = 0; i < 50; i++) {
    const trBody = document.createElement("tr");

    const th = document.createElement("th");
    th.setAttribute("scope", "col");
    th.textContent = `${i + 1}`;

    const tdName = document.createElement("td");
    tdName.textContent = "Edro";

    const tdGender = document.createElement("td");
    tdGender.textContent = "Male";

    const tdPhone = document.createElement("td");
    tdPhone.textContent = "01033022863";

    const tdDate = document.createElement("td");
    tdDate.textContent = "15/7/2020";

    const tdAddress = document.createElement("td");
    tdAddress.textContent = "faques - sharqia";

    const tdQueue = document.createElement("td");
    tdQueue.textContent = "23";

    // append data to trbody
    trBody.appendChild(th);
    trBody.appendChild(tdName);
    trBody.appendChild(tdGender);
    trBody.appendChild(tdPhone);
    trBody.appendChild(tdDate);
    trBody.appendChild(tdAddress);
    trBody.appendChild(tdQueue);

    // append tr to tbody
    tbody.appendChild(trBody);
  }
  //   append thead and tbody to table
  table.appendChild(thead);
  table.appendChild(tbody);

  divTableGrid.appendChild(table);

  divContent.appendChild(divTableGrid);
  //   divContent.textContent = "";
}

function loadClinics() {
  divContent.textContent = "";

  const divCardGrid = document.createElement("div");
  divCardGrid.classList.add("card-grid");

  for (let i = 0; i < 50; i++) {
    // create doctor card
    const divCard = document.createElement("div");
    divCard.classList.add(
      "card",
      "m-auto",
      "animate__animated",
      "animate__zoomIn"
    );
    divCard.style = "max-width: 540px;";

    const divRow = document.createElement("div");
    divRow.classList.add("row", "no-gutters");

    const divImgCol = document.createElement("div");
    divImgCol.classList.add("col-md-4");

    const img = document.createElement("img");
    img.setAttribute("src", "imgs/clinic.jpg");
    img.classList.add("card-img", "h-100");

    const divBodyCol = document.createElement("div");
    divBodyCol.classList.add("col-md-8");

    const divBody = document.createElement("div");
    divBody.classList.add("card-body");

    const h5Body = document.createElement("h5");
    h5Body.classList.add("card-title");
    h5Body.textContent = "Card title";

    const p1Body = document.createElement("p");
    p1Body.classList.add("card-text");
    p1Body.textContent =
      "This is a wider card with supporting text below as a natural lead-in to additional content. This content is a little bit longer.";

    const p2Body = document.createElement("p");
    p2Body.classList.add("card-text");

    const aDetails = document.createElement("a");
    aDetails.classList.add("btn", "btn-primary");
    aDetails.textContent = "Details";
    aDetails.setAttribute("href", "#");

    // append element to create card tree
    divCard.appendChild(divRow);

    divRow.appendChild(divImgCol);
    divRow.appendChild(divBodyCol);

    divImgCol.appendChild(img);

    divBodyCol.appendChild(divBody);

    divBody.appendChild(h5Body);
    divBody.appendChild(p1Body);
    divBody.appendChild(p2Body);

    p2Body.appendChild(aDetails);

    // append card to grid div
    divCardGrid.appendChild(divCard);
  }
  divContent.appendChild(divCardGrid);
  //   fr.textContent = "";
}