using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vidly.Models
{
    [Serializable]
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "Customer firstname is required")]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }

        [Index(IsUnique = true)]
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public string Address { get; set; }

        [Column(TypeName = "date")]      
        public DateTime BirthDate { get; set; }
        [Column(TypeName = "date")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Column(TypeName = "date")]
        public DateTime? UpdatedAt { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DeletedAt { get; set; } 

        //public ICollection<Transaction> Transactions { get; set; }
    }
}