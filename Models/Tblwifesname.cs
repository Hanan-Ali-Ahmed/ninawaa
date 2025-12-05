using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Ninwa_Employee.Models;

public partial class Tblwifesname
{
    public int Id { get; set; }

    public string? WifesName { get; set; }

    public string? UserId { get; set; }


  
}
