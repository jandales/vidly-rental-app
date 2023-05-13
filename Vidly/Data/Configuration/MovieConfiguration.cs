using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Reflection;
using System.Web;
using Vidly.Models;

namespace Vidly.Data.Configuration
{
    public class MovieConfiguration : EntityTypeConfiguration<Movie>
    {
        public MovieConfiguration()
        {

            this.ToTable("Movies");

            this.HasKey(m => m.MovieId);

            this.Property(p => p.Title)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(p => p.Description)
                .IsRequired();

            this.Property(p => p.Genre)
                .IsRequired()
                .HasMaxLength(25);

            this.Property(p => p.Price)
                .IsRequired();

            this.Property(p => p.Copies)
                .IsRequired();

            this.Property(p => p.ReleaseDate)
                .IsRequired();
              

                

        }
    }
}