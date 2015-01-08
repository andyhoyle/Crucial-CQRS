
(function(angular) {
  'use strict';

    angular.module('qyz.categoriesFactory', [])
	
	.factory('Category', ['$resource', function($resource) {
  		return $resource('http://localhost:41194/api/categories/:id', { id: '@id' }); // Note the full endpoint address
	}]);

})(window.angular);