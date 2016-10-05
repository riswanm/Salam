var townApp = angular.module('townApp', ['ngRoute']);

townApp.config(function ($routeProvider) {

    $routeProvider.when('/:type/:pageno', {
        templateUrl: 'pages/entityDataSearch.html',
        controller: 'entityDataSearchController'
    });
        
    
});

townApp.controller('mainController', function ($scope) {
});

townApp.directive("fileread", [function () {
    return {
        scope: {
            fileread: "="
        },
        link: function (scope, element, attributes) {
            element.bind("change", function (changeEvent) {
                var reader = new FileReader();
                reader.onload = function (loadEvent) {
                    scope.$apply(function () {
                        scope.fileread = loadEvent.target.result;
                    });
                }
                reader.readAsDataURL(changeEvent.target.files[0]);
            });
        }
    }
}]);




