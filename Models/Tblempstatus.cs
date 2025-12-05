using System;
using System.Collections.Generic;

namespace Ninwa_Employee.Models;

public partial class Tblempstatus
{
    public int Id { get; set; }

    public string? empstatus { get; set; }

    public virtual ICollection<Tbluserdatum> Tbluserdata { get; set; } = new List<Tbluserdatum>();


}