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

        $scope.$on("$routeChangeSuccess", bindEvents);
        $scope.$on("$destroy", unbindEvents);

        /////////////////////////////

        $scope.categories = Category.query();

        var categoryEventHub = null;

        /////////////////////////////

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

        function unbindEvents() {
            categoryEventHub.off('userCategoryCreated', categoryCreated);
            categoryEventHub.off('userCategoryNameChanged', categoryNameChanged);
            categoryEventHub.off('userCategoryDeleted', categoryDeleted);
            categoryEventHub.stop();
        }

        /// Event Handlers

        function categoryCreated(category) {
            category.deleteIcon="delete";
            $scope.categories.push(category);
        };

        function categoryNameChanged(category) {
            category.CreatedDate = $scope.categories[utils.indexOf($scope.categories, category)].CreatedDate
            $scope.categories[utils.indexOf($scope.categories, category)] = category;
        }

        function categoryDeleted(id) {
            var idx = utils.indexOf($scope.categories, { Id: id });
            $scope.categories.splice(idx, 1);
        }

        /// Hook up Event Handlers
        function bindEvents() {
            categoryEventHub = signalRHubProxy(signalRHubProxy.defaultServer, 'categoryEventHub', { logging: false });

            categoryEventHub.on('userCategoryCreated', categoryCreated);
            categoryEventHub.on('userCategoryNameChanged', categoryNameChanged);
            categoryEventHub.on('userCategoryDeleted', categoryDeleted);

            categoryEventHub.start();
        }
    }]);

})(window.angular);