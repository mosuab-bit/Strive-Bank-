using System.Runtime.Serialization;

namespace BankSystem.API.Shared
{
    public enum AccountType:byte
    {
        [EnumMember(Value = "Current Account")]
        CurrentAccount = 1,
        [EnumMember(Value = "SavingsAccount")]
        SavingsAccount = 2,
        [EnumMember(Value = "Fixed Deposit Account")]
        FixedDepositAccount = 3,
        [EnumMember(Value = "Islamic Investment Account")]
        IslamicInvestmentAccount = 4,
        [EnumMember(Value = "Payroll Account")]
        PayrollAccount = 5,
        [EnumMember(Value = "Youth Account")]
        YouthAccount = 6,
        [EnumMember(Value = "Kids Account")]
        KidsAccount = 7,
        [EnumMember(Value = "Business Account")]
        BusinessAccount = 8,
        [EnumMember(Value = "Digital Account")]
        DigitalAccount = 9
    }
}
