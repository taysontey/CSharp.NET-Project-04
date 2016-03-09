var myApp = angular.module('myApp', ['ngFileUpload']);

myApp.controller('loginCtrl', function ($scope, $http) {

    $scope.msg = "";

    $scope.login = function (usuario) {
        $http.post("/Usuario/AutenticarUsuario", { model: usuario })
        .success(function (json) {
            if (json.isRedirect) {
                window.location.href = json.redirectUrl;
            }
            else {
                $scope.msg = json;
            }
        })
        .error(function (msg) {
            $scope.msg = msg;
        });
    }

});

myApp.controller('usuarioCtrl', ['$scope', 'Upload', '$http', function ($scope, Upload, $http) {

    $scope.msg = "";

    $scope.cadastrar = function (usuario, file) {

        Upload.upload({
            url: '/Usuario/CadastrarUsuario',
            data: { model: usuario, file: file }
        }).then(function (resp) {
            $scope.msg = resp.data;
        });
    }
}]);

