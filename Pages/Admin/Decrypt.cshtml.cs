using Ninwa_Employee.Data;
using Ninwa_Employee.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;

namespace Ninwa_Employee.Pages.Admin
{
    public class DecryptModel : PageModel
    {

        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public DecryptModel(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _context = context;
        }

        public string Email { get; set; }
        [BindProperty]
        public string? SearchResultMessage { get; set; }

        [BindProperty]
        public string SearchInput { get; set; }

       

        public Tbluserdatum Tbluserdatum { get; set; } = default!;
        //public Tblpoint Tblpoint { get; set; } = default!;
        public string[] DecryptedDataParts { get; set; }

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

                return Page();
            }


            Tbluserdatum = await _context.Tbluserdatum
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
                .FirstOrDefaultAsync(m => m.UserId == UserId);

            if (Tbluserdatum == null)
            {
                return NotFound();
            }



            //Tblpoint = await _context.Tblpoints.FirstOrDefaultAsync(p => p.UserId == UserId);
            //if (Tblpoint == null)
            //{
            //    return NotFound();
            //}


            //var wives = await _context.Tblwifesnames.Where(w => w.UserId == UserId).ToListAsync();
            //if (wives.Count > 0) FirstWife = wives.ElementAtOrDefault(0)?.WifesName;
            //if (wives.Count > 1) SecondWife = wives.ElementAtOrDefault(1)?.WifesName;
            //if (wives.Count > 2) ThirdWife = wives.ElementAtOrDefault(2)?.WifesName;
            //if (wives.Count > 3) FourthWife = wives.ElementAtOrDefault(3)?.WifesName;


            return Page();
        }
        public async Task<IActionResult> OnPostSearchUserAsync()
        {
            if (string.IsNullOrEmpty(SearchInput))
            {
                SearchResultMessage = "«œŒ· «·√”„ «·—»«⁄Ì «Ê «”„ «·„” Œœ„";
                //await LoadRelatedData();
                return Page();
            }

            var user = await _context.Tbluserdatum
                .FirstOrDefaultAsync(u => u.UserName == SearchInput);

            if (user != null)
            {
                return RedirectToPage(new { UserId = user.UserId });
            }
            else
            {
                SearchResultMessage = "«·„” Œœ„ €Ì— „ÊÃÊœ.";
                //await LoadRelatedData();
                return Page();
            }
        }
        //public async Task<IActionResult> OnPostAsync()
        //{
        //    //var user = await _userManager.GetUserAsync(User);
        //    //if (user != null)
        //    //{

        //    //    Email = user.Email;
        //    //}

        //    try
        //    {

        //        _context.Attach(Tbluserdatum).State = EntityState.Modified;
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!TbluserdatumExists(Tbluserdatum.Id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return Page();
        //}

        //private bool TbluserdatumExists(long id)
        //{
        //    return _context.Tbluserdata.Any(e => e.Id == id);
        //}

    }
}