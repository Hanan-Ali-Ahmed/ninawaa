using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Ninwa_Employee.Data;
using Ninwa_Employee.Models;

namespace Ninwa_Employee.Pages
{
    public class CreateModel : PageModel
    {
        private readonly Ninwa_Employee.Data.ApplicationDbContext _context;

        public CreateModel(Ninwa_Employee.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Tbluserdatum Tbluserdatum { get; set; } = default!;

        // دالة لإعادة ملء القوائم الترميزية
        private void PopulateDropdowns()
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

        // عند فتح صفحة Create
        public IActionResult OnGet()
        {
            PopulateDropdowns();
            return Page();
        }

        // عند الضغط على زر Create
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                PopulateDropdowns(); // ⬅️ إعادة ملء القوائم الترميزية عند وجود خطأ
                return Page();
            }

            _context.Tbluserdatum.Add(Tbluserdatum);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Admin/Reports");
        }
    }
}
