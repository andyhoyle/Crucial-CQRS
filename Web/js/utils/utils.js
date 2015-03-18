var utils = angular.module('Utils', []);

utils.service('UtilsService', function () {
    var outputLog = function (message, coll) {
        console.log(message);
        angular.forEach(coll, function (u, i) {
            console.log(i, u);
        });
    };

    var angularIndexOf = function (arr, obj) {
        for (var i = 0; i < arr.length; i++) {
            if (arr[i].Id === obj.Id) {
                return i;
            }
        };
        return -1;
    };

    return {
        log: outputLog,
        indexOf: angularIndexOf
    };
});

utils.filter('fromNow', function () {
    return function (date) {
        return moment(date).fromNow();
    }
});