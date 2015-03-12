var module = angular.module('Qyz.Category', ['ngRoute']);

module.config(['$routeProvider', function ($routeProvider) {
    $routeProvider
        .when('/', {
            controller: 'CategoryController',
            templateUrl: 'templates/category/category.html',
            title: 'Categories'
        })
        .when('/categories/new', {
            controller: 'CategoryController',
            templateUrl: 'templates/category/new.html',
            title: 'Categories'
        });
}]);