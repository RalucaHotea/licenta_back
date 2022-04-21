using BusinessLogicLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoschStore.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class StatisticsController : ControllerBase
    {
        private readonly IStatisticsService statisticsService;

        public StatisticsController(
       IStatisticsService statisticsService
        )
        {
            this.statisticsService = statisticsService;
        }

        [HttpGet]
        [Route("GetOverviewOrdersStatistics")]
        public async Task<IActionResult> GetOverviewOrdersStatistics()
        {
            var statistics = await statisticsService.GetProductsStatisticsDto();

            if (statistics == null)
            {
                return BadRequest("No statistics could be generated.");
            }

            return Ok(statistics);
        }

        [HttpGet]
        [Route("GetMonthlyStatistics")]
        public async Task<IActionResult> GetMonthlyStatistics([FromQuery] int year)
        {
            var statistics = await statisticsService.GetMonthlyStatistics(year);

            if (statistics == null)
            {
                return BadRequest("No statistics could be generated.");
            }

            return Ok(statistics);
        }
    }
}
