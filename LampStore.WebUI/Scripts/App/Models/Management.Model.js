(function (ng) {
    ng.module('LampStore.models')
        .factory('ProductManagementModel', ['serverModel', 'ManagementProxy',
            function (serverModel, proxy) {
                var model = {};

                if (serverModel.Product) {
                    model = {
                        productId: serverModel.Product.ProductId,
                        name: serverModel.Product.Name,
                        description: serverModel.Product.Description,
                        price: serverModel.Product.Price,
                        count: serverModel.Product.Count,
                        type: serverModel.Product.EnumType,
                        imageUrl: serverModel.Product.Picture,
                        image: serverModel.Product.GuidPicture,
                        types: serverModel.ProductsTypes
                    };
                } else {
                    model = {
                        products: serverModel.Products
                    }
                }              

                model.saveProduct = function (data, onSuccess, onError) {
                    proxy.saveProduct(data, onSuccess, onError);
                };

                model.deleteProduct = function (data, onSuccess, onError) {
                    proxy.deleteProduct(data, onSuccess, onError);
                };

                return model;
            }
        ]);
})(angular)