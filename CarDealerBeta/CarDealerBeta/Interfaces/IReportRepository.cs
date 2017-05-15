using CarDealerBeta.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealerBeta.Interfaces
{
    public interface IReportRepository
    {
        List<InventoryReportViewModel> GetInventoryReport(bool isNewVehicleReport);
        List<SalesReportViewModel> GetSalesReport(int? userId, DateTime? fromDate, DateTime? toDate);
    }
}
