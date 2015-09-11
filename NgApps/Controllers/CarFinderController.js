
//var app = angular.module('CarFinderApp');

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
        $scope.selectedMake = '';
    };
    $scope.getYears();

    $scope.getMakes = function () {
        $("#trimList").hide();
        $("#trimHeading").hide();
        carSvc.getmakes($scope.selectedYear).then(function (data) { $scope.makes = data; });
        $scope.selectedModel = '';
    }

    $scope.getModels = function () {
        $("#trimList").hide();
        $("#trimHeading").hide();
        carSvc.getmodels($scope.selectedYear, $scope.selectedMake).then(function (data) { $scope.models = data; });
        $scope.selectedTrim = '';
    }

    $scope.getTrims = function () {
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
                $scope.selectedTrim = '';
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