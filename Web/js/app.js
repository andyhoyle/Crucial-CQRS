(function(angular) {
  'use strict';

  var app = angular.module('qyz', [
      //app
      'Qyz.Category',
      'Qyz.Settings',
      'Qyz.Question',
      'Qyz.SignalR',
      //utils
      'Utils',
      //angular modules
      'ngResource',
      'ngRoute',
      'ngMaterial',
      'ngMdIcons'
  ]);
  app.value('signalRServer', 'http://localhost:41194/');
  app.config(['$locationProvider','$routeProvider', function($locationProvider, $routeProvider) {
        
      $routeProvider
          .otherwise({
              redirectTo: '/'
          });

      $locationProvider.html5Mode(true);
  }]);

  app.run(['$rootScope', '$route', function($rootScope, $route) {
      $rootScope.$on('$routeChangeSuccess', function (newVal, oldVal) {
          if (oldVal !== newVal) {
              document.title = $route.current.title;
              $rootScope.title = $route.current.title;
          }
      });
  }]);

  app.controller('AppCtrl', function ($scope, $timeout, $mdSidenav, $log) {
      $scope.toggleLeft = function () {
          $mdSidenav('left').toggle()
                            .then(function () {
                                $log.debug("toggle left is done");
                            });
      };
  });

  app.controller('LeftCtrl', function ($scope, $timeout, $mdSidenav, $log) {
      $scope.close = function () {
          $mdSidenav('left').close()
                            .then(function () {
                                $log.debug("close LEFT is done");
                            });
      };

      $scope.open = function () {
          $mdSidenav('left').open()
                            .then(function () {
                                $log.debug("open LEFT is done");
                            });
      };
  });

})(window.angular);


