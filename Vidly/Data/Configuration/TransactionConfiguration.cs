using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity.ModelConfiguration;
using Vidly.Models;

namespace Vidly.Data.Configuration
{
    public class TransactionConfiguration : EntityTypeConfiguration<Transaction>
    {
        public TransactionConfiguration()
        {
            this.ToTable("Transactions")
                .HasKey(t => t.TransactionId);      

            Property(p => p.TransactionNumber)
                .IsRequired();

            Property(p => p.Status)
                .IsRequired()
                .HasMaxLength(25);

            //HasMany(t => t.RentedMovies)
            //  .WithRequired(r => r.Transaction)
            //  .HasForeignKey(r => r.TransactionId);\











        }


    }
}