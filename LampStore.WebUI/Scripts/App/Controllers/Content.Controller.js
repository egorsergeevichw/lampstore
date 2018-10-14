(function (ng) {
    ng.module('LampStore.controllers')
        .controller('NavbarController', ['$scope', '$rootScope', '$modal', '$window', 'AuthModel', 'toastr',
            function ($scope, $rootScope, $modal, $window, model, toastr) {
                $scope.showModal = function () {
                    var modal = $modal({
                        scope: $scope,
                        templateUrl: "loginModal",
                        backdrop: 'static'
                    });

                    modal.$promise.then(modal.show);
                };

                $rootScope.$on('cartItemsCountChanged', function (event, data) {
                    if (data.count === -1) {
                        $scope.viewModel.cartItemsCount--;
                        return;
                    };
                    $scope.viewModel.cartItemsCount = data.count;
                });

                $scope.logout = function () {
                    var onSuccess = function () {
                        $window.location.reload();
                    },
                        onError = function () {
                            toastr.error("Please, try later", "Error!");
                        };

                    model.logout(null, onSuccess, onError);
                }

                $scope.search = function () {
                    if ($scope.searchPage) {
                        $window.location = "/management/products/all/1?section=1&query=" + encodeURIComponent($scope.viewModel.query);
                        return;
                    }
                    $window.location = "/content/products/all/1?section=1&query=" + encodeURIComponent($scope.viewModel.query);
                }
            }
        ])
        .controller('ProductController', ['$scope', '$rootScope', '$modal', '$window', '$document', 'ProductModel', 'CartModel', 'toastr',
            function ($scope, $rootScope, $modal, $window, $document, productModel, cartModel, toastr) {
                $scope.viewModel = angular.copy(productModel);

                $scope.isAdded = false;
                var modal;

                $scope.showModal = function (productId) {
                    $scope.viewModel.product = this.viewModel.products[productId];

                    modal = $modal({
                        scope: $scope,
                        templateUrl: "productModal",
                        backdrop: 'static'
                    });

                    modal.$promise.then(modal.show);
                };

                $scope.continue = function () {
                    modal.hide();
                    $scope.isAdded = false;
                }

                $scope.addToCart = function (userId, productId) {
                    var data = {
                        userId: userId,
                        productId: productId,
                        count: $scope.viewModel.count
                    },
                        onSuccess = function (response) {
                            $scope.isAdded = true;
                            $scope.viewModel.totalPrice = response.data.totalPrice;
                            $scope.viewModel.cartItemsCount = response.data.cartItemsCount;
                            $rootScope.$emit('cartItemsCountChanged', { count: response.data.cartItemsCount });
                        },
                        onError = function (response) {
                            if (response.status === 400) {
                                $window.location.assign($window.location.origin + "/auth/login");
                                return;
                            }
                            toastr.error("Please, try later", "Error!");
                        };

                    cartModel.addToCart(data, onSuccess, onError);
                }
            }
        ])
        .controller('FeedbackController', ['$scope', 'FeedbackModel', 'toastr',
            function ($scope, model, toastr) {
                $scope.sendMessage = function () {
                    $scope.isSuccess = false;

                    var data = {
                        name: $scope.viewModel.name,
                        email: $scope.viewModel.email,
                        message: $scope.viewModel.message
                    },
                        onSuccess = function () {
                            $scope.isSuccess = true;
                        },
                        onError = function () {
                            toastr.error("Please, try later", "Error!");
                        };

                    model.sendMessage(data, onSuccess, onError);
                }
            }
        ]);
})(angular)