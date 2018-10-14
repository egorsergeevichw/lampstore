using LampStore.Domain.Enums;

namespace LampStore.Domain.Models.Requests
{
    public class SaveProductRequest
    {
        public string ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Picture { get; set; }
        public int Count { get; set; }
        public ProductTypeEnum Type { get; set; }
    }
}