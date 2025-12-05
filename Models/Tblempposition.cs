using System;
using System.Collections.Generic;

namespace Ninwa_Employee.Models;

public partial class Tblempposition
{
    public int Id { get; set; }

    public string? empposition { get; set; }

    public virtual ICollection<Tbluserdatum> Tbluserdata { get; set; } = new List<Tbluserdatum>();


}
