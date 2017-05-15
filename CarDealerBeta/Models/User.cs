using Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
   public class User
    {
        public int Id { get; set; }

        [Required]
        [StringLength(70)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(70)]
        public string LastName { get; set; }

        [Required]
        [StringLength(120)]
        public string Email { get; set; }

        [Required]
        [StringLength(30)]
        public string Role { get; set; }

        [Required]
        [StringLength(70)]
        public string Password { get; set; }
    }
}
