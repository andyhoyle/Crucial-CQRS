(function(angular) {
  'use strict';

    var app = angular.module('qyz', ['qyz.indexCtrl', 'qyz.settingsCtrl', 'qyz.categoriesFactory', 'ngResource', 'ngRoute', 'ng-polymer-elements'])
   
    .config(['$locationProvider','$routeProvider', function($locationProvider, $routeProvider) {
        
        $routeProvider
            .when('/', {
                controller: 'indexCtrl',
                templateUrl: 'templates/category.html',
                title: 'Categories'
            })
            .when('/index.html', {
                redirectTo: '/'
            })
            .when('/settings', {
                controller: 'settingsCtrl',
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