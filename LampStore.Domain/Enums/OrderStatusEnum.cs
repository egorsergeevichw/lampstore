using System.ComponentModel;

namespace LampStore.Domain.Enums
{
    public enum OrderStatusEnum
    {
        [Description("In process")]
        InProcess = 0,

        [Description("Sent")]
        Sent = 1,
    }
}
