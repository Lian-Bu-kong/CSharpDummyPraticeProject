using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DummyCPLWeb.Models;
using DummyCPLWeb.Models.Repo;
using DummyCPLWeb.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace DummyCPLWeb.Controllers
{

    public class CoilScheduleController : Controller
    {
        private readonly ICoilRepository _coilRepository;


        public CoilScheduleController(ICoilRepository coilRepository)
        {
            _coilRepository = coilRepository;
        }


        public IActionResult Index()
        {
            var items = _coilRepository.GetAllCoilSchedule();

            var coilScheduleViewModel = new CoilScheduleViewModel
            {
                CoilSchedules = items,
                CoilScheduleTotal = items.Count()
            };

            return View(coilScheduleViewModel);
        }


       


    }
}
