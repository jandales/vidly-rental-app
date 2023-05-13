using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection.Emit;
using System.Web;
using Vidly.Data.Configuration;
using Vidly.Models;


namespace Vidly.Data
{
    public class DataContext : DbContext
    {
        public DataContext() : base("name=DbConnection") {
            this.Configuration.ValidateOnSaveEnabled = true;
            //this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;
        }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Transaction> Transactions { get; set; }       
        public DbSet<RentedMovie> RentedMovies { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new MovieConfiguration());

            modelBuilder.Configurations.Add(new CustomerConfiguration());

            modelBuilder.Configurations.Add(new RentedMoviesConfiguration());

            modelBuilder.Configurations.Add(new TransactionConfiguration());

            

            //modelBuilder.Entity<Customer>()
            //    .ToTable("Customers")
            //    .HasKey(c => c.CustomerId);
               

            //modelBuilder.Entity<Movie>()
            //    .ToTable("Movies")
            //    .HasKey(m => m.MovieId);

            //modelBuilder.Entity<Transaction>()
            //    .ToTable("Transactions")
            //    .HasKey(t => t.TransactionId);

            //modelBuilder.Entity<RentedMovie>()
            //    .ToTable("RentedMovies")
            //    .HasKey(rm => rm.RentedMovieId);                               

            //modelBuilder.Entity<Transaction>()
            //    .HasRequired(t => t.Customer)
            //    .WithMany(c => c.Transactions)
            //    .HasForeignKey(t => t.CustomerId);

            //modelBuilder.Entity<RentedMovie>()
            //    .HasRequired(r => r.Transaction)
            //    .WithMany(t => t.RentedMovies)                
            //    .HasForeignKey(r => r.TransactionId);
               

            //modelBuilder.Entity<RentedMovie>()
            //   .HasRequired(r => r.Movie) 
            //   .WithMany(m => m.RentedMovies)
            //   .HasForeignKey(r => r.MovieId);


        }
         

    }
}