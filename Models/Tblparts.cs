using System;
using System.Collections.Generic;

namespace Ninwa_Employee.Models;

public partial class Tblparts
{
    public int Id { get; set; }

    public string? empparts { get; set; }

    public virtual ICollection<Tbluserdatum> Tbluserdata { get; set; } = new List<Tbluserdatum>();


}