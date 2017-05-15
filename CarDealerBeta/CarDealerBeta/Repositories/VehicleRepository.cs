using CarDealerBeta.Interfaces;
using CarDealerBeta.Models;
using Data;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Web;

namespace CarDealerBeta.Repositories
{
    public class SearchParams
    {
        public bool? IsNewVehicle { get; set; }
        public string SearchText { get; set; }
        public int? MinPrice { get; set; }
        public int? MaxPrice { get; set; }
        public int? MinYear { get; set; }
        public int? MaxYear { get; set; }
    }
    public class VehicleRepository : IVehicleRepository
    {
        CarEntities db;
        public VehicleRepository()
        {
            db = new CarEntities();
        }

        public List<VehicleViewModel> SearchVehicles(SearchParams searchParam)
        {
            var vehicleQuery = db.Vehicles.AsQueryable();
            vehicleQuery = vehicleQuery.Where(s => s.Sales.Count == 0);//Sold vehicles should not be listed in search
            if (searchParam.IsNewVehicle.HasValue)
            {
                var isNew = BitConverter.GetBytes(searchParam.IsNewVehicle.Value);
                vehicleQuery = vehicleQuery.Where(s => s.New == isNew);
            }
            if (!string.IsNullOrWhiteSpace(searchParam.SearchText))
            {
                vehicleQuery = vehicleQuery.Where(s => s.Model.Name.Contains(searchParam.SearchText)
                                     || s.Model.Make.Name.Contains(searchParam.SearchText)
                                     || s.Year.ToString() == searchParam.SearchText);

            }
            if (searchParam.MinPrice.HasValue && searchParam.MaxPrice.HasValue)
            {
                vehicleQuery = vehicleQuery.Where(s => s.MSRP >= searchParam.MinPrice.Value && s.MSRP <= searchParam.MaxPrice.Value);
            }
            else
            {
                if (searchParam.MinPrice.HasValue)
                {
                    vehicleQuery = vehicleQuery.Where(s => s.MSRP >= searchParam.MinPrice.Value);
                }
                if (searchParam.MaxPrice.HasValue)
                {
                    vehicleQuery = vehicleQuery.Where(s => s.MSRP >= searchParam.MaxPrice.Value);
                }
            }

            if (searchParam.MinYear.HasValue && searchParam.MaxYear.HasValue)
            {
                vehicleQuery = vehicleQuery.Where(s => s.Year >= searchParam.MinYear.Value && s.Year <= searchParam.MaxYear.Value);
            }
            else
            {
                if (searchParam.MinYear.HasValue)
                {
                    vehicleQuery = vehicleQuery.Where(s => s.Year >= searchParam.MinYear.Value);
                }
                if (searchParam.MaxYear.HasValue)
                {
                    vehicleQuery = vehicleQuery.Where(s => s.Year >= searchParam.MaxYear.Value);
                }
            }

            List<VehicleViewModel> viewModels = new List<Models.VehicleViewModel>();
            foreach (var item in vehicleQuery.ToList())
            {
                viewModels.Add(MapVehicleToViewModel(item));
            }
            return viewModels;
        }
        public List<Vehicle> GetFeaturedVehicles()
        {
            byte[] byteEqualentForOne = BitConverter.GetBytes(1);
            return db.Vehicles.Where(v => v.Stocks.Any(s => s.Feature != null && s.Feature == byteEqualentForOne)).ToList();
        }
        public VehicleViewModel SaveVehicle(VehicleViewModel model)
        {
            Vehicle vehicle;
            if (model.Id > 0)//Add
            {
                vehicle = db.Vehicles.Where(s => s.Id == model.Id).FirstOrDefault();
                if (vehicle == null)
                {
                    throw new Exception($"Vehicle with Id: {model.Id} not found.");
                }
            }
            //edit
            else
            {
                vehicle = new Vehicle();
                db.Vehicles.Add(vehicle);
            }
            if (vehicle.Stocks == null || vehicle.Stocks.Count == 0)
            {
                vehicle.Stocks.Add(new Stock
                {
                    FloorPrice = model.SalesPrice.HasValue ? model.SalesPrice.Value : 0,
                    Feature = BitConverter.GetBytes(model.IsFeaturedVehicle),
                    Available = BitConverter.GetBytes(model.IsAvailable)
                });
            }
            else
            {
                var stock = vehicle.Stocks.FirstOrDefault();
                if (stock != null)
                {
                    stock.Available = BitConverter.GetBytes(model.IsAvailable);
                    stock.Feature = BitConverter.GetBytes(model.IsFeaturedVehicle);
                    stock.FloorPrice = model.SalesPrice.HasValue ? model.SalesPrice.Value : 0;
                }
            }

            MapVehicleViewModelToDbModel(model, vehicle);
            try
            {
                db.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                var errorMessage = string.Empty;
                foreach (var eve in e.EntityValidationErrors)
                {
                    errorMessage += string.Format("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        errorMessage += string.Format("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
            model.Id = vehicle.Id;

            return model;
        }

        public VehicleViewModel GetVehicleById(int id)
        {
            VehicleViewModel model;
            var vehicle = db.Vehicles.Where(s => s.Id == id).FirstOrDefault();
            if (vehicle == null)
            {
                throw new Exception($"The Vehicle with Id : {id} not found.");
            }
            model = MapVehicleToViewModel(vehicle);
            return model;
        }
        public void DeleteVehicle(int id)
        {
            var vehicle = db.Vehicles.Where(s => s.Id == id).FirstOrDefault();
            if (vehicle == null)
            {
                throw new Exception($"The Vehicle with Id : {id} not found.");
            }
            db.Stocks.RemoveRange(vehicle.Stocks);
            db.Sales.RemoveRange(vehicle.Sales);
            db.Vehicles.Remove(vehicle);
            db.SaveChanges();
        }

        public void PurchaseVehicle(PurchaseViewModel model, int salesPersonId)
        {
            Customer customer = new Customer
            {
                FirstName = model.Sales.FirstName,
                City = model.Sales.City,
                Email = model.Sales.Email,
                LastName = model.Sales.LastName,
                Phone = model.Sales.Phone,
                StateId = model.Sales.StateId,
                Street1 = model.Sales.Street1,
                Street2 = model.Sales.Street2,
                ZipCode = model.Sales.ZipCode,
               
            };

            customer.Sales.Add(new Sale {
                Date=DateTime.Now,
                PurchasePrice=model.Sales.PurchasePrice.HasValue?model.Sales.PurchasePrice.Value:0,
                PurchaseTypeId=model.Sales.PurchaseTypeId,
                SalespersonId=salesPersonId,
                VehicleId=model.Vehicle.Id
            });

            db.Customers.Add(customer);
            db.SaveChanges();
        }
        private static VehicleViewModel MapVehicleToViewModel(Vehicle model)
        {
            VehicleViewModel viewModel = new Models.VehicleViewModel();

            viewModel.Id = model.Id;
            viewModel.ExteriorId = model.ExteriorId;
            viewModel.ImageFile = model.ImageFile;
            viewModel.InteriorId = model.InteriorId;
            viewModel.Mileage = model.Mileage;
            viewModel.ModelId = model.ModelId;
            viewModel.MakeId = model.Model.MakeId;
            viewModel.MSRP = model.MSRP;
            viewModel.IsNew = BitConverter.ToBoolean(model.New, 0);
            viewModel.StyleId = model.StyleId;
            viewModel.TransmissionId = model.TransmissionId;
            viewModel.Vin = model.Vin;
            viewModel.Year = model.Year;

            viewModel.ImageFile = "placeholder.png";
            if (!string.IsNullOrWhiteSpace(model.ImageFile))
            {
                if (File.Exists(HttpContext.Current.Server.MapPath("~/Images/" + model.ImageFile)))
                {
                    viewModel.ImageFile = model.ImageFile;
                }
            }
            
            viewModel.Description = model.Description;
            viewModel.ModelName = model.Model.Name;
            viewModel.MakeName = model.Model.Make.Name;
            viewModel.BodyStyleText = model.Style.Description;
            viewModel.TransmissionText = model.Transmission.Description;
            viewModel.ExteriorColor = model.Color.Name;
            viewModel.InteriorColor = model.Color1.Name;

            var stock = model.Stocks.FirstOrDefault();
            if (stock != null)
            {
                viewModel.SalesPrice = stock.FloorPrice;
                viewModel.IsAvailable = BitConverter.ToBoolean(stock.Available, 0);
                viewModel.IsFeaturedVehicle = BitConverter.ToBoolean(stock.Feature, 0);
            }

            return viewModel;
        }
        private static void MapVehicleViewModelToDbModel(VehicleViewModel model, Vehicle vehicle)
        {
            vehicle.Id = model.Id;
            vehicle.ExteriorId = model.ExteriorId;
            vehicle.ImageFile = model.ImageFile;
            vehicle.InteriorId = model.InteriorId;
            vehicle.Mileage = model.Mileage.HasValue ? model.Mileage.Value : 0;
            vehicle.ModelId = model.ModelId;
            vehicle.MSRP = model.MSRP.HasValue ? model.MSRP.Value : 0;
            vehicle.New = BitConverter.GetBytes(model.IsNew);
            vehicle.StyleId = model.StyleId;
            vehicle.TransmissionId = model.TransmissionId;
            vehicle.Vin = model.Vin;
            vehicle.Year = model.Year.HasValue ? model.Year.Value : (short)2000;
            vehicle.ImageFile = string.IsNullOrWhiteSpace(model.ImageFile) ? "placeholder.png" : model.ImageFile;
            vehicle.Description = model.Description;
        }
    }
}