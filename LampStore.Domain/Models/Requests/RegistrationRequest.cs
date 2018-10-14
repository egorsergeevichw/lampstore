﻿namespace LampStore.Domain.Models.Requests
{
    public class RegistrationRequest
    {
        public string FullName { get; set; }
        public string CompanyName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}