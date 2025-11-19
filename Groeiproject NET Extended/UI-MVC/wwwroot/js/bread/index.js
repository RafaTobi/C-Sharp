document.addEventListener("DOMContentLoaded", loadBread)

const loadBreadbtn = document.getElementById("loadBreads");
loadBreadbtn.addEventListener("click", loadBread);

function loadBread() {
    fetch("https://localhost:7182/api/breads",
        {
            method: "GET",
            headers: {"Accept": "application/json"}
        })
        .then(response => response.json())
        .then(data => showBreads(data))
        .catch(reason => alert("Call failed: " + reason));
}

function showBreads(breads) {
    const tbody = document.getElementById("breadTableBody");
    tbody.innerHTML = "";

    breads.forEach(bread => addBread(bread));
}

function addBread(bread) {
    const tbody = document.getElementById("breadTableBody");
    tbody.innerHTML += `<tr>
                                <td>${bread.id}</td>
                                <td>${bread.name}</td>
                                <td>${bread.price !== null ? "€" + bread.price : "Unknown"}</td>
                                <td>${bread.weight + "g"}</td>
                            </tr>`;
}