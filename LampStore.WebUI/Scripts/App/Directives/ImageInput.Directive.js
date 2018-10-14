(function (ng) {
	ng.module('LampStore.directives')
        .directive('imageInput', ['SharedImage',
            function (sharedImage) {
        	    return {
        		    restrict: 'EA',
        		    link: function (scope, element, attr) {

        			    var input = angular.element("input[type='file']");

        			    element.click(function () {
        				    input.click();
        			    });

        			    var handleFileSelect = function (evt) {
        				    var file = evt.currentTarget.files[0];
        				    var reader = new FileReader();

        				    reader.onload = function (evt) {
        					    sharedImage.rawImage = evt.target.result;
        					    scope.showModal();
        				    };
        				    reader.readAsDataURL(file);
        			    };

        			    input.click(function () {
        				    this.value = null;
        			    });

        			    input.change(handleFileSelect);
        		    }
        	    };
            }
        ]);
})(angular)