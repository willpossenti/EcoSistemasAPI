'use strict';
app.controller('homeController', ['$scope', '$location', 'localStorageService', function ($scope, $location, localStorageService) {

    var authData = localStorageService.get('authorizationData');

    if (!authData) {

        $location.path('/login');
    }
   
}]);