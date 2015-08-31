
//var app = angular.module('CarFinderApp');

app.controller('CarFinderController', ['$scope', 'carSvc', function ($scope, carSvc) {

    $("#homepageCarousel").hide();
    $("#displayedInfo").hide();

    $scope.getYears = function () {
        $scope.years = [];
        $scope.makes = [];
        $scope.models = [];
        $scope.trims = [];
        $scope.selectedYear = '';
        $scope.selectedMake = '';
        $scope.selectedModel = '';
        $scope.selectedTrim = '';
        carSvc.getyears().then(function (data) { $scope.years = data; });
    };
    $scope.getYears();
    
    $scope.getMakes = function () {
        $scope.makes = [];
        $scope.models = [];
        $scope.trims = [];
        $scope.selectedMake = '';
        $scope.selectedModel = '';
        $scope.selectedTrim = '';
        carSvc.getmakes($scope.selectedYear).then(function (data) { $scope.makes = data; });
    }
   
    $scope.getModels = function () {
        $scope.models = [];
        $scope.trims = [];
        $scope.selectedModel = '';
        $scope.selectedTrim = '';
        carSvc.getmodels($scope.selectedYear, $scope.selectedMake).then(function (data) { $scope.models = data; });
    }

    $scope.getTrims = function () {
        $scope.trims = [];
        $scope.selectedTrim = '';
        carSvc.gettrims($scope.selectedYear, $scope.selectedMake, $scope.selectedModel).then(function (data) {
            if (data != "") {
                $scope.trims = data;
                $("#trimList").prop('disabled', false);
            }
            else {
                $("#trimList").prop('disabled', true);
                $scope.getCar();
            }
        });
    }

    $scope.car = [];
    $scope.getCar = function () {
        carSvc.getcar($scope.selectedYear, $scope.selectedMake, $scope.selectedModel, $scope.selectedTrim).then(function (data) { $scope.car = data; });
        $("#displayedInfo").show();
        $("#homepageCarousel").show();
    }








}]);