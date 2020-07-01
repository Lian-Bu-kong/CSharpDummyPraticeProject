using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DummyControllerViewModel.Models;
using Microsoft.AspNetCore.Mvc;

namespace DummyControllerViewModel.Controllers
{
    public class HomeController : Controller
    {
        //建立GET與POST的Create方法
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Product product)
        {
            ViewBag.PId = product.PId;
            ViewBag.PName = product.PName;
            ViewBag.Price = product.Price;

            return View();
        }
    }
}
