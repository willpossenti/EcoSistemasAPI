'use strict';
app.controller('usersController', ['$scope', 'usersService', 'localStorageService', '$location', function ($scope, usersService, localStorageService, $location) {


    $scope.users = [];


    usersService.getusers().then(function (results) {

        var authData = localStorageService.get('authorizationData');

        if (authData) {

            console.log('procurou');
            $scope.users = results;
            
        } else {
            console.log('n procurou');
           
   
        }
    }, function (error) {
        console.log(error.data.message);
    });

}]);