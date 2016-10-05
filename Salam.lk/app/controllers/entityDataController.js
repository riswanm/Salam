
townApp.controller('entityDataSearchController', function ($scope, $routeParams, $location, entityDataService) {

    $scope.searchText = '';
    $scope.pageSize = 2;
    $scope.type = 0;
    var fields;
    
    function search() {
        var entityType = getEntityType($routeParams);
        if (fields) {
            if (fields.type != entityType) {
                entityDataService.getFields({ type: entityType }).success(function (data) {
                    fields = { type: entityType, columns: data };
                    loadData();
                    }
                );
            }
        }
      
    }

    function loadData() {
        var searchParams = { searchText: $scope.searchText, pageIndex: getPageIndex($routeParams), pageSize: $scope.pageSize, type: getEntityType($routeParams) }

        entityDataService.search(searchParams).success(function (data) {

            $scope.models = data;

        }).error(function (err) {
            alert('Error');
        });
    }

    $scope.loadMore = function () {
        $location.path('/' + getEntityType($routeParams) + '/' + (parseInt(getPageIndex($routeParams)) + 1));
      // search();
    }
    
    $scope.startSearch = function () {
        $location.path('/1');
    };

   
    search();

    function getPageIndex(routeParam) {
        return (!routeParam.pageno || routeParam.pageno == 0) ? 1 : routeParam.pageno;
    }
   
    function getEntityType(routeParam) {
        return (!routeParam.type || routeParam.type == 0) ? 0 : routeParam.type;
    }

    
});