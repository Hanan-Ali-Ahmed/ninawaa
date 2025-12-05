using Ninwa_Employee.Data;
using Ninwa_Employee.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
namespace Ninwa_Employee.Pages.User
{
    public class UserLoginModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationDbContext _context;

       public UserLoginModel(
       UserManager<ApplicationUser> userManager,
       SignInManager<ApplicationUser> signInManager,
       ApplicationDbContext context)

        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;

        }

        [BindProperty]
        public string UserName { get; set; }

        [BindProperty]
        public string Password { get; set; }


        public Tblsetting Setting { get; set; } = default!;


        public async Task<IActionResult> OnGetAsync()
        {

            var tblsetting = await _context.Tblsetting.FirstOrDefaultAsync(m => m.Id == 1);

            if (tblsetting == null)
            {
                return NotFound();
            }

            Setting = tblsetting;

            if (Setting.IsOpen == "مغلقة")
            {
                TempData["FailedIsNotOpen"] = "ÇáÇÓÊãÇÑÉ ãÛáÞÉ ÍÇæá ÝíãÇ ÈÚÏ";
                //ModelState.AddModelError(string.Empty, "ÇáÇÓÊãÇÑÉ ãÛáÞÉ ÍÇæá ÝíãÇ ÈÚÏ");
                return Page();

            }
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {



            if (ModelState.IsValid)
            {
                // signIn Prossess & Check
                var userSignIn = await _signInManager.PasswordSignInAsync(UserName, $"p#{Password}sO", false, false);
                if (userSignIn.Succeeded)
                {

                    // fitch setting from  tblsetting 
                    var tblsetting = await _context.Tblsetting.FirstOrDefaultAsync(m => m.Id == 1);

                    if (tblsetting == null)
                    {
                        return NotFound();
                    }

                    Setting = tblsetting;

                    // fitch userdata from  Users table 
                    var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == UserName);

                    if (user == null)
                    {
                        return NotFound();
                    }

                    if (user.NumberOfEdit == 0)
                    {
                        ModelState.AddModelError(string.Empty, "ÚÐÑÇ ,  äÝÐÊ ÚÏÏ ãÍÇæáÇÊ ÊÚÏíá ÇáÇÓÊãÇÑÉ áÏíß");
                        return Page();
                    }

                    if (user.NumberOfEdit == null)
                    {
                        user.NumberOfEdit = Setting.NumberOfEdit;
                        _context.Users.Update(user);
                        await _context.SaveChangesAsync();
                    }

                    //if (user.NumberOfEdit <= Setting.NumberOfEdit)
                    //{
                    //    user.NumberOfEdit = user.NumberOfEdit - 1;
                    //    _context.Users.Update(user);
                    //    await _context.SaveChangesAsync();
                    //}

                    // check if user exist In tblUserData before add him
                    var checkuserexistIntblUserData = await _context.Tbluserdatum.FirstOrDefaultAsync(u => u.UserName == user.UserName);

                    if (checkuserexistIntblUserData == null)
                    {
                        var tblUserData = new Tbluserdatum
                        {
                            UserId = user.Id,
                            FullName = user.UserFullName,
                            UserName = user.UserName,
                            CertificateId3 = 1,
                            vacationId = 1,                         
                            CreatedAt = DateTime.Now,
                            UpdatedAt = DateTime.Now   // 👈 أول مرة فقط
                            
                        };

                        _context.Tbluserdatum.Add(tblUserData);
                        await _context.SaveChangesAsync();
                    }

                    // ⚡⚡ هنا الجزء الأهم — اجلب الـ id الصحيح من tbluserdatum
                    var userData = await _context.Tbluserdatum
                        .FirstOrDefaultAsync(x => x.UserId == user.Id);

                    if (userData != null)
                    {
                        // افتح صفحة Edit مع الـ id الصحيح
                        return RedirectToPage("Edit", new { id = userData.Id });
                    }

                    return RedirectToPage("/Error");
                }
            }


            else
            {
                ModelState.AddModelError(string.Empty, "خطأ في تسجيل الدخول");
                return Page();
            }



            return Page();
        }
    }
}