function callAPI(userInput) {
    return fetch('https://llamastudio.dev/api/clrm8ahih0001jo08lm3zg1jr', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({ input: userInput })
    })
        .then(response => {
            if (!response.ok) {
                throw new Error('Network response was not ok');
            }
            return response.json();
        })
        .then(data => {
            return JSON.stringify(data, null, 2);
        })
        .catch(error => {
            return JSON.stringify(error, null, 2);
        });
}

function getResponse() {
    var userInput = document.getElementById('input').value;

    // Call the API and handle the Promise
    callAPI(userInput)
        .then(response => {
            document.getElementById('output').innerHTML = '<p>API Response:</p><pre>' + response + '</pre>';
        })
        .catch(error => {
            document.getElementById('output').innerHTML = '<p>Error:</p><pre>' + error + '</pre>';
        });
}