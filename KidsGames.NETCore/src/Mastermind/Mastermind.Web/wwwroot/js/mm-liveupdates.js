var socket;
var scheme = document.location.protocol == "https:" ? "wss" : "ws";
var port = document.location.port ? (":" + document.location.port) : "";
var connectionUrl = scheme + "://" + document.location.hostname + port + "/ws";
var stateLabel = document.getElementById("stateLabel");

function onUpdateMessage(event) {
    stateLabel.innerHTML = event.data;
}

function connectToWS() {
    socket = new WebSocket(connectionUrl);
    socket.onclose = function(event) {
        stateLabel.innerHTML = 'web socket closed';
    };
    socket.onerror = function() {
        stateLabel.innerHTML = 'web socket error';
    };

    socket.onmessage = onUpdateMessage;
}