(function (ng) {
    ng.module('LampStore.controllers')
        .controller('CartController', ['$scope', '$rootScope', 'CartModel', 'toastr',
            function ($scope, $rootScope, model, toastr) {
                $scope.viewModel = {
                    cartItems: model.Cart.CartItems,
                    totalPrice: model.Cart.TotalPrice
                };

                $scope.viewModel.cartItems.forEach(function (item) {
                    item.isDeleted = false;
                });

                $scope.changeTotalPrice = function (index) {
                    $scope.viewModel.totalPrice -= $scope.viewModel.cartItems[index].Price;
                    $scope.viewModel.totalPrice += $scope.viewModel.cartItems[index].Product.Price * $scope.viewModel.cartItems[index].Count;
                    $scope.viewModel.cartItems[index].Price = $scope.viewModel.cartItems[index].Product.Price * $scope.viewModel.cartItems[index].Count;
                };

                $scope.deleteCartItem = function (cartItemId, index) {
                    var data = {
                        cartItemId: cartItemId
                    },
                        onSuccess = function () {
                            toastr.success("Item was deleted", "Success!");
                            $scope.viewModel.cartItems[index].isDeleted = true;
                            $scope.viewModel.totalPrice -= $scope.viewModel.cartItems[index].Price;
                            $rootScope.$emit('cartItemsCountChanged', { count: -1 });
                        },
                        onError = function () {
                            toastr.error("Please, try later", "Error!");
                        };

                    model.deleteCartItem(data, onSuccess, onError);
                };
            }
        ])
        .controller('OrderController', ['$scope', '$rootScope', 'OrderModel', 'toastr',
            function ($scope, $rootScope, model, toastr) {
                $scope.viewModel = angular.copy(model);

                $scope.viewModel.firstName = $scope.viewModel.fullName[0];
                $scope.viewModel.lastName = $scope.viewModel.fullName[1];

                $scope.isOrderInfo = false;
                $scope.isSuccess = false;

                $scope.validateInfo = function () {
                    $scope.isOrderInfo = true;
                };

                $scope.makeOrder = function () {
                    var data = angular.copy($scope.viewModel),
                        onSuccess = function () {
                            $scope.isSuccess = true;
                            $rootScope.$emit('cartItemsCountChanged', { count: 0 });
                        },
                        onError = function () {
                            toastr.error("Please, try later", "Error!");
                        };

                    model.makeOrder(data, onSuccess, onError);
                };
            }
        ]);
})(angular)