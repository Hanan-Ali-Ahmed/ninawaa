using Microsoft.AspNetCore.Identity;

namespace Ninwa_Employee.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string UserFullName { get; set; } = "";
        public int? NumberOfEdit { get; set; }
        public int? Depart { get; set; }

        public string Permissions { get; set; } = ""; // مثال: "AddUser,EditUser,Stats,Reports"


    }
}

