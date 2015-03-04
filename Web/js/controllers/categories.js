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
    
    module.controller('CategoryController', function($scope, Category) {
        $scope.categories = Category.query();
        $scope.category = {};
        $scope.buttonMode = "Add";

        $scope.add = function () {
            Category.save($scope.category);
        	$scope.categories.push($scope.category);
        	$scope.category = {};
        };

        $scope.startEdit = function (category) {
            $scope.category = category;
            $scope.buttonMode = "Edit";
        };

        $scope.commitEdit = function () {
            console.log($scope.category);
            Category.update($scope.category);
            $scope.category = {};
            $scope.buttonMode = "Add";
        }
    });

})(window.angular);