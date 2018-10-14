using System;

namespace LampStore.Domain.Models.Requests
{
    public class MakeOrderRequest
    {
        public string CompanyName { get; set; }
        public string Inn { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
    }
}