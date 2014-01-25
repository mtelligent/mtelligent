/// <reference path="knockout-3.0.0.debug.js" />
/// <reference path="jquery-1.9.1.intellisense.js" />

function experiment(serverObj) {
    this.id = ko.observable(0);
    this.name = ko.observable();
    this.systemName = ko.observable();
    this.targetCohortId = ko.observable(0);

    this.variables = ko.observableArray();

    this.segments = ko.observableArray();

    if (serverObj != null) {
        this.id(serverObj.Id);
        this.name(serverObj.Name);
        this.systemName(serverObj.SystemName);
        this.targetCohortId(serverObj.TargetCohortId);
    }

}

function variable(key, value) {
    this.key = ko.observable(key);
    this.value = ko.observable(value);
}

function segment(serverObj) {
    this.id = ko.observable(0);
    this.experimentId = ko.observable(0);
    this.name = ko.observable();
    this.systemName = ko.observable();
    this.targetPercentage = ko.observable(0);
    this.isDefault = ko.observable(false);

    this.variables = ko.observableArray();

    if (serverObj != null) {
        this.id(serverObj.Id);
        this.experimentId(serverObj.ExperimentId);
        this.name(serverObj.Name);
        this.systemName(serverObj.SystemName);
        this.targetPercentage(serverObj.TargetPercentage);
        this.isDefault(serverObj.IsDefault);
    }

}

var viewModel = {
    experiment: ko.observable(),
    variables: ko.observableArray(),

    errorMessage: ko.observable(),
    showErrorMessage: ko.observable(false),
    initalVariableCount: ko.observable(),
    loadExperiment: function (serverExp) {
        viewModel.experiment(serverExp);

        if (serverExp != null) {
            $.each(serverExp.Segments, function (index, serverSegment) {
                viewModel.experiment().segments.push(new segment(serverSegment));
            });

            $.each(serverExp.Variables, function (index, variable) {
                viewModel.experiment().variables.push(variable);
            });
        }

    },

    addVariable: function () {
        console.log("called");
        viewModel.variables.push("");
    }
};