document.addEventListener("DOMContentLoaded", function() {
    const updateButton = document.getElementById("updateButton");

    if (updateButton) {
        updateButton.addEventListener("click", updateAddress); 
    }
});

function updateAddress(){
    const bakeryId = document.getElementById("bakeryIdSpan").innerText;
    const newAddress = document.getElementById("updatedAddress").value;

    fetch(`https://localhost:7182/api/Bakeries/${bakeryId}/update`, {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(newAddress)
    })
        .then(response => {
            if (response.ok) {
                alert('Address updated successfully!');
            }
        })
}