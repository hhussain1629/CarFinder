app.factory('carSvc', ['$http', function ($http) {

    var factory = {};

    factory.getyears = function () {
        return $http.get('/api/Years/').then(function (response) { return response.data; });
    };

    factory.getmakes = function (year) {
        var options  = { params: {year: year} };
        return $http.get('/api/Makes/', options).then(function (response) { return response.data; });
    }

    factory.getmodels = function (year, make) {
        var options = { params: {year: year, make: make} };
        return $http.get('/api/Models/', options).then(function (response) { return response.data; });
    }

    factory.gettrims = function (year, make, model) {
        var options = { params: { year: year, make: make, model: model } };
        return $http.get('/api/Trims/', options).then(function (response) { return response.data; });
    }

    factory.getcar = function (year, make, model, trim) {
        var options = { params: { year: year, make: make, model: model, trim: trim } };
        return $http.get('/api/Cars/', options).then(function (response) { return response.data; });
    }

    //factory.getrecalls = function (year, make, model) {
    //    var url = "http://www.nhtsa.gov/webapi/api/Recalls/vehicle/modelyear/" + year + "/make/" + make + "/model/" + model + "?format=json"
    //    //var NHTSAAPI = $resource(url, { callback: "JSON_CALLBACK" }, { get: { method: "JSONP" } });
    //    results = $http.get(url).then(function (response) { return response.data; });
    //    return results;
    //}

    return factory;
}]);