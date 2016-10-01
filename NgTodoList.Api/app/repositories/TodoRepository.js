(function () {
    'use strict';

    angular
        .module('app')
        .factory('TodoRepository', TodoRepository);

    TodoRepository.$inject = ['$http', '$rootScope', '$location'];

    function TodoRepository($http, $rootScope, $location) {
        return {
            getTodos: function () {
                return $http.get("/api/todos", { headers: { 'Authorization': 'Bearer ' + localStorage.getItem('token') } });
            },
            sync: function (todos) {
                return $http.post("/api/todos", todos, { headers: { 'Authorization': 'Bearer ' + localStorage.getItem('token') } });
            },
        };
    }
})();