using BankSystem.API.Models.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankSystem.API.Config
{
    public class LoanTypeConfig : IEntityTypeConfiguration<LoanType>
    {
        public void Configure(EntityTypeBuilder<LoanType> builder)
        {

            // تعيين الـ Primary Key
            builder.HasKey(lt => lt.LoanTypeId);

            // تعريف LoanTypeName كمطلوب وبطول معين
            builder.Property(lt => lt.LoanTypeName)
                   .IsRequired()
                   .HasMaxLength(100);

            // تعريف Description كمطلوب
            builder.Property(lt => lt.Description)
                   .IsRequired()
                   .HasMaxLength(255);

            // تعريف معدل الفائدة InterestRate كـ double مع تحديد الدقة (مثال: 18,2)
            builder.Property(lt => lt.InterestRate)
                   .HasPrecision(18, 2)
                   .IsRequired();

            builder.HasData(
            new LoanType
            {
                LoanTypeId = 1,
                LoanTypeName = "Personal Loan",
                InterestRate = 5.5,
                Description = "Unsecured loan for general purposes such as travel or medical expenses.",
                LoanTermMonths = 60
            },
            new LoanType
            {
                LoanTypeId = 2,
                LoanTypeName = "Home Loan",
                InterestRate = 3.8,
                Description = "Loan for purchasing or building a house.",
                LoanTermMonths = 240
            },
            new LoanType
            {
                LoanTypeId = 3,
                LoanTypeName = "Car Loan",
                InterestRate = 4.2,
                Description = "Loan for financing a vehicle purchase.",
                LoanTermMonths = 72
            },
            new LoanType
            {
                LoanTypeId = 4,
                LoanTypeName = "Business Loan",
                InterestRate = 6.0,
                Description = "Loan for starting or expanding a business.",
                LoanTermMonths = 120
            },
            new LoanType
            {
                LoanTypeId = 5,
                LoanTypeName = "Education Loan",
                InterestRate = 4.5,
                Description = "Loan for covering higher education expenses.",
                LoanTermMonths = 96
            }
        );

        }
    }
}
