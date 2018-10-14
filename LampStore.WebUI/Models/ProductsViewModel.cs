using System.Collections.Generic;
using LampStore.Domain.Enums;
using LampStore.Domain.Models;
using LampStore.Domain.Utils;

namespace LampStore.WebUI.Models
{
    public class ProductsViewModel
    {
        public ProductsViewModel(List<ProductModel> products, int productsCount, string productsType, int productsPage, int productsSection)
        {
            Products = products;
            ProductsCount = productsCount;
            ProductsType = productsType;
            ProductsPage = productsPage;
            ProductsSection = productsSection;
            ProductsTypes = EnumUtils.GetEnumModel(typeof(ProductTypeEnum));    
        }

        public List<ProductModel> Products { get; set; }
        public int ProductsCount { get; set; }
        public string ProductsType { get; set; }
        public int ProductsPage { get; set; }
        public int ProductsSection { get; set; }
        public List<EnumModel> ProductsTypes { get; set; }

    }
}