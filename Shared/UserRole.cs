using System.Runtime.Serialization;

namespace BankSystem.API.Shared
{
    public enum UserRole:byte
    {
        [EnumMember(Value = "Customer")]
        Customer,
        [EnumMember(Value = "Teller")]
        Teller,
        [EnumMember(Value = "BranchManager")]
        BranchManager,
        [EnumMember(Value = "Admin")]
        SystemAdministrator,
        [EnumMember(Value = "LoanOfficer")]
        LoanOfficer,
        [EnumMember(Value = "CreditCardOfficer")]
        CreditCardOfficer
    }
}
