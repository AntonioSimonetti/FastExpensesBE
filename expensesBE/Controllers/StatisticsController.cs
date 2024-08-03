using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using expensesBE.Data.DTO.Interfaces;
using System.Threading.Tasks;

namespace expensesBE.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class StatisticsController : ControllerBase
    {
        private readonly IStatisticsServices _statisticsServices;

        public StatisticsController(IStatisticsServices statisticsServices)
        {
            _statisticsServices = statisticsServices;
        }

        [HttpGet]
        public async Task<IActionResult> GetExpenseAmountPerCategory()
        {
            try
            {
                var result = await _statisticsServices.GetExpenseAmountPerCategory();
                return Ok(result);
            }
            catch (Exception ex)
            {
                // Handle the exception appropriately
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
    }
}
