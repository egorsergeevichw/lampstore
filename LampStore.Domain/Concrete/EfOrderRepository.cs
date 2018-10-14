using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using LampStore.Domain.Abstract;
using LampStore.Domain.Entities;
using LampStore.Domain.Enums;
using LampStore.Domain.Models;
using LampStore.Domain.Models.Requests;

namespace LampStore.Domain.Concrete
{
    public class EfOrderRepository : IOrderRepository
    {
        private readonly EfDbContext _context = new EfDbContext();

        public CartModel AddToCart(AddToCartRequest request)
        {
            var productEntity = _context.Products
                .SingleOrDefault(x => x.ProductId == new Guid(request.ProductId));

            var userEntity = _context.Users
                .Include(x => x.Cart)
                .SingleOrDefault(x => x.UserId == new Guid(request.UserId));

            var cartEntity = _context.Carts
                .Include(x => x.CartItems)
                .Include(x => x.CartItems.Select(y => y.Product))
                .SingleOrDefault(x => x.User.UserId == userEntity.UserId);

            var cartItemEntity = cartEntity.CartItems
                .SingleOrDefault(x => x.Product.ProductId == productEntity.ProductId);

            var model = new CartModel();

            if (cartItemEntity != null)
            {
                cartItemEntity.Count += request.Count;
                cartItemEntity.Price += productEntity.Price * request.Count;
                cartEntity.TotalPrice += productEntity.Price * request.Count;

                _context.CartItems.AddOrUpdate(cartItemEntity);
                _context.SaveChanges();

                model = new CartModel(cartEntity);

                return model;
            }

            var cartItem = new CartItemEntity()
            {
                CartItemId = Guid.NewGuid(),
                Product = productEntity,
                Cart = userEntity.Cart,
                Count = request.Count,
                Price = request.Count * productEntity.Price
            };

            cartEntity.TotalPrice += cartItem.Price;

            _context.CartItems.Add(cartItem);
            _context.Carts.AddOrUpdate(cartEntity);
            _context.SaveChanges();

            model = new CartModel(cartEntity);

            return model;
        }

        public void DeleteCartItem(string cartItemId)
        {
            var cartItemEntity = _context.CartItems
                .SingleOrDefault(x => x.CartItemId == new Guid(cartItemId));

            var cartEntity = _context.Carts
                .Include(x => x.CartItems)
                .ToList()
                .SingleOrDefault(x => x.CartItems.Contains(cartItemEntity));

            cartEntity.TotalPrice -= cartItemEntity.Price;

            _context.Carts.AddOrUpdate(cartEntity);
            _context.CartItems.Remove(cartItemEntity);
            _context.SaveChanges();
        }

        public CartModel GetCart(Guid userId)
        {
            var userEntity = _context.Users
                .SingleOrDefault(x => x.UserId == userId);

            var cartEntity = _context.Carts
                .Include(x => x.CartItems)
                .Include(x => x.CartItems.Select(y => y.Product))
                .SingleOrDefault(x => x.User.UserId == userEntity.UserId);

            var model = new CartModel(cartEntity);

            return model;
        }

        public OrderModel MakeOrder(Guid userId, MakeOrderRequest request)
        {
            var userEntity = _context.Users
                .SingleOrDefault(x => x.UserId == userId);

            var cartEntity = _context.Carts
                .Include(x => x.CartItems)
                .Include(x => x.CartItems.Select(y => y.Product))
                .SingleOrDefault(x => x.User.UserId == userEntity.UserId);

            var index = 1;

            if (_context.Orders.Count() != 0)
            {
                index = _context.Orders
                    .OrderByDescending(x => x.Index)
                    .ToList()
                    .First()
                    .Index + 1;
            }

            var orderEntity = new OrderEntity()
            {
                OrderId = Guid.NewGuid(),
                Index = index,
                TotalPrice = cartEntity.TotalPrice,
                PurchaseDate = DateTime.Now,
                Status = OrderStatusEnum.InProcess,
                User = userEntity
            };

            var orderItemsEntities = new List<OrderItemEntity>();

            foreach (var item in cartEntity.CartItems)
            {
                var orderItemEntity = new OrderItemEntity()
                {
                    OrderItemId = Guid.NewGuid(),
                    Count = item.Count,
                    Price = item.Price,
                    Product = item.Product,
                    Order = orderEntity,
                };

                orderItemsEntities.Add(orderItemEntity);
            }

            orderEntity.OrderItems = orderItemsEntities;
            cartEntity.TotalPrice = decimal.Zero;
            userEntity.Address = request.Address;
            userEntity.Inn = request.Inn;
            userEntity.CompanyName = request.CompanyName;
            userEntity.PhoneNumber = request.PhoneNumber;

            _context.Orders.Add(orderEntity);
            _context.OrderItems.AddRange(orderItemsEntities);
            _context.CartItems.RemoveRange(cartEntity.CartItems);
            _context.Carts.AddOrUpdate(cartEntity);
            _context.Users.AddOrUpdate(userEntity);
            _context.SaveChanges();

            var model = new OrderModel(orderEntity);

            return model;
        }

        public List<OrderModel> GetOrders(Guid? userId)
        {
            var ordersEntities = _context.Orders
                .Include(x => x.OrderItems)
                .Include(x => x.OrderItems.Select(y => y.Product))
                .Include(x => x.User)
                .ToList();

            var model = ordersEntities.ConvertAll(x => new OrderModel(x));

            if (userId != null)
            {
                model = model.Where(x => x.User.UserId == userId).ToList();
            }

            return model;
        }

        public OrderModel GetOrder(Guid orderId)
        {
            var orderEntity = _context.Orders
                .Include(x => x.OrderItems)
                .Include(x => x.OrderItems.Select(y => y.Product))
                .Include(x => x.User)
                .SingleOrDefault(x => x.OrderId == orderId);

            var model = new OrderModel(orderEntity);

            return model;
        }
    }
}

