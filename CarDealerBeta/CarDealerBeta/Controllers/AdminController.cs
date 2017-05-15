using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CarDealerBeta.Models;
using CarDealerBeta.Interfaces;
using CarDealerBeta.Repositories;
using Data;
using System.IO;

namespace CarDealerBeta.Controllers
{
    public class AdminController : BaseController
    {
        ILookupRepository lookupRepository;
        IVehicleRepository vehicleRepository;
        public AdminController()
        {
            lookupRepository = new LookupRepository();
            vehicleRepository = new VehicleRepository();
        }
        // GET: Admin
        public String Index()
        {
            return "This is my <b>default</b> action...";
            //return View();
        }

        /******* Users *********/

        public ActionResult Users()
        {
            UserViewModel user = new UserViewModel()
            {
                Id = 500,
                FirstName = "Test",
                LastName = "Last",
                Email = "Email@Email.com",
                Role = "This is text"
            };
            List<UserViewModel> users = new List<UserViewModel>();
            users.Add(user);
            return View(users);
        }

        public ActionResult AddUser()
        {
            return View();
        }

        public ActionResult EditUser()
        {
            return View();
        }

        /******* Vehices *********/
        public ActionResult Vehicles()
        {
            return View();
        }

        public ActionResult AddVehicle()
        {
            InitializeViewBag();
            return View("SaveVehicle", new VehicleViewModel());
        }

        [HttpPost]
        public ActionResult SaveVehicle(VehicleViewModel model, HttpPostedFileBase file)
        {
            var allowedExtensions = new[] {
                ".png", ".jpg", ".jpeg"
            };
            InitializeViewBag();
            if (model.Id <= 0)
            {
                if (file == null || file.ContentLength <= 0)
                {
                    ModelState.AddModelError(nameof(model.ImageFile), $"Picture is required.");
                }
            }
            if (file != null && file.ContentLength > 0)
            {
                // extract only the fielname
                var fileName = Path.GetFileName(file.FileName);
                var extension = Path.GetExtension(fileName).ToLower();
                if (!allowedExtensions.Contains(extension)) //check what type of extension  
                {
                    ModelState.AddModelError(nameof(model.ImageFile), $"Allowed extensions are .png, .jpg and .jpeg");
                }
            }
            if (model.Year.HasValue)
            {
                int maxValue = DateTime.Now.Year + 1;
                int minValue = 2000;
                if (model.Year.Value < minValue || model.Year.Value > maxValue)
                {
                    ModelState.AddModelError(nameof(model.Year), $"Year should be between {minValue} and {maxValue} .");
                }
            }
            else
            {
                ModelState.AddModelError(nameof(model.Year), $"Year is required.");
            }
            if (model.Mileage.HasValue)
            {
                if (model.IsNew)
                {
                    if (model.Mileage.Value < 0 || model.Mileage.Value > 1000)
                    {
                        ModelState.AddModelError(nameof(model.Mileage), $"Mileage should be between 0 and 1000 for a New vehicle.");
                    }
                }
                else
                {
                    if (model.Mileage.Value < 1000)
                    {
                        ModelState.AddModelError(nameof(model.Mileage), $"Mileage should be greater than 1000 for a Used vehicle.");
                    }
                }
            }
            if (model.SalesPrice.HasValue && model.MSRP.HasValue)
            {
                if (model.SalesPrice.Value <= 0)
                {
                    ModelState.AddModelError(nameof(model.SalesPrice), $"Sale Price must be a positive number.");
                }
                if (model.MSRP.Value <= 0)
                {
                    ModelState.AddModelError(nameof(model.MSRP), $"MSRP must be a positive number.");
                }
                if (model.SalesPrice.Value > model.MSRP.Value)
                {
                    ModelState.AddModelError(nameof(model.SalesPrice), $"Sale Price shouldn't be greater than MSRP.");
                }
            }
            if (ModelState.IsValid)
            {
                vehicleRepository.SaveVehicle(model);
                if (file != null && file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var extension = Path.GetExtension(fileName);
                    var imageFileName = "Inventory-" + model.Id + extension;
                    var path = Path.Combine(Server.MapPath("~/Images"), imageFileName);
                    file.SaveAs(path);

                    //Call save again to update the filename in database
                    model.ImageFile = imageFileName;
                    vehicleRepository.SaveVehicle(model);
                }
                return RedirectToAction("EditVehicle", new { id = model.Id });
            }
            return View(model);
        }
        private void InitializeViewBag()
        {
            ViewBag.Makes = lookupRepository.GetMakes();
            ViewBag.Models = lookupRepository.GetModels();
            ViewBag.Styles = lookupRepository.GetStyles();
            ViewBag.Transmissions = lookupRepository.GetTransmissions();
            ViewBag.Colors = lookupRepository.GetColors();
        }

        public ActionResult EditVehicle(int id)
        {
            InitializeViewBag();
            var vehicleViewModel = vehicleRepository.GetVehicleById(id);
            return View("SaveVehicle", vehicleViewModel);
        }
        public JsonResult GetModelByMakeId(int id)
        {
            List<VehicleModelViewModel> viewModel = new List<Models.VehicleModelViewModel>();
            var models = lookupRepository.GetModelByMakeId(id).ToList();
            foreach (var item in models)
            {
                viewModel.Add(new Models.VehicleModelViewModel { Id = item.Id, Name = item.Name });
            }

            return Json(viewModel, JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteVehicle(int id)
        {
            var vehicle = vehicleRepository.GetVehicleById(id);
            vehicleRepository.DeleteVehicle(id);

            if (!string.IsNullOrWhiteSpace(vehicle.ImageFile) && !vehicle.ImageFile.EndsWith("placeholder.png"))
            {
                if (System.IO.File.Exists(HttpContext.Server.MapPath("~/Images/" + vehicle.ImageFile)))
                {
                    System.IO.File.Delete(HttpContext.Server.MapPath("~/Images/" + vehicle.ImageFile));
                }
            }
            return RedirectToAction("Index", "Home");
        }

    }
}