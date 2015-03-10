(function(angular) {
    'use strict';
    
    var module = angular.module('Qyz.Category', []);

    module.factory('Category', ['$resource', function ($resource) {
        return $resource('http://localhost:41194/api/categories/:id', { id: '@Id' }, {
            update: {
                method: 'PUT', params: { id: '@Id' }
            }
        }); // Note the full endpoint address
    }]);
 
    module.controller('CategoryController', ['$scope', 'Category', 'signalRHubProxy', function ($scope, Category, signalRHubProxy) {
        $scope.categories = Category.query();
        $scope.category = {};
        $scope.buttonMode = "Add";

        $scope.add = add;
        $scope.startEdit = startEdit;
        $scope.commitEdit = commitEdit;

        var outputLog = function (message) {
            console.log(message);
            angular.forEach($scope.categories, function (u, i) {
                console.log(i, u);
            });
        };

        var angularIndexOf = function (arr, obj) {
            for (var i = 0; i < arr.length; i++) {
                if (arr[i].Id === obj.Id) {
                    return i;
                }
            };
            return -1;
        }

        function add() {
            Category.save($scope.category);
        	$scope.category = {};
        };

        function startEdit(category) {
            $scope.category = category;
            $scope.buttonMode = "Edit";
        };

        function commitEdit() {
            Category.update($scope.category);
            $scope.category = {};
            $scope.buttonMode = "Add";
        }

        var categoryEventHub = signalRHubProxy(signalRHubProxy.defaultServer, 'categoryEventHub', { logging: true });

        categoryEventHub.on('userCategoryCreated', function (category) {
            $scope.categories.push(category);
            var x = categoryEventHub.connection.id;
        });

        categoryEventHub.on('userCategoryNameChanged', function (category) {
            $scope.categories[angularIndexOf($scope.categories, category)] = category;
        });

        categoryEventHub.start();
    }]);

})(window.angular);