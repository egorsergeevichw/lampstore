(function (ng) {
    ng.module('LampStore.directives')
    .directive('btnRequestState', ['$rootScope',
        function ($rootScope) {
            return {
                restrict: 'AC',
                link: function (scope, element, attrs) {

                    var target;

                    element.on('click',
                        function () {
                            target = element;
                        });

                    $rootScope.$on('onRequestStart', function () {
                        if (element === target) {
                            angular.element(element).addClass('disabled');
                            angular.element(element).attr('disabled', 'true');
                        }
                    });

                    $rootScope.$on('onRequestComplete', function () {
                        if (element === target) {
                            angular.element(element).removeClass('disabled');
                            angular.element(element).removeAttr('disabled');
                        }
                    });
                }
            };
        }]);
})(angular)