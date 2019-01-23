using System.ComponentModel;

namespace BackendTest.Domain.Enum
{
    public enum Title
    {
        [Description("")]
        Unknown = 0,
        [Description("Mr.")]
        Mr = 1,
        [Description("Miss.")]
        Miss = 2,
        [Description("Mrs.")]
        Mrs = 3,
        [Description("Miss.")]
        Mis = 4
    }
}