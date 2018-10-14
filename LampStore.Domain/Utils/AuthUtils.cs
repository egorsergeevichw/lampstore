using System;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Security;
using LampStore.Domain.Entities;
using LampStore.Domain.Models;

namespace LampStore.Domain.Utils
{
    public static class AuthUtils
    {
        public static string CurrentUserKey = "CurrentUserKey";

        public static UserModel GetCurrentUser()
        {
            if (HttpContext.Current == null)
            {
                throw new InvalidOperationException();
            }

            if (!HttpContext.Current.User.Identity.IsAuthenticated)
            {
                return new UserModel();
            }

            var userEntity = HttpContext.Current.Items[CurrentUserKey] as UserEntity;

            return new UserModel(userEntity);
        }

        public static string GetMd5Hash(string input)
        {
            const string salt = "Co8t4b6kHs";

            var md5 = MD5.Create();
            var inputBytes = Encoding.ASCII.GetBytes(input);
            var hash = md5.ComputeHash(inputBytes);

            var sb = new StringBuilder();

            foreach (var t in hash)
            {
                sb.Append(t.ToString("x2"));
            }

            var result = sb + salt;
            return result;
        }

        public static HttpCookie SetCookie(Guid userId)
        {
            var tiket = new FormsAuthenticationTicket(2, userId.ToString(), DateTime.Now, DateTime.Now.AddDays(1), true, string.Empty);
            var encTicket = FormsAuthentication.Encrypt(tiket);

            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket)
            {
                Expires = DateTime.Now.AddDays(1)
            };

            return cookie;
        }
    }
}
