using System.ComponentModel;

namespace LampStore.Domain.Enums
{
    public enum UserRolesEnum
    {
        [Description("Administrator")]
        Administrator = 0,

        [Description("Customer")]
        Customer = 1
    }
}
