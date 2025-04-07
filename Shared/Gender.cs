using System.Runtime.Serialization;

namespace BankSystem.API.Shared
{
    public enum Gender:byte
    {
        [EnumMember(Value = "Male")]
        Male,
        [EnumMember(Value = "Female")]
        Female
    }
}
