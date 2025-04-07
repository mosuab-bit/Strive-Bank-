using BankSystem.API.Models.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankSystem.API.Config
{
    public class LoanApplicationConfig : IEntityTypeConfiguration<LoanApplication>
    {
        public void Configure(EntityTypeBuilder<LoanApplication> builder)
        {
            // تحديد المفتاح الأساسي
            builder.HasKey(la => la.LoanApplicationId);

            // تحديد العلاقة مع CustomerAccount
            builder.HasOne(la => la.CustomerAccount)  // كل طلب قرض مرتبط بحساب واحد
                   .WithMany(ca => ca.LoansApplication)  // حساب واحد يمكن أن يحتوي على عدة طلبات قروض
                   .HasForeignKey(la => la.CustomerAccountId)  // المفتاح الأجنبي في LoanApplication
                   .OnDelete(DeleteBehavior.Restrict); // عند حذف حساب العميل لا يتم حذف طلبات القروض

            // العلاقة مع LoanType (Many-to-One)
            builder.HasOne(la => la.LoanType)
                   .WithMany(lt => lt.LoanApplications) // نوع القرض يحتوي على عدة طلبات
                   .HasForeignKey(la => la.LoanTypeId)
                   .OnDelete(DeleteBehavior.Restrict); // لا يمكن حذف LoanType إذا كان هناك طلبات مرتبطة به

            // العلاقة مع Loan (One-to-One)
            builder.HasOne(la => la.Loan)
                   .WithOne(l => l.LoanApplication) // كل طلب قرض مرتبط بقرض واحد
                   .HasForeignKey<LoanApplication>(la => la.LoanApplicationId)
                   .OnDelete(DeleteBehavior.Cascade); // عند حذف طلب القرض، يتم حذف القرض المرتبط به

            // تحديد الخصائص الأساسية المطلوبة
            builder.Property(la => la.ApplicantName).IsRequired();
            builder.Property(la => la.Address).IsRequired();
            builder.Property(la => la.ContactNumber).IsRequired();
            builder.Property(la => la.Email).IsRequired();
            builder.Property(la => la.EmploymentStatus).IsRequired();
            builder.Property(la => la.BankAccountNumber).IsRequired();
            builder.Property(la => la.RepaymentSchedule).IsRequired();
            builder.Property(la => la.CollateralDescription).IsRequired();
            builder.Property(la => la.ApplicationStatus).IsRequired();
            builder.Property(l => l.Income)
                   .HasPrecision(18, 2);
            builder.Property(l => l.LoanAmount)
                   .HasPrecision(18, 2);

            // القيم الافتراضية
            builder.Property(la => la.ApplicationDate)
                   .HasDefaultValueSql("GETDATE()"); // تعيين تاريخ الطلب تلقائيًا إلى التاريخ الحالي
        }
    }
}
