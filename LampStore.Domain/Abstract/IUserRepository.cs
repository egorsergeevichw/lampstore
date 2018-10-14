using System;
using LampStore.Domain.Entities;
using System.Collections.Generic;
using LampStore.Domain.Enums;
using LampStore.Domain.Models;
using LampStore.Domain.Models.Requests;

namespace LampStore.Domain.Abstract
{
    public interface IUserRepository
    {
        UserModel GetUser(Guid userId);
        Guid AddUser(RegistrationRequest request);
        Guid LoginUser(LoginRequest request);       
        void ConfirmEmail(Guid? userId);        
    }
}
