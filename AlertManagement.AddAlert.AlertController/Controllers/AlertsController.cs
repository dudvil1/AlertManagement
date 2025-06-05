using AlertManagement.AddAlert.AlertModels;
using AlertManagement.AddAlert.AlertBusinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using AlertManagement.AddAlert.AlertModels.AlertModels;

namespace AlertManagement.AddAlert.AlertController.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlertsController : ControllerBase
    {
        private readonly IAlertService _alertService;

        public AlertsController(IAlertService alertService)
        {
            _alertService = alertService;
        }

        [HttpPost]
        public async Task<IActionResult> AddAlert([FromBody] AddAlertRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new AddAlertResponse
                {
                    Success = false,
                    Message = "Invalid input data"
                });
            }

            var result = await _alertService.AddAlertAsync(request);

            if (!result.Success)
            {
                return Conflict(result); // או BadRequest תלוי בסיבה
            }

            return Ok(result);
        }
    }
}
