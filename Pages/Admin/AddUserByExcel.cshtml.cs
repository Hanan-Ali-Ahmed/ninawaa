using ExcelDataReader;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Ninwa_Employee.Data;
using Ninwa_Employee.Pages.User;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Ninwa_Employee.Pages.Admin
{
   [Authorize(Roles ="Admin")]
    public class AddUserByExcelModel : PageModel
    {
        private readonly UserImportService _userImportService;
        
        private readonly ApplicationDbContext _context;
        public AddUserByExcelModel(UserImportService userImportService,
           
           ApplicationDbContext context)
        {
            _userImportService = userImportService;
            
            _context = context;
        }
        

     
        [BindProperty]

        public IFormFile ExcelFile { get; set; }

        [BindProperty]
        public string UserNameOrName { get; set; }
        [BindProperty]
        public string UserName { get; set; }

        [BindProperty]
        public string OldMasterCardNumber { get; set; }

        [BindProperty]
        public string NewMasterCardNumber { get; set; }

        [BindProperty]
        public string ConfirmMasterCardNumber { get; set; }

        public bool UserFound { get; set; }
        public bool UserSearchAttempted { get; set; }
        public string FoundUserName { get; set; }
       
        //  [BindProperty]
        //public string MasterCardNumber { get; set; }
        public async Task<IActionResult> OnPostUploadExcelAsync()
        {
            if (User.Identity.IsAuthenticated)
            {
                if (User.IsInRole("Admin"))
                {

                }
                else
                {
                    // User is authenticated but not an Admin

                    return NotFound();
                }
            }
            else
            {
                // User is not authenticated

                return NotFound();
            }
            if (ExcelFile == null || ExcelFile.Length == 0)
            {
                //ModelState.AddModelError("ExcelFile", "Please select a valid Excel file.");
                return Page();

            } 
            
                // Use .xlsx extension for the temporary file
                var filePath = Path.GetTempFileName().Replace(".tmp", ".xlsx");
                //var filePath = Path.GetTempFileName();
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await ExcelFile.CopyToAsync(stream);
                }
                await _userImportService.ImportUsersFromExcelAsync(filePath);
            TempData["SuccessMessageForExcel"] = "تم الحفظ بنجاح";
            return Page();

            //if (string.IsNullOrEmpty(UserNameOrName))
            //{
            //    ModelState.AddModelError(string.Empty, "الحقل مطلوب");
            //    return Page();

        }
        public async Task<IActionResult> OnPostFindUserAsync()
        {
            UserSearchAttempted = true;

            if (string.IsNullOrEmpty(UserNameOrName))
            {
                ModelState.AddModelError(string.Empty, "الاسم الرباعي مطلوب / الرقم الوظيفي");
                return Page();
            }

            //var userFromTblUsers = await _context.TblUsers
            //    .FirstOrDefaultAsync(u => u.UserName == UserNameOrName || u.UserName == UserNameOrName);

            //if (userFromTblUsers != null)
            //{
            //    FoundUserName = userFromTblUsers.UserName;
            //}

            //var user = await _context.Users
            //    .FirstOrDefaultAsync(u => u.UserName == UserNameOrName);

            //if (user != null )
            //{
            //    UserFound = true;

            //    FoundUserName = user.UserName;
            //    UserNameOrName = user.UserName;
            //    UserName = user.UserName;
            //}

            return Page();
        }


        public async Task<IActionResult> OnPostUpdateUserCardNumderAsync()
        {
            //if (string.IsNullOrEmpty(UserName) ||
            //    string.IsNullOrEmpty(OldMasterCardNumber) ||
            //    string.IsNullOrEmpty(NewMasterCardNumber) ||
            //    string.IsNullOrEmpty(ConfirmMasterCardNumber))
            //{
            //    ModelState.AddModelError(string.Empty, "جميع الحقول مطلوبة");
            //    return Page();
            //}

            //if (NewMasterCardNumber != ConfirmMasterCardNumber)
            //{
            //    ModelState.AddModelError(string.Empty, "خطا  : عدم تطابق يرجى تاكيد رقم البطاقة");
            //    return Page();
            //}

            ////var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == UserName);
            //if (user == null)
            //{
            //    ModelState.AddModelError(string.Empty, "لم يتم العثور على المستخدم");
            //    return Page();
            //}

            //// Verify the old MasterCard number
            //if (!_hashingService.Verify(OldMasterCardNumber, user.MasterCardNumberHash, user.MasterCardNumberSalt))
            //{
            //    ModelState.AddModelError(string.Empty, "رقم البطاقة السابق غير صحيح يرجى المحاولة مرة اخرى");
            //    return Page();
            //}

            //// Hash the new MasterCard number
            //var (newHashedMasterCardNumber, newSalt) = _hashingService.Hash(NewMasterCardNumber);
            ////var newSalt = _hashingService.GenerateSalt();
            ////var newHashedMasterCardNumber = _hashingService.HashNewUser(NewMasterCardNumber, newSalt);

            //// Update the user record
            //user.MasterCardNumberHash = newHashedMasterCardNumber;
            //user.MasterCardNumberSalt = newSalt;

            //_context.Users.Update(user);
            //await _context.SaveChangesAsync();

            //TempData["SuccessMessage"] = "تم تحديث رقم البطاقة بنجاح";
            return Page();
        }
    }
}
