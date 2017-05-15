namespace Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;


    [Table("Stock")]
    public partial class Stock
    {
        public int Id { get; set; }

        public int VehicleId { get; set; }

        [Column(TypeName = "money")]
        public decimal FloorPrice { get; set; }

        [Required]
        [MaxLength(1)]
        public byte[] Available { get; set; }

        [Required]
        [MaxLength(1)]
        public byte[] Feature { get; set; }

        public virtual Vehicle Vehicle { get; set; }
    }
}
