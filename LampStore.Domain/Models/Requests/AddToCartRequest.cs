using System;

namespace LampStore.Domain.Models.Requests
{
    public class AddToCartRequest
    {
        public string ProductId { get; set; }
        public string UserId { get; set; }
        public int Count { get; set; }
    }
}