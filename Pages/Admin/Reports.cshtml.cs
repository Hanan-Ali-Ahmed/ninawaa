using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Ninwa_Employee.Data;
using Ninwa_Employee.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ninwa_Employee.Pages.Admin
{
    public class ReportsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public ReportsModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Tbluserdatum> Tbluserdatum { get; set; } = new List<Tbluserdatum>();
        public IList<Tblwifesname> Tblwifesname { get; set; } = new List<Tblwifesname>();

        public int PageSize { get; set; } = 1;
        public int TotalRecords { get; set; }
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? LastUserId { get; set; }

        [BindProperty(SupportsGet = true)]
        public bool loadAll { get; set; } = false; // لتحديد إذا كان المستخدم ضغط "تحميل الكل"
        [BindProperty(SupportsGet = true)]
        public string? SearchTerm { get; set; }

        public async Task OnGetAsync()
        {
            Tblwifesname = await _context.Tblwifesnames.ToListAsync();
            TotalRecords = await _context.Tbluserdatum.CountAsync();
            TotalPages = (int)Math.Ceiling(TotalRecords / (double)PageSize);

            if (loadAll)
            {
                // تحميل كل البيانات دفعة واحدة
                Tbluserdatum = await _context.Tbluserdatum
                    .Include(t => t.Certificate)
                    .Include(t => t.Certificate2)
                    .Include(t => t.Certificate3)
                    .Include(t => t.Gender)
                    .Include(t => t.MaritalStatus)
                    .Include(t => t.answer1)
                    .Include(t => t.answer2)
                    .Include(t => t.answer3)
                    .Include(t => t.answer4)
                    .Include(t => t.answer5)
                    .Include(t => t.answer6)
                    .Include(t => t.empparts)
                    .Include(t => t.empposition)
                    .Include(t => t.empstatus)
                    .Include(t => t.scientifictitle)
                    .Include(t => t.vacation)
                    .OrderBy(t => t.UserId)
                    .ToListAsync();
            }
            else
            {
                // Lazy loading
                if (string.IsNullOrEmpty(LastUserId))
                {
                    Tbluserdatum = await LoadNextPageAsync(null);
                }
                else
                {
                    Tbluserdatum = await LoadNextPageAsync(LastUserId);
                }

                if (Tbluserdatum.Any())
                {
                    LastUserId = Tbluserdatum.Last().UserId;
                }

            }

        }

        public async Task<IList<Tbluserdatum>> LoadNextPageAsync(string? lastUserId)
        {
            var query = _context.Tbluserdatum
                .Include(t => t.Certificate)
                .Include(t => t.Certificate2)
                .Include(t => t.Certificate3)
                .Include(t => t.Gender)
                .Include(t => t.MaritalStatus)
                .Include(t => t.answer1)
                .Include(t => t.answer2)
                .Include(t => t.answer3)
                .Include(t => t.answer4)
                .Include(t => t.answer5)
                .Include(t => t.answer6)
                .Include(t => t.empparts)
                .Include(t => t.empposition)
                .Include(t => t.empstatus)
                .Include(t => t.scientifictitle)
                .Include(t => t.vacation)
                .OrderBy(t => t.UserId)
                .AsQueryable();

            if (!string.IsNullOrEmpty(lastUserId))
            {
                query = query.Where(t => string.Compare(t.UserId, lastUserId) > 0);
            }

            if (!string.IsNullOrEmpty(SearchTerm)) // هنا
            {
                query = query.Where(t => t.UserName == SearchTerm ||  // مطابق تماماً
                                         t.FullName.Contains(SearchTerm) ||
                                         t.FirstName.Contains(SearchTerm) ||
                                         t.LastName.Contains(SearchTerm));
            }

            return await query.Take(PageSize).ToListAsync();
        }
    }
}