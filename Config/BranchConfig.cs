using BankSystem.API.Models.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankSystem.API.Config
{
    public class BranchConfig : IEntityTypeConfiguration<Branch>
    {
        public void Configure(EntityTypeBuilder<Branch> builder)
        {
            builder.HasKey(b => b.BranchId);

            
            builder.Property(b => b.BranchName)
                   .IsRequired()
                   .HasMaxLength(100); 

            builder.Property(b => b.BranchLocation)
                   .IsRequired()
                   .HasMaxLength(200); 

            
            builder.HasMany(b => b.Employees)
                   .WithOne(e => e.BranchEmployee)  
                   .HasForeignKey(e => e.BranchId) 
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
