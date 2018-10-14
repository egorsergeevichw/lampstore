using System.Web.Optimization;

namespace LampStore.WebUI.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/bundles/styles").Include(

                // Libs

                "~/Content/Libs/Bootstrap/css/bootstrap.css",
                "~/Content/Libs/Bootstrap/css/bootstrap-theme.css",
                "~/Content/Libs/FontAwesome/css/font-awesome.css",
                "~/Content/Libs/Toastr/angular-toastr.css",
                "~/Content/Libs/ImageCrop/image-crop.css",

                // Project

                "~/Content/Project/Common/footer.css",
                "~/Content/Project/Common/header.css",
                "~/Content/Project/Common/main.css",

                "~/Content/Project/Management/products.css",
                "~/Content/Project/Management/orders.css",

                "~/Content/Project/Auth/auth.css",

                "~/Content/Project/User/orders.css",

                "~/Content/Project/Content/about.css",
                "~/Content/Project/Content/feedback.css",
                "~/Content/Project/Content/products.css",

                "~/Content/Project/Order/cart.css",
                "~/Content/Project/Order/order.css"
            ));

            bundles.Add(new ScriptBundle("~/bundles/scripts").Include(

                // Libs

                "~/Scripts/Libs/Angular/angular.js",
                "~/Scripts/Libs/Angular/angular-animate.js",
                "~/Scripts/Libs/JQuery/jquery.js",
                "~/Scripts/Libs/Bootstrap/bootstrap.js",
                "~/Scripts/Libs/Toastr/angular-toastr.tpls.js",

                // Angular Strap

                "~/Scripts/Libs/AngularStrap/core.js",
                "~/Scripts/Libs/AngularStrap/dimensions.js",
                "~/Scripts/Libs/AngularStrap/modal.js",
                "~/Scripts/Libs/AngularStrap/tooltip.js",
                "~/Scripts/Libs/AngularStrap/popover.js",

                // App

                "~/Scripts/App/App.js",
                "~/Scripts/App/Core/Proxy.js",

                "~/Scripts/App/Controllers/Management.Controller.js",
                "~/Scripts/App/Controllers/Auth.Controller.js",
                "~/Scripts/App/Controllers/Content.Controller.js",
                "~/Scripts/App/Controllers/Order.Controller.js",

                "~/Scripts/App/Models/Management.Model.js",
                "~/Scripts/App/Models/Auth.Model.js",
                "~/Scripts/App/Models/Content.Model.js",
                "~/Scripts/App/Models/Order.Model.js",

                "~/Scripts/App/Directives/ImageInput.Directive.js",
                "~/Scripts/App/Directives/BtnRequestState.Directive.js",
                "~/Scripts/App/Directives/ImageCrop.Directive.js"
            ));
        }
    }
}