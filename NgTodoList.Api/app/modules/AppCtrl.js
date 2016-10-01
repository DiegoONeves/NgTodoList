(function () {
    'use strict';

    angular
        .module('app')
        .controller('AppCtrl', AppCtrl);

    AppCtrl.$inject = ['$rootScope', '$scope', '$location', 'UserRepository'];

    function AppCtrl($rootScope, $scope, $location, UserRepository) {
        $rootScope.changeTheme = function (theme) {
            $rootScope.theme = theme;
            localStorage.setItem('theme', theme);
        };

        $scope.logout = function () {
            UserRepository.clearUserData();
        }
    }
})();