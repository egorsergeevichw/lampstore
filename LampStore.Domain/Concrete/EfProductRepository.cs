using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using LampStore.Domain.Abstract;
using LampStore.Domain.Entities;
using LampStore.Domain.Enums;
using LampStore.Domain.Models;
using LampStore.Domain.Models.Requests;

namespace LampStore.Domain.Concrete
{
    public class EfProductRepository : IProductRepository
    {
        private readonly EfDbContext _context = new EfDbContext();

        public List<ProductModel> GetFeaturedProducts()
        {
            var count = _context.Products
                .Count();

            if (count == 0)
                return new List<ProductModel>();

            var productsEntity = _context.Products
                .OrderBy(p => p.Rating)
                .Skip(count - 4)
                .ToList();

            var model = productsEntity.Select(x => new ProductModel(x)).ToList();

            return model;
        }

        public List<ProductModel> GetNewProducts()
        {
            var count = _context.Products
                .Count();

            if (count == 0)
                return new List<ProductModel>();

            var productsEntity = _context.Products
                .OrderBy(p => p.AddingDate)
                .Skip(count - 4)
                .ToList();

            var model = productsEntity.Select(x => new ProductModel(x)).ToList();

            return model;
        }

        public List<ProductModel> GetProducts(int page, string type)
        {
            var model = new List<ProductModel>();

            var productsEntity = _context.Products
                .OrderBy(p => p.Index)
                .ToList();

            if (type == "all")
            {
                model = productsEntity.Select(x => new ProductModel(x)).ToList();

                return model;
            }

            var en = (ProductTypeEnum)Enum.Parse(typeof(ProductTypeEnum), type);

            model = productsEntity.Where(x => x.Type == en).Select(x => new ProductModel(x)).ToList();

            return model;
        }

        public ProductModel GetProduct(Guid? productId)
        {
            var productEntity = _context.Products
                .SingleOrDefault(x => x.ProductId == productId);

            var model = new ProductModel(productEntity);

            return model;
        }

        public void SaveProduct(SaveProductRequest request)
        {
            var productEntity = _context.Products
                .SingleOrDefault(x => x.ProductId == new Guid(request.ProductId));

            if (productEntity != null)
            {
                productEntity.Picture = request.Picture;
                productEntity.Type = request.Type;
                productEntity.Count = request.Count;
                productEntity.Description = request.Description;
                productEntity.Name = request.Name;
                productEntity.Price = request.Price;

                _context.Products.AddOrUpdate(productEntity);
                _context.SaveChanges();

                return;
            }

            var index = 1;

            if (_context.Products.Count() != 0)
            {
                index = _context.Feedbacks
                    .OrderByDescending(x => x.Index)
                    .ToList()
                    .First()
                    .Index + 1;
            }

            var product = new ProductEntity()
            {
                ProductId = Guid.NewGuid(),
                AddingDate = DateTime.Now,
                Index = index,
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                Picture = request.Picture,
                Count = request.Count,
                Type = request.Type,
                Rating = 0
            };

            _context.Products.Add(product);
            _context.SaveChanges();
        }

        public void DeleteProduct(string productId)
        {
            var productEntity = _context.Products
                .SingleOrDefault(x => x.ProductId == new Guid(productId));

            _context.Products.Remove(productEntity);
            _context.SaveChanges();
        }

        public void SaveFeedbackMessage(FeedbackRequest request)
        {
            var index = 1;

            if (_context.Feedbacks.Count() != 0)
            {
                index = _context.Feedbacks
                    .OrderByDescending(x => x.Index)
                    .ToList()
                    .First()
                    .Index + 1;
            }

            var feedbackEntity = new FeedbackEntity()
            {
                FeedbackId = Guid.NewGuid(),
                Index = index,
                Name = request.Name,
                Email = request.Email,
                Message = request.Message,
                SendDate = DateTime.Now
            };

            _context.Feedbacks.Add(feedbackEntity);
            _context.SaveChanges();
        }
    }
}
