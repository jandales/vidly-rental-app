using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity.ModelConfiguration;
using Vidly.Models;

namespace Vidly.Data.Configuration
{
    public class RentedMoviesConfiguration : EntityTypeConfiguration<RentedMovie>
    {
        public RentedMoviesConfiguration()
        {        

            this.HasKey(r => r.RentedMovieId);           

            this.Property(p => p.MovieId)
                .IsRequired();

            this.Property(p => p.Price)
                .IsRequired();

            this.Property(p => p.Qty)
                .IsRequired();

            //this.HasRequired(r => r.Transaction)
            //    .WithMany(t => t.RentedMovies)
            //    .HasForeignKey(a => a.TransactionId);


            //this.HasRequired(r => r.Movie)
            //    .WithMany()
            //    .HasForeignKey(r => r.MovieId);






        }
    }
}