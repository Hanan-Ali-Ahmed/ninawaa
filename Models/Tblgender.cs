using System;
using System.Collections.Generic;
namespace Ninwa_Employee.Models;
public partial class Tblgender
{
    public int Id { get; set; }

    public string? Gender { get; set; }

    public virtual ICollection<Tbluserdatum> Tbluserdata { get; set; } = new List<Tbluserdatum>();


}