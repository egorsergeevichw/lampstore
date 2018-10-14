using System;
using System.Data.Entity.Migrations;
using System.Linq;
using LampStore.Domain.Abstract;
using LampStore.Domain.Entities;
using LampStore.Domain.Enums;
using LampStore.Domain.Models;
using LampStore.Domain.Models.Requests;
using LampStore.Domain.Utils;

namespace LampStore.Domain.Concrete
{
    public class EfUserRepository : IUserRepository
    {
        private readonly EfDbContext _context = new EfDbContext();

        public UserModel GetUser(Guid userId)
        {
            var userEntity = _context.Users
                .SingleOrDefault(x => x.UserId == userId);

            if (userEntity == null)
            {
                return new UserModel();
            }

            var model = new UserModel(userEntity);

            return model;
        }

        public Guid AddUser(RegistrationRequest request)
        {
            var userEntity = _context.Users
                .SingleOrDefault(x => x.Email == request.Email);

            if (userEntity != null)
            {
                return Guid.Empty;
            }

            var index = 1;

            if (_context.Users.Count() != 0)
            {
                index = _context.Users
                    .OrderByDescending(x => x.Index)
                    .ToList()
                    .First()
                    .Index + 1;
            }

            var cart = new CartEntity()
            {
                CartId = Guid.NewGuid()
            };

            var user = new UserEntity()
            {
                UserId = Guid.NewGuid(),
                Index = index,
                FullName = request.FullName,
                CompanyName = request.CompanyName,
                Email = request.Email,
                Cart = cart,
                Password = AuthUtils.GetMd5Hash(request.Password),
                Role = UserRolesEnum.Customer
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            var id = user.UserId;

            return id;
        }

        public Guid LoginUser(LoginRequest request)
        {
            var password = AuthUtils.GetMd5Hash(request.Password);

            var userEntity = _context.Users
                .SingleOrDefault(x => x.Email == request.Email);

            if (userEntity == null || userEntity.Password != password || !userEntity.ConfirmEmail)
            {
                return Guid.Empty;
            }

            var id = userEntity.UserId;

            return id;
        }

        public void ConfirmEmail(Guid? userId)
        {
            var userEntity = _context.Users
                .SingleOrDefault(x => x.UserId == userId);

            if (userEntity != null)
            {
                userEntity.ConfirmEmail = true;

                _context.Users.AddOrUpdate(userEntity);
                _context.SaveChanges();
            }
        }
    }
}
