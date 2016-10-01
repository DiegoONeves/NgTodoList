(function () {
    'use strict';

    angular
        .module('app')
        .factory('AuthRepository', AuthRepository);

    AuthRepository.$inject = ['$http', '$rootScope', 'UserRepository'];

    function AuthRepository($http, $rootScope, UserRepository) {
        return {
            getToken: function (username, password) {
                var data = "grant_type=password&username=" + username + "&password=" + password;
                return $http.post("/api/security/token", data, { headers: { 'Content-Type': 'application/x-www-form-urlencoded' } });
            }
        };
    }
})();