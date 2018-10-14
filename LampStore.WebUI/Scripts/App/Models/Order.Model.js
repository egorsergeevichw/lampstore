(function (ng) {
    ng.module('LampStore.models')
        .factory('CartModel', ['serverModel', 'OrderProxy',
            function (serverModel, proxy) {
                var model = angular.copy(serverModel);

                model.addToCart = function (data, onSuccess, onError) {
                    proxy.addToCart(data, onSuccess, onError);
                };

                model.deleteCartItem = function (data, onSuccess, onError) {
                    proxy.deleteCartItem(data, onSuccess, onError);
                };

                return model;
            }
        ])
        .factory('OrderModel', ['serverModel', 'OrderProxy',
            function (serverModel, proxy) {
                var model = {
                    fullName: serverModel.User.FullName.split(" "),
                    address: serverModel.User.Address,
                    companyName: serverModel.User.CompanyName,
                    email: serverModel.User.Email,
                    phoneNumber: serverModel.User.PhoneNumber,
                    inn: serverModel.User.Inn
                }

                model.makeOrder = function (data, onSuccess, onError) {
                    proxy.makeOrder(data, onSuccess, onError);
                };

                return model;
            }
        ]);
})(angular)