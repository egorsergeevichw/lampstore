using System.Collections.Generic;
using System.Linq;
using LampStore.Domain.Models;

namespace LampStore.WebUI.Models
{
    public class AboutViewModel
    {
        public AboutViewModel(List<ProductModel> featuredProducts, List<ProductModel> newProducts)
        {
            Products = featuredProducts.Concat(newProducts).ToList();
        }

        public List<ProductModel> Products { get; set; }
    }
}