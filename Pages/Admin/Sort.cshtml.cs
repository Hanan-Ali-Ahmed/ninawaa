using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Ninwa_Employee.Pages.Admin
{
    //[Authorize(Roles = "Admin")]
  //  [Authorize]
    public class SortModel : PageModel
    {
        public async Task<IActionResult> OnGetAsync()
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
            return Page();
        }
    }
}
