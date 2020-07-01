using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DummyMVCEFModel.Models;
using DummyMVCEFModel.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace DummyMVCEFModel.Controllers
{
    public class HomeController : Controller
    {

        //於HomeController建立DB物件
        dbEmployeeContext db = new dbEmployeeContext();

        //public IActionResult Index()
        //{
        //    return View();
        //}

        public ActionResult Index(int depId = 1)
        {
            ViewBag.DeptName = "正在管理" + db.TDepartment.Where(m => m.FDepId == depId).FirstOrDefault().FDepName + "部門";
            ViewBag.DepId = depId;
            VMDeptEmp vm = new VMDeptEmp()
            {
                department = db.TDepartment.ToList(),
                employee = db.TEmployee.Where(m => m.FDepId == depId).ToList()
            };


            return View(vm);
        }


        //於HomeController建立GET與POST的Create Action
        public ActionResult Create(int depId)
        {
            ViewBag.DepId = depId;
            return View(db.TDepartment.ToList());
        }

        [HttpPost]
        public ActionResult Create(TEmployee emp)
        {
            db.TEmployee.Add(emp);
            db.SaveChanges();

            return RedirectToAction("Index", new { depId = emp.FDepId });
        }

        //於HomeController建立POST的Delete Action
        public ActionResult Delete(string fEmpId)
        {
            var emp = db.TEmployee.Find(fEmpId);
            db.TEmployee.Remove(emp);
            db.SaveChanges();

            return RedirectToAction("Index", new { depId = emp.FDepId });
        }
    }
}
