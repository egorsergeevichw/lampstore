(function (ng) {
    ng.module('LampStore.models')
        .factory('ProductModel', ['serverModel', 'ContentProxy',
            function (serverModel, proxy) {
                var model = {
                    products: serverModel.Products,
                    productsCount: serverModel.ProductsCount,
                    productsPage: serverModel.ProductsPage,
                    productsType: serverModel.ProductsType
                };

                return model;
            }
        ])
        .factory('FeedbackModel', ['serverModel', 'ContentProxy',
            function (serverModel, proxy) {
                var model = {};

                model.sendMessage = function (data, onSuccess, onError) {
                    proxy.sendMessage(data, onSuccess, onError);
                };

                return model;
            }
        ]);
})(angular)