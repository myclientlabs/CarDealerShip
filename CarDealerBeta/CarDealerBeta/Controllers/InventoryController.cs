using CarDealerBeta.Interfaces;
using CarDealerBeta.Models;
using CarDealerBeta.Repositories;
using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarDealerBeta.Controllers
{
    public class InventoryController : BaseController
    {
        IVehicleRepository vehicleRepository;
        public InventoryController()
        {
            vehicleRepository = new VehicleRepository();
        }
        // GET: Inventory
        public ActionResult New()
        {
            ViewBag.IsNewVehicle = true;
            ViewBag.Title = "New Vehicles";
            return View("Search");
        }
        public ActionResult Used()
        {
            ViewBag.IsNewVehicle = false;
            ViewBag.Title = "Used Vehicles";
            return View("Search");
        }
        public ActionResult Details(int id)
        {
            var vehicle = vehicleRepository.GetVehicleById(id);
            return View(vehicle);
        }
        [HttpPost]
        public JsonResult SearchVehicles(SearchParams searchParam)
        {
            return Json(vehicleRepository.SearchVehicles(searchParam), JsonRequestBehavior.AllowGet);
        }
    }
}