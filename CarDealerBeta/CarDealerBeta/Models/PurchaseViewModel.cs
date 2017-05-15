using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarDealerBeta.Models
{
    public class PurchaseViewModel
    {
        public VehicleViewModel Vehicle { get; set; }
        public SalesViewModel Sales { get; set; }
    }
}