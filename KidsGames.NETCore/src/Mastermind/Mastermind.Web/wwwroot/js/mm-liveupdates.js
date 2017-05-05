var socket;
var scheme = document.location.protocol == "https:" ? "wss" : "ws";
var port = document.location.port ? (":" + document.location.port) : "";
var connectionUrl = scheme + "://" + document.location.hostname + port + "/ws";
var stateLabel = document.getElementById("stateLabel");

function onUpdateMessage(event) {
    var result = JSON.parse(event.data);
    for (var i = 0; i < result.Guesses.length; i++) {
        viewModel.guesses()[result.TurnNumber].items()[i].value(result.Guesses[i]);
        viewModel.results()[result.TurnNumber].items()[i].value(result.GuessResults[i]);
    }
    
}

function connectToWS() {
    socket = new WebSocket(connectionUrl);
    socket.onclose = function(event) {
        stateLabel.innerHTML = 'web socket closed';
    };
    socket.onerror = function() {
        stateLabel.innerHTML = 'web socket error';
    };
    socket.onopen = function () {
        socket.send(JSON.stringify(sessionInfo));
    }

    socket.onmessage = onUpdateMessage;
}