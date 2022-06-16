using cwiczenia_6.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace cwiczenia_6.Services
{
    public interface IDbService
    {
        Task<IEnumerable<SomeSortOfDoctor>> GetDoctors();
        Task<bool> AddDoctor(SomeSortOfDoctor doctor);
        Task<bool> EditDoctor(SomeSortOfDoctor doctor, int IdDoctor);
        Task<bool> DeleteDoctor(int IdDoctor);
        Task<IEnumerable<SomeSortOfPrescription>> GetPrescription(int IdPrescription);
    }
}
