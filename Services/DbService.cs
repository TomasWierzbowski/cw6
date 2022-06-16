using cwiczenia_6.DTO;
using cwiczenia_6.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cwiczenia_6.Services
{
    public class DbService : IDbService
    {
        private readonly MainDbContext _dbContext;
        public DbService(MainDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<SomeSortOfDoctor>> GetDoctors()
        {
            return await _dbContext.Doctors
                .Select(e => new SomeSortOfDoctor
                {
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    Email = e.Email
                }).ToListAsync();
        }

        public async Task<bool> AddDoctor(SomeSortOfDoctor doctor)
        {
            // do not allow do add doctor with same information
            var doctorsCheck = await _dbContext.Doctors.Where(e => e.FirstName == doctor.FirstName).Where(e => e.LastName == doctor.LastName).Where(e => e.Email == doctor.Email).FirstOrDefaultAsync();
            if (doctorsCheck != null)
            {
                return false;
            }

            var addDoctor = new Doctor { FirstName = doctor.FirstName, LastName = doctor.LastName, Email = doctor.Email };
            var added = _dbContext.Add(addDoctor);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> EditDoctor(SomeSortOfDoctor doctor, int IdDoctor)
        {

            // check if all fields are passed
            if (String.IsNullOrEmpty(IdDoctor.ToString()) || String.IsNullOrEmpty(doctor.FirstName) || String.IsNullOrEmpty(doctor.LastName) || String.IsNullOrEmpty(doctor.Email))
            {
                return false;
            }

            // check if we have this doctor, if yes can update
            var doctorsCheck = await _dbContext.Doctors.Where(e => e.IdDoctor == IdDoctor).FirstOrDefaultAsync();
            if (doctorsCheck is null)
            {
                return false;
            }

            var editDoctor = await _dbContext.Doctors.Where(e => e.IdDoctor == IdDoctor).FirstOrDefaultAsync();
            editDoctor.FirstName = doctor.FirstName;
            editDoctor.LastName = doctor.LastName;
            editDoctor.Email = doctor.Email;
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteDoctor(int IdDoctor)
        {
            // check if doctor exists
            var checkDoctor = await _dbContext.Doctors.Where(e => e.IdDoctor == IdDoctor).FirstOrDefaultAsync();
            if (checkDoctor is null)
            {
                return false;
            }

            // check if he he exists as foreign key in any prescription; if yes we cannot delete as he is added to some prescriptions
            var checkPrescriptions = await _dbContext.Prescriptions.Where(e => e.IdDoctor == IdDoctor).FirstOrDefaultAsync();
            if (checkPrescriptions != null)
            {
                return false;
            }

            _dbContext.Attach(checkDoctor);
            _dbContext.Remove(checkDoctor);

            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<SomeSortOfPrescription>> GetPrescription(int IdPrescription)
        {
            return await _dbContext.Prescriptions
                .Include(e => e.Doctor)
                .Include(e => e.Patient)
                .Include(e => e.Prescription_Medicaments)
                    .ThenInclude(e => e.Medicament)
                .Select(e => new SomeSortOfPrescription
                {
                    DoctorFirstName = e.Doctor.FirstName,
                    DoctorLastName = e.Doctor.LastName,
                    DoctorEmail = e.Doctor.Email,
                    PatientFirstName = e.Patient.FirstName,
                    PatientLastName = e.Patient.LastName,
                    PatientBrithDate = e.Patient.BirthDate,
                    IdPrescription = e.IdPrescription,
                    // why below isnt working?
                    Medicaments = e.Prescription_Medicaments.Select(e => new SomeSortOfMedicament { Name = e.Medicament.Name, Description = e.Medicament.Description, Type = e.Medicament.Type }).ToList()
                })
                .Where(e => e.IdPrescription == IdPrescription)
                .ToListAsync();
        }
    }
}
