using cwiczenia_6.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace cwiczenia_6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrescriptionsController : ControllerBase
    {
        private readonly IDbService _dbService;
        public PrescriptionsController(IDbService dbService)
        {
            _dbService = dbService;
        }

        [HttpGet("{IdPrscription}")]
        public async Task<IActionResult> GetPrescription(int IdPrscription)
        {
            var prescriptionDetails = await _dbService.GetPrescription(IdPrscription);
            return Ok(prescriptionDetails);
        }

    }
}
