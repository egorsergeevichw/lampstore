using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using LampStore.Domain.Concrete;
using LampStore.Domain.Entities;
using LampStore.Domain.Enums;
using LampStore.Domain.Models;
using LampStore.Domain.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LampStore.UnitTests
{
    [TestClass]
    public class Tests
    {
        [TestMethod]
        public void CalculateMd5Hash()
        {
            const string input = "gbkfgbkf123";

            var md5 = MD5.Create();
            var inputBytes = Encoding.ASCII.GetBytes(input);
            var hash = md5.ComputeHash(inputBytes);

            var sb = new StringBuilder();

            foreach (var t in hash)
            {
                sb.Append(t.ToString("x2"));
            }

            var result = sb.ToString();
        }

        [TestMethod]
        public void SendMail()
        {
            const string from = "yodaskillme@gmail.com";
            const string password = "gbkfgbkf123";
            const string host = "smtp.gmail.com";
            const string to = "egor.vdovenko2018@yandex.ru";

            var body = $"<h3>Уважаемый <strong>пользователь!</strong></h3> " +
                       $"<p>Вам на почту отправлена ссылка на подтверждения вашего адреса.</p>" +
                       $"<a href=\"http://localhost:63228/auth/confirm\">Нажмите сдесь.</a>";
            var subject = "ПРОВЕРКА";

            var msg = new MailMessage(from, to, subject, body);

            msg.IsBodyHtml = true;

            var client = new SmtpClient(host, 587)
            {
                Credentials = new NetworkCredential(from, password),
                EnableSsl = true,
            };

            client.Send(msg);
        }


        [TestMethod]
        public void AddAdminUser()
        {
            const string fullName = "Egor Vdovenko";
            const string email = "yodaskillme@gmail.ru";
            const string password = "admin31071992classic";

            var context = new EfDbContext();

            var index = 1;

            if (context.Users.Count() != 0)
            {
                index = context.Users
                    .OrderByDescending(x => x.Index)
                    .ToList()
                    .First()
                    .Index + 1;
            }

            var cart = new CartEntity()
            {
                CartId = Guid.NewGuid()
            };

            var admin = new UserEntity()
            {
                UserId = Guid.NewGuid(),
                Index = index,
                FullName = fullName,
                CompanyName = "",
                Email = email,
                Cart = cart,
                ConfirmEmail = true,
                Password = AuthUtils.GetMd5Hash(password),
                Role = UserRolesEnum.Administrator
            };

            context.Users.Add(admin);
            context.SaveChanges();
        }

        [TestMethod]
        public void AddProducts()
        {
            var context = new EfDbContext();
            var random = new Random();
            var pictures = new List<string>();
            var types = Enum.GetValues(typeof(ProductTypeEnum));

            for (var i = 1; i < 9; i++)
            {
                var picture = context.Products.SingleOrDefault(x => x.Index == i).Picture;

                pictures.Add(picture);
            }

            for (var i = 0; i < 6; i++)
            {
                for (var j = 1; j <= 50; j++)
                {
                    var index = context.Products
                        .OrderByDescending(x => x.Index)
                        .ToList()
                        .First()
                        .Index + 1;

                    var productEntity = new ProductEntity()
                    {
                        ProductId = Guid.NewGuid(),
                        AddingDate = DateTime.Now,
                        Index = index,
                        Name = "Lamp" + j,
                        Description = "Description" + j,
                        Price = random.Next(0, 1000),
                        Picture = pictures[random.Next(0, 8)],
                        Count = random.Next(0, 1000),
                        Type = (ProductTypeEnum)types.GetValue(i),
                        Rating = random.Next(0, 1000)
                    };

                    context.Products.Add(productEntity);
                    context.SaveChanges();
                }
            }
        }

        [TestMethod]
        public void GetEnumModel()
        {
            var result = new List<EnumModel>();

            var values = Enum.GetValues(typeof(ProductTypeEnum));

            foreach (var value in values)
            {
                var model = new EnumModel((ProductTypeEnum)value);

                result.Add(model);
            }
        }
    }
}
