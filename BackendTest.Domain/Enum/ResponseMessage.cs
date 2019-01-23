using System.ComponentModel;

namespace BackendTest.Domain.Enum
{
    public enum ResponseMessage
    {
        [Description("Unknown message")]
        Unknown = 0,

        [Description("Operation done successfully.")]
        OperationSucceeded = 1,

        [Description("Operation failed!")]
        OperationFailed = 2,
    }
}