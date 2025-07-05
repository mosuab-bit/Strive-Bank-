using System.Runtime.Serialization;

namespace BankSystem.API.Shared
{
    public enum CreditCardType
    {
        [EnumMember(Value = "Visa")]
        Visa,
        [EnumMember(Value = "MasterCard")]
        MasterCard,
        [EnumMember(Value = "AmericanExpress")]
        AmericanExpress,
        [EnumMember(Value = "UnionPay")]
        UnionPay,
    }
}
