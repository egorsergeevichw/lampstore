(function (ng) {
    ng.module('LampStore.core')
        .factory('BaseProxy', ['$rootScope', '$http',
            function ($rootScope, $http) {
                var proxy = {},
                    finaly = function () {
                        $rootScope.$emit('onRequestComplete');
                    };

                proxy.post = function (url, data, onSuccess, onError) {
                    $rootScope.$emit('onRequestStart');

                    var promise = $http.post(url, data, this.requestConfig);
                    promise = promise.then(onSuccess, onError);

                    return promise.finally(finaly);
                };

                return proxy;
            }
        ])
        .factory('AuthProxy', ['BaseProxy',
            function (baseProxy) {
                var proxy = {};

                proxy.login = function (data, onSuccess, onError) {
                    baseProxy.post('/auth/login', data, onSuccess, onError);
                };

                proxy.logout = function (data, onSuccess, onError) {
                    baseProxy.post('/auth/logout', data, onSuccess, onError);
                };

                proxy.registration = function (data, onSuccess, onError) {
                    baseProxy.post('/auth/registration', data, onSuccess, onError);
                };

                return proxy;
            }
        ])
        .factory('ManagementProxy', ['BaseProxy',
            function (baseProxy) {
                var proxy = {};

                proxy.saveProduct = function (data, onSuccess, onError) {
                    baseProxy.post('/management/saveProduct', data, onSuccess, onError);
                };

                proxy.deleteProduct = function (data, onSuccess, onError) {
                    baseProxy.post('/management/deleteProduct', data, onSuccess, onError);
                };

                return proxy;
            }
        ])
        .factory('ContentProxy', ['BaseProxy',
            function (baseProxy) {
                var proxy = {};

                proxy.sendMessage = function (data, onSuccess, onError) {
                    baseProxy.post('/content/feedback', data, onSuccess, onError);
                };

                return proxy;
            }
        ])
        .factory('OrderProxy', ['BaseProxy',
            function (baseProxy) {
                var proxy = {};

                proxy.addToCart = function (data, onSuccess, onError) {
                    baseProxy.post('/order/addToCart', data, onSuccess, onError);
                };

                proxy.deleteCartItem = function (data, onSuccess, onError) {
                    baseProxy.post('/order/deleteCartItem', data, onSuccess, onError);
                };

                proxy.makeOrder = function (data, onSuccess, onError) {
                    baseProxy.post('/order/make', data, onSuccess, onError);
                };

                return proxy;
            }
        ]);
})(angular);