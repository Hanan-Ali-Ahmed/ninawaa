using Ninwa_Employee.Data;
using Ninwa_Employee.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
namespace Ninwa_Employee.Pages.User
{

    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public EditModel(ApplicationDbContext context, SignInManager<ApplicationUser> signInManager)
        {
            _context = context;
            _signInManager = signInManager;
        }

        [BindProperty]
        public Tbluserdatum Tbluserdatum { get; set; } = default!;
        [BindProperty]

        public Tblsetting Tblsetting { get; set; } = default!;


        [BindProperty]

        public string? FirstWife { get; set; }

        [BindProperty]
        public string? SecondWife { get; set; }

        [BindProperty]
        public string? ThirdWife { get; set; }

        [BindProperty]
        public string? FourthWife { get; set; }

        [BindProperty]

        public int? DisplayNumberOfEdit { get; set; }

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (!User.Identity.IsAuthenticated || !User.IsInRole("User"))
                return NotFound();

            if (id == null)
                return NotFound();

            var tbluserdatum = await _context.Tbluserdatum
                .FirstOrDefaultAsync(x => x.Id == id);

            if (tbluserdatum == null)
                return NotFound();

            tbluserdatum.answer = false;
            Tbluserdatum = tbluserdatum;

            var tblsetting = await _context.Tblsetting
                .FirstOrDefaultAsync(m => m.Id == 1);

            if (tblsetting == null)
                return NotFound();

            Tblsetting = tblsetting;

            // تحميل أسماء الزوجات
            var wives = await _context.Tblwifesnames
                .Where(w => w.UserId == tbluserdatum.UserId)
                .ToListAsync();

            if (wives.Count > 0) FirstWife = wives.ElementAtOrDefault(0)?.WifesName;
            if (wives.Count > 1) SecondWife = wives.ElementAtOrDefault(1)?.WifesName;
            if (wives.Count > 2) ThirdWife = wives.ElementAtOrDefault(2)?.WifesName;
            if (wives.Count > 3) FourthWife = wives.ElementAtOrDefault(3)?.WifesName;

            // SelectLists
            ViewData["CertificateId"] = new SelectList(_context.Tblcertificates, "Id", "CertificateType");
            ViewData["CertificateId2"] = new SelectList(_context.Tblcertificates, "Id", "CertificateType");
            ViewData["CertificateId3"] = new SelectList(_context.Tblcertificates, "Id", "CertificateType");

            ViewData["GenderId"] = new SelectList(_context.Tblgender, "Id", "Gender");
            ViewData["MaritalStatusId"] = new SelectList(_context.Tblmaritalstatus, "Id", "MaritalStatus");

            var answers = new SelectList(_context.Tblanswer, "Id", "answer");
            ViewData["AnswerId"] = answers;
            ViewData["CountcertificateId"] = answers;
            ViewData["AddId"] = answers;
            ViewData["DropId"] = answers;
            ViewData["MartyrsId"] = answers;
            ViewData["ChapterId"] = answers;

            ViewData["EmppartsId"] = new SelectList(_context.Tblparts, "Id", "empparts");
            ViewData["EmppositionId"] = new SelectList(_context.Tblempposition, "Id", "empposition");
            ViewData["EmpstatusId"] = new SelectList(_context.Tblempstatus, "Id", "empstatus");

            ViewData["scientifictitleId"] = new SelectList(_context.Tblscientifictitle, "Id", "scientifictitle");
            ViewData["vacationId"] = new SelectList(_context.Tblvacation, "Id", "vacation");

            return Page();
        }


        public async Task<IActionResult> OnPostAsync(string? id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            //if (Tbluserdatum.GenderId == null || Tbluserdatum.GenderId == 0 ||
            //   Tbluserdatum.BirthplaceId == null || Tbluserdatum.BirthplaceId == 0 ||
            //   Tbluserdatum.PlaceLastTenYear == null || Tbluserdatum.PlaceLastTenYear == 0 ||
            //   Tbluserdatum.CurrentPlaceId == null || Tbluserdatum.CurrentPlaceId == 0 ||
            //   Tbluserdatum.CertificateId == null || Tbluserdatum.CertificateId == 0 ||
            //   Tbluserdatum.MaritalStatusId == null || Tbluserdatum.MaritalStatusId == 0 ||
            //   Tbluserdatum.AddOrDropService == null || Tbluserdatum.AddOrDropService == 0 ||
            //   Tbluserdatum.EmployeeRecivedLand == null || Tbluserdatum.EmployeeRecivedLand == 0 ||
            //   Tbluserdatum.WorkPlaceAddToId == null || Tbluserdatum.WorkPlaceAddToId == 0 ||
            //   Tbluserdatum.BirthDate == null || Tbluserdatum.StartDate == null ||
            //   Tbluserdatum.SpecialNeeds == null

            //   )
            //{
            //    ModelState.AddModelError("خطا", "هذا الحقل مطلوب");
            //    return RedirectToPage();
            //}

            if (Tbluserdatum.answer == false)
            {
                ModelState.AddModelError("خطا", "هذا الحقل مطلوب");
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



            return RedirectToPage("Details", new { id = Tbluserdatum.Id });
        }

        private async Task UpdateUserDatumAsync()
        {
            Tbluserdatum.UpdatedAt = DateTime.Now;

            _context.Attach(Tbluserdatum).State = EntityState.Modified;
            _context.Entry(Tbluserdatum).Property(x => x.CreatedAt).IsModified = false;

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


    }

}
