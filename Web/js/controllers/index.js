(function(angular) {
  'use strict';
    
    angular.module('qyz.indexCtrl', [])
    
    .controller('indexCtrl', function($scope, Category) {
        $scope.categories = Category.query();// [{id:1,name:'Cat 1'},{id:2,name:'Cat 2'}];
        $scope.categoryName = 'Add new category';
        $scope.add = function(a) {
        	Category.save({name: a});
        	$scope.categories.push( {id: 3, name: a });
        	$scope.categoryName = '';
        };
    });

})(window.angular);