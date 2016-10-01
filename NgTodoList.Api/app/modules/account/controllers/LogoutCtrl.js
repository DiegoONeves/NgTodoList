(function () {
    'use strict';

    angular
        .module('app')
        .controller('LogoutCtrl', LogoutCtrl);

    LogoutCtrl.$inject = ['$location', 'UserRepository'];

    function LogoutCtrl($location, UserRepository) {
        UserRepository.clearUserData();
        $location.path('/login');
    }
})();