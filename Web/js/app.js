(function(angular) {
  'use strict';

  var app = angular.module('qyz', ['Qyz.Category', 'Qyz.Settings', 'ngResource', 'ngRoute', 'ng-polymer-elements'])
   
    .config(['$locationProvider','$routeProvider', function($locationProvider, $routeProvider) {
        
        $routeProvider
            .when('/', {
                controller: 'CategoryController',
                templateUrl: 'templates/category.html',
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