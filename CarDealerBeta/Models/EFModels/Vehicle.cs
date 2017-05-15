namespace Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;


    [Table("Vehicle")]
    public partial class Vehicle
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Vehicle()
        {
            Inquiries = new HashSet<Inquiry>();
            Sales = new HashSet<Sale>();
            Stocks = new HashSet<Stock>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(17)]
        public string Vin { get; set; }

        public int ModelId { get; set; }

        public short Year { get; set; }

        [Required]
        [MaxLength(1)]
        public byte[] New { get; set; }

        public int StyleId { get; set; }

        public int TransmissionId { get; set; }

        public int InteriorId { get; set; }

        public int ExteriorId { get; set; }

        public int Mileage { get; set; }

        [Column(TypeName = "money")]
        public decimal MSRP { get; set; }

        [Required]
        [StringLength(500)]
        public string Description { get; set; }

        [Required]
        [StringLength(255)]
        public string ImageFile { get; set; }

        public virtual Color Color { get; set; }

        public virtual Color Color1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Inquiry> Inquiries { get; set; }

        public virtual Model Model { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Sale> Sales { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Stock> Stocks { get; set; }

        public virtual Style Style { get; set; }

        public virtual Transmission Transmission { get; set; }
    }
}
