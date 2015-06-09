'use strict';

var module = angular.module('Qyz.SignalR', []);

module.service('signalRHubProxy', ['$rootScope', 'signalRServer',
    function ($rootScope, signalRServer) {
        function signalRHubProxyFactory(serverUrl, hubName, startOptions) {
            var connection = $.hubConnection(signalRServer);
            var proxy = connection.createHubProxy(hubName);

            connection.logging = startOptions.logging;

            return {
                running: function () {
                    return connection;
                },
                stop: function () {
                    connection.stop(true, true);
                },
                start: function () {
                    connection.start(startOptions)
                        .done(function () {
                            if (startOptions.logging) {
                                console.log("Hub started:" + hubName);
                            }
                        })
                        .fail(function (error) {
                            console.log(error);
                        });
                },
                on: function (eventName, callback) {
                    proxy.on(eventName, function (result) {
                        if (startOptions.logging) {
                            console.log(result);
                        }
                        $rootScope.$apply(function () {
                            if (callback) {
                                callback(result);
                            }
                        });
                    });
                },
                off: function (eventName, callback) {
                    proxy.off(eventName, function (result) {
                        $rootScope.$apply(function () {
                            if (callback) {
                                callback(result);
                            }
                        });
                    });
                },
                invoke: function (methodName, callback) {
                    proxy.invoke(methodName)
                        .done(function (result) {
                            $rootScope.$apply(function () {
                                if (callback) {
                                    callback(result);
                                }
                            });
                        });
                }
            };
        };

        return signalRHubProxyFactory;
    }]);