using System;
using LampStore.Domain.Entities;
using LampStore.Domain.Utils;

namespace LampStore.Domain.Models
{
    public class ProductModel
    {
        public ProductModel() {}

        public ProductModel(ProductEntity entity)
        {
            ProductId = entity.ProductId;
            Picture = $"/Files/{entity.Picture}.png";
            GuidPicture = entity.Picture;
            Name = entity.Name;
            Description = entity.Description;
            Count = entity.Count;
            Price = entity.Price;
            Rating = entity.Rating;
            Type = EnumUtils.GetEnumDescription(entity.Type);
            EnumType = (int)entity.Type;
        }

        public Guid ProductId { get; set; }
        public string Picture { get; set; }
        public string GuidPicture { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Count { get; set; }
        public decimal Price { get; set; }
        public int? Rating { get; set; }
        public string Type { get; set; }
        public int EnumType { get; set; }
    }
}
