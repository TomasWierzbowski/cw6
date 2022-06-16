using cwiczenia_6.Models;
using System;
using System.Collections.Generic;

namespace cwiczenia_6.DTO
{
    public class SomeSortOfPrescription
    {
        // from prescription
        public int IdPrescription { get; set; }
        public ICollection<SomeSortOfMedicament> Medicaments { get; set; }
        // from doctor
        public string DoctorFirstName { get; set; }
        public string DoctorLastName { get; set; }
        public string DoctorEmail { get; set; }
        //from patient
        public string PatientFirstName { get; set; }
        public string PatientLastName { get; set; }
        public DateTime PatientBrithDate { get; set; }
    }
}
