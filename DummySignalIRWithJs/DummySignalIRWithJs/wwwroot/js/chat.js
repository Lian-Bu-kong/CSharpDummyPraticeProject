"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

//Disable send button until connection is established
document.getElementById("sendButton").disabled = true;

// Connect BackEnd and FrontEnd Init
connection.start().then(function () {

    // Connect成功
    document.getElementById("sendButton").disabled = false;


}).catch(function (err) {
    return console.error(err.toString());
});


connection.on("ReceiveMessage", function (user, message) {

    // ChatHub 廣播
    var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
    var encodedMsg = user + " says " + msg;
    var li = document.createElement("li");

    li.textContent = encodedMsg;

    document.getElementById("messagesList").appendChild(li);
});

document.getElementById("sendButton").addEventListener("click", function (event) {

    // Call ChatHub
    var user = document.getElementById("userInput").value;
    var message = document.getElementById("messageInput").value;

    connection.invoke("SendMessage", user, message).catch(function (err) {
        return console.error(err.toString());
    });

    event.preventDefault();
});