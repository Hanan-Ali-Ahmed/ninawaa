using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Ninwa_Employee.Data;
using Ninwa_Employee.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ninwa_Employee.Pages.Admin
{
    [Authorize(Roles = "Admin")]

    public class StatisticsModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public StatisticsModel(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
      

        public List<DistrictStatistics> DistrictStats { get; set; } = new List<DistrictStatistics>();

        // إجماليات عامة
        public int TotalUsers { get; set; }
        public int Totalcheck { get; set; }
        public int Totalnotcheck { get; set; }


        // الإحصائيات المتعلقة بالجنس
        public int TotalMaleCount { get; set; }
        public int TotalFemaleCount { get; set; }

        // الإحصائيات المتعلقة بالحالة الوظيفية
        public int Totalempstatus1 { get; set; }
        public int Totalempstatus2 { get; set; }
        public int Totalempstatus3 { get; set; }
        public int Totalempstatus4 { get; set; }

        public int TotalExcludedCount { get; set; }

        public List<ApplicationUser> ExcludedUsersList { get; set; } = new List<ApplicationUser>();
        private async Task<List<string>> GetUserPermissionsAsync()
        {
            // جلب الـ ApplicationUser الحالي من قاعدة البيانات
            var userId = _userManager.GetUserId(User);
            var currentUser = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);

            // إذا لم يوجد المستخدم أو الصلاحيات فارغة، أرجع قائمة فارغة
            if (currentUser == null || string.IsNullOrEmpty(currentUser.Permissions))
                return new List<string>();

            // تقسيم الصلاحيات إلى قائمة
            var permissions = currentUser.Permissions.Split(',', System.StringSplitOptions.RemoveEmptyEntries)
                                                    .Select(p => p.Trim())
                                                    .ToList();

            return permissions;
        }


        public async Task<IActionResult> OnGetAsync()
        {
            if (!User.Identity.IsAuthenticated || !User.IsInRole("Admin"))
            {
                return Forbid();
            }

            // جلب الصلاحيات الحالية بشكل صحيح
            var userPermissions = await GetUserPermissionsAsync();

            if (!userPermissions.Contains("Statistics"))
            {
                return Forbid();
            }

            // إجماليات التدقيق
            Totalcheck = await _context.Tbluserdatum.CountAsync(u => u.check == true);
            Totalnotcheck = await _context.Tbluserdatum.CountAsync(u => u.check == false);

            // إجمالي المستخدمين
            TotalUsers = await _context.Tbluserdatum.CountAsync(u => u.answer == true);

            // تجميع البيانات حسب القسم
            DistrictStats = await _context.Tbluserdatum
                .Where(u => u.answer == true)
                .GroupBy(u => new { u.EmppartsId, u.empparts.empparts })
                .Select(d => new DistrictStatistics
                {
                    DistrictId = d.Key.EmppartsId,
                    empparts = d.Key.empparts,
                    TotalUsers = d.Count(),
                    MaleCount = d.Count(u => u.GenderId == 1),
                    FemaleCount = d.Count(u => u.GenderId == 2),
                    empstatus1 = d.Count(u => u.EmpstatusId == 1),
                    empstatus2 = d.Count(u => u.EmpstatusId == 2),
                    empstatus3 = d.Count(u => u.EmpstatusId == 3),
                    empstatus4 = d.Count(u => u.EmpstatusId == 4),
                    CheckCount = d.Count(u => u.check == true),
                    NotCheckCount = d.Count(u => u.check == false),
                    ExcludedCount = 0
                })
                .ToListAsync();

            // حساب الإجماليات
            TotalMaleCount = DistrictStats.Sum(d => d.MaleCount);
            TotalFemaleCount = DistrictStats.Sum(d => d.FemaleCount);
            Totalempstatus1 = DistrictStats.Sum(d => d.empstatus1);
            Totalempstatus2 = DistrictStats.Sum(d => d.empstatus2);
            Totalempstatus3 = DistrictStats.Sum(d => d.empstatus3);
            Totalempstatus4 = DistrictStats.Sum(d => d.empstatus4);

            // حساب عدد المستخدمين غير المحدثين
            TotalExcludedCount = await _context.Users
                .Where(u => !_context.Tbluserdatum.Any(t => t.UserId == u.Id))
                .Where(u => !(_context.UserRoles
                                .Join(_context.Roles,
                                      ur => ur.RoleId,
                                      r => r.Id,
                                      (ur, r) => new { ur.UserId, r.Name })
                                .Any(x => x.UserId == u.Id && x.Name == "Admin")))
                .CountAsync();

            ExcludedUsersList = await _context.Users
    .Where(u => !_context.Tbluserdatum.Any(t => t.UserId == u.Id))
    .Where(u => !(_context.UserRoles
                    .Join(_context.Roles,
                          ur => ur.RoleId,
                          r => r.Id,
                          (ur, r) => new { ur.UserId, r.Name })
                    .Any(x => x.UserId == u.Id && x.Name == "Admin")))
    .ToListAsync();


            return Page();   // ← هذا هو الناقص
        }


        public class DistrictStatistics
        {
            public int? DistrictId { get; set; }
            public string empparts { get; set; }
            public int TotalUsers { get; set; }
            public int MaleCount { get; set; }
            public int FemaleCount { get; set; }
            public int empstatus1 { get; set; }
            public int empstatus2 { get; set; }
            public int empstatus3 { get; set; }
            public int empstatus4 { get; set; }
            public int CheckCount { get; set; }
            public int NotCheckCount { get; set; }
            public int ExcludedCount { get; set; }
        }
    }
}
