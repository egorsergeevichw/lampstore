using System.ComponentModel;

namespace LampStore.Domain.Enums
{
    public enum ProductTypeEnum
    {
        [Description("Led bulb")]
        ledbulb = 0,

        [Description("Halogen bulb")]
        halogenbulb = 1,

        [Description("Energy saving lamp")]
        energysavinglamp = 2,

        [Description("Led luminaire")]
        ledluminaire = 3,

        [Description("Desktop lamp")]
        desktoplamp = 4,

        [Description("Garden lamp")]
        gardenlamp = 5
    }
}
