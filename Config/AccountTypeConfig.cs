using BankSystem.API.Models.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankSystem.API.Config
{
    public class AccountTypeConfig : IEntityTypeConfiguration<AccountType>
    {
        public void Configure(EntityTypeBuilder<AccountType> builder)
        {
            // تحديد المفتاح الأساسي
            builder.HasKey(at => at.AccountTypeId);

            // تحديد أن اسم نوع الحساب يجب أن يكون فريدًا
            builder.HasIndex(at => at.AccountTypeName).IsUnique();

            // تحديد الحد الأقصى للطول للحقول
            builder.Property(at => at.AccountTypeName).HasMaxLength(100);
            builder.Property(at => at.Description).HasMaxLength(500);

            // إضافة علاقة مع CustomerAccount
            builder.HasMany(at => at.CustomerAccounts)
                .WithOne(ca => ca.AccountType)  // Assuming there's an AccountType in CustomerAccount
                .HasForeignKey(ca => ca.AccountTypeId);

            // إضافة البيانات المبدئية
            builder.HasData(
                new AccountType { AccountTypeId = 1, AccountTypeName = "Current Account", Description = "A transactional account used for day-to-day operations like deposits, withdrawals, and payments. It typically offers easy access and no interest or minimal interest." },
                new AccountType { AccountTypeId = 2, AccountTypeName = "Savings Account", Description = "A type of account that earns interest on the balance. It is designed for saving money and is less frequently used for transactions." },
                new AccountType { AccountTypeId = 3, AccountTypeName = "Fixed Deposit Account", Description = "Account is a low-risk investment where a lump sum is deposited for a fixed term at a predetermined interest rate, offering guaranteed returns at maturity." },
                new AccountType { AccountTypeId = 4, AccountTypeName = "Islamic Investment Account", Description = "An Islamic Investment Account is a Sharia-compliant account where returns are based on profit-sharing, not interest, with investments in halal businesses." },
                new AccountType { AccountTypeId = 5, AccountTypeName = "Payroll Account", Description = "Used by employers to deposit employee salaries and wages directly." },
                new AccountType { AccountTypeId = 6, AccountTypeName = "Youth Account", Description = "Designed for young people, typically under 18, offering easy access to banking services with lower fees and often including financial education tools." },
                new AccountType { AccountTypeId = 7, AccountTypeName = "Kids Account", Description = "For children, usually managed by a parent or guardian. It helps teach children basic money management skills, with limited access to funds and no fees." },
                new AccountType { AccountTypeId = 8, AccountTypeName = "Business Account", Description = "A type of account designed for businesses, allowing them to manage their financial transactions, including paying employees, receiving payments, and tracking expenses." },
                new AccountType { AccountTypeId = 9, AccountTypeName = "Digital Account", Description = "An account managed entirely online, with no physical branches. It offers services like payments, transfers, and account management through mobile apps or websites." }
            );
        }
    }
}
