// adds the resource to umbraco.resources module:
angular.module('umbraco.resources').factory('crolowFormResource', function ($q, $http, umbRequestHelper) {
    return {
        getComments: function (application, status, page, pagesize) {
            var config = {
                params: {
                    application: application,
                    status: status,
                    page: page,
                    pagesize: pagesize
                }
            }
            return umbRequestHelper.resourcePromise($http.get("/umbraco/backoffice/Api/CrolowFormBoApi/GetComments", config), "Failed to retrieve comments");
        },
        getApplications: function () {
            return umbRequestHelper.resourcePromise($http.get("/umbraco/backoffice/Api/CrolowFormBoApi/GetApplications"), "Failed to retrieve applications");
        },
        updateComment: function (data) {
            return umbRequestHelper.resourcePromise($http.post("/umbraco/backoffice/Api/CrolowFormBoApi/UpdateComment", data), "Failed to retrieve applications");
        },
        deleteComment: function (data) {
            return umbRequestHelper.resourcePromise($http.post("/umbraco/backoffice/Api/CrolowFormBoApi/DeleteComment", data), "Failed to retrieve applications");
        }


    };
});