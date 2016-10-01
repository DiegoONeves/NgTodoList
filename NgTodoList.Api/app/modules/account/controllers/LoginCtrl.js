(function () {
    'use strict';

    angular
        .module('app')
        .controller('LoginCtrl', LoginCtrl);

    LoginCtrl.$inject = ['$rootScope', '$scope', '$location', 'AuthRepository', 'UserRepository'];

    function LoginCtrl($rootScope, $scope, $location, AuthRepository, UserRepository) {

        $scope.login = {
            email: '',
            password: ''
        }

        $scope.authenticate = function () {
            var promise = AuthRepository.getToken($scope.login.email, $scope.login.password).then(
                   function (result) {
                       localStorage.setItem('token', result.data.access_token);
                       $rootScope.isAuthorized = true;
                       UserRepository.setCurrentProfile();
                       $location.path('/');
                   },
                   function (error) {
                       toastr.error(error.data.error_description, 'Falha na autenticação');
                   });
        }
    }
})();