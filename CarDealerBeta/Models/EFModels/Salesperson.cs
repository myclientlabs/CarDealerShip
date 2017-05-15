namespace Data
{
    using Models;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;


    [Table("Salesperson")]
    public partial class Salesperson
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Salesperson()
        {
            Sales = new HashSet<Sale>();
        }

        public int Id { get; set; }
        public int UserId { get; set; }

        public virtual User User { get; set; }

        //[Required]
        //[StringLength(70)]
        //public string FirstName { get; set; }

        //[Required]
        //[StringLength(70)]
        //public string LastName { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Sale> Sales { get; set; }
    }
}
