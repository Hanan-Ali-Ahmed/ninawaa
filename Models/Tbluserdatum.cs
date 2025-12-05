using Ninwa_Employee.Data;
using System;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ninwa_Employee.Models;

public partial class Tbluserdatum
{
    public long Id { get; set; }

    public string UserId { get; set; }
    public string FullName { get; set; }
  

    public string? FirstName { get; set; }
 

    public string? SecondName { get; set; }
    

    public string? ThirdName { get; set; }
    
    public string? LastName { get; set; }
  
    public string? Tittle { get; set; }
    
    public string UserName { get; set; }

    public int? GenderId { get; set; }
   

    public DateOnly? BirthDate { get; set; }

    public string? MotherFullName { get; set; }
    

    public int? MaritalStatusId { get; set; }
   
    public int? NumberOfWives { get; set; }
    public int? AnswerId { get; set; }
   
    public string? PersonalCardNumber { get; set; }
    public string? NationalityNumber { get; set; }
    public string? NewspaperNumber { get; set; }
    public string? RegistrationNumber { get; set; }

    public string? Gover { get; set; }
   
    public string? Judic { get; set; }
   
    public string? Side { get; set; }
  
    public string? City { get; set; }
    
    public string? PhoneNumber { get; set; }
   

    public string? WorkName { get; set; }
   

    public int? EmppartsId { get; set; }
   

    public string? UnitSchoole { get; set; }
   

    public int? EmppositionId { get; set; }
   

    public int? CertificateId { get; set; }
   
    public string? University { get; set; }

    public string? College { get; set; }
    public string? Specialization { get; set; }

    public string? Countrystudy { get; set; }
    public string? Gradyear { get; set; }
    public int? CertificateId2 { get; set; }
   

    public int? CountcertificateId { get; set; }
   

    public int? CertificateId3 { get; set; }
    public DateOnly? LastcertificDate { get; set; }

    public int? scientifictitleId { get; set; }
    

    public DateOnly? HiringDate { get; set; }

    public DateOnly? StartDate { get; set; }

    public int? DropId { get; set; }

    public DateOnly? DropDate { get; set; }

    public DateOnly? Startafterdrop { get; set; }

    public int? BookNumReturnHiring { get; set; }
    public DateOnly? DateBookReturnHiring { get; set; }
    public int? AddId { get; set; }
    public string? AddType { get; set; }
    public int? AddDays { get; set; }
    public int? AddMonths { get; set; }
    public int? AddYears { get; set; }

    public int? EmpstatusId { get; set; }
    public int? vacationId { get; set; }
    public int? vacationMonths { get; set; }
    public int? vacationYears { get; set; }
    public DateOnly? BreakupDate { get; set; }

    public int? MartyrsId { get; set; }

    public int? NumberBookMartyrs { get; set; }
    public DateOnly? DateBookMartyrs { get; set; }

    public int? ChapterId { get; set; }
    public int? ChapterDays { get; set; }
    public int? ChapterMonths { get; set; }
    public int? ChapterYears { get; set; }
    public int? NumberBookChapter { get; set; }
    public DateOnly?DateBookChapter { get; set; }
    public string? Notee { get; set; }


    /////////////// <اتعهد>
    public bool answer { get; set; }

    public bool check { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    
    //TblCertificate
    [ForeignKey("CertificateId")]
    public virtual Tblcertificates? Certificate { get; set; }

    [ForeignKey("CertificateId2")]
    public virtual Tblcertificates? Certificate2 { get; set; }

    [ForeignKey("CertificateId3")]
    public virtual Tblcertificates? Certificate3 { get; set; }

    //Tblanswer
    [ForeignKey("AnswerId")]
    public virtual Tblanswer? answer1 { get; set; }

    [ForeignKey("CountcertificateId")]
    public virtual Tblanswer? answer2 { get; set; }

    [ForeignKey("DropId")]
    public virtual Tblanswer? answer3 { get; set; }
    [ForeignKey("AddId")]
    public virtual Tblanswer? answer4 { get; set; }

    [ForeignKey("MartyrsId")]
    public virtual Tblanswer? answer5 { get; set; }

    [ForeignKey("answer")]
    public virtual Tblanswer? answer6 { get; set; }
    
    public virtual Tblempposition? empposition { get; set; }

    public virtual Tblgender? Gender { get; set; }

    public virtual Tblmaritalstatus? MaritalStatus { get; set; }
    public virtual Tblempstatus? empstatus { get; set; }
    public virtual Tblparts? empparts { get; set; }
    public virtual Tblscientifictitle? scientifictitle { get; set; }
    public virtual Tblvacation? vacation { get; set; }
}