using System;
using LampStore.Domain.Entities;
using System.Collections.Generic;
using LampStore.Domain.Models;
using LampStore.Domain.Models.Requests;

namespace LampStore.Domain.Abstract
{
    public interface IProductRepository
    {
        List<ProductModel> GetFeaturedProducts();
        List<ProductModel> GetNewProducts();
        List<ProductModel> GetProducts(int page, string type);
        ProductModel GetProduct(Guid? productId);
        void SaveProduct(SaveProductRequest request);
        void DeleteProduct(string productId);
        void SaveFeedbackMessage(FeedbackRequest request);
    }
}
