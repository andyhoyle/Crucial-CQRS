(function (angular) {
    'use strict';

    var module = angular.module('Qyz.Question');

    module.factory('Question', ['$resource', function ($resource) {
        return $resource('http://localhost:41194/api/questions/:id', 
            { id: '@Id' }, 
            {
                update: {
                    method: 'PUT', params: { id: '@Id' }
                },
                delete: {
                    method: 'POST', params: { id: '@Id' }
                }
            }
        ); // Note the full endpoint address
    }]);

    module.factory('QuestionActions', function($resource) {
        return $resource('http://localhost:41194/api/questions/:id/:action/:actionId', { id: '@Id', action: '@action', actionId: '@actionId' });
    });

})(window.angular);