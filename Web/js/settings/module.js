(function (angular) {
    'use strict';

    var module = angular.module('Qyz.Settings', ['ngRoute']);

    module.config(['$routeProvider', function ($routeProvider) {
        $routeProvider
            .when('/settings', {
                controller: 'SettingsController',
                templateUrl: 'templates/settings.html',
                title: 'Settings'
            });
    }]);

})(window.angular);