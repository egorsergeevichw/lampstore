(function (ng) {
    ng.module('LampStore.models')
        .factory('AuthModel', ['serverModel', 'AuthProxy',
            function(serverModel, proxy) {
                var model = {
                };

                model.login = function(data, onSuccess, onError) {
                    proxy.login(data, onSuccess, onError);
                };

                model.logout = function (data, onSuccess, onError) {
                    proxy.logout(data, onSuccess, onError);
                };

                model.registration = function (data, onSuccess, onError) {
                    proxy.registration(data, onSuccess, onError);
                };

                return model;
            }
        ]);
})(angular)