var sessionInfo = null;
var currentGuessNum;

var config = {
    difficultyLevel: "Beginner",
    NumberOfGamePieces: 3
};


function allowDrop(ev) {
    ev.preventDefault();
}

function drag(ev) {
    ev.dataTransfer.setData("id", ev.target.id);
}

function drop(ev) {
    ev.preventDefault();
    var data = ev.dataTransfer.getData("id").split(':');
    var newVal = Math.floor(data[1]);
    var id = ev.target.id.split(':');
    var purpose = Math.floor(id[0]);

    if (purpose === PiecePurpose.Board) {
        currentGuessNum = Math.floor(id[1]);
        viewModel.guesses()[id[1]].items()[id[2]].value(newVal);
    }
    else if (purpose === PiecePurpose.Code) {
        viewModel.secretCode()[id[1]].value(newVal);
    }
}

function codeBreakerMoved(event) {
    var result = JSON.parse(event.data);
    for (var i = 0; i < result.Guesses.length; i++) {
        viewModel.guesses()[result.TurnNumber].items()[i].value(result.Guesses[i]);
        viewModel.results()[result.TurnNumber].items()[i].value(result.GuessResults[i]);
    }
}

function connectToWS() {
    var scheme = document.location.protocol == "https:" ? "wss" : "ws";
    var port = document.location.port ? (":" + document.location.port) : "";
    var connectionUrl = scheme + "://" + document.location.hostname + port + "/ws";
    var stateLabel = document.getElementById("stateLabel");
    var socket = new WebSocket(connectionUrl);

    socket.onclose = function (event) {
        stateLabel.innerHTML = 'web socket closed';
    };
    socket.onerror = function () {
        stateLabel.innerHTML = 'web socket error';
    };
    socket.onopen = function () {
        socket.send(JSON.stringify(sessionInfo));
    }

    socket.onmessage = codeBreakerMoved;
}

function setCode() {
    var secretCode = new Array();
    for (var i = 0; i < viewModel.secretCode().length; i++) {
        var val = viewModel.secretCode()[i].value();
        secretCode.push(val);
    }

    var postData = JSON.stringify({
        difficultyLevel: config.difficultyLevel,
        code: secretCode,
        sessionInfo: sessionInfo
    });

    //$.post("Home/SetSecretCode", postData, setCodeSuccess, "application/json");
    $.ajax({
        type: "POST",
        url: "Home/SetSecretCode",
        data: postData,
        dataType: "json",
        contentType: "application/json",
        success: function (data) {
            viewModel.codeSetMessage("Code successfully set");
            viewModel.isCodeSet(true);
        }
    });
}

function joinExistingGame(gameId) {
    var data = JSON.stringify({
        sessionId: gameId,
        role: 1
    });

    $.ajax({
        type: "POST",
        url: "Home/JoinGame",
        data: data,
        dataType: "json",
        contentType: "application/json",
        success: function (data) {
            sessionInfo = data;
            viewModel.gameInitialized(true);
            viewModel.role(sessionInfo.role);
        }
    });
}

function makeGuess() {
    var guessedPieces = new Array();
    for (var i = 0; i < config.NumberOfGamePieces; i++) {
        guessedPieces.push(viewModel.guesses()[currentGuessNum].items()[i].value());
    }

    var postData = JSON.stringify({
        guess: guessedPieces,
        sessionInfo: sessionInfo
    });

    $.ajax({
        type: "POST",
        url: "Home/MakeGuess",
        data: postData,
        dataType: "json",
        contentType: "application/json",
        success: function (data) {
            for (var i = 0; i < config.NumberOfGamePieces; i++) {
                viewModel.results()[currentGuessNum].items()[i].value(data[i]);
            }
        }
    });
}

function createNewGame() {
    $.ajax({
        type: "POST",
        url: "Home/CreateNewGame",
        data: config.difficultyLevel,
        dataType: "json",
        contentType: "application/json",
        success: function (data) {
            sessionInfo = data;
            viewModel.games.push(new GameVMItem(data.id, "Game " + viewModel.games.length));
            viewModel.gameInitialized(true);
            viewModel.role(data.role);
            connectToWS();
        }
    });
}

function getGames(data) {
    for (var i = 0; i < data.length; i++) {
        viewModel.games.push(new GameVMItem(data[i], "Game " + i));
    }
}

// Activates knockout.js
var viewModel = new AppViewModel();
ko.applyBindings(viewModel);

$.get("Home/GetGames", getGames);