using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Vidly.Models;

namespace Vidly.Data.Configuration
{
    public class CustomerConfiguration  : EntityTypeConfiguration<Customer>
    {
        public CustomerConfiguration()
        {
            this.ToTable("Customers");

                HasKey(c => c.CustomerId);

            this.Property(p => p.FirstName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(p => p.LastName)
                .IsRequired()       
                .HasMaxLength(50);

            this.Property(p => p.Email)
               .IsRequired()
               .HasMaxLength(50);

            this.Property(p => p.PhoneNumber)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(p => p.Address)
                .IsRequired()
                .HasMaxLength(225);

            this.Property(p => p.Gender)
               .IsRequired()
               .HasMaxLength(25);

            // Relationship with Transactions
            //HasMany(c => c.Transactions)
            //    .WithRequired(t => t.Customer)
            //    .HasForeignKey(t => t.CustomerId);


   
            

        }
    }
}