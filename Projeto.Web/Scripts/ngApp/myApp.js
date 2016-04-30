var usuarioApp = angular.module('usuarioApp', ['ngFileUpload', 'oitozero.ngSweetAlert']);
var fornecedorApp = angular.module('fornecedorApp', ['oitozero.ngSweetAlert', 'ngMask']);
var produtoApp = angular.module('produtoApp', ['oitozero.ngSweetAlert', 'ngFileUpload', 'ngMask']);
var clienteApp = angular.module('clienteApp', []);
var myApp = angular.module('myApp', ['usuarioApp', 'fornecedorApp', 'produtoApp', 'clienteApp']);

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

    $scope.consultar = function () {
        $http.get("/Fornecedor/ConsultarFornecedor")
        .success(function (lista) {
            $scope.fornecedores = lista;
        })
        .error(function (msg) {
            $scope.msg = msg.data;
        })
    }

    $scope.consultar();

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

    $scope.editar = function (id) {
        $http.post("/Fornecedor/EditarFornecedor", { id: id })
        .success(function (result) {
            $scope.fornecedor = result;
        })
        .error(function (msg) {
            $scope.msg = msg.data;
        });
    };

    $scope.atualizar = function (fornecedor) {
        $http.post("/Fornecedor/AtualizarFornecedor", { model: fornecedor })
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

    $scope.excluir = function (id) {

        SweetAlert.swal({
            title: "Atenção!",
            text: "Todos os produtos ligados à ele também serão excluídos.",
            type: "warning",
            showCancelButton: true,
            cancelButtonText: "Não",
            confirmButtonColor: "#DD6B55",
            confirmButtonText: "Sim",
            closeOnConfirm: false
        },
        function (isConfirm) {
            if (isConfirm) {
                $http.post("/Fornecedor/ExcluirFornecedor", { id: id })
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

    $http.get("/Produto/ConsultarProduto")
    .success(function (lista) {
        $scope.produtos = lista;
    })
    .error(function (msg) {
        $scope.msg = msg.data;
    });

    $scope.cadastrarCategoria = function (categoria) {
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

    $scope.excluirCategoria = function (id) {
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

    $scope.editar = function (id) {
        $http.post("/Produto/EditarProduto", { id: id })
        .success(function (result) {
            $scope.produto = result;
            $scope.display = "display:block";
        })
        .error(function (msg) {
            $scope.msg = msg.data;
        });
    };

    $scope.atualizar = function (produto) {
        $http.post("/Produto/AtualizarProduto", { model: produto })
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

    $scope.upload = function (file, id) {

        Upload.upload({
            url: '/Produto/AtualizarFoto',
            data: { file: file, id: id }
        }).then(function (resp) {
            SweetAlert.swal("", resp.data, "success");  //mensagem
            window.setTimeout(function () {
                location.reload()
            }, 3000)
        });
    };

    $scope.cancelar = function () {
        $scope.display = "display:none";
    };

    $scope.excluir = function (id) {

        SweetAlert.swal({
            title: "Atenção!",
            text: "Deseja realmente excluir esse produto?",
            type: "warning",
            showCancelButton: true,
            cancelButtonText: "Não",
            confirmButtonColor: "#DD6B55",
            confirmButtonText: "Sim",
            closeOnConfirm: false
        },
        function (isConfirm) {
            if (isConfirm) {
                $http.post("/Produto/ExcluirProduto", { id: id })
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
    };
});

clienteApp.controller('clienteCtrl', function ($scope, $http) {

    $http.get("/Cliente/Consultar")
    .success(function (lista) {
        $scope.produtos = lista;
    })
    .error(function (msg) {
        $scope.msg = msg.data;
    });

    $scope.consultar = function (categoria) {
        $http.post("/Cliente/Consultar", { categoria: categoria })
        .success(function (lista) {
            $scope.display = "display:block";
            $scope.produtos = lista;
        })
        .error(function (msg) {
            $scope.msg = msg.data;
        });
    };

});