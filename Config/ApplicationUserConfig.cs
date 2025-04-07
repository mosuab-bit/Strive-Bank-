using BankSystem.API.Models.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankSystem.API.Config
{
    public class ApplicationUserConfig : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            // تحديد الحقول
            builder.Property(u => u.FullName)
                   .IsRequired()
                   .HasMaxLength(100); // تحديد أن الحقل مطلوب مع تحديد الحد الأقصى للطول

            //builder.Property(u => u.Phonenumber)
            //       .IsRequired()
            //       .HasMaxLength(15); // تحديد أن رقم الهاتف مطلوب مع تحديد الحد الأقصى للطول

            builder.Property(u => u.Address)
                   .IsRequired()
                   .HasMaxLength(100); // تحديد أن العنوان مطلوب مع تحديد الحد الأقصى للطول

            builder.Property(u => u.DateOfBirth)
                   .IsRequired(); // تحديد أن تاريخ الميلاد مطلوب

            builder.Property(u => u.Role)
                   .IsRequired()
                   .HasMaxLength(20); // تحديد أن الدور مطلوب مع تحديد الحد الأقصى للطول

            builder.Property(u => u.Gender)
                   .IsRequired()
                   .HasMaxLength(10); // تحديد أن النوع مطلوب مع تحديد الحد الأقصى للطول

            builder.Property(u => u.PersonalImage)
                   .HasMaxLength(500); // تحديد طول الصورة الشخصية (اختياري)

            builder.Property(u => u.IsDeleted)
                   .HasDefaultValue(false); // تحديد قيمة افتراضية لـ IsDeleted

           
        }
    }
}
