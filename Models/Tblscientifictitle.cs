using System;
using System.Collections.Generic;

namespace Ninwa_Employee.Models;

public partial class Tblscientifictitle
{
    public int Id { get; set; }

    public string? scientifictitle { get; set; }

    public virtual ICollection<Tbluserdatum> Tbluserdata { get; set; } = new List<Tbluserdatum>();


}
