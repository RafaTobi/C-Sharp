document.addEventListener("DOMContentLoaded", function () {
    loadTable();
    loadOptions();
});

//functie om bakery table in bakerDetails te vullen
function loadTable() {
    const idSpan = document.getElementById("bakerId");
    const id = Number(idSpan.innerText);
    fetch(`https://localhost:7182/api/Bakeries/${id}/bakeriesFromBaker`,
        {
            method: "GET",
            headers: {"Accept": "application/json"}
        })
        .then(response => response.json())
        .then(data => showBakeries(data))
        .catch(reason => alert("Call Failed load table: " + reason));
}

function showBakeries(bakeries) {
    const tableBody = document.getElementById("bakeryTableBody");
    tableBody.innerHTML = "";

    bakeries.forEach(bakery => tableBody.innerHTML += `<tr id="bakery_${bakery.id}">
                                <td>${bakery.name}</td>
                                <td>${bakery.adres}</td>
                            </tr>`);

}

//functie om de select te vullen met namen van bakeries
function loadOptions() {
    fetch("https://localhost:7182/api/Bakeries")
        .then(response => response.json())
        .then(records => {
            const selectBox = document.getElementById("bakerySelect");

            records.forEach(record => {
                const option = document.createElement("option");
                option.value = record.id;
                option.textContent = record.name;
                selectBox.appendChild(option);
            });
        })
        .catch(error => console.error("Fout bij het ophalen van records:", error));
}

const addBakeryBtn = document.getElementById("addBakeryBtn");
addBakeryBtn.addEventListener("click", async function () {
    await addBakeryToBaker();
    setTimeout(loadTable,500); //kleine delay anders gebeurde loadTable te snel 
});

async function addBakeryToBaker() {
    const idSpan = document.getElementById("bakerId");
    const bakerId = Number(idSpan.innerText);
    const bakeryId = document.getElementById("bakerySelect").value;
    const duration = document.getElementById("duration").value;
    const price = document.getElementById("price").value;
    const startDate = new Date();
    const endDate = new Date();
    endDate.setMonth(startDate.getMonth() + duration);
    
    fetch("https://localhost:7182/api/Bakeries", {
                method: "POST",
                body: JSON.stringify({
                    bakeryId: bakeryId,
                    bakerId: bakerId,
                    startDate: startDate.toISOString(),
                    endDate: endDate.toISOString(),
                    price: price,
                }),
                headers: {
                    "Content-Type": "application/json",
                    "Accept": "application/json"
                }
            })
        .then(response => {
            if (!response.ok) throw Error(`Received status code ${response.status}.`);
        })
        .catch(error => {
            console.error(`Call failed: ${error.message}`);
        });
}