using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidly.Common
{
    public class MovieRequest
    {
        [Required(ErrorMessage = "Movie name is required")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Movie description is required")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Movie genre is required")]
        public string Genre { get; set; } = string.Empty;

        [Required(ErrorMessage = "Movie price is required")]
        public float Price { get; set; } = 0;

        [Required(ErrorMessage = "Movie price is required")]
        public int Copies { get; set; } = 0;

        [Required(ErrorMessage = "Movie date released is required")]
        public DateTime ReleaseDate { get; set; }
    }
}