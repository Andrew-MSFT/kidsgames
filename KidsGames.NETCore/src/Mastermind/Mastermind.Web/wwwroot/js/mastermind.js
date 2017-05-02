var sessionInfo;
var currentGuessNum;

function config() {
    this.difficultyLevel = "Beginner";
}


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

function setCodeSuccess(data) {
    viewModel.codeSetMessage("Code successfully set");
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
        success: setCodeSuccess,
        dataType: "json",
        contentType: "application/json"
    });
}

function getNumberOfGamePieces() {
    return 3;
}

function guessResult(data) {
    for (var i = 0; i < getNumberOfGamePieces(); i++) {
        viewModel.results()[currentGuessNum].items()[i].value(data[i]);
    }
}

function makeGuess() {
    var guessedPieces = new Array();
    for (var i = 0; i < getNumberOfGamePieces(); i++) {
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
        success: guessResult,
        dataType: "json",
        contentType: "application/json"
    });
}

function getGameInfoCompleted(data) {
    sessionInfo = data;
    viewModel.role(data.role);
}

// Activates knockout.js
var viewModel = new AppViewModel();
ko.applyBindings(viewModel);

$.get("Home/GetGameInfo", getGameInfoCompleted);