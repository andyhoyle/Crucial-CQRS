var module = angular.module('Qyz.Question', ['ngRoute']);

module.config(['$routeProvider', function ($routeProvider) {
    $routeProvider
        .when('/questions', {
            controller: 'QuestionController',
            templateUrl: 'templates/question/question.html',
            title: 'Questions'
        })
        .when('/questions/new', {
            controller: 'QuestionController',
            templateUrl: 'templates/question/new.html',
            title: 'Questions'
        });
}]);