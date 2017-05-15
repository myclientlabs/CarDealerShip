using CarDealerBeta.Interfaces;
using CarDealerBeta.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarDealerBeta.Controllers
{
    public class ReportsController : BaseController
    {
        IReportRepository reportRepository;
        IUserRepository userRepository;
        public ReportsController()
        {
            reportRepository = new ReportRepository();
            userRepository = new UserRepository();
        }
        // GET: Reports
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Inventory()
        {
            var newVehiclesReport= reportRepository.GetInventoryReport(true);
            var oldVehiclesReport = reportRepository.GetInventoryReport(false);

            ViewBag.NewVehiclesReport = newVehiclesReport;
            ViewBag.OldVehiclesReport = oldVehiclesReport;

            return View();
        }
        public ActionResult Sales()
        {
            ViewBag.Users = from u in userRepository.GetAllUsers()
                            select new { Id = u.Id, Name = u.FirstName + ", " + u.LastName };
            return View();
        }

        public JsonResult GetSalesReport(int? userId, DateTime? fromDate, DateTime? toDate)
        {
            var salesReport = reportRepository.GetSalesReport(userId, fromDate, toDate);
            return Json(salesReport, JsonRequestBehavior.AllowGet);
        }


    }
}