(function (angular) {
    'use strict';

    var module = angular.module('Qyz.Question');

    function DialogController($scope, $mdDialog, question, title) {
        $scope.question = question;
        $scope.title = title;

        $scope.cancel = function () {
            $mdDialog.cancel();
        };

        $scope.save = function (output) {
            $mdDialog.hide(output);
        }
    }

    module.controller('QuestionController', ['$scope', 'Question', 'signalRHubProxy', 'UtilsService', '$mdDialog', function ($scope, Question, signalRHubProxy, utils, $mdDialog) {
        /////////////////////////////

        $scope.question = {};
        $scope.buttonMode = "Add";

        $scope.add = showAdd;
        $scope.edit = startEdit;
        $scope.delete = deleteQuestion;
        $scope.toggle = toggle;
        $scope.addIcon = "add";
        $scope.getDeleteIcon = getDeleteIcon;

        /////////////////////////////

        $scope.questions = Question.query();

        function getDeleteIcon(ques) {
            if (ques.isDeleted) {
                return "done";
            } else {
                return "delete";
            }
        }

        function showDialog(title, callback) {
            $mdDialog.show({
                controller: DialogController,
                templateUrl: '/templates/question/new.html',
                locals: { question: $scope.question, title: title }
            })
            .then(function (item) {
                callback(item);
            }, function () {
                cancel()
            });
        }

        function showAdd() {
            $scope.addIcon = "more_horiz";
            showDialog('Add Question', add);
        };

        function startEdit(question) {
            $scope.addIcon = "more_horiz";
            $scope.question = question;
            showDialog('Edit Question', commitEdit);
        };

        function add(ques) {
            $scope.addIcon = "add";
            Question.save(ques);
            $scope.question = {};
        };

        function commitEdit(ques) {
            $scope.addIcon = "add";
            Question.update(ques);
            $scope.question = {};
        }

        function deleteQuestion(question) {
            question.isDeleted = true;
            Question.delete(question);
        }

        function cancel() {
            $scope.addIcon = "add";
            $scope.question = {};
        }

        function toggle(id) {
            var idx = utils.indexOf($scope.questions, { Id: id });
            $scope.questions[idx].enabled = !$scope.questions[idx].enabled;
        }

        var questionEventHub = signalRHubProxy(signalRHubProxy.defaultServer, 'questionEventHub', { logging: true });

        questionEventHub.on('questionCreated', function (question) {
            question.deleteIcon = "delete";
            $scope.questions.push(question);
        });

        questionEventHub.on('questionTextChangedCreated', function (question) {
            question.CreatedDate = $scope.questions[utils.indexOf($scope.questions, question)].CreatedDate;
            $scope.questions[utils.indexOf($scope.questions, question)] = question;
        });

        questionEventHub.on('questionDeleted', function (id) {
            var idx = utils.indexOf($scope.questions, { Id: id });
            $scope.questions.splice(idx, 1);
        });
        
        questionEventHub.start();
    }]);

})(window.angular);