angular.module('townApp')
  .service('entityDataService', function entityDataService($http) {
      this.search = function (searchParam) {
          return $http.get('api/Entities/?searchText=' + searchParam.searchText + '&pageSize='
              + searchParam.pageSize
              + '&currentPage=' + searchParam.pageIndex + '&type=' + searchParam.type);
      },
      this.getFields = function (searchParams) {
          return $http.get('api/DataFields/?type=' + searchParam.type);
      }


  });