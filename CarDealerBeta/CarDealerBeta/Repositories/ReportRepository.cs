using CarDealerBeta.Interfaces;
using CarDealerBeta.Models;
using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarDealerBeta.Repositories
{
    public class ReportRepository : IReportRepository
    {
        CarEntities db;
        public ReportRepository()
        {
            db = new CarEntities();
        }
        public List<SalesReportViewModel> GetSalesReport(int? userId, DateTime? fromDate, DateTime? toDate)
        {
            List<SalesReportViewModel> report = new List<Models.SalesReportViewModel>();

            var salesQuery = db.Sales.AsQueryable();
            if(userId.HasValue)
            {
                salesQuery = salesQuery.Where(s => s.Salesperson.UserId == userId.Value);
            }
            if (fromDate.HasValue && toDate.HasValue)
            {
                salesQuery = salesQuery.Where(s => s.Date >= fromDate.Value && s.Date <= toDate.Value);
            }
            else
            {
                if (fromDate.HasValue)
                {
                    salesQuery = salesQuery.Where(s => s.Date >= fromDate.Value);
                }
                if (toDate.HasValue)
                {
                    salesQuery = salesQuery.Where(s => s.Date <= toDate.Value);
                }
            }

            var salesReportGroupedByUser = salesQuery.GroupBy(s => s.Salesperson.UserId);

            foreach (var item in salesReportGroupedByUser)
            {
                SalesReportViewModel model = new SalesReportViewModel();
                var user = db.Users.Where(s => s.Id == item.Key).FirstOrDefault();
                if (user != null)
                {
                    model.UserName = user.FirstName + ", " + user.LastName;
                }
                model.TotalVehicles = item.Count();
                model.TotalSales = item.Sum(s => s.PurchasePrice);

                report.Add(model);
            }

            return report;
        }
        public List<InventoryReportViewModel> GetInventoryReport(bool isNewVehicleReport)
        {
            List<InventoryReportViewModel> report = new List<Models.InventoryReportViewModel>();
            var isNew = BitConverter.GetBytes(isNewVehicleReport);

            var VehicleGroupedByModelAndYear = from v in db.Vehicles
                                               where v.New == isNew
                                               group v by new
                                               {
                                                   v.Year,
                                                   v.Model.Name,
                                                   //v.Model.Make.Name
                                               } into grp
                                               select new
                                               {
                                                   Year = grp.Key.Year,
                                                   Model = grp.Key.Name,
                                                   Make = grp.FirstOrDefault()==null?string.Empty: grp.FirstOrDefault().Model.Make.Name,
                                                   Vehicles = grp.ToList()
                                               };

            foreach (var item in VehicleGroupedByModelAndYear)
            {
                InventoryReportViewModel model = new Models.InventoryReportViewModel();
                model.Count = item.Vehicles.Count;
                model.Model = item.Model;
                model.Year = item.Year;
                model.Make = item.Make;
                model.StockValue = item.Vehicles.Sum(s => s.MSRP);
                report.Add(model);
            }
            return report;
        }
    }
}