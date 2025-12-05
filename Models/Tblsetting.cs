using System.ComponentModel.DataAnnotations;

namespace Ninwa_Employee.Models
{
    public class Tblsetting
    {

        public int Id { get; set; }
        public int? NumberOfEdit { get; set; }
        public DateOnly? ClosingDate { get; set; }
        public string? IsOpen { get; set; }

    }
}
