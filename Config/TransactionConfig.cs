using BankSystem.API.Models.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankSystem.API.Config
{
    public class TransactionConfig : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            // تحديد الـ Primary Key
            builder.HasKey(t => t.TransactionId);

            // تحديد خصائص الحقول
            builder.Property(t => t.TransactionType)
                   .IsRequired()
                   .HasMaxLength(50); // أقصى طول لنوع المعاملة

            builder.Property(t => t.Amount)
                   .IsRequired()
                   .HasColumnType("decimal(18,2)"); // أفضل من double لتجنب الأخطاء العشرية

            builder.Property(t => t.TransactionDate)
                   .IsRequired()
                   .HasDefaultValueSql("GETDATE()"); // تعيين التاريخ تلقائيًا عند الإدخال

            builder.HasOne(t => t.CustomerAccount)  // كل Transaction مرتبط بحساب عميل واحد
               .WithMany(ca => ca.Transactions)  // حساب واحد يمكن أن يحتوي على عدة Transactions
               .HasForeignKey(t => t.CustomerAccountId)  // المفتاح الأجنبي في Transaction
               .OnDelete(DeleteBehavior.Cascade); // حذف المعاملة عند حذف الحساب
        }
    }
}
