namespace Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;


    [Table("Inquiry")]
    public partial class Inquiry
    {
        public int Id { get; set; }

        public int ContactId { get; set; }

        [Required]
        [StringLength(2000)]
        public string Message { get; set; }

        public int? VehicleID { get; set; }

        public virtual Contact Contact { get; set; }

        public virtual Vehicle Vehicle { get; set; }
    }
}
