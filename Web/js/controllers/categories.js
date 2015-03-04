(function(angular) {
    'use strict';
    
    var module = angular.module('Qyz.Category', []);

    module.factory('Category', ['$resource', function ($resource) {
        return $resource('http://localhost:41194/api/categories/:id', { id: '@id' }); // Note the full endpoint address
    }]);
    
    module.controller('CategoryController', function($scope, Category) {
        $scope.categories = Category.query();
        $scope.category = {};

        $scope.add = function () {
            Category.save($scope.category);
        	$scope.categories.push($scope.category);
        	$scope.category = {};
        };

        $scope.edit = function (category) {
            $scope.category = category;
            Category.edit(category);
            $scope.category = {};
        };
    });

})(window.angular);