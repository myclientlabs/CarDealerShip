using CarDealerBeta.Models;
using CarDealerBeta.Repositories;
using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealerBeta.Interfaces
{
    public interface IVehicleRepository
    {
        List<Vehicle> GetFeaturedVehicles();
        VehicleViewModel SaveVehicle(VehicleViewModel model);
        VehicleViewModel GetVehicleById(int id);
        List<VehicleViewModel> SearchVehicles(SearchParams searchParam);
        void DeleteVehicle(int id);
        void PurchaseVehicle(PurchaseViewModel model, int salesPersonId);
    }
}
