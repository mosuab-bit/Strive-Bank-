using BankSystem.API.Models.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankSystem.API.Config
{
    public class CustomerAccountConfig : IEntityTypeConfiguration<CustomerAccount>
    {
        public void Configure(EntityTypeBuilder<CustomerAccount> builder)
        {
            // تحديد المفتاح الأساسي
            builder.HasKey(ca => ca.CustomerAccountId);

            // تحديد العلاقة مع ApplicationUser (Many-to-One)
            builder.HasOne(ca => ca.User)
                   .WithMany(u => u.CustomerAccounts) // تطبيق العلاقة بين User و CustomerAccount (One-to-Many)
                   .HasForeignKey(ca => ca.UserId)
                   .OnDelete(DeleteBehavior.Restrict); // تحديد أن الحذف غير مسموح به إذا كانت هناك بيانات مرتبطة

            // تحديد العلاقة مع AccountType (Many-to-One)
            builder.HasOne(ca => ca.AccountType)
                   .WithMany(at => at.CustomerAccounts) // حسابات العملاء مرتبطة بنوع الحساب
                   .HasForeignKey(ca => ca.AccountTypeId)
                   .OnDelete(DeleteBehavior.Restrict); // تحديد أنه لا يمكن حذف AccountType إذا كان هناك حساب مرتبط

            // العلاقة مع Transactions (One-to-Many)
            //builder.HasMany(ca => ca.Transactions)
            //       .WithOne(t => t.CustomerAccount) // كل Transaction مرتبط بحساب واحد
            //       .HasForeignKey(t => t.CustomerAccountId)
            //       .OnDelete(DeleteBehavior.Cascade); // عند حذف الحساب، يتم حذف جميع المعاملات المرتبطة به

            // العلاقة مع Transfers (One-to-Many) - تحويلات العميل
            //builder.HasMany(ca => ca.Transfers) // CustomerAccount يحتوي على عدة تحويلات
            //       .WithOne(t => t.SenderAccount) // كل Transfer مرتبط بحساب واحد كمرسل
            //       .HasForeignKey(t => t.SenderAccountId) // المفتاح الأجنبي لربط التحويل
            //       .OnDelete(DeleteBehavior.Cascade); 

            //builder.HasMany(ca => ca.Transfers) // CustomerAccount يحتوي على عدة تحويلات
            //       .WithOne(t => t.RecipientAccount) // كل Transfer مرتبط بحساب واحد كمستقبل
            //       .HasForeignKey(t => t.RecipientAccountId) // المفتاح الأجنبي لربط التحويل
            //       .OnDelete(DeleteBehavior.Cascade); //يتم حذف التحويلات في حال حذف الحساب



            // العلاقة مع Loans (One-to-Many)
            //builder.HasMany(ca => ca.Loans)
            //       .WithOne(l => l.CustomerAccount) // كل Loan مرتبط بحساب واحد
            //       .HasForeignKey(l => l.CustomerAccountId)
            //       .OnDelete(DeleteBehavior.Cascade); // عند حذف الحساب، يتم حذف جميع القروض المرتبطة به

            // العلاقة مع LoanApplications (One-to-Many)
            //builder.HasMany(ca => ca.LoansApplication)
            //       .WithOne(la => la.CustomerAccount) // كل LoanApplication مرتبط بحساب واحد
            //       .HasForeignKey(la => la.CustomerAccountId)
            //       .OnDelete(DeleteBehavior.Cascade); // عند حذف الحساب، يتم حذف جميع طلبات القروض المرتبطة به
            builder.Property(c => c.Balance)
                   .HasPrecision(18, 2);

            // تحديد الخصائص الأخرى
            builder.Property(ca => ca.AccountNumber)
                   .IsRequired(); // AccountNumber مطلوب

            builder.Property(ca => ca.IsDeleted)
                   .HasDefaultValue(false); // القيمة الافتراضية لـ IsDeleted هي false

            builder.Property(ca => ca.CreatedDate)
                   .HasDefaultValueSql("GETDATE()"); // تعيين القيمة الافتراضية لـ CreatedDate لتاريخ ووقت الإنشاء
        }
    }
}
