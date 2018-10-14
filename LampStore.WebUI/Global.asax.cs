using System;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using LampStore.Domain.Concrete;
using LampStore.Domain.Utils;
using LampStore.WebUI.App_Start;

namespace LampStore.WebUI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
            if (HttpContext.Current.User == null)
                return;
            if (!HttpContext.Current.User.Identity.IsAuthenticated)
                return;
            if (!(HttpContext.Current.User.Identity is FormsIdentity))
                return;

            var id = (FormsIdentity) HttpContext.Current.User.Identity;

            var context = new EfDbContext();
            var user = context.Users.SingleOrDefault(x => x.UserId == new Guid(id.Ticket.Name));

            if (user == null)
            {
                HttpContext.Current.User = null;
            }
            else
            {
                var roles = new[] {user.Role.ToString()};

                HttpContext.Current.Items[AuthUtils.CurrentUserKey] = user;
                HttpContext.Current.User = new GenericPrincipal(id, roles);

                var x = User.IsInRole("Administrator");
            }
        }
    }
}
