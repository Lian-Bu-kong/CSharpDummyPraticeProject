using DummyCPLWeb.Models;
using DummyCPLWeb.Models.Repo;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DummyCPLWeb.AppControllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class CoilScheduleController : ControllerBase
    {
        private readonly ICoilRepository _coilRepository;

        public CoilScheduleController(ICoilRepository coilRepository)
        {
            _coilRepository = coilRepository;
        }

        [HttpGet("GetAllCoilSchedules")]
        public async Task<ActionResult<List<CoilSchedule>>> GetAllCoilSchedules()
        {
            var items = _coilRepository.GetAsyAllCoilSchedule();

            if (items == null)
            {
                return NotFound();
            }

            return await items;
        }



    }
}
