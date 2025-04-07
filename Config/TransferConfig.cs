using BankSystem.API.Models.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankSystem.API.Config
{
    public class TransferConfig : IEntityTypeConfiguration<Transfer>
    {
        public void Configure(EntityTypeBuilder<Transfer> builder)
        {
            // تعيين المفتاح الأساسي
            builder.HasKey(t => t.TransferId);

            // ضبط الخصائص
            builder.Property(t => t.SenderAccountNum)
                   .IsRequired()
                   .HasMaxLength(20);

            builder.Property(t => t.RecipientAccounNum)
                   .IsRequired()
                   .HasMaxLength(20);

            builder.Property(t => t.SenderBalanceBeforeTransfer)
                   .IsRequired()
                   .HasColumnType("decimal(18,2)");

            builder.Property(t => t.RecipientBalanceBeforeTransfer)
                   .IsRequired()
                   .HasColumnType("decimal(18,2)");

            builder.Property(t => t.TransferDateTime)
                   .IsRequired()
                   .HasDefaultValueSql("GETDATE()"); // تعيين التاريخ تلقائيًا عند الإدخال

            builder.Property(t => t.SenderBalanceAfterTransfer)
                   .IsRequired()
                   .HasColumnType("decimal(18,2)");

            builder.Property(t => t.RecipientBalanceAfterTransfer)
                   .IsRequired()
                   .HasColumnType("decimal(18,2)");

            // تحديد العلاقة مع CustomerAccount
            builder.HasOne(t => t.CustomerAccount)  // كل Transfer مرتبط بحساب عميل واحد
                   .WithMany(ca => ca.Transfers) // حساب واحد يمكن أن يحتوي على عدة Transfers
                   .HasForeignKey(t => t.CustomerAccountId)  // المفتاح الأجنبي في Transfer
                   .OnDelete(DeleteBehavior.Restrict);  // لا يتم حذف التحويل عند حذف الحساب (يمكنك تغييرها حسب الحاجة)
        }
    }
}
