(function (ng) {
    ng.module('LampStore.controllers')
        .controller('LoginController', ['$scope', '$window', 'AuthModel', 'toastr',
            function ($scope, $window, model, toastr) {
                $scope.login = function () {
                    var data = {
                        email: $scope.viewModel.email,
                        password: $scope.viewModel.password
                    },
                        onSuccess = function () {
                            $window.location = "/content/products/all/1?section=1";
                        },
                        onError = function () {
                            toastr.error("Invalid login or password", "Error!");
                        };

                    model.login(data, onSuccess, onError);
                }
            }
        ])
        .controller('RegistrationController', ['$scope', 'AuthModel', 'toastr',
            function ($scope, model, toastr) {
                $scope.viewModel = {
                    company: ""
                }

                $scope.invalid = false;
                $scope.isSuccess = false;

                $scope.check = function () {
                    if ($scope.viewModel.password !== $scope.viewModel.rePassword && $scope.viewModel.rePassword !== undefined) {
                        $scope.invalid = true;
                    } else {
                        $scope.invalid = false;
                    }
                }

                $scope.registration = function () {
                    debugger;
                    if ($scope.viewModel.password !== $scope.viewModel.rePassword) {
                        toastr.error("You entered two different passwords", "Error!");
                        return;
                    }

                    var data = {
                        fullName: $scope.viewModel.firstName + " " + $scope.viewModel.lastName,
                        companyName: $scope.viewModel.companyName,
                        email: $scope.viewModel.email,
                        password: $scope.viewModel.password
                    },
                    onSuccess = function () {
                        $scope.isSuccess = true;
                    },
                    onError = function (response) {
                        if (response.status === 409) {
                            toastr.error("This mailbox is already registered", "Error!");
                            return;
                        };
                    };

                    model.registration(data, onSuccess, onError);
                }
            }
        ]);
})(angular)