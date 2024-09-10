var botWindowVisible = false;

function toggleBot() {
    const chatContainer = document.getElementById('chatContainer');
    const inputBox = document.getElementById('userInput');
    botWindowVisible = !botWindowVisible;

    if (botWindowVisible) {
        chatContainer.style.display = 'block';
    }
    else {
        chatContainer.style.display = 'none';
        inputBox.focus();
    }
}

function sendMessage() {
    var userInput = document.getElementById('userInput').value;
    var chatBox = document.getElementById('chatBox');
    const inputBox = document.getElementById('userInput');

    // Display user's message in the chat box
    chatBox.innerHTML += `<div class="user-message">User: ${userInput}</div>`;

    // Simulate processing and generating a response
    getResponse(userInput)
 
    // Scroll to the bottom of the chat box
    chatBox.scrollTop = chatBox.scrollHeight;
    inputBox.focus();

    // Clear the input field
    document.getElementById('userInput').value = '';
}

function getResponse(message) {
    // Getting User input
    var chatBox = document.getElementById('chatBox');

    callAPI(userInput)
        .then(botResponse => {
            chatBox.innerHTML += `<div class="bot-message">Bot: ${botResponse}</div>`;
        })
        .catch(error => {
            chatBox.innerHTML += `<div class="bot-message">Bot: ${error}</div>`;
        });
}


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