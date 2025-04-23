using BankSystem.API.Data;
using BankSystem.API.Helper;
using BankSystem.API.Models.Domain;
using BankSystem.API.Models.DTO;
using BankSystem.API.Repositories.Interface;
using BankSystem.API.Shared;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Security.Cryptography;

namespace BankSystem.API.Repositories.Service
{
    public class AccountRepository(BankSystemDbContext context, UserManager<ApplicationUser> userManager
            , SignInManager<ApplicationUser> signInManager
            , IEmail _Email
            , IConfiguration configuration, IUrlHelperFactory urlHelperFactory
            , IHttpContextAccessor httpContextAccessor
            ) : IAccountRepository
    {
        //public async Task<Response_RegistrationDto> RegisterUserAsync(RegisterRequestDto registerDto)
        //{
        //    string bankName = "Strive Bank";
        //    string savedFileName = null;

        //    if (registerDto.ImageUrl != null)
        //    {
        //        savedFileName = await SaveImageAsync(registerDto.ImageUrl);
        //    }

        //    var UserInfo = CreateUserInfo(registerDto, savedFileName);
        //    var result = await userManager.CreateAsync(UserInfo,registerDto.Password);

        //    if (!result.Succeeded)
        //    {
        //        return new Response_RegistrationDto { Message = string.Join(", ", result.Errors.Select(e => e.Description)) };
        //    }

        //   var roleResult = await userManager.AddToRoleAsync(UserInfo,registerDto.UserRole.ToString());

        //    if (!roleResult.Succeeded)
        //    {
        //        return new Response_RegistrationDto { Message = string.Join(", ", roleResult.Errors.Select(e => e.Description)) };
        //    }

        //    var confirmationToken = await userManager.GenerateEmailConfirmationTokenAsync(UserInfo);
        //    var encodedToken = WebUtility.UrlEncode(confirmationToken);

        //    var confirmationUrl = urlHelper.Action(
        //        action: "confirmemail",
        //        controller: "Account",
        //        values: new { userId = UserInfo.Id, token = encodedToken },
        //        protocol: "https"
        //    );

        //    string emailHtmlContent = EmailContentForEmployees(bankName, registerDto.UserName, confirmationUrl);

        //    if (registerDto.UserRole == UserRole.Customer)
        //    {

        //        var accountCreationResult = await HandleCustomerAccountAsync(UserInfo.Id, registerDto);
        //        if (!accountCreationResult.Success)
        //            return accountCreationResult;

        //        string accountNumber = accountCreationResult.AccountNmber;

        //        emailHtmlContent = EmailContentForCustomers(bankName, registerDto.UserName, confirmationUrl, accountNumber);
        //    }
        //    else
        //    {
        //        var employeeCreationResult = await HandleEmployeeAsync(UserInfo.Id, registerDto);

        //        if (!employeeCreationResult.Success)
        //            return employeeCreationResult;



        //        emailHtmlContent = EmailContentForEmployees(bankName, registerDto.UserName, confirmationUrl);
        //    }

        //    // Send confirmation email
        //    await _Email.SendEmailAsync(registerDto.Email, "NovaBank", $"{bankName} - Confirm Your Email", emailHtmlContent);

        //    var accessToken = _jwtService.GenerateJwtToken(UserInfo);

        //    return new Response_RegistrationDto
        //    {
        //        UserName = registerDto.UserName,
        //        Success = true,
        //    };
        //}

        public async Task<Response_RegistrationDto> RegisterUserAsync(RegisterRequestDto registerDto)
        {
            string bankName = "Strive Bank";
            string savedFileName = null;

            if (registerDto.ImageUrl != null)
            {
                savedFileName = await SaveImageAsync(registerDto.ImageUrl);
            }

            var UserInfo = CreateUserInfo(registerDto, savedFileName);
            var result = await userManager.CreateAsync(UserInfo, registerDto.Password);

            if (!result.Succeeded)
            {
                return new Response_RegistrationDto { Message = string.Join(", ", result.Errors.Select(e => e.Description)) };
            }

            var roleResult = await userManager.AddToRoleAsync(UserInfo, registerDto.UserRole.ToString());

            if (!roleResult.Succeeded)
            {
                return new Response_RegistrationDto { Message = string.Join(", ", roleResult.Errors.Select(e => e.Description)) };
            }

            var confirmationToken = await userManager.GenerateEmailConfirmationTokenAsync(UserInfo);
            var encodedToken = WebUtility.UrlEncode(confirmationToken);

            var confirmationUrl = $"https://localhost:7289/api/account/confirmemail?userId={UserInfo.Id}&token={encodedToken}";

            string emailHtmlContent = EmailContentForEmployees(bankName, registerDto.UserName, confirmationUrl);

            if (registerDto.UserRole == UserRole.Customer)
            {
                var accountCreationResult = await HandleCustomerAccountAsync(UserInfo.Id, registerDto);
                if (!accountCreationResult.Success)
                    return accountCreationResult;

                string accountNumber = accountCreationResult.AccountNmber;
                emailHtmlContent = EmailContentForCustomers(bankName, registerDto.UserName, confirmationUrl, accountNumber);
            }
            else
            {
                var employeeCreationResult = await HandleEmployeeAsync(UserInfo.Id, registerDto);
                if (!employeeCreationResult.Success)
                    return employeeCreationResult;

                emailHtmlContent = EmailContentForEmployees(bankName, registerDto.UserName, confirmationUrl);
            }

            // ✅ إرسال البريد الإلكتروني مع رابط التأكيد
            await _Email.SendEmailAsync(registerDto.Email, "Strive Bank", $"{bankName} - Confirm Your Email", emailHtmlContent);


            return new Response_RegistrationDto
            {
                UserName = registerDto.UserName,
                Success = true, 
            };
        }

        private async Task<Response_RegistrationDto> HandleEmployeeAsync(string UserId, RegisterRequestDto registerDto)
        {
            var EmployeeIsExist = await context.Employees.AnyAsync(e=>e.UserId==UserId);

            if (EmployeeIsExist)
            {
                return new Response_RegistrationDto
                {
                    Message = "An Employee is already exist",
                    Success = false
                };
            }

            var newEmployee = new Employee
            {
                UserId = UserId,
                HireDate = DateTime.Now,
                EmployeeSalary = registerDto.Salary,
                BranchId = (int)registerDto.BranchName
            };
            context.Employees.Add(newEmployee);
            await context.SaveChangesAsync();

            return new Response_RegistrationDto
            {
                Success = true,
            };
        }

        private string EmailContentForCustomers(string bankName, string userName, string confirmationUrl, string accountNumber)
        {
            string content = $@"
<html>
<head>
    <style>
        /* Same styles as above */
    </style>
</head>
<body>
    <div class='email-container'>
        <div class='email-header'>
            <h1>Welcome to {bankName}</h1>
        </div>
        <p>Dear {userName},</p>
        <p>Thank you for choosing <strong>{bankName}</strong>. We are thrilled to have you onboard!</p>
        <p>Your account number is: <strong>{accountNumber}</strong></p>
        <p>Please confirm your account by clicking the button below:</p>
        <a class='btn' href='{confirmationUrl}'>Confirm Your Account</a>
    </div>
</body>
</html>";
            return content;
        }

        private async Task<Response_RegistrationDto> HandleCustomerAccountAsync(string UserId, RegisterRequestDto registerDto)
        {
            var existingCustomerAccount = await context.CustomerAccounts
               .AnyAsync(ca => ca.UserId == UserId && ca.AccountTypeId == (int)registerDto.accountType);

            if (existingCustomerAccount)
            {
                return new Response_RegistrationDto
                {
                    Message = "A customer account with the same type already exists.",
                    Success = false
                };
            }

            string accountNumber = await GenerateUniqueAccountNumAsync();
            string encodedAccountNumber = EncryptionHelper.Encrypt(accountNumber);

            var userAccountInfo = new CustomerAccount
            {
                UserId = UserId,
                AccountNumber = encodedAccountNumber,
                AccountTypeId = (int)registerDto.accountType,
                CreatedDate = DateTime.Now
            };

            context.CustomerAccounts.Add(userAccountInfo);
            await context.SaveChangesAsync();

            return new Response_RegistrationDto
            {
                Success = true,
                AccountNmber = accountNumber
            };
        }

        private async Task<string> GenerateUniqueAccountNumAsync()
        {
            var existingAccounts = (await context.CustomerAccounts
                    .Select(c => c.AccountNumber)
                    .ToListAsync()) 
                    .ToHashSet();

            string accountNum;

            do
            {
                accountNum = GenerateRandomNumber(1000, 9999);
            }
            while (existingAccounts.Contains(accountNum));

            return accountNum;
        }
        private string GenerateRandomNumber(int min, int max)
        {
            byte[] randomNumber = new byte[4];
            RandomNumberGenerator.Fill(randomNumber);
            int result = BitConverter.ToInt32(randomNumber, 0) & int.MaxValue; 
            return (min + (result % (max - min + 1))).ToString();
        }
        private string EmailContentForEmployees(string bankName, string userName, string confirmationUrl)
        {
            string content = $@"
<html>
<head>
    <style>
        body {{
            font-family: Arial, sans-serif;
            background-color: #f9f9f9;
            color: #333;
            line-height: 1.6;
        }}
        .email-container {{
            max-width: 600px;
            margin: 20px auto;
            padding: 20px;
            background-color: #ffffff;
            border: 1px solid #dddddd;
            border-radius: 10px;
        }}
        .email-header {{
            text-align: center;
            margin-bottom: 20px;
        }}
        .email-header h1 {{
            color: #0073e6;
        }}
        .email-footer {{
            margin-top: 30px;
            text-align: center;
            font-size: 0.9em;
            color: #555;
        }}
        .btn {{
            display: inline-block;
            background-color: #0073e6;
            color: #ffffff;
            padding: 10px 20px;
            text-decoration: none;
            border-radius: 5px;
            margin-top: 20px;
        }}
        .btn:hover {{
            background-color: #005bb5;
        }}
    </style>
</head>
<body>
    <div class='email-container'>
        <div class='email-header'>
            <h1>Welcome to {bankName}</h1>
        </div>
        <p>Dear {userName},</p>
        <p>Thank you for choosing <strong>{bankName}</strong>. We are thrilled to have you onboard!</p>
        <p>Please confirm your account by clicking the button below:</p>
        <a class='btn' href='{confirmationUrl}'>Confirm Your Account</a>
    </div>
</body>
</html>";

            return content;
        }

        private ApplicationUser CreateUserInfo(RegisterRequestDto registerDto, string savedFile)
        {
            return new ApplicationUser
            {
                FullName = string.Join(" ", registerDto.FirstName, registerDto.SecondName, registerDto.ThirdName, registerDto.LastName).Trim(),
                PhoneNumber = registerDto.PhoneNumber,
                Address = registerDto.Address,
                Gender = registerDto.Gender.ToString(),
                UserName = registerDto.UserName,
                Email = registerDto.Email,
                Role = registerDto.UserRole.ToString(),
                DateOfBirth = registerDto.DateOfBirth,
                PersonalImage = savedFile,
                LockoutEnd = DateTime.Now,
            };
        }

        public async Task<string> SaveImageAsync(IFormFile imageFile)
        {
            if (imageFile == null || imageFile.Length == 0)
                throw new ArgumentException("Invalid file. File cannot be empty.");

            // ✅ 1. التحقق من الامتداد المسموح به
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
            string fileExtension = Path.GetExtension(imageFile.FileName).ToLower();

            if (!allowedExtensions.Contains(fileExtension))
                throw new ArgumentException("Invalid file type. Only JPG, JPEG, and PNG are allowed.");

            // ✅ 2. التحقق من حجم الملف (أقصى حد 5 ميجابايت)
            long maxFileSize = 5 * 1024 * 1024; // 5MB
            if (imageFile.Length > maxFileSize)
                throw new ArgumentException("File size exceeds the maximum allowed size of 5MB.");

            // ✅ 3. تحديد مسار المجلد الذي سيتم حفظ الصور داخله
            string uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");

            // ✅ 4. التحقق من وجود المجلد، وإذا لم يكن موجودًا يتم إنشاؤه
            if (!Directory.Exists(uploadFolder))
                Directory.CreateDirectory(uploadFolder);

            // ✅ 5. إنشاء اسم فريد للملف
            string fileName = Guid.NewGuid().ToString() + fileExtension;
            string filePath = Path.Combine(uploadFolder, fileName);

            try
            {
                // ✅ 6. حفظ الصورة في المسار المحدد
                using var stream = new FileStream(filePath, FileMode.Create);
                await imageFile.CopyToAsync(stream);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while saving the file.", ex);
            }

            return fileName; // ✅ إرجاع اسم الملف المحفوظ
        }
    }
}
