using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace MAUIWebAPI.Models
{
    public class User
    {
        
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public DateTime DOB { get; set; }
        
        public string POB { get; set; }
        
        public string Email { get; set; }
        
        public string UserId { get; set; }
        
        public string Password { get; set; }

        public byte[] myArray { get; set; }
    }
}
