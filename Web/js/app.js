(function(angular) {
  'use strict';

  var module = angular.module('Qyz.Category', []);

  var app = angular.module('qyz', ['Qyz.Category', 'Qyz.Settings', 'Qyz.SignalR', 'Utils', 'ngResource', 'ngRoute', 'ngMaterial'])
    .value('signalRServer', 'http://localhost:41194/')
    .config(['$locationProvider','$routeProvider', function($locationProvider, $routeProvider) {
        
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
            })
            .when('/index.html', {
                redirectTo: '/'
            })
            .when('/settings', {
                controller: 'SettingsController',
                templateUrl: 'templates/settings.html',
                title: 'Settings'
            })
            .otherwise({
                redirectTo: '/'
            });

        $locationProvider.html5Mode(true);
    }])

    .run(['$rootScope', '$route', function($rootScope, $route) {
        $rootScope.$on('$routeChangeSuccess', function(newVal, oldVal) {
        if (oldVal !== newVal) {
            document.title = $route.current.title;
            $rootScope.title = $route.current.title;
        }
    });
}]);

})(window.angular);


