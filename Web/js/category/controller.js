(function (angular) {
    'use strict';

    var module = angular.module('Qyz.Category');

    function DialogController($scope, $mdDialog, category, title) {
        $scope.category = category;
        $scope.title = title;

        $scope.cancel = function () {
            $mdDialog.cancel();
        };

        $scope.save = function (output) {
            $mdDialog.hide(output);
        }
    }

    module.controller('CategoryController', ['$scope', 'Category', 'signalRHubProxy', 'UtilsService', '$mdDialog', function ($scope, Category, signalRHubProxy, utils, $mdDialog) {
        $scope.categories = Category.query();
        $scope.category = {};
        $scope.buttonMode = "Add";

        $scope.add = showAdd;
        $scope.edit = startEdit;
        $scope.delete = deleteCategory;
        $scope.toggle = toggle;

        function showDialog(title, callback) {
            $mdDialog.show({
                controller: DialogController,
                templateUrl: '/templates/category/new.html',
                locals: { category: $scope.category, title: title }
            })
            .then(function (item) {
                callback(item);
            }, function () {
                cancel()
            });
        }

        function showAdd() {
            showDialog('Add Category', add);
        };

        function startEdit(category) {
            $scope.category = category;
            showDialog('Edit Category', commitEdit);
        };

        function add(cat) {
            Category.save(cat);
            $scope.category = {};
        };

        function commitEdit(cat) {
            Category.update(cat);
            $scope.category = {};
        }

        function deleteCategory(id) {
            var idx = utils.indexOf($scope.categories, { Id: id });
            var cat = $scope.categories[idx];
            Category.delete(cat);
            $scope.categories.splice(idx, 1);
        }

        function cancel() {
            $scope.category = {};
        }

        function toggle(id) {
            var idx = utils.indexOf($scope.categories, { Id: id });
            $scope.categories[idx].enabled = !$scope.categories[idx].enabled;
        }

        var categoryEventHub = signalRHubProxy(signalRHubProxy.defaultServer, 'categoryEventHub', { logging: true });

        categoryEventHub.on('userCategoryCreated', function (category) {
            $scope.categories.push(category);
        });

        categoryEventHub.on('userCategoryNameChanged', function (category) {
            $scope.categories[utils.indexOf($scope.categories, category)] = category;
        });

        categoryEventHub.start();
    }]);

})(window.angular);