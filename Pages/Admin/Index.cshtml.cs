using Ninwa_Employee.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ninwa_Employee.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Identity;

namespace Ninwa_Employee.Pages.Admin
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        private readonly UserManager<ApplicationUser> _userManager;
        public IndexModel(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [BindProperty]
        public Tbluserdatum Tbluserdatum { get; set; } = new Tbluserdatum();
        [BindProperty]
        public Tblsetting Tblsetting { get; set; } = default!;
        [BindProperty]
        public string? UserNameOrName { get; set; }

        [BindProperty]
        public string? SearchResultMessage { get; set; }

        [BindProperty]
        public string? FirstWife { get; set; }

        [BindProperty]
        public string? SecondWife { get; set; }

        [BindProperty]
        public string? ThirdWife { get; set; }

        [BindProperty]
        public string? FourthWife { get; set; }

        public async Task<IActionResult> OnGetAsync(string? UserId)
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


            if (string.IsNullOrEmpty(UserId))
            {
                await LoadRelatedData();
                return Page();
            }

            Tbluserdatum = await _context.Tbluserdatum.FirstOrDefaultAsync(m => m.UserId == UserId);

            if (Tbluserdatum == null)
            {
                return NotFound();
            }



            Tblsetting = await _context.Tblsetting.FirstOrDefaultAsync();
            if (Tblsetting == null)
            {
                return NotFound();
            }

            var wives = await _context.Tblwifesnames.Where(w => w.UserId == UserId).ToListAsync();
            if (wives.Count > 0) FirstWife = wives.ElementAtOrDefault(0)?.WifesName;
            if (wives.Count > 1) SecondWife = wives.ElementAtOrDefault(1)?.WifesName;
            if (wives.Count > 2) ThirdWife = wives.ElementAtOrDefault(2)?.WifesName;
            if (wives.Count > 3) FourthWife = wives.ElementAtOrDefault(3)?.WifesName;

            await LoadRelatedData();
            return Page();
        }

        public async Task<IActionResult> OnPostSearchUserAsync()
        {
            if (string.IsNullOrEmpty(UserNameOrName))
            {
                SearchResultMessage = "ادخل الأسم الرباعي او اسم المستخدم";
                await LoadRelatedData();
                return Page();
            }

            var user = await _context.Tbluserdatum
                .FirstOrDefaultAsync(u => u.FullName == UserNameOrName || u.UserName == UserNameOrName);

            if (user != null)
            {
                return RedirectToPage(new { UserId = user.UserId });
            }
            else
            {
                SearchResultMessage = "المستخدم غير موجود.";
                await LoadRelatedData();
                return Page();
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                await LoadRelatedData();
                return Page();
            }

            try
            {


                await UpdateUserDatumAsync();
                await SaveOrUpdateWivesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TbluserdatumExists(Tbluserdatum.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            TempData["SuccessMessage"] = "تم الحفظ بنجاح";

            return RedirectToPage("Index");
        }

        private async Task UpdateUserDatumAsync()
        {
            Tbluserdatum.UpdatedAt = DateTime.Now;
            _context.Entry(Tbluserdatum).Property(x => x.CreatedAt).IsModified = false;

            _context.Attach(Tbluserdatum).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }


        private async Task SaveOrUpdateWivesAsync()
        {
            var wives = await _context.Tblwifesnames.Where(w => w.UserId == Tbluserdatum.UserId).ToListAsync();

            SaveOrUpdateWife(wives, 0, FirstWife);
            SaveOrUpdateWife(wives, 1, SecondWife);
            SaveOrUpdateWife(wives, 2, ThirdWife);
            SaveOrUpdateWife(wives, 3, FourthWife);

            await _context.SaveChangesAsync();
        }

        private void SaveOrUpdateWife(List<Tblwifesname> wives, int index, string? wifeName)
        {
            if (index < wives.Count)
            {
                wives[index].WifesName = wifeName;
                _context.Entry(wives[index]).State = EntityState.Modified;
            }
            else if (!string.IsNullOrEmpty(wifeName))
            {
                _context.Tblwifesnames.Add(new Tblwifesname
                {
                    UserId = Tbluserdatum.UserId,
                    WifesName = wifeName
                });
            }
        }

        private bool TbluserdatumExists(long id)
        {
            return _context.Tbluserdatum.Any(e => e.Id == id);
        }

        private async Task LoadRelatedData()
        {
            ViewData["CertificateId"] = new SelectList(_context.Set<Tblcertificates>(), "Id", "CertificateType");
            ViewData["CertificateId2"] = new SelectList(_context.Set<Tblcertificates>(), "Id", "CertificateType");
            ViewData["CertificateId3"] = new SelectList(_context.Set<Tblcertificates>(), "Id", "CertificateType");
            ViewData["GenderId"] = new SelectList(_context.Set<Tblgender>(), "Id", "Gender");
            ViewData["MaritalStatusId"] = new SelectList(_context.Set<Tblmaritalstatus>(), "Id", "MaritalStatus");
            ViewData["AnswerId"] = new SelectList(_context.Set<Tblanswer>(), "Id", "answer");
            ViewData["CountcertificateId"] = new SelectList(_context.Set<Tblanswer>(), "Id", "answer");
            ViewData["AddId"] = new SelectList(_context.Set<Tblanswer>(), "Id", "answer");
            ViewData["DropId"] = new SelectList(_context.Set<Tblanswer>(), "Id", "answer");
            ViewData["MartyrsId"] = new SelectList(_context.Set<Tblanswer>(), "Id", "answer");
            ViewData["ChapterId"] = new SelectList(_context.Set<Tblanswer>(), "Id", "answer");
            ViewData["EmppartsId"] = new SelectList(_context.Set<Tblparts>(), "Id", "empparts");
            ViewData["EmppositionId"] = new SelectList(_context.Set<Tblempposition>(), "Id", "empposition");
            ViewData["EmpstatusId"] = new SelectList(_context.Set<Tblempstatus>(), "Id", "empstatus");
            ViewData["scientifictitleId"] = new SelectList(_context.Set<Tblscientifictitle>(), "Id", "scientifictitle");
            ViewData["vacationId"] = new SelectList(_context.Set<Tblvacation>(), "Id", "vacation");
        }



    }
}
