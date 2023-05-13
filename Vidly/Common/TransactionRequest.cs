using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Vidly.Common.Request;

namespace Vidly.Common
{
    public class TransactionRequest
    {
        
        public int CustomerId { get; set; } 
        public double TotalAmount { get; set; } = 0;
        public int TotalItems { get; set; } = 0;  
        public DateTime RentDate { get; set; }    
        public DateTime DueDate { get; set; }    
        public List<TransactionMovie> Movies { get; set; }
    }
}