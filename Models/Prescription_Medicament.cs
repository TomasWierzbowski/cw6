using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cwiczenia_6.Models
{
    public class Prescription_Medicament
    {
        public int IdMedicament;
        public int IdPrescription;
        public int Dose;
        public string Details;
        public virtual Medicament Medicament { get; set; }
        public virtual Prescription Prescription { get; set; }
    }
}
