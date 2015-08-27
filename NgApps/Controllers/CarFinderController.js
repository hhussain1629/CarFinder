
//var app = angular.module('CarFinderApp');

app.controller('CarFinderController', ['$scope', 'carSvc', function ($scope, carSvc) {
    $scope.selectedYear = '';
    $scope.years = [];
    $scope.getYears = function () {
        //Replace with call to service later. 
        carSvc.getyears().then(function (data) { $scope.years = data; });
    };
    $scope.getYears();

    $scope.selectedMake = '';
    $scope.makes = [];
    $scope.getMakes = function () {
        //Replace with call to service later. 
        //$scope.makes = ['Toyota', 'Nissan', 'Mistubishi'];
        carSvc.getmakes($scope.selectedYear).then(function (data) { $scope.makes = data; });
    }
   


    $scope.selectedModel = '';
    $scope.models = [];
    $scope.getModels = function () {
        //Replace with call to service later. 
        //$scope.models = ['Model 1', 'Model 2', 'Model 3', 'Versa'];
        carSvc.getmodels($scope.selectedYear, $scope.selectedMake).then(function (data) { $scope.models = data; });
    }


    $scope.selectedTrim = '';
    $scope.trims = [];
    $scope.getTrims = function () {
        //Replace with call to service later. 
        //$scope.trims = ['Sedan', 'Hatchback'];
        carSvc.gettrims($scope.selectedYear, $scope.selectedMake, $scope.selectedModel).then(function (data) { $scope.trims = data; });
        //if ($scope.trims.length == 0)
        //    $scope.getCar();
    }

    
    $scope.car = [];
    $scope.getCar = function () {
        carSvc.getcar($scope.selectedYear, $scope.selectedMake, $scope.selectedModel, $scope.selectedTrim).then(function (data) { $scope.car = data; });
       
    }

    



}]);