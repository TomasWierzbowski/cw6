using Microsoft.EntityFrameworkCore;
using System;

namespace cwiczenia_6.Models
{
    public class MainDbContext : DbContext
    {
        protected MainDbContext()
        {
        }
        public MainDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Patient> Patients { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Prescription> Prescriptions { get; set; }
        public DbSet<Medicament> Medicaments { get; set; }
        public DbSet<Prescription_Medicament> Prescription_Medicaments { get; set; }

        //    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
        //       optionsBuilder.UseSqlServer("");
        //;        }

        protected override void OnModelCreating(ModelBuilder modelbuilder) {
            base.OnModelCreating(modelbuilder);

           modelbuilder.Entity<Patient>(p => {
               p.HasKey(e => e.IdPatient);
               p.Property(e => e.FirstName).IsRequired().HasMaxLength(100);
               p.Property(e => e.LastName).IsRequired().HasMaxLength(100);
               p.Property(e => e.BirthDate).IsRequired();

               p.HasData(
                   new Patient { IdPatient = 1, FirstName = "Jan", LastName = "Kowalski", BirthDate = DateTime.Parse("2000-01-01") },
                   new Patient { IdPatient = 2, FirstName = "Ada", LastName = "Nowak", BirthDate = DateTime.Parse("2001-01-01") }
                   );

           });

            modelbuilder.Entity<Doctor>(d => {
                d.HasKey(e => e.IdDoctor);
                d.Property(e => e.FirstName).IsRequired().HasMaxLength(100);
                d.Property(e => e.LastName).IsRequired().HasMaxLength(100);
                d.Property(e => e.Email).IsRequired().HasMaxLength(60);

                d.HasData(
                    new Doctor { IdDoctor = 1, FirstName = "Jan", LastName = "Kowalski", Email = "jan@kowalski.com"},
                    new Doctor { IdDoctor = 2, FirstName = "Ada", LastName = "Nowak", Email = "ada@nowak.com" }
    );
            });

            modelbuilder.Entity<Prescription>(p => {
                p.HasKey(e => e.IdPrescription);
                p.Property(e => e.Date).IsRequired();
                p.Property(e => e.DueDate).IsRequired();

                p.HasOne(e => e.Patient).WithMany(e => e.Prescriptions).HasForeignKey(e => e.IdPatient);
                p.HasOne(e => e.Doctor).WithMany(e => e.Prescriptions).HasForeignKey(e => e.IdDoctor);

                p.HasData(
                    new Prescription { IdPrescription = 1, Date = DateTime.Parse("2022-01-01"), DueDate = DateTime.Parse("2022-03-01"), IdDoctor=1, IdPatient = 2 },
                    new Prescription { IdPrescription = 2, Date = DateTime.Parse("2022-01-01"), DueDate = DateTime.Parse("2022-03-01"), IdDoctor = 2, IdPatient = 1 }
);
            });

            modelbuilder.Entity<Medicament>(m => {
                m.HasKey(e => e.IdMedicament);
                m.Property(e => e.Name).IsRequired().HasMaxLength(100);
                m.Property(e => e.Description).IsRequired().HasMaxLength(100);
                m.Property(e => e.Type).IsRequired().HasMaxLength(100);

                m.HasData(
                    new Medicament { IdMedicament = 1, Name = "tabletka na katar", Description = "Super tableka na katar", Type="Testowy typ 1" },
                    new Medicament { IdMedicament = 2, Name = "tabletka na bol glowy", Description = "Super tableka na bol glowy", Type="Testowy typ 2" }
                    );

            });

            modelbuilder.Entity<Prescription_Medicament>(pm => {
                pm.HasKey(e => new {
                    e.IdMedicament,
                    e.IdPrescription
                });
                pm.Property(e => e.Dose).IsRequired();
                pm.Property(e => e.Details).IsRequired().HasMaxLength(100);

                pm.HasOne(e => e.Medicament).WithMany(e => e.Prescription_Medicaments).HasForeignKey(e => e.IdMedicament);
                pm.HasOne(e => e.Prescription).WithMany(e => e.Prescription_Medicaments).HasForeignKey(e => e.IdPrescription);

                pm.HasData(
                    new Prescription_Medicament { IdMedicament = 1, IdPrescription = 1, Dose = 3, Details = "nie przekraczac dawki" },
                    new Prescription_Medicament { IdMedicament = 2, IdPrescription = 2, Dose = 2, Details = "nie przekraczac dawki 2" }
    );
            });

        }

    }
}
