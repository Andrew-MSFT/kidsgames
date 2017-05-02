var sessionInfo;

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

    if (id[0] === "board") {
        viewModel.turns()[id[1]].items()[id[2]].value(newVal);
    }
    else if (id[0] === "code") {
        viewModel.secretCode()[id[1]].value(newVal);
    }
}

function setCodeSuccess(data) {
    var a = 0;
}

function setCode() {
    var secretCode = new Array();
    for (var i = 0; i < viewModel.secretCode().length; i++) {
        var val = viewModel.secretCode()[i].value();
        if (val == 0) {
            secretCode.push("Empty");
        }
        else {
            secretCode.push("Piece" + val);
        }
    }

    var postData = JSON.stringify({ difficultyLevel: "Beginner", code: secretCode });

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

function getGameInfoCompleted(data) {
    sessionInfo = data;
    viewModel.role(data.role);
}

// Activates knockout.js
var viewModel = new AppViewModel();
ko.applyBindings(viewModel);

$.get("Home/GetGameInfo", getGameInfoCompleted);