﻿function allowDrop(ev) {
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
    var data = new Array();
    for (var i = 0; i < viewModel.secretCode().length; i++) {
        var val = viewModel.secretCode()[i].value();
        data.push("Piece" + val);
    }
    var object = {
        codes: data
    };
    var postData = JSON.stringify(object);
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

function ViewModelItem(value, id) {
    var self = this;
    this.value = ko.observable(value);
    this.id = id;
    this.displayedPiece = ko.computed(function () {
        switch (this.value()) {
            case 1:
                return "piece1";
            case 2:
                return "piece2";
            case 3:
                return "circle";
            default:
                return "empty";
        }
    }, this);
}

function ViewModelRow(row, cols) {
    this.items = ko.observableArray();

    for (var col = 0; col < cols; col++) {
        this.items.push(new ViewModelItem(0, "board:" + row + ":" + col));
    }
}

// This is a simple *viewmodel* - JavaScript that defines the data and behavior of your UI
function AppViewModel() {
    var rows = 4;
    var cols = 3;

    this.secretCode = ko.observableArray();
    for (var col = 0; col < cols; col++) {
        this.secretCode.push(new ViewModelItem(0, "code:" + col));
    }

    this.turns = ko.observableArray();
    for (var row = 0; row < rows; row++) {
        this.turns.push(new ViewModelRow(row, cols));
    }

    this.piecePalette = [new ViewModelItem(1, "palette:1"),
    new ViewModelItem(2, "palette:2"),
    new ViewModelItem(3, "palette:3")];
}

ko.bindingHandlers.id = {
    init: function (element, valueAccessor, allBindings, viewModel, bindingContext) {
        // This will be called when the binding is first applied to an element
        // Set up any initial state, event handlers, etc. here
        element.id = viewModel.id;
    },
    update: function (element, valueAccessor, allBindings, viewModel, bindingContext) {
        // This will be called once when the binding is first applied to an element,
        // and again whenever any observables/computeds that are accessed change
        // Update the DOM element based on the supplied values here.
        element.id = viewModel.id;
    }
};

// Activates knockout.js
var viewModel = new AppViewModel();
ko.applyBindings(viewModel);