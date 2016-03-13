var myApp = angular.module('myApp', ['ngFileUpload', 'oitozero.ngSweetAlert']);

myApp.controller('loginCtrl', function ($scope, $http,  SweetAlert) {

    $scope.msg = "";

    $scope.login = function (usuario) {
        $http.post("/Usuario/AutenticarUsuario", { model: usuario })
        .success(function (json) {
            if (json.isRedirect) {
                window.location.href = json.redirectUrl;
            }
            else {
                SweetAlert.swal("", json, "error");
            }
        })
        .error(function (msg) {
            $scope.msg = msg;
        });
    }

});

myApp.controller('usuarioCtrl', ['$scope', 'Upload', '$http', 'SweetAlert', function ($scope, Upload, $http, SweetAlert) {

    $scope.msg = "";

    $scope.cadastrar = function (usuario, file) {

        Upload.upload({
            url: '/Usuario/CadastrarUsuario',
            data: { model: usuario, file: file }
        }).then(function (resp) {
            SweetAlert.swal("", resp.data, "success");         //mensagem
            $scope.usuario = "";            //reseta os valores do usuario
            $scope.myForm.$setPristine();   //reseta o form "myForm"
        });
    }
}]);

myApp.directive('passwordCheck', function () {
    return {
        require: 'ngModel',
        link: function (scope, elem, attrs, model) {

            scope.$watch(attrs.passwordCheck, function (value) {
                if (model.$viewValue !== undefined && model.$viewValue !== '') {
                    model.$setValidity('passwordCheck', value === model.$viewValue);
                }
            });
            model.$parsers.push(function (value) {
                if (value === undefined || value === '') {
                    model.$setValidity('passwordCheck', true);
                    return value;
                }
                var isValid = value === scope.$eval(attrs.passwordCheck);
                model.$setValidity('passwordCheck', isValid);
                return isValid ? value : undefined;
            });
        }
    };
});

