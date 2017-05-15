using CarDealerBeta.Interfaces;
using CarDealerBeta.Models;
using CarDealerBeta.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarDealerBeta.Controllers
{
    public class SalesController : BaseController
    {
        IVehicleRepository vehicleRepository;
        ILookupRepository lookupRepository;
        ISalesPersonRepository salesPersonRepository;
        public SalesController()
        {
            vehicleRepository = new VehicleRepository();
            lookupRepository = new LookupRepository();
            salesPersonRepository = new SalesPersonRepository();
        }
        // GET: Sales
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Purchase(int id)
        {
            var vehicle = vehicleRepository.GetVehicleById(id);
            PurchaseViewModel model = new Models.PurchaseViewModel();
            model.Vehicle = vehicle;
            model.Sales = new Models.SalesViewModel();
            InitializeViewBag();

            return View(model);
        }        

        [HttpPost]
        public ActionResult Purchase(PurchaseViewModel model)
        {
            var vehicle = vehicleRepository.GetVehicleById(model.Vehicle.Id);
            model.Vehicle = vehicle;
            InitializeViewBag();

            if (model.Sales.ZipCode != null && model.Sales.ZipCode.Length != 5)
            {
                ModelState.AddModelError("Sales.ZipCode", $"Zip code must be a 5 digit number.");
            }
            if (model.Sales.PurchasePrice.HasValue)
            {
                decimal maxPricePercentage = (decimal)0.95;

                if (model.Sales.PurchasePrice.Value < (model.Vehicle.MSRP.Value * maxPricePercentage))
                {
                    ModelState.AddModelError("Sales.PurchasePrice", $"The purchase price cannot be less than 95% of the sales price.");
                }
                else if (model.Sales.PurchasePrice.Value > model.Vehicle.MSRP.Value)
                {
                    ModelState.AddModelError("Sales.PurchasePrice", $"The purchase price cannot exceed the MSRP.");
                }
            }


            if (ModelState.IsValid)
            {
                //save and redirect
                var salesPerson = salesPersonRepository.GetSalesPersonbyUserId(LoggedInUser.Id);
                vehicleRepository.PurchaseVehicle(model, salesPerson.Id);
                return RedirectToAction("Index", "Sales");
            }
            return View(model);
        }

        private void InitializeViewBag()
        {
            ViewBag.States = lookupRepository.GetStates();
            ViewBag.PurchaseTypes = lookupRepository.GetPurchaseTypes();
        }
    }
}