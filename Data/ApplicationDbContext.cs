using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Ninwa_Employee.Models;

namespace Ninwa_Employee.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        //==========================
        //   جـداول النظــــام
        //==========================
        public DbSet<Tbluserdatum> Tbluserdatum { get; set; }
        public DbSet<Tblcertificates> Tblcertificates { get; set; }
        public DbSet<Tblgender> Tblgender { get; set; }
        public DbSet<Tblanswer> Tblanswer { get; set; }
        public DbSet<Tblmaritalstatus> Tblmaritalstatus { get; set; }
        public DbSet<Tblscientifictitle> Tblscientifictitle { get; set; }
        public DbSet<Tblempstatus> Tblempstatus { get; set; }
        public DbSet<Tblvacation> Tblvacation { get; set; }
        public DbSet<Tblempposition> Tblempposition { get; set; }
        public DbSet<Tblparts> Tblparts { get; set; }
        public DbSet<Tblsetting> Tblsetting { get; set; }

        public DbSet<Tblwifesname> Tblwifesnames { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ================================
            //   جداول الشهادات
            // ================================
            modelBuilder.Entity<Tblcertificates>().ToTable("Tblcertificates");

            modelBuilder.Entity<Tblcertificates>().HasData(
                 new Tblcertificates { Id = 1, CertificateType = "دكتوراه" },
                new Tblcertificates { Id = 2, CertificateType = "ماجستير" },
                new Tblcertificates { Id = 3, CertificateType = "دبلوم عالي" },
                new Tblcertificates { Id = 4, CertificateType = "بكالوريوس" },
                new Tblcertificates { Id = 5, CertificateType = "دبلوم" },
                new Tblcertificates { Id = 6, CertificateType = "اعدادية" },
                new Tblcertificates { Id = 7, CertificateType = "متوسطة" },
                new Tblcertificates { Id = 8, CertificateType = "ابتدائية" },
                new Tblcertificates { Id = 9, CertificateType = "محو اميه" },
                new Tblcertificates { Id = 10, CertificateType = "يقرأ ويكتب" },
                new Tblcertificates { Id = 11, CertificateType = "امي" },
                new Tblcertificates { Id = 12, CertificateType = "لا يوجد" }
            );

            // ================================
            //  باقي الجداول الثابتة (Seed)
            // ================================

            modelBuilder.Entity<Tblgender>().HasData(
                new Tblgender { Id = 1, Gender = "ذكر" },
                new Tblgender { Id = 2, Gender = "انثى" }
            );

            modelBuilder.Entity<Tblanswer>().HasData(
                new Tblanswer { Id = 1, answer = "نعم" },
                new Tblanswer { Id = 2, answer = "كلا" }
            );

            modelBuilder.Entity<Tblmaritalstatus>().HasData(
                new Tblmaritalstatus { Id = 1, MaritalStatus = "اعزب" },
                new Tblmaritalstatus { Id = 2, MaritalStatus = "باكر" },
                new Tblmaritalstatus { Id = 3, MaritalStatus = "متزوج" },
                new Tblmaritalstatus { Id = 4, MaritalStatus = "مطلق" },
                new Tblmaritalstatus { Id = 5, MaritalStatus = "ارمل" }
            );

            modelBuilder.Entity<Tblscientifictitle>().HasData(
                new Tblscientifictitle { Id = 1, scientifictitle = "استاذ" },
                new Tblscientifictitle { Id = 2, scientifictitle = "استاذ مساعد" },
                new Tblscientifictitle { Id = 3, scientifictitle = "مدرس" },
                new Tblscientifictitle { Id = 4, scientifictitle = "مدرس مساعد" },
                new Tblscientifictitle { Id = 5, scientifictitle = "لايوجد" }
            );

            modelBuilder.Entity<Tblempstatus>().HasData(
                new Tblempstatus { Id = 1, empstatus = "مستمر" },
                new Tblempstatus { Id = 2, empstatus = "مجاز" },
                new Tblempstatus { Id = 3, empstatus = "سحب يد" },
                new Tblempstatus { Id = 4, empstatus = "اخرى" }
            );

            modelBuilder.Entity<Tblvacation>().HasData(
                new Tblvacation { Id = 1, vacation = "لا يوجد" },
                new Tblvacation { Id = 2, vacation = "سنة بدون راتب" },
                new Tblvacation { Id = 3, vacation = "سنتان بدون راتب" },
                new Tblvacation { Id = 4, vacation = "خمس سنوات" },
                new Tblvacation { Id = 5, vacation = "اجازة دراسية" },
                new Tblvacation { Id = 6, vacation = "اجازة امومة" },
                new Tblvacation { Id = 7, vacation = "اجازة مرضية" },
                new Tblvacation { Id = 8, vacation = "اجازة المعين المتفرغ" },
                new Tblvacation { Id = 9, vacation = "اخرى" }
            );

            modelBuilder.Entity<Tblempposition>().HasData(
                new Tblempposition { Id = 1, empposition = "المدير العام" },
                new Tblempposition { Id = 2, empposition = "معاون المدير العام" },
                new Tblempposition { Id = 3, empposition = "مدير قسم" },
                new Tblempposition { Id = 4, empposition = "معاون مدير قسم" },
                new Tblempposition { Id = 5, empposition = "مسؤول شعبة" },
                new Tblempposition { Id = 6, empposition = "مسؤول وحدة" },
                new Tblempposition { Id = 7, empposition = "مدير مدرسة" },
                new Tblempposition { Id = 8, empposition = "معاون مدير مدرسة" },
                new Tblempposition { Id = 9, empposition = "لا يوجد" }
            );

            modelBuilder.Entity<Tblparts>().HasData(
                 new Tblparts { Id = 1, empparts = "مكتب المدير العام" },
                new Tblparts { Id = 2, empparts = "مكتب المعاون الاداري" },
                new Tblparts { Id = 3, empparts = "مكتب المعاون الفني" },
                new Tblparts { Id = 4, empparts = "التخطيط التربوي" },
                new Tblparts { Id = 5, empparts = "الامتحانات" },
                new Tblparts { Id = 6, empparts = "الاعداد والتدريب" },
                new Tblparts { Id = 7, empparts = "شؤون المناهج والتقنيات التربوية" },
                new Tblparts { Id = 8, empparts = "التعليم المهني" },
                new Tblparts { Id = 9, empparts = "النشاط الرياضي" },
                new Tblparts { Id = 10, empparts = "النشاط المدرسي" },
                new Tblparts { Id = 11, empparts = "شؤون المالية" },
                new Tblparts { Id = 12, empparts = "الادارة والتجهيزات" },
                new Tblparts { Id = 13, empparts = "الموارد البشرية" },
                new Tblparts { Id = 14, empparts = "التعليم العام والملاك" },
                new Tblparts { Id = 15, empparts = "محو الامية" },
                new Tblparts { Id = 16, empparts = "التدقيق والرقابة الداخلية" },
                new Tblparts { Id = 17, empparts = "الاشراف الاختصاصي" },
                new Tblparts { Id = 18, empparts = "الاشراف التربوي" },
                new Tblparts { Id = 19, empparts = "الدراسة الايزيدية" },
                new Tblparts { Id = 20, empparts = "الدراسة الكردية" },
                new Tblparts { Id = 21, empparts = "الدراسة السريانية" },
                new Tblparts { Id = 22, empparts = "الدراسة التركمانية" },
                new Tblparts { Id = 23, empparts = "تربية ام الربيعين" },
                new Tblparts { Id = 24, empparts = "تربية الحدباء" },
                new Tblparts { Id = 25, empparts = "تربية قضاء الموصل" },
                new Tblparts { Id = 26, empparts = "تربية تلعفر" },
                new Tblparts { Id = 27, empparts = "تربية الحضر" },
                new Tblparts { Id = 28, empparts = "تربية البعاج" },
                new Tblparts { Id = 29, empparts = "تربية سنجار" },
                new Tblparts { Id = 30, empparts = "تربية ربيعة" },
                new Tblparts { Id = 31, empparts = "تربية النمرود" },
                new Tblparts { Id = 32, empparts = "تربية القيروان" },
                new Tblparts { Id = 33, empparts = "تربية المحلبية" },
                new Tblparts { Id = 34, empparts = "تربية تلكيف" },
                new Tblparts { Id = 35, empparts = "تربية الحمدانية" },
                new Tblparts { Id = 36, empparts = "تربية بعشيقة" },
                new Tblparts { Id = 37, empparts = "تربية اربيل المؤقت" },
                new Tblparts { Id = 38, empparts = "تربية دهوك المؤقت" }
            );

            // ===============================
            // العلاقات مع Tbluserdatum
            // ===============================

            modelBuilder.Entity<Tbluserdatum>()
                .HasOne(d => d.Certificate)
                .WithMany()
                .HasForeignKey(d => d.CertificateId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Tbluserdatum>()
                .HasOne(d => d.empposition)
                .WithMany(p => p.Tbluserdata)
                .HasForeignKey(d => d.EmppositionId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Tbluserdatum>()
                .HasOne(d => d.Certificate2)
                .WithMany()
                .HasForeignKey(d => d.CertificateId2)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Tbluserdatum>()
                .HasOne(d => d.Certificate3)
                .WithMany()
                .HasForeignKey(d => d.CertificateId3)
                .OnDelete(DeleteBehavior.Restrict);

            // ===============================
            // العلاقات مع Tblanswer
            // ===============================

            modelBuilder.Entity<Tbluserdatum>()
                .HasOne(d => d.answer1)
                .WithMany()
                .HasForeignKey(d => d.AnswerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Tbluserdatum>()
                .HasOne(d => d.answer2)
                .WithMany()
                .HasForeignKey(d => d.CountcertificateId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Tbluserdatum>()
                .HasOne(d => d.answer3)
                .WithMany()
                .HasForeignKey(d => d.DropId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Tbluserdatum>()
               .HasOne(d => d.answer4)
               .WithMany()
               .HasForeignKey(d => d.AddId)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Tbluserdatum>()
                .HasOne(d => d.answer5)
                .WithMany()
                .HasForeignKey(d => d.MartyrsId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Tbluserdatum>()
              .HasOne(d => d.answer6)
              .WithMany()
              .HasForeignKey(d => d.ChapterId)
              .OnDelete(DeleteBehavior.Restrict);

           
        }
    }
}
