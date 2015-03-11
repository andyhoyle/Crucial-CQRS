(function (angular) {
    'use strict';

    var module = angular.module('Qyz.Category');

    module.factory('Category', ['$resource', function ($resource) {
        return $resource('http://localhost:41194/api/categories/:id', { id: '@Id' }, {
            update: {
                method: 'PUT', params: { id: '@Id' }
            },
            delete: {
                method: 'POST', params: { id: '@Id' }
            }
        }); // Note the full endpoint address
    }]);

})(window.angular);