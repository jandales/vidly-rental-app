using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vidly.Common
{
    public class CustomerRequest
    {        
        public string FirstName { get; set; } = string.Empty;      
        public string LastName { get; set; } = string.Empty;    
        public string Email { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;            
        public string PhoneNumber { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; } = DateTime.Now;
    }
}