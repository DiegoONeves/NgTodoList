(function () {
    'use strict';

    angular
        .module('app')
        .controller('HomeCtrl', HomeCtrl);

    HomeCtrl.$inject = ['$scope', '$http', 'TodoRepository'];

    function HomeCtrl($scope, $http, TodoRepository) {
        $scope.todo = {
            id: 0,
            text: '',
            done: false
        }

        $scope.todos = [];

        Load();

        $scope.remaining = function () {
            var count = 0;
            angular.forEach($scope.todos, function (todo) {
                count += todo.done ? 0 : 1;
            });
            return count;
        };

        $scope.save = function (todo) {
            if (todo.id == 0) {
                Save($scope.todo);
            } else {
                Edit();
            }
            New();
        }

        $scope.new = function () {
            New();
        }

        $scope.edit = function (todo) {
            $scope.todo = todo;
        }

        $scope.delete = function (todo) {
            var index = $scope.todos.indexOf(todo)
            $scope.todos.splice(index, 1);
        }

        $scope.sync = function () {
            Sync();
        }

        function Save(item) {
            item.id = $scope.todos.length + 1;
            $scope.todos.push(item);
        }

        function New() {
            $scope.todo = {
                id: 0,
                text: '',
                done: false
            }
        }

        function Load() {
            if (navigator.onLine) {
                ReadCloud();
            } else {
                ReadLocal();
            }
        }

        function Sync() {
            if (navigator.onLine) {
                SaveCloud();
            } else {
                SaveLocal();
            }
        }

        function ReadLocal() {
            $scope.todos = angular.fromJson(localStorage.getItem("todos"));
            toastr.info('Informações carregadas do disco local...', 'Tudo certo!');
        }

        function SaveLocal() {
            localStorage.setItem("todos", angular.toJson($scope.todos));
            toastr.warning('Informações persistidas localmente...', 'Sincronizando');
        }

        function ReadCloud() {
            TodoRepository
                .getTodos()
                .then(
                    function (result) {
                        $scope.todos = result.data;
                    },
                    function (error) {
                        toastr.error(error.data, "Falha na requisição");
                    });
        }

        function SaveCloud() {
            TodoRepository
                .sync($scope.todos)
                .then(
                    function (result) {
                        toastr.info(result.data, "Sincronização completa")
                    },
                    function (error) {
                        toastr.error(error.data, "Falha na requisição");
                    });
        }
    }
})();