using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Vidly.Models
{
    [Serializable]
    public class Transaction
    {
        [Key]
        public int TransactionId { get; set; }        
        public string TransactionNumber { get; set; }   
        public double TotalAmount { get; set; } = 0;
        public int TotalItems { get; set; } = 0;
        public string Status { get; set; }
        [Column(TypeName = "date")]
        public DateTime RentDate { get; set; }
        [Column(TypeName = "date")]
        public DateTime DueDate { get; set; }
        [Column(TypeName = "date")]
        public DateTime? ReturnDate { get; set; }  
        [Column(TypeName = "date")]
        public DateTime CreatedDate { get; set; }
        [Column(TypeName = "date")]
        public DateTime? UpdatedDate { get; set; }
        [Column(TypeName = "date")]
        public DateTime? DeletedDate { get; set; }
        [ForeignKey("Customer")]
        public int CustomerId { get; set; }
     
        public virtual Customer Customer { get; set; }   
   
        public virtual ICollection<RentedMovie> RentedMovies { get; set; }
    }
}