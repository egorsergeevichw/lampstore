(function (ng) {
    ng.module('LampStore.controllers', ['ngImgCrop'])
        .factory('SharedImage', [
            function () {
                var obj = {
                    rawImage: '',
                    croppedImage: ''
                };

                return obj;
            }
        ])
        .controller('EditProductController', ['$scope', '$http', '$modal', '$rootScope', 'ProductManagementModel', 'toastr',
            function ($scope, $http, $modal, $rootScope, model, toastr) {
                $scope.viewModel = angular.copy(model);

                $rootScope.$on('onImageLoadComplete',
                    function (event, args) {
                        $scope.viewModel.imageUrl = "~/Files/" + args.id + ".png";
                        $scope.viewModel.image = args.id;
                    });

                $scope.showModal = function () {
                    var modal = $modal({
                        scope: $scope,
                        templateUrl: "imageModal",
                        backdrop: 'static'
                    });

                    modal.$promise.then(modal.show);
                };

                $scope.saveProduct = function () {
                    var data = {
                        productId: $scope.viewModel.productId,
                        name: $scope.viewModel.name,
                        description: $scope.viewModel.description,
                        price: $scope.viewModel.price,
                        picture: $scope.viewModel.image,
                        count: $scope.viewModel.count,
                        type: $scope.viewModel.type
                    },
                    onSuccess = function () {
                        toastr.success('Product was saved', "Success!");
                    },
                    onError = function () {
                        toastr.error('Please, try later', "Error!");
                    };

                    model.saveProduct(data, onSuccess, onError);
                }
            }
        ])
        .controller('ManagementProductsController', ['$scope', 'ProductManagementModel', 'toastr',
            function ($scope, model, toastr) {
                $scope.viewModel.products.forEach(function (product) {
                    product.isDeleted = false;
                });

                $scope.deleteProduct = function (productId, index) {
                    var data = {
                            productId: productId
                        },
                        onSuccess = function() {
                            toastr.success('Product was deleted', "Success!");
                            $scope.viewModel.products[index].isDeleted = true;
                        },
                        onError = function() {
                            toastr.error('Please, try later', "Error!");
                        };

                    model.deleteProduct(data, onSuccess, onError);
                };
            }
        ])
        .controller('ImageCropController', ['$scope', '$rootScope', '$http', 'SharedImage', 'toastr',
            function ($scope, $rootScope, $http, sharedImage, toastr) {
                $scope.viewModel = {
                    rawImage: '',
                    croppedImage: ''
                };

                $scope.viewModel.rawImage = sharedImage.rawImage;

                $scope.saveImage = function () {
                    var data = {
                        base64Image: $scope.viewModel.croppedImage
                    };

                    $http.post('/management/uploadProductImage', data)
                        .then(function (response) {
                            $rootScope.$emit('onImageLoadComplete', { id: response.data.id, path: response.data.url });
                            $scope.$hide();
                            toastr.success('Upload success', "Success!");
                        },
                        function (response) {
                            toastr.error('Upload error', "Error!");
                        });
                };
            }
        ]);
})(angular)