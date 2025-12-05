using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Ninwa_Employee.Data;
using Ninwa_Employee.Models;

namespace Ninwa_Employee.Pages
{
    public class DeleteModel : PageModel
    {
        private readonly Ninwa_Employee.Data.ApplicationDbContext _context;

        public DeleteModel(Ninwa_Employee.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Tbluserdatum Tbluserdatum { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbluserdatum = await _context.Tbluserdatum.FirstOrDefaultAsync(m => m.Id == id);

            if (tbluserdatum == null)
            {
                return NotFound();
            }
            else
            {
                Tbluserdatum = tbluserdatum;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbluserdatum = await _context.Tbluserdatum.FindAsync(id);
            if (tbluserdatum != null)
            {
                Tbluserdatum = tbluserdatum;
                _context.Tbluserdatum.Remove(Tbluserdatum);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
