using BankSystem.API.Models.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankSystem.API.Config
{
    public class LoanConfig : IEntityTypeConfiguration<Loan>
    {
        public void Configure(EntityTypeBuilder<Loan> builder)
        {
            // تحديد المفتاح الأساسي
            builder.HasKey(l => l.LoanId);

            // تحديد العلاقة مع CustomerAccount في Loan
            builder.HasOne(l => l.CustomerAccount)  // كل Loan مرتبط بحساب واحد
                   .WithMany(ca => ca.Loans)  // حساب واحد يمكن أن يحتوي على عدة قروض
                   .HasForeignKey(l => l.CustomerAccountId)
                   .OnDelete(DeleteBehavior.Cascade); // حذف القرض يؤدي إلى حذف البيانات المرتبطة

            // العلاقة مع LoanType (Many-to-One)
            builder.HasOne(l => l.LoanType)
                   .WithMany(lt => lt.Loans) // LoanType يحتوي على عدة قروض
                   .HasForeignKey(l => l.LoanTypeId)
                   .OnDelete(DeleteBehavior.Restrict); // لا يمكن حذف LoanType إذا كان هناك قروض مرتبطة


            // العلاقة مع LoanRepayments (One-to-Many)
            //builder.HasMany(l => l.LoanRepayments)
            //       .WithOne(lr => lr.Loan) // كل LoanRepayment مرتبط بقرض واحد
            //       .HasForeignKey(lr => lr.LoanId)
            //       .OnDelete(DeleteBehavior.Cascade); // عند حذف القرض يتم حذف جميع المدفوعات المرتبطة به


            // تحديد الخصائص الأخرى
            builder.Property(l => l.Status)
                   .IsRequired(); // Status مطلوب

            builder.Property(l => l.LoanAmount)
                   .HasPrecision(18, 2);

            builder.Property(l => l.StartDate)
                   .IsRequired(); // StartDate مطلوب

            builder.Property(l => l.EndDate)
                   .IsRequired(); // EndDate مطلوب

            builder.Property(l => l.ApprovedDate)
                   .IsRequired(); // ApprovedDate مطلوب
        }
    }
}
