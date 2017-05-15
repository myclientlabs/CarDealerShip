using CarDealerBeta.Interfaces;
using CarDealerBeta.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarDealerBeta.Controllers
{

    //[Authorize]
    public class HomeController : BaseController
    {
        ISpecialsRepository specialsRepository;
        IVehicleRepository vehicleRepository;
        public HomeController()
        {
            this.specialsRepository = new SpecialsRepository();
            this.vehicleRepository = new VehicleRepository();
        }
        public ActionResult Index()
        {
           var featuredVehicles= this.vehicleRepository.GetFeaturedVehicles();
            return View(featuredVehicles);
        }

        public ActionResult Contact()
        {
            return View();
        }
        public ActionResult Specials()
        {
            var specials = this.specialsRepository.GetSpecials();
            return View(specials);
        }
    }
}