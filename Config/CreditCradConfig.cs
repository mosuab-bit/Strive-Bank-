using BankSystem.API.Models.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankSystem.API.Config
{
    public class CreditCradConfig : IEntityTypeConfiguration<CreditCard>
    {
        public void Configure(EntityTypeBuilder<CreditCard> builder)
        {
            builder.HasKey(cc => cc.CreditCardId);

            
            builder.HasOne(cc => cc.CustomerAccount)
                   .WithMany(ca => ca.CreditCards)  
                   .HasForeignKey(cc => cc.CustomerAccountId)
                   .OnDelete(DeleteBehavior.Cascade); 

            builder.Property(cc => cc.CardType)
                   .IsRequired(); 

            builder.Property(cc => cc.CreatedBy)
                   .IsRequired(); 

            builder.Property(cc => cc.Status)
                   .IsRequired(); 

            builder.Property(cc => cc.PinCode)
                   .IsRequired(); 

            builder.Property(cc => cc.CreatedAt)
                   .HasDefaultValue(DateTime.Now); 

            builder.Property(cc => cc.IsDeleted)
                   .HasDefaultValue(false);
        }
    }
}
