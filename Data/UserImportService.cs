using Ninwa_Employee.Models;
using ExcelDataReader;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Ninwa_Employee.Data
{
    public class UserImportService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<UserImportService> _logger;

        public UserImportService(UserManager<ApplicationUser> userManager,
            ILogger<UserImportService> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }

        public async Task ImportUsersFromExcelAsync(string filePath)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            var users = new List<ImportUserModel>();

            // ????? ??? ??????
            using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    var result = reader.AsDataSet();

                    bool isFirstRow = true;
                    foreach (DataTable table in result.Tables)
                    {
                        foreach (DataRow row in table.Rows)
                        {
                            if (isFirstRow)
                            {
                                isFirstRow = false; // ???? ??? ??????
                                continue;
                            }

                            var user = new ImportUserModel
                            {
                                UserFullName = row[0]?.ToString(),
                                UserName = row[1]?.ToString(),
                                Password = row[2]?.ToString()
                            };

                            users.Add(user);
                        }
                    }
                }
            }

            // ????? ?????????? ?? Identity
            foreach (var user in users)
            {
                string email = $"{user.UserName}@example.com"; // Email ???????
                string defaultPassword = $"p#{user.Password}sO"; // ???? ???? ??????

                var identityUser = new ApplicationUser
                {
                    UserFullName = user.UserFullName,
                    UserName = user.UserName,
                    Email = email
                };

                var result = await _userManager.CreateAsync(identityUser, defaultPassword);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(identityUser, "User");
                }
                else
                {
                    _logger.LogError("Failed to create user: {UserName}, errors: {Errors}",
                        user.UserName, string.Join(", ", result.Errors));
                }
            }
        }
    }
}
