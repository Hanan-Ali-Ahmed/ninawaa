using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Ninwa_Employee.Data;
using Ninwa_Employee.Models;
using System.Threading.Tasks;

namespace Ninwa_Employee.Pages.User
{
    public class DetailsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DetailsModel(ApplicationDbContext context)
        {
            _context = context;

        }
        public IList<Tblwifesname> Tblwifesname { get; set; } = default!;

        [BindProperty]
      
        public string? FirstWife { get; set; }

        [BindProperty]
        public string? SecondWife { get; set; }

        [BindProperty]
        public string? ThirdWife { get; set; }

        [BindProperty]
        public string? FourthWife { get; set; }

        public Tbluserdatum Tbluserdatum { get; set; } = default!;
        public string? EncryptedData { get; set; }
       public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (!User.Identity.IsAuthenticated || !User.IsInRole("User"))
            {
                return NotFound();
            }

            if (id == null)
            {
                return NotFound();
            }

            // 1. جلب بيانات المستخدم أولاً
            Tbluserdatum? tbluserdatum = await _context.Tbluserdatum
                .Include(t => t.Certificate)
                .Include(t => t.Certificate2)
                .Include(t => t.Certificate3)
                .Include(t => t.empposition)
                .Include(t => t.empparts)
                .Include(t => t.answer1)
                .Include(t => t.answer2)
                .Include(t => t.answer3)
                .Include(t => t.answer4)
                .Include(t => t.answer5)
                .Include(t => t.answer6)
                .Include(t => t.MaritalStatus)
                .Include(t => t.Gender)
                .Include(t => t.empstatus)
                .Include(t => t.vacation)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (tbluserdatum == null)
            {
                return NotFound();
            }

            Tbluserdatum = tbluserdatum;

            // 2. التأكد من UserId قبل استخدامه
            string? userIdStr = Tbluserdatum.UserId?.Trim();
            if (!string.IsNullOrEmpty(userIdStr))
            {
                var wives = await _context.Tblwifesnames
                    .Where(w => w.UserId.Trim() == userIdStr)
                    .ToListAsync();

                if (wives.Count > 0) FirstWife = wives[0].WifesName;
                if (wives.Count > 1) SecondWife = wives[1].WifesName;
                if (wives.Count > 2) ThirdWife = wives[2].WifesName;
                if (wives.Count > 3) FourthWife = wives[3].WifesName;
            }
            else
            {
                FirstWife = SecondWife = ThirdWife = FourthWife = "لا يوجد";
            }

            // 3. تشفير بيانات العرض (اختياري)
            string dataToEncrypt = $"{Tbluserdatum.UserName}\n{Tbluserdatum.FirstName} {Tbluserdatum.SecondName} {Tbluserdatum.ThirdName} {Tbluserdatum.LastName}";
            EncryptedData = dataToEncrypt;

            return Page();
        }

    }
}