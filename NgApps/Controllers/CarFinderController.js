
//var app = angular.module('CarFinderApp');

app.controller('CarFinderController', ['$scope', 'carSvc', function ($scope, carSvc) {

    $("#errorDisplay").hide();
    $("#homepageCarousel").hide();
    $("#displayedInfo").hide();

    $scope.selectedYear = '';
    $scope.selectedMake = '';
    $scope.selectedModel = '';
    $scope.selectedTrim = '';
   
    

    $scope.years = [];
    $scope.makes = [];
    $scope.models = [];
    $scope.trims = [];
    $scope.car = [];

    $scope.getYears = function () {
        //$scope.years = [];
        carSvc.getyears().then(function (data) { $scope.years = data; });
    };
    $scope.getYears();
    
    $scope.getMakes = function () {
        //$scope.makes = [];
        carSvc.getmakes($scope.selectedYear).then(function (data) { $scope.makes = data; });
    }
   
    $scope.getModels = function () {
        //$scope.models = [];
        carSvc.getmodels($scope.selectedYear, $scope.selectedMake).then(function (data) { $scope.models = data; });
    }

    $scope.getTrims = function () {
        //$scope.trims = [];
       
        carSvc.gettrims($scope.selectedYear, $scope.selectedMake, $scope.selectedModel).then(function (data) {
            if (data != "") {
                $scope.trims = data;
                $("#trimList").prop('disabled', false);
            }
            else {
                $scope.trims = [];
                $("#trimList").prop('disabled', true);
                $scope.selectedTrim = '';
            }
        });
    }

    
    $scope.getCar = function () {
        carSvc.getcar($scope.selectedYear, $scope.selectedMake, $scope.selectedModel, $scope.selectedTrim).then(function (data) {
            if (data != "") {
                $("#errorDisplay").hide();
                $scope.car = data;
                $("#displayedInfo").show();
                $("#homepageCarousel").show();
            }
            else {
                $scope.car = [];
                $("#homepageCarousel").hide();
                $("#displayedInfo").hide();
                $("#errorDisplay").show();
            }
        });
       
    }








}]);