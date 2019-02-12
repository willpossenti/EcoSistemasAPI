'use strict';
app.controller('indexController', ['$scope', '$location', 'authService', function ($scope, $location, authService) {

    $scope.logOut = function () {
        authService.logOut();
        $location.path('/home');
    }

    //var authData = localStorageService.get('authorizationData');
    //console.log(authData);


    $scope.authentication = authService.authentication;

}]);