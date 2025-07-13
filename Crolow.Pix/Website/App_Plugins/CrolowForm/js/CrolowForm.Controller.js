angular.module("umbraco").controller("CrolowFormController", function ($scope, crolowFormResource) {
    var vm = this;
    vm.currentApplication = "";
    vm.currentStatus = 0;
    vm.page = 0;
    vm.pageSize = 20;
    vm.statuses = [
        { id: 0, value: "Awaiting" },
        { id: 1, value: "In progress" },
        { id: 2, value: "Accepted" },
        { id: 3, value: "Rejected" },
    ];

    init();


    function init() {
        crolowFormResource.getApplications().then(function (response) {
            vm.applications = response;
            crolowFormResource.getComments("", 0, 0, 20).then(function (response) {
                vm.data = response;
            });
        });
    };

    vm.reset = function () {
        crolowFormResource.getComments(vm.currentApplication, vm.currentStatus, vm.page, vm.pageSize).then(function (response) {
            vm.data = response;
        });
    };

    vm.changeStatus = function (status, item) {
        item.status = status;
        crolowFormResource.updateComment(item).then(function (response) {
            vm.reset();
        });
    };

    vm.deleteComment = function (item) {
        crolowFormResource.deleteComment(item).then(function (response) {
            vm.reset();
        });
    };
});