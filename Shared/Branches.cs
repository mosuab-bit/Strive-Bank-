using System.Runtime.Serialization;

namespace BankSystem.API.Shared
{
    public enum Branches:byte
    {

        [EnumMember(Value = "Main Branch")]
          MainBranch = 1,
        [EnumMember(Value = "West Branch")]
          WestBranch = 2,
        [EnumMember(Value = "East Branch")]
          EastBranch = 3
    }
}
