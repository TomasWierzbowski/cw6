using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace cwiczenia_6.Models
{
    public class Medicament
    {
        [Key]
        public int IdMedicament;
        [Required]
        [MaxLength(100)]
        public string Name;
        [Required]
        [MaxLength(100)]
        public string Description;
        [Required]
        [MaxLength(100)]
        public string Type;
        public virtual ICollection<Prescription_Medicament> Prescription_Medicaments { get; set; }
    }
}
