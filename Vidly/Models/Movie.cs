using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace Vidly.Models
{
    [Serializable]
    public class Movie
    {
        [Key]
        public int MovieId { get; set; }        
        public string Title { get; set; } = string.Empty;     
        public string Description { get; set; } = string.Empty;     
        public string Genre { get; set; } = string.Empty;
        public double Price { get; set; } = 0;
        public int Copies { get; set; } = 0;
        
        [Column(TypeName = "date")]
        public DateTime ReleaseDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Column(TypeName = "date")]
        public DateTime? UpdatedAt { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DeletedAt { get; set; }


        //public virtual ICollection<RentedMovie> RentedMovies { get; set; }
    }
}