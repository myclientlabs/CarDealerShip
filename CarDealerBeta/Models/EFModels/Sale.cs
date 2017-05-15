namespace Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;


    public partial class Sale
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }

        public int SalespersonId { get; set; }

        public int VehicleId { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime Date { get; set; }

        [Column(TypeName = "money")]
        public decimal PurchasePrice { get; set; }

        public int PurchaseTypeId { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual PurchaseType PurchaseType { get; set; }

        public virtual Salesperson Salesperson { get; set; }

        public virtual Vehicle Vehicle { get; set; }
    }
}
