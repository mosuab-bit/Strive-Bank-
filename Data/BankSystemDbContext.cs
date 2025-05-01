using BankSystem.API.Models.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using System;
namespace BankSystem.API.Data
{
    public class BankSystemDbContext:IdentityDbContext<ApplicationUser, IdentityRole, string>
    {
        public BankSystemDbContext(DbContextOptions<BankSystemDbContext> options):base(options) 
        {
            
        } 
        public DbSet<AccountType> AccountTypes { get; set;}
        public DbSet<ATM> ATMs { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<CreditCard> CreditCards { get; set; }
        public DbSet<CustomerAccount> CustomerAccounts { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Loan> Loans { get; set; }
        public DbSet<LoanApplication> LoanApplications { get; set; }
        public DbSet<LoanRepayment> LoanRepayments { get; set; }
        public DbSet<LoanType> LoanTypes { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Transfer> Transfers { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(typeof(BankSystemDbContext).Assembly);

            builder.Entity<IdentityRole>()
                .HasData(
            new IdentityRole { Id = "28d29ad0-5271-46a1-8a4b-325d2bcec816", Name = "Customer", NormalizedName = "CUSTOMER" },
            new IdentityRole { Id = "2ce758b4-8fdc-441c-86d6-6bd2ca9adb0a", Name = "Admin", NormalizedName = "ADMIN" },
            new IdentityRole { Id = "41d28f70-1fc4-4aa2-9d7e-0b994d0b83c8", Name = "Teller", NormalizedName = "TELLER" },
            new IdentityRole { Id = "d90dc06c-6373-4f6f-8b3b-d56fc8da5e21", Name = "BranchManager", NormalizedName = "BRANCHMANAGER" },
            new IdentityRole { Id = "6d83404f-2a81-4f4d-ab7e-4b29f053eec2", Name = "LoanOfficer", NormalizedName = "LOANOFFICER" },
            new IdentityRole { Id = "f380beb5-cd7b-499b-b697-64555e351cd4", Name = "CreditCardOfficer", NormalizedName = "CREDITCARDOFFICER" }
            );
        }
    }
}
