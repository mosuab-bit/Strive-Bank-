using BankSystem.API.Data;
using BankSystem.API.Models.Domain;
using BankSystem.API.Models.DTO;
using BankSystem.API.Repositories.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BankSystem.API.Repositories.Service
{
    public class EmployeeRepository(BankSystemDbContext context, UserManager<ApplicationUser> userManager) : IEmployeeRepository
    {
        public async Task<Response_EmployeeDto> GetEmployeeInfoByUserNameAsync(string userName)
        {
            var employee = await context.Employees.Include(e=>e.User)
                .Include(e=>e.BranchEmployee)
                .FirstOrDefaultAsync(e=>e.User.UserName == userName);
            if (employee == null)
                throw new KeyNotFoundException("Employee not found.");

            return new Response_EmployeeDto
            {
                FullName = employee.User.FullName,
                Email = employee.User.Email,
                PhoneNumber = employee.User.PhoneNumber,
                Address = employee.User.Address,
                Position = employee.User.Role.ToString(),
                Salary = employee.EmployeeSalary,
                HireDate = employee.HireDate,
                PersonalImage = employee.User.PersonalImage,
                BranchName = employee.BranchEmployee.BranchName,
                BranchLocation = employee.BranchEmployee.BranchLocation,
                IsDeleted = employee.IsDeleted,
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

        public async Task<bool> UpdateEmployeeInfoByUserNameAsync(string username, Request_UpdateEmployeeInfoDto Dto)
        {
            var employee = await context.Employees.Include(e => e.User)
                 .Include(e => e.BranchEmployee)
                 .FirstOrDefaultAsync(e => e.User.UserName == username);

            if (employee == null)
                throw new KeyNotFoundException("Employee not found.");

            employee.User.Email = Dto.Email;
            employee.User.PhoneNumber = Dto.PhoneNumber;
            employee.User.Address = Dto.Address;
            if(Dto.ImageUrl!=null && Dto.ImageUrl.Length!=0)
            employee.User.PersonalImage = await SaveImageAsync(Dto.ImageUrl);
            employee.User.UserName = Dto.UserName;

            context.Employees.Update(employee);
            await context.SaveChangesAsync();

            return true;
        }
    }
}
