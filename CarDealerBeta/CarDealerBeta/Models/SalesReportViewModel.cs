using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarDealerBeta.Models
{
    public class SalesReportViewModel
    {
        public string UserName { get; set; }
        public decimal TotalSales { get; set; }
        public int TotalVehicles { get; set; }
    }
}