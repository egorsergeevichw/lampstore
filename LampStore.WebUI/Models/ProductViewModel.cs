using System;
using System.Collections.Generic;
using LampStore.Domain.Enums;
using LampStore.Domain.Models;
using LampStore.Domain.Utils;

namespace LampStore.WebUI.Models
{
    public class ProductViewModel
    {
        public ProductViewModel(ProductModel product)
        {
            Product = product ?? new ProductModel();
            ProductsTypes = EnumUtils.GetEnumModel(typeof(ProductTypeEnum));
        }

        public List<EnumModel> ProductsTypes { get; set; }
        public ProductModel Product { get; set; }
    }
}