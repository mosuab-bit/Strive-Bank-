using BankSystem.API.Models.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankSystem.API.Config
{
    public class ATMConfig : IEntityTypeConfiguration<ATM>
    {
        public void Configure(EntityTypeBuilder<ATM> builder)
        {
            builder.HasKey(atm => atm.ATMId);

            builder.Property(atm => atm.Location)
                   .IsRequired()
                   .HasMaxLength(100); 

            builder.Property(atm => atm.Status)
                   .IsRequired()
                   .HasMaxLength(50); 

           
            builder.HasOne(atm => atm.Branch)
                   .WithMany(b => b.ATMs)
                   .HasForeignKey(atm => atm.BranchId)
                   .OnDelete(DeleteBehavior.Cascade); 
        }
    }
}
