using BankSystem.API.Models.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankSystem.API.Config
{
    public class LoanRepaymentConfig : IEntityTypeConfiguration<LoanRepayment>
    {
        public void Configure(EntityTypeBuilder<LoanRepayment> builder)
        {

            // تحديد المفتاح الأساسي
            builder.HasKey(lr => lr.LoanRepaymentId);

            builder.Property(lr => lr.PaymentDate)
                   .HasDefaultValueSql("GETDATE()"); // تعيين القيمة الافتراضية لـ PaymentDate لتاريخ ووقت الدفع إذا لم يتم تحديده
            
            builder.Property(l => l.AmountPaid)
                   .HasPrecision(18, 2);

            builder.Property(l => l.RemainingBalance)
                   .HasPrecision(18, 2);

            // تحديد العلاقة مع Loan
            builder.HasOne(lr => lr.Loan)  // كل LoanRepayment مرتبط بـ Loan واحد
                   .WithMany(l => l.LoanRepayments) // Loan يمكن أن يحتوي على عدة LoanRepayments
                   .HasForeignKey(lr => lr.LoanId)  // المفتاح الأجنبي في LoanRepayment
                   .OnDelete(DeleteBehavior.Cascade);  // عند حذف Loan، يتم حذف LoanRepayments المرتبطة به

        }
    }
}
