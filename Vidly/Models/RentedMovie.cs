using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Vidly.Models
{
    [Serializable]
    public class RentedMovie
    {
        [Key]
        public int RentedMovieId { get; set; } 
        public int Qty { get; set; }
        public double Price { get; set; }
        [Column(TypeName = "date")]
        public DateTime? ReturnDate { get; set; }

        [ForeignKey("Transaction")]
        public int TransactionId { get; set; }
        public virtual Transaction Transaction { get; set; }


        [ForeignKey("Movie")]
        public int MovieId { get; set; }
        public virtual Movie Movie { get; set; }
    }
}