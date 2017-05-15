using Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CarDealerBeta.Models
{
    public class VehicleViewModel
    {
        public VehicleViewModel()
        {
            IsNew = true;
            IsAvailable = true;
        }
        public int Id { get; set; }

        [Required]
        [StringLength(17)]
        public string Vin { get; set; }

        public int MakeId { get; set; }

        public int ModelId { get; set; }

        public short? Year { get; set; }

        [Required]
        public bool IsNew { get; set; }

        public int StyleId { get; set; }

        public int TransmissionId { get; set; }

        public int InteriorId { get; set; }

        public int ExteriorId { get; set; }

        [Required]
        public int? Mileage { get; set; }

        [Required]
        public decimal? MSRP { get; set; }

        [Required]
        public decimal? SalesPrice { get; set; }

        [Required]
        [StringLength(500)]
        public string Description { get; set; }

        public string ImageFile { get; set; }

        public bool IsFeaturedVehicle { get; set; }
        public bool IsAvailable { get; set; }

        public string ModelName { get; set; }
        public string MakeName { get; set; }
        public string BodyStyleText { get; set; }
        public string TransmissionText { get; set; }
        public string ExteriorColor { get; set; }
        public string InteriorColor { get; set; }
    }
}