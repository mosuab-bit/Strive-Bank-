using BankSystem.API.Models.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankSystem.API.Config
{
    public class EmployeeConfig : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            //حديد المفتاح الأساسي
            builder.HasKey(e => e.EmployeeId);

            // العلاقة مع ApplicationUser (1:1)
            builder.HasOne(e => e.User)
                   .WithOne(u => u.Employee)
                   .HasForeignKey<Employee>(e => e.UserId)
                   .OnDelete(DeleteBehavior.Restrict); // لا تحذف المستخدم عند حذف الموظف

           

            // تحديد الحقول الاختيارية والافتراضية
            builder.Property(e => e.EmployeeSalary)
                   .IsRequired(false); // يمكن أن يكون `null`

            builder.Property(e => e.IsDeleted)
                   .HasDefaultValue(false); // القيمة الافتراضية للحقل `IsDeleted` هي `false`

            builder.Property(e => e.HireDate)
                   .HasDefaultValueSql("GETDATE()"); // تعيين تاريخ التوظيف افتراضيًا إلى التاريخ الحالي
        }
    }
}
