using cwiczenia_6.DTO;
using cwiczenia_6.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace cwiczenia_6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorsController : Controller
    {
        private readonly IDbService _dbService;
        public DoctorsController(IDbService dbService)
        {
            _dbService = dbService;
        }

        [HttpGet]
        public async Task<IActionResult> GetDoctors()
        {

            var doctors = await _dbService.GetDoctors();
            return Ok(doctors);

        }

        [HttpPost]
        public async Task<IActionResult> AddDoctor(SomeSortOfDoctor doctor)
        {

            var added = await _dbService.AddDoctor(doctor);

            if (added)
            {
                return Ok("Doctor has been added");
            }
            else
            {
                return BadRequest("Cannot add doctor");
            }
        }

        [HttpPut("{IdDoctor}")]
        public async Task<IActionResult> EditDoctor(SomeSortOfDoctor doctor, int IdDoctor)
        {
            var edited = await _dbService.EditDoctor(doctor, IdDoctor);

            if (edited)
            {
                return Ok("Doctor has been edited");
            }
            else
            {
                return BadRequest("Cannot edit doctor");
            }
        }
        [HttpDelete("{IdDoctor}")]
        public async Task<IActionResult> DeleteDoctor(int IdDoctor)
        {
            var deleted = await _dbService.DeleteDoctor(IdDoctor);

            if (deleted)
            {
                return Ok("Doctor has been deleted");
            }
            else {
                return BadRequest("Cannot delete doctor");
            }
        }
    }
}
