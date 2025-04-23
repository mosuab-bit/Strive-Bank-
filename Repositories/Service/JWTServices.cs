using BankSystem.API.Data;
using BankSystem.API.Models.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BankSystem.API.Repositories.Service
{
    public class JWTServices(IConfiguration configuration,BankSystemDbContext context)
    {
        public string GenerateJwtToken(ApplicationUser user)
        {
            // Common claims for both employees and customers
            var claims = new List<Claim>
          {
              new Claim(ClaimTypes.NameIdentifier, user.Id), // User's Id
              new Claim(ClaimTypes.Name, user.UserName), // User's Name
              new Claim(ClaimTypes.Role, user.Role) // User's Role (Employee or Customer)
          };
            // Add role-specific claims
            if (user.Role == "Customer")
            {
                var customerAccount = context.CustomerAccounts
                    .Where(c => c.UserId == user.Id)
                    .FirstOrDefault();

                if (customerAccount != null)
                {
                    claims.Add(new Claim("AccountNumber", customerAccount.AccountNumber));
                    // claims.Add(new Claim("AccountBalance", customerAccount.Balance.ToString())); // Optional
                }
            }
            else if (user.Role == "Employee")
            {
                var employeeDetails = context.Employees
                    .Where(e => e.UserId == user.Id)
                    .FirstOrDefault();

                if (employeeDetails != null)
                {
                    claims.Add(new Claim("EmployeeId", employeeDetails.EmployeeId.ToString()));

                }
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: configuration["Jwt:Issuer"],
                audience: configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(configuration["Jwt:ExpireDurationInMinutes"])),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
