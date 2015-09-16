app.controller('CarFinderController', ['$scope', 'carSvc', function ($scope, carSvc) {

    $("#errorDisplay").hide();
    $("#displayedInfo").hide();

    $scope.years = [];
    $scope.makes = [];
    $scope.models = [];
    $scope.trims = [];
    $scope.car = [];

    $scope.getYears = function () {
        $("#trimList").hide();
        $("#trimHeading").hide();
        carSvc.getyears().then(function (data) { $scope.years = data; });
    };
    $scope.getYears();

    $scope.getMakes = function () {
        $("#trimList").hide();
        $("#trimHeading").hide();
        $scope.selectedMake = '';
        carSvc.getmakes($scope.selectedYear).then(function (data) { $scope.makes = data; });
    }

    $scope.getModels = function () {
        $("#trimList").hide();
        $("#trimHeading").hide();
        $scope.selectedModel = '';
        carSvc.getmodels($scope.selectedYear, $scope.selectedMake).then(function (data) { $scope.models = data; });
    }

    $scope.getTrims = function () {
        $scope.selectedTrim = '';
        carSvc.gettrims($scope.selectedYear, $scope.selectedMake, $scope.selectedModel).then(function (data) {
            if (data != "") {
                $scope.trims = data;
                $("#trimList").show();
                $("#trimHeading").show();
            }
            else {
                $scope.trims = [];
                $("#trimList").hide();
                $("#trimHeading").hide();
                $scope.getCar();
            }
        });
    }

    $scope.getCar = function () {
        carSvc.getcar($scope.selectedYear, $scope.selectedMake, $scope.selectedModel, $scope.selectedTrim).then(function (data) {
            if (data != "") {
                $("#errorDisplay").hide();
                $scope.car = data;
                $("#displayedInfo").show();
            }
            else {
                $scope.car = [];
                $("#displayedInfo").hide();
                $("#errorDisplay").show();
            }
        });

    }

}]);