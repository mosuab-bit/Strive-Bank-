using System.Runtime.Serialization;

namespace BankSystem.API.Shared
{
    public enum CreditCardStatus
    {
        [EnumMember(Value = "Active")]
        Active,
        [EnumMember(Value = "InActive")]
        Inactive,
        [EnumMember(Value = "Blocked")]
        Blocked
    }
}
