using Ninwa_Employee.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Ninwa_Employee.Data;

namespace Ninwa_Employee.Pages.Admin
{
    public class SettingsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public SettingsModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Tblsetting Tblsetting { get; set; } = new Tblsetting();

        public async Task<IActionResult> OnGetAsync()
        {
            // السماح فقط للمدير
            if (!User.Identity.IsAuthenticated || !User.IsInRole("Admin"))
                return NotFound();

            // جلب أول سجل إعدادات (عادة يوجد سجل واحد فقط)
            var setting = await _context.Tblsetting.FirstOrDefaultAsync();

            if (setting != null)
            {
                Tblsetting = setting;
            }

            return Page();
        }

        public async Task<IActionResult> OnPostUpdataSettingsAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            // جلب سجل الإعدادات الوحيد
            var setting = await _context.Tblsetting.FirstOrDefaultAsync();

            if (setting == null)
            {
                // لا يوجد سجل → إضافة
                setting = new Tblsetting
                {
                    NumberOfEdit = Tblsetting.NumberOfEdit,
                    ClosingDate = Tblsetting.ClosingDate,
                    IsOpen = Tblsetting.IsOpen
                };

                _context.Tblsetting.Add(setting);
            }
            else
            {
                // يوجد سجل → تعديل
                setting.NumberOfEdit = Tblsetting.NumberOfEdit;
                setting.ClosingDate = Tblsetting.ClosingDate;
                setting.IsOpen = Tblsetting.IsOpen;

                _context.Tblsetting.Update(setting);

                // ✨ تحديث جميع المستخدمين بنفس القيمة الجديدة
                var allUsers = await _context.Users.ToListAsync();
                foreach (var user in allUsers)
                {
                    user.NumberOfEdit = Tblsetting.NumberOfEdit;
                    _context.Users.Update(user);
                }
            }

            await _context.SaveChangesAsync();

            TempData["SuccessSaveMessage"] = "تم حفظ التغييرات بنجاح";
            return RedirectToPage();
        }

    }
}
