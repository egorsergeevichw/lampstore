(function (ng) {
    var app = ng.module('LampStoreApp',
        [
            'LampStore.controllers',
            'LampStore.directives',
            'LampStore.models',
            'LampStore.core',

            'mgcrea.ngStrap.helpers.dimensions',
            'mgcrea.ngStrap.modal',
            'mgcrea.ngStrap.tooltip',
            'mgcrea.ngStrap.popover',
            'mgcrea.ngStrap.core',

            'ngAnimate',
            'toastr'
        ]);

    app.config(['$httpProvider', function ($httpProvider) {
        $httpProvider.defaults.headers.common['Accept'] = 'application/json, text/plain';
        $httpProvider.defaults.headers.common['Content-Type'] = 'application/json; charset=utf-8';
    }]);

    ng.module('LampStore.controllers', []);
    ng.module('LampStore.directives', []);
    ng.module('LampStore.models', []);
    ng.module('LampStore.core', []);
})(angular)