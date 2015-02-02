(function(angular) {
  'use strict';
    
    angular.module('qyz.indexCtrl', [])
    
    .controller('indexCtrl', function($scope, Category) {
        $scope.categories = Category.query();// [{id:1,name:'Cat 1'},{id:2,name:'Cat 2'}];
        $scope.category = {};

        $scope.add = function () {
            Category.save($scope.category);
        	$scope.categories.push($scope.category);
        	$scope.category = {Name: ''};
        };
    });

})(window.angular);