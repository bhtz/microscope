using System;
using System.ComponentModel.DataAnnotations;

namespace IronHasura.Models
{
    public partial class User
    {
        [Key]
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Role { get; set; }
    }
}