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

        $scope.add = function () {
            Category.save($scope.category);
            outputLog("After save");
        	$scope.category = {};
        };

        $scope.startEdit = function (category) {
            $scope.category = category;
            $scope.buttonMode = "Edit";
        };

        $scope.commitEdit = function () {
            console.log("Editing:");
            console.log($scope.category);
            outputLog("Before update");
            Category.update($scope.category);
            $scope.category = {};
            $scope.buttonMode = "Add";
        }

        var categoryEventHub = signalRHubProxy(signalRHubProxy.defaultServer, 'categoryEventHub', { logging: true });

        categoryEventHub.on('userCategoryCreated', function (category) {
            outputLog("Before Created Event");
            $scope.categories.push(category);
            outputLog("After Created Event");
            var x = categoryEventHub.connection.id;
        });

        categoryEventHub.on('userCategoryNameChanged', function (category) {
            outputLog("Before Updated Event");
            $scope.categories[angularIndexOf($scope.categories, category)] = category;
            outputLog("After Updated Event");
        });

        categoryEventHub.start();
    }]);

})(window.angular);