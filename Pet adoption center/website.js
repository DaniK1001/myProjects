function getCurrentDate() {
    const currentDate = new Date();

    const days = ['Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday'];
    const months = ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December'];

    const dayOfWeek = days[currentDate.getDay()];
    const month = months[currentDate.getMonth()];
    const date = currentDate.getDate();
    const year = currentDate.getFullYear();

    const formattedDate = dayOfWeek + ", " + month + " " + date + ", " + year;

    return formattedDate;
}

// Function to update the date and time
function updateDateTime() {
    const currentDate = new Date();
    const currentTime = currentDate.toLocaleTimeString([], {hour12: false});

    const currentDateFormatted = getCurrentDate();

    const dateTimeString = currentDateFormatted + ' ' + currentTime;
    document.getElementById('currentDateTime').innerHTML = dateTimeString;
}

// Update the date and time every second
setInterval(updateDateTime);



function toggleInterested(button) {
    if (button.classList.contains('interested')) {
        button.classList.remove('interested');
        button.textContent = 'Interested';
    } else {
        button.classList.add('interested');
        button.textContent = 'Interested';
    }
}


document.addEventListener('DOMContentLoaded', function() {
    const form = document.getElementById('petForm');

    form.addEventListener('submit', function(event) {
        event.preventDefault(); // Prevent form submission

        // Retrieve form field values
        const petType = document.getElementById('petType').value;
        const breed = document.getElementById('breed').value;
        const age = document.getElementById('age').value;
        const gender = document.getElementById('gender').value;

        // Check if any field is set to "Please choose"
        if (petType === "Please-Choose" || breed === "Please-Choose" || age === "Please-Choose" || gender === "Please-Choose") {
            alert('Please fill in all fields.'); // Display error message
            return; // Exit the function
        }

        // If all fields are filled, allow form submission
        form.submit();
    });
});


document.addEventListener('DOMContentLoaded', function() {
    const form = document.getElementById('giveawayForm');

    form.addEventListener('submit', function(event) {
        event.preventDefault(); // Prevent form submission

        // Retrieve form field values
        const petType = document.getElementById('petType').value;
        const breed = document.getElementById('breed').value;
        const age = document.getElementById('age').value;
        const gender = document.getElementById('gender').value;
        const comments = document.getElementById('comments').value;
        const ownerName = document.getElementById('ownerName').value;
        const ownerEmail = document.getElementById('ownerEmail').value;

        // Regex for email validation, Found it online
        const emailPattern =   /^(([^<>()[\]\.,;:\s@\"]+(\.[^<>()[\]\.,;:\s@\"]+)*)|(\".+\"))@(([^<>()[\]\.,;:\s@\"]+\.)+[^<>()[\]\.,;:\s@\"]{2,})$/i;

        // Check for empty fields and invalid email format
        if (petType === "Please-Choose" || breed === "Please-Choose" || age === "Please-Choose" || gender === "Please-Choose" || comments.trim() === "" || ownerName.trim() === "" || ownerEmail.trim() === "" || !emailPattern.test(ownerEmail)) {
            alert('Please fill in all fields correctly.'); // Display error message
            return; // Exit the function
        }

        // If all fields are filled and email is valid, allow form submission
        form.submit();
    });
});



