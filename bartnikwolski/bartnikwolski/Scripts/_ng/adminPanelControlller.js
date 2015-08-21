angular.module('App')
.controller('adminPanel', function ($scope, $http) {

    $http({
        method: 'GET',
        url: '/Admin/GetProducts'
    }).success(function (data) {
        $scope.Products = data;
    })
    .error(function () {
        alert('Wystąpił problem przy pobieraniu danych.')
    });

    $scope.options = [];
    $scope.PageTitle = null;
    $scope.pSource = "";

    $scope.delete = function () {
        var productId = this.product.ProductId;
        var response = $http({
            method: "post",
            url: "/Admin/Delete",
            params: {
                id: JSON.stringify(productId)
            }
        });
        $.each($scope.Products, function (i) {
            if ($scope.Products[i].ProductId === productId) {
                $scope.Products.splice(i, 1);
                return false;
            }
        });
    }

    $scope.save = function () {
        var product = this.product;
        product.PictureSource = $scope.pSource;
        var response = $http({
            method: "post",
            url: "/Admin/SaveChanges",
            data: JSON.stringify(product),
            dataType: "json"
        })
        this.product.editMode = !this.product.editMode;
    };

    $scope.toggleEdit = function () {
        $scope.pSource = this.product.PictureSource;
        this.product.editMode = !this.product.editMode;
    }

    $scope.uploadFile = function (files) {
        var fd = new FormData();
        fd.append("file", files[0]);
        var source = this.product.PictureSource;

        var response = $http({
            method: "post",
            url: "/Admin/DeleteFile",
            params: {
                source: source
            }
        }).success(function () {
            $http.post('/Admin/SaveFile', fd, {
                withCredentials: true,
                headers: { 'Content-Type': undefined },
                transformRequest: angular.identity
            }).success(function (data) {
                $scope.pSource = data;
            }).error('..damn!...');
        });
    };
});