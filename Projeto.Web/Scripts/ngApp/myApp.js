var usuarioApp = angular.module('usuarioApp', ['ngFileUpload', 'oitozero.ngSweetAlert']);
var fornecedorApp = angular.module('fornecedorApp', ['oitozero.ngSweetAlert', 'ngMask']);
var produtoApp = angular.module('produtoApp', ['oitozero.ngSweetAlert', 'ngFileUpload', 'ngMask']);
var myApp = angular.module('myApp', ['usuarioApp', 'fornecedorApp', 'produtoApp']);

usuarioApp.controller('loginCtrl', function ($scope, $http, SweetAlert) {

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
            $scope.msg = msg.data;
        });
    }

});

usuarioApp.controller('usuarioCtrl', ['$scope', 'Upload', '$http', 'SweetAlert', function ($scope, Upload, $http, SweetAlert) {

    $scope.msg = "";

    $scope.cadastrar = function (usuario, file) {

        Upload.upload({
            url: '/Usuario/CadastrarUsuario',
            data: { model: usuario, file: file }
        }).then(function (resp) {
            SweetAlert.swal("", resp.data, "success");  //mensagem
            $scope.usuario = "";                        //reseta os valores do usuario
            $scope.myForm.$setPristine();
        });
    }
}]);

usuarioApp.directive('passwordCheck', function () {
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

fornecedorApp.controller('fornecedorCtrl', function ($scope, $http, SweetAlert) {

    $scope.msg = "";

    $http.get("/Fornecedor/ConsultarFornecedor")
    .success(function (lista) {
        $scope.fornecedores = lista;
    })
    .error(function (msg) {
        $scope.msg = msg.data;
    });

    $scope.cadastrar = function (fornecedor) {
        $http.post("/Fornecedor/CadastrarFornecedor", { model: fornecedor })
        .success(function (msg) {
            SweetAlert.swal("", msg, "success");
            $scope.fornecedor = "";            //reseta os valores do usuario
            $scope.myForm.$setPristine();      //reseta o form "myForm"
        })
        .error(function (msg) {
            $scope.msg = msg.data;
        })
    }

    $scope.excluir = function (fornecedor) {

        SweetAlert.swal({
            title: "",
            text: "Deseja realmente excluir esse fornecedor?",
            type: "warning",
            showCancelButton: true,
            cancelButtonText: "Não",
            confirmButtonColor: "#DD6B55",
            confirmButtonText: "Sim",
            closeOnConfirm: false
        },
        function (isConfirm) {
            if (isConfirm) {
                $http.post("/Fornecedor/ExcluirFornecedor", { model: fornecedor })
                .success(function (msg) {
                    SweetAlert.swal("", msg, "success");
                    window.setTimeout(function () {
                        location.reload()
                    }, 3000)
                })
                .error(function (msg) {
                    $scope.msg = msg.data;
                });
            }
        });
    }
});

produtoApp.controller('produtoCtrl', function ($scope, $http, SweetAlert, Upload) {

    $scope.msg = "";
    $scope.display = "display:none";

    $scope.showDiv = function () {
        $scope.display = "display:block";
    };

    $scope.hideDiv = function () {
        $scope.display = "display:none";
    };

    $http.get("/Produto/DropDownFornecedor")
    .success(function (lista) {
        $scope.fornecedores = lista;
    })
    .error(function (msg) {
        $scope.msg = msg.data;
    });

    $http.get("/Produto/DropDownCategoria")
    .success(function (lista) {
        $scope.categorias = lista;
    })
    .error(function (msg) {
        $scope.msg = msg.data;
    });

    $scope.cadastrar = function (categoria) {
        $http.post("/Produto/CadastrarCategoria", { model: categoria })
        .success(function (msg) {
            SweetAlert.swal("", msg, "success");
            $scope.categoria = "";          //reseta os valores dos campos
            window.setTimeout(function () {
                location.reload()
            }, 3000)
        })
        .error(function (msg) {
            $scope.msg = msg.data;
        });
    };

    $scope.excluir = function (id) {
        $http.post("/Produto/ExcluirCategoria", { id: id })
        .success(function (msg) {
            SweetAlert.swal("", msg, "success");
            window.setTimeout(function () {
                location.reload()
            }, 3000)
        })
        .error(function (msg) {
            $scope.msg = msg.data;
        });
    };

    $scope.cadastrar = function (produto, file) {
        
        Upload.upload({
            url: '/Produto/CadastrarProduto',
            data: { model: produto, file: file }
        }).then(function (resp) {
            SweetAlert.swal("", resp.data, "success");  //mensagem
            $scope.produto = "";
            $scope.myForm.$setPristine();
        });
    };

    $http.get("/Produto/ConsultarProduto")
    .success(function (lista) {
        $scope.produtos = lista;
    })
    .error(function (msg) {
        $scope.msg = msg.data;
    });

});