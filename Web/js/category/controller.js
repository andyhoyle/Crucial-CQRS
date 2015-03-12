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
        /////////////////////////////

        $scope.category = {};
        $scope.buttonMode = "Add";

        $scope.add = showAdd;
        $scope.edit = startEdit;
        $scope.delete = deleteCategory;
        $scope.toggle = toggle;
        $scope.addIcon = "add";
        $scope.getDeleteIcon = getDeleteIcon;

        /////////////////////////////

        $scope.categories = Category.query();

        function getDeleteIcon(cat) {
            if (cat.isDeleted) {
                return "done";
            } else {
                return "delete";
            }
        }

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
            $scope.addIcon = "more_horiz";
            showDialog('Add Category', add);
        };

        function startEdit(category) {
            $scope.addIcon = "more_horiz";
            $scope.category = category;
            showDialog('Edit Category', commitEdit);
        };

        function add(cat) {
            $scope.addIcon = "add";
            Category.save(cat);
            $scope.category = {};
        };

        function commitEdit(cat) {
            $scope.addIcon = "add";
            Category.update(cat);
            $scope.category = {};
        }

        function deleteCategory(category) {
            category.isDeleted = true;
            Category.delete(category);
        }

        function cancel() {
            $scope.addIcon = "add";
            $scope.category = {};
        }

        function toggle(id) {
            var idx = utils.indexOf($scope.categories, { Id: id });
            $scope.categories[idx].enabled = !$scope.categories[idx].enabled;
        }

        var categoryEventHub = signalRHubProxy(signalRHubProxy.defaultServer, 'categoryEventHub', { logging: true });

        categoryEventHub.on('userCategoryCreated', function (category) {
            category.deleteIcon="delete";
            $scope.categories.push(category);
        });

        categoryEventHub.on('userCategoryNameChanged', function (category) {
            $scope.categories[utils.indexOf($scope.categories, category)] = category;
        });

        categoryEventHub.on('userCategoryDeleted', function (id) {
            var idx = utils.indexOf($scope.categories, { Id: id });
            $scope.categories.splice(idx, 1);
        });
        
        categoryEventHub.start();
    }]);

})(window.angular);