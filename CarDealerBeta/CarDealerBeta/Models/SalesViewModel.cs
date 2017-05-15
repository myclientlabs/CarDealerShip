using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CarDealerBeta.Models
{
    public class SalesViewModel
    {
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
        [Required]
        public string Street1 { get; set; }
        [Required]
        public string Street2 { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public int StateId { get; set; }
        [Required]
        public string ZipCode { get; set; }
        [Required]
        public decimal? PurchasePrice { get; set; }
        [Required]
        public int PurchaseTypeId { get; set; }
    }
}