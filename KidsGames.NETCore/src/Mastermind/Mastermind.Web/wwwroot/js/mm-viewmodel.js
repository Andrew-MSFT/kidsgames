var PiecePurpose = {
    Board: 1,
    Palette: 2,
    Result: 3,
    Code: 4
};

function GameVMItem(id, name) {
    this.id = id;
    this.name = ko.observable(name);
    this.clicked = function () {
        alert("clicked " + this.id);
    }
}

function ViewModelItem(value, id, purpose) {
    var self = this;
    this.purpose = purpose;
    this.value = ko.observable(value);
    this.id = id;
    this.displayedPiece = ko.computed(function () {
         if (this.purpose === PiecePurpose.Result) {
            //public enum GuessResult { Empty, Incorrect, CorrectButWrongLocation, Correct }
            switch (this.value()) {
                case 1:
                    return "incorrect-result";
                case 2:
                    return "partially-correct-result";
                case 3:
                    return "correct-result";
                default:
                    return "empty";
            }
        }
        else {
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
        }
    }, this);
}

function ViewModelRow(row, cols, purpose) {
    this.items = ko.observableArray();

    for (var col = 0; col < cols; col++) {
        this.items.push(new ViewModelItem(0, purpose + ":" + row + ":" + col, purpose));
    }
}

// This is a simple *viewmodel* - JavaScript that defines the data and behavior of your UI
function AppViewModel() {
    var rows = 4;
    var cols = 3;

    this.secretCode = ko.observableArray();
    for (var col = 0; col < cols; col++) {
        this.secretCode.push(new ViewModelItem(0, PiecePurpose.Code + ":" + col, PiecePurpose.Palette));
    }

    this.guesses = ko.observableArray();
    for (var row = 0; row < rows; row++) {
        this.guesses.push(new ViewModelRow(row, cols, PiecePurpose.Board));
    }

    this.results = ko.observableArray();
    for (var row = 0; row < rows; row++) {
        this.results.push(new ViewModelRow(row, cols, PiecePurpose.Result));
    }

    this.piecePalette = [new ViewModelItem(1, "palette:1"),
    new ViewModelItem(2, "palette:2"),
    new ViewModelItem(3, "palette:3")];

    this.games = ko.observableArray();

    this.role = ko.observable(0);
    this.playerRole = ko.computed(function () {
        if (this.role() === 0) {
            return "Code Setter";
        }
        else {
            return "Code Breaker";
        }
    }, this);

    this.codeSetMessage = ko.observable("");
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