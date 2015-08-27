
var app = angular.module('CarFinderApp');
app.controller('CarFinderController', ['$scope', '$http', function ($scope, $http) {
    $scope.selectedYear = '';
    $scope.years = [];
    $scope.getYears = function () {
        //Replace with call to service later. 
        $scope.years = ['2000', '2001', '2003', '2004', '2005'];
    }
    $scope.getYears();

    $scope.selectedMake = '';
    $scope.makes = [];
    $scope.getMakes = function () {
        //Replace with call to service later. 
        $scope.makes = ['Toyota', 'Nissan', 'Mistubishi'];
    }


    $scope.selectedModel = '';
    $scope.models = [];
    $scope.getModels = function () {
        //Replace with call to service later. 
        $scope.models = ['Model 1', 'Model 2', 'Model 3', 'Versa'];
    }


    $scope.selectedTrim = '';
    $scope.trims = [];
    $scope.getTrims = function () {
        //Replace with call to service later. 
        $scope.trims = ['Sedan', 'Hatchback'];
    }
}]);






















